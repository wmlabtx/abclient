namespace ABClient.PostFilter
{
    using Helpers;
    using System;

    internal static partial class Filter
    {
        private static byte[] TopJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            var posone = html.IndexOf("()", StringComparison.OrdinalIgnoreCase);
            if (posone != -1)
            {
                posone += "()".Length;
                html = html.Substring(0, posone) + "{ return ''; }";
            }

            return Russian.Codepage.GetBytes(html);
        }
    }
}
