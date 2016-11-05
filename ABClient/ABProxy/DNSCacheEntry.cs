namespace ABClient.ABProxy
{
    using System;
    using System.Net;

    internal sealed class DNSCacheEntry
    {
        internal DNSCacheEntry(IPHostEntry hostEntry)
        {
            HostEntry = hostEntry;
            LastLookup = Environment.TickCount;
        }

        internal int LastLookup { get; private set; }

        internal IPHostEntry HostEntry { get; private set; }
    }
}