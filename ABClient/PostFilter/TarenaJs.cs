using ABClient.Properties;

namespace ABClient.PostFilter
{
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] TarenaJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = Resources.json2 + " " + html;
            return Russian.Codepage.GetBytes(html);
        }
    }
}