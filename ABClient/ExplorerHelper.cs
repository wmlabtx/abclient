namespace ABClient
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    using ABForms;

    /// <summary>
    /// Класс предназначен для работы с кешем IE.
    /// </summary>
    internal static class ExplorerHelper
    {
        /// <summary>
        /// Очистка кеша IE.
        /// </summary>
        internal static void ClearCache()
        {
            ThreadPool.QueueUserWorkItem(ClearCacheAsync);
        }

        private static void ClearCacheAsync(object stateInfo)
        {
            Thread.CurrentThread.Name = "ClearCacheAsync";
            Thread.Sleep(500);

            var is64Bit = IntPtr.Size == 8;
            var cacheEntryInfoBufferSizeInitial = 0;
                        
            var enumHandle = NativeMethods.FindFirstUrlCacheEntry(null, IntPtr.Zero, ref cacheEntryInfoBufferSizeInitial);
            if (enumHandle == IntPtr.Zero && 259 == Marshal.GetLastWin32Error())
            {
                return;
            }

            var cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
            var cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize);
            enumHandle = NativeMethods.FindFirstUrlCacheEntry(null, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
            var possibleChars = new[] { '@' };

            while (AppVars.ClearExplorerCacheFormMain != null && AppVars.ClearExplorerCacheFormMain.IsAllowed)
            {
                cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize;
                var storedUrl = GetUrl(cacheEntryInfoBuffer, is64Bit);
                if (!string.IsNullOrEmpty(storedUrl))
                {
                    if (storedUrl.IndexOf(".neverlands.ru", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        if (
                            !storedUrl.StartsWith("Visited:", StringComparison.OrdinalIgnoreCase) &&
                            (storedUrl.IndexOfAny(possibleChars) != -1 ||
                             storedUrl.EndsWith(".htm", StringComparison.OrdinalIgnoreCase) ||
                             storedUrl.EndsWith(".css", StringComparison.OrdinalIgnoreCase) ||
                             storedUrl.EndsWith(".html", StringComparison.OrdinalIgnoreCase) ||
                             storedUrl.Contains(".js")))
                        {
                            try
                            {
                                AppVars.ClearExplorerCacheFormMain.BeginInvoke(
                                    new ClearExplorerCacheFormWriteDelegate(AppVars.ClearExplorerCacheFormMain.Write),
                                    new object[] {storedUrl});
                            }
                            catch (InvalidOperationException)
                            {
                            }

                            var ptr = Marshal.StringToBSTR(storedUrl);
                            NativeMethods.DeleteUrlCacheEntry(ptr);
                            Marshal.FreeBSTR(ptr);
                        }
                    }
                }

                var returnValue = NativeMethods.FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                if (!returnValue && 259 == Marshal.GetLastWin32Error())
                {
                    break;
                }

                if (returnValue || cacheEntryInfoBufferSizeInitial <= cacheEntryInfoBufferSize)
                {
                    continue;
                }

                cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial;
                cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, (IntPtr)cacheEntryInfoBufferSize);
                returnValue = NativeMethods.FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, ref cacheEntryInfoBufferSizeInitial);
                if (!returnValue && 259 == Marshal.GetLastWin32Error())
                {
                    break;
                }
            }

            Marshal.FreeHGlobal(cacheEntryInfoBuffer);

            try
            {
                if (AppVars.ClearExplorerCacheFormMain != null)
                {
                    AppVars.ClearExplorerCacheFormMain.Invoke(
                        new ClearExplorerCacheFormCancelDelegate(AppVars.ClearExplorerCacheFormMain.Cancel));
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private static string GetUrl(IntPtr cacheEntryInfoBuffer, bool is64Bit)
        {
            if (!is64Bit)
            {
                var internetCacheEntry = (InternetCacheEntryInfoa)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(InternetCacheEntryInfoa));
                var result = Marshal.PtrToStringAnsi(internetCacheEntry.LpszSourceUrlName);
                return result;
            }
            else
            {
                var internetCacheEntry = (InternetCacheEntryInfoa64)Marshal.PtrToStructure(cacheEntryInfoBuffer, typeof(InternetCacheEntryInfoa64));
                var result = Marshal.PtrToStringAnsi(internetCacheEntry.LpszSourceUrlName);
                return result;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InternetCacheEntryInfoa
        {
            [FieldOffset(4)]
            internal readonly IntPtr LpszSourceUrlName;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InternetCacheEntryInfoa64
        {
            [FieldOffset(8)]
            internal readonly IntPtr LpszSourceUrlName;
        }
    }
}