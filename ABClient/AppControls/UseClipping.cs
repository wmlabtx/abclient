namespace ABClient.AppControls
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    internal sealed class UseClipping : IDisposable
    {
        private readonly Graphics _graph;
        private readonly Region _oldRegion;

        internal UseClipping(Graphics graphics, GraphicsPath path)
        {
            _graph = graphics;
            _oldRegion = graphics.Clip;
            var clip = _oldRegion.Clone();
            clip.Intersect(path);
            _graph.Clip = clip;
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
                _graph.Clip = _oldRegion;
            }
        }
    }
}