using System.Threading;

namespace ABClient.ABProxy
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;

    internal static class DNSResolver
    {
        private static readonly Dictionary<string, DNSCacheEntry> DictAddresses = new Dictionary<string, DNSCacheEntry>();
        private static readonly ReaderWriterLock Rwl = new ReaderWriterLock();

        internal static IPAddress GetIPAddress(string remoteHost, bool checkCache)
        {
            IPAddress address = Utilities.IPFromString(remoteHost);
            if (address == null)
            {
                DNSCacheEntry cacheEntry;
                IPHostEntry hostEntry = null;
                if (checkCache && DictAddresses.TryGetValue(remoteHost, out cacheEntry))
                {
                    if (cacheEntry.LastLookup > (Environment.TickCount - 0xea60))
                    {
                        hostEntry = cacheEntry.HostEntry;
                    }
                    else
                    {
                        try
                        {
                            Rwl.AcquireWriterLock(5000);
                            try
                            {
                                DictAddresses.Remove(remoteHost);
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

                if (hostEntry == null)
                {
                    hostEntry = Dns.GetHostEntry(remoteHost);
                    try
                    {
                        Rwl.AcquireWriterLock(5000);
                        try
                        {
                            if (!DictAddresses.ContainsKey(remoteHost))
                            {
                                DictAddresses.Add(remoteHost, new DNSCacheEntry(hostEntry));
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
                }

                address = hostEntry.AddressList[0];
                if (address.AddressFamily != AddressFamily.InterNetworkV6)
                {
                    return address;
                }

                for (int i = 1; i < hostEntry.AddressList.Length; i++)
                {
                    if (hostEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return hostEntry.AddressList[i];
                    }
                }
            }

            return address;
        }
    }
}