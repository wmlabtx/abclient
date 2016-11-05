namespace ABClient.AppControls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public sealed class UseAntiAlias : IDisposable
    {
        private readonly Graphics g;
        private readonly SmoothingMode _old;

        public UseAntiAlias(Graphics graphics)
        {
            g = graphics;
            _old = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                g.SmoothingMode = _old;
            }
        }
    }
}