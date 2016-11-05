namespace ABClient.AppControls
{
    using System.ComponentModel;

    public sealed class WebBrowserExtendedNavigatingEventArgs : CancelEventArgs
    {
        public WebBrowserExtendedNavigatingEventArgs(string address, string frame)
        {
            Address = address;
            Frame = frame;
        }

        public string Address { get; private set; }

        public string Frame { get; private set; }
    }
}