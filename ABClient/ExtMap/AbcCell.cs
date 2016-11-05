using System;

namespace ABClient.ExtMap
{
    public class AbcCell
    {
        public string RegNum { set; get; }
        public string Label { set; get; }
        public int Cost { set; get; }
        public DateTime Visited { set; get; }
        public DateTime Verified { set; get; }
    }
}
