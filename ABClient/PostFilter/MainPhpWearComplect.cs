namespace ABClient.PostFilter
{
    using System;

    internal static partial class Filter
    {
        private static string MainPhpWearComplect(string html, string complect)
        {
            // compl_view("1","15887640014a589e349d6d9","d4dc0c67fff151d4871f6ab22ffaa925");
            // compl_view("Текущий 3","213536645948f1b1b854fb0","3bac7c7434b08b1b225c0e74fd2459da");

            var pos = 0;
            while (pos != -1)
            {
                const string matr = @"compl_view(""";
                pos = html.IndexOf(matr, pos);
                if (pos == -1)
                {
                    break;
                }

                pos += matr.Length;
                var pos1 = html.IndexOf('"', pos + 1);
                if (pos1 == -1)
                {
                    break;
                }

                var complName = html.Substring(pos, pos1 - pos);
                if (!complect.Equals(complName, StringComparison.OrdinalIgnoreCase))
                {
                    pos = pos1;
                    continue;
                }

                var pos2 = pos1 + 3;
                var pos3 = html.IndexOf('"', pos2);
                if (pos3 == -1)
                {
                    break;
                }

                var magicKey = html.Substring(pos2, pos3 - pos2);
                var pos4 = pos3 + 3;
                var pos5 = html.IndexOf('"', pos4);
                if (pos5 == -1)
                {
                    break;
                }

                var magicVcode = html.Substring(pos4, pos5 - pos4);
                var messageWear = string.Format(
                    "Одеваем комплект <b>&laquo;{0}&raquo;</b>...",
                    complect);
                var link = string.Format(
                    "main.php?get_id=57&uid={0}&s=2&vcode={1}",
                    magicKey,
                    magicVcode);
                html = BuildRedirect(messageWear, link);
                return html;
            }

            return string.Empty;
        }
    }
}