namespace ABClient.PostFilter
{
    using Helpers;
    using System.Text;

    internal static partial class Filter
    {
        private static byte[] PvJs(byte[] array)
        {
            var sb = new StringBuilder(Russian.Codepage.GetString(array));
            sb.Replace("'%clan% '", "'%clan%'");
            return Russian.Codepage.GetBytes(sb.ToString());
        }
    }
}