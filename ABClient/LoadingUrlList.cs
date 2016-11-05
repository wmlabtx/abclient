using System;
using System.Threading;

namespace ABClient
{
    using System.Collections;

    /// <summary>
    /// Работа со списком активных URL (для статусной строки основной формы).
    /// </summary>
    internal static class LoadingUrlList
    {
        private static readonly Hashtable Table = new Hashtable();
        private static readonly ReaderWriterLock Rwl = new ReaderWriterLock();


        internal static string Add(string address)
        {
            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    if (!Table.ContainsKey(address))
                    {
                        Table.Add(address, string.Empty);
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            return address;
        }

        internal static string Remove(string address)
        {
            var result = string.Empty;

            try
            {
                Rwl.AcquireWriterLock(5000);
                try
                {
                    if (Table.ContainsKey(address))
                    {
                        Table.Remove(address);
                    }

                    if (Table.Count > 0)
                    {
                        var enumerator = Table.Keys.GetEnumerator();
                        enumerator.MoveNext();
                        result = enumerator.Current as string;
                    }
                }
                finally
                {
                    Rwl.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            return result;
        }
    }
}