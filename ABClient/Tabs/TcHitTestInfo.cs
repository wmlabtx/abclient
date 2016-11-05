namespace ABClient.Tabs
{
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct TcHitTestInfo
    {
        public Point pt;
        public TcHitTestFlags flags;
        public TcHitTestInfo(int x, int y)
        {
            pt = new Point(x, y);
            flags = TcHitTestFlags.TCHT_ONITEM;
        }
    }
}