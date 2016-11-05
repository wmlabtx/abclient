namespace ABClient.ABForms
{
    using System.Windows.Forms;
    using MyForms;

    internal sealed partial class FormMain
    {
        internal void ShowSmiles(int index)
        {
            using (var fs = new FormSmiles(index))
            {
                if (fs.ShowDialog() == DialogResult.OK)
                {
                    AddMessageToPrompt(fs.Result);
                }
            }
        }
    }
}