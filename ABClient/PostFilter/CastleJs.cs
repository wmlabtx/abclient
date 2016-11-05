using ABClient.Helpers;
using ABClient.Properties;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] CastleJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = Resources.json2 + " " + html;
            return Russian.Codepage.GetBytes(html);
        }
    }
}
