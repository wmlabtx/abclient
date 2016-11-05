using ABClient.Properties;
using ABClient.Helpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] TowerJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = Resources.json2 + " " + html;
            return Russian.Codepage.GetBytes(html);
        }
    }
}