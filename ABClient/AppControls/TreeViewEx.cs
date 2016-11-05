namespace ABClient.AppControls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class TreeViewEx : TreeView
    {
        private const int TvFirst = 0x1100;
        private const int TvmSetbkcolor = TvFirst + 29;
        private const int TvmSetextendedstyle = TvFirst + 44;
        private const int TvsExDoublebuffer = 0x0004;

        public TreeViewEx()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            if (!NativeMethods.IsWinVista)
            {
                SetStyle(ControlStyles.UserPaint, true);
            }
        }

        private void UpdateExtendedStyles()
        {
            int Style = 0;

            if (DoubleBuffered)
            {
                Style |= TvsExDoublebuffer;
            }

            if (Style != 0)
            {
                NativeMethods.SendMessage(Handle, TvmSetextendedstyle, (IntPtr)TvsExDoublebuffer, (IntPtr)Style);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateExtendedStyles();
            if (!NativeMethods.IsWinXP)
            {
                NativeMethods.SendMessage(Handle, TvmSetbkcolor, IntPtr.Zero, (IntPtr)ColorTranslator.ToWin32(BackColor));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint))
            {
                var m = new Message
                            {
                                HWnd = Handle,
                                Msg = 0x0318,
                                WParam = e.Graphics.GetHdc(),
                                LParam = (IntPtr)0x00000004
                            };
                DefWndProc(ref m);
                e.Graphics.ReleaseHdc(m.WParam);
            }

            base.OnPaint(e);
        }
    }

}
