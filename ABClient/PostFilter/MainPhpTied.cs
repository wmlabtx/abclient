namespace ABClient.PostFilter
{
    using System;
    using ABForms;

    internal static partial class Filter
    {
        private static void MainPhpTied(string html, int postied)
        {
            var pos2 = html.IndexOf("</b>", postied, StringComparison.OrdinalIgnoreCase);
            if (pos2 == -1) return;
            var stied = html.Substring(postied, pos2 - postied);
            int tied;
            if (!int.TryParse(stied, out tied))
            {
                return;
            }

            try
            {
                if (AppVars.MainForm != null)
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTiedDelegate(AppVars.MainForm.UpdateTied),
                        new object[] { tied });
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}