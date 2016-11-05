namespace ABClient.AppControls
{
    using System;
    using System.Drawing;
    using System.Drawing.Text;

    public sealed class UseClearTypeGridFit : IDisposable
    {
        private readonly Graphics g;
        private readonly TextRenderingHint _old;

        public UseClearTypeGridFit(Graphics graphics)
        {
            g = graphics;
            _old = g.TextRenderingHint;
            g.TextRenderingHint = TextRenderingHint.SystemDefault;
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
                g.TextRenderingHint = _old;
            }
        }
    }
}