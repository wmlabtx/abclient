namespace ABClient.PostFilter
{
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] NlPinfoJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = html.Replace(
                @"+alt+",
                @"+window.external.InfoToolTip(arr[0],alt)+");

            return Russian.Codepage.GetBytes(html);
        }
    }
}