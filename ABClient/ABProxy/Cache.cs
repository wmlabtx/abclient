using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace ABClient.ABProxy
{

    internal static class Cache
    {
        private static readonly ReaderWriterLock Rwl = new ReaderWriterLock();
        private static readonly string CacheDir = Path.Combine(Application.StartupPath, "abcache");
        private static readonly SortedDictionary<string, byte[]> MemCache = new SortedDictionary<string, byte[]>();

        private static string GetKey(string url)
        {
            const string httpAndSlash = "http://";

            string key = url.ToLower();
            if (key.StartsWith(httpAndSlash))
                key = key.Substring(httpAndSlash.Length);
            var posask = key.LastIndexOf('?');
            if (posask != -1)
                key = key.Substring(0, posask);

            return key;
        }

        internal static byte[] Get(string url, bool cacheRefresh)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            var key = GetKey(url);

            byte[] data;
            if (!MemCache.TryGetValue(key, out data))
            {
                data = GetDisk(key, cacheRefresh);
            }

            return data;
        }

        internal static void Store(string url, byte[] data, bool storetodisk)
        {
            if (string.IsNullOrEmpty(url) || data == null || data.Length == 0)
                return;
            
            var key = GetKey(url);

            StoreMem(key, data);
            
            if (!storetodisk)
                return;

            try
            {
                var fullPath = Path.Combine(CacheDir, key.Replace('/', '\\'));
                var fullDir = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
                if (!Directory.Exists(fullDir))
                {
                    Directory.CreateDirectory(fullDir);
                }

                File.WriteAllBytes(fullPath, data);
            }
            catch (Exception ex)
            {
                Debug.Print("Cache.Store :" + ex.Message);
            }
        }

        internal static void Clear()
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    MemCache.Clear();
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        private static byte[] GetDisk(string key, bool cacheRefresh)
        {
            if (string.IsNullOrEmpty(key) || cacheRefresh)
                return null;

            if (key.IndexOf(".js", StringComparison.InvariantCultureIgnoreCase) != -1)
                return null;

            try
            {
                var fullPath = Path.Combine(CacheDir, key.Replace('/', '\\'));
                var isExists = File.Exists(fullPath);
                if (!isExists)
                    return null;

                var data = File.ReadAllBytes(fullPath);
                return data;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void StoreMem(string key, byte[] data)
        {
            if (string.IsNullOrEmpty(key) || data == null || data.Length == 0)
                return;

            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    if (MemCache.ContainsKey(key))
                        MemCache[key] = data;
                    else
                        MemCache.Add(key, data);
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }
    }
}