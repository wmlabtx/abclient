namespace ABClient.ABForms
{
    using System.Windows.Forms;

    internal delegate void ClearExplorerCacheFormCancelDelegate();

    internal delegate void ClearExplorerCacheFormWriteDelegate(string message);

    /// <summary>
    /// Форма очистки кеша IE.
    /// </summary>
    internal sealed partial class ClearExplorerCacheForm : Form
    {
        internal ClearExplorerCacheForm()
        {
            InitializeComponent();
            Icon = Properties.Resources.ABClientIcon;

            IsAllowed = true;
        }

        internal bool IsAllowed { get; private set; }

        internal void Cancel()
        {
            Close();
        }

        internal void Write(string message)
        {
            labelText.Text = message;
        }

        private void ButtonCancelClick(object sender, System.EventArgs e)
        {
            IsAllowed = false;
        }
    }
}