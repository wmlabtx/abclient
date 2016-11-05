namespace ABClient.Tabs
{
    using System.Windows.Forms;
    using AppControls;

    internal class TabClass
    {
        internal TabType MyType { get; set; }
        internal string Nick { get; set; }
        //internal ExtendedWebBrowser WB { get; set; }
        internal WebBrowser WB { get; set; }
        internal TextBox Note { get; set; }
        internal string Address { get; set; }
        internal bool Delayed { get; set; }
        internal string AddressInit { get; set; }
    }
}