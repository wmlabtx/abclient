namespace ABClient.MyHelpers
{
    using System;
    using System.Collections.Generic;

    internal static class HelperStrings
    {
        internal static string Replace(string html, string s1, string s2, string newStr)
        {
            var p1 = html.IndexOf(s1, StringComparison.OrdinalIgnoreCase);
            if (p1 == -1)
            {
                return html;
            }

            var p2 = html.IndexOf(s2, p1 + s1.Length, StringComparison.OrdinalIgnoreCase);
            return p2 == -1
                       ? html
                       : html.Substring(0, p1 + s1.Length) +
                         newStr +
                         html.Substring(p2);
        }

        internal static string SubString(string html, string s1, string s2)
        {
            var p1 = html.IndexOf(s1, StringComparison.OrdinalIgnoreCase);
            if (p1 == -1)
            {
                return null;
            }

            var p2 = html.IndexOf(s2, p1 + s1.Length, StringComparison.OrdinalIgnoreCase);
            return p2 == -1 ? null : html.Substring(p1 + s1.Length, p2 - p1 - s1.Length);
        }

        internal static string[] ParseArguments(string str)
        {
            var list = new List<string>();
            var pos = 0;
            do
            {
                var pa = pos;
                if (str[pa] == '\'')
                {
                    var pb = str.IndexOf('\'', pa + 1);
                    if (pb == -1)
                    {
                        break;
                    }

                    var quotedArg = str.Substring(pa + 1, pb - pa - 1);
                    list.Add(quotedArg);
                    pos = pb + 1;
                    if (pos < str.Length)
                    {
                        if (str[pos] != ',')
                        {
                            break;
                        }

                        pos++;
                    }
                }
                else
                {
                    var pb = str.IndexOf(',', pa + 1);
                    if (pb == -1)
                    {
                        pb = str.Length;
                    }

                    var nonquotedArg = str.Substring(pa, pb - pa);
                    list.Add(nonquotedArg);
                    pos = pb + 1;
                }
            } 
            while (pos < str.Length);
            return list.ToArray();
        }

        internal static string[] ParsingUserinfo(string posu)
        {
            var list = new List<string>();
            var pos = 0;
            do
            {
                // Ищем первый '
                var p1 = posu.IndexOf('\'', pos);
                if ((p1 == -1) || ((p1 + 1) == posu.Length)) break;
                // Ищем второй '
                var p2 = posu.IndexOf('\'', p1 + 1);
                if (p2 == -1) break;
                list.Add(posu.Substring(p1 + 1, p2 - p1 - 1));
                // Ищем ,
                if ((p2 + 1) == posu.Length) break;
                var p3 = posu.IndexOf(',', p2 + 1);
                if ((p3 == -1) || ((p3 + 1) == posu.Length)) break;
                pos = p3 + 1;
            } while (true);

            return list.ToArray();
        }

        internal static string[] RandomArray(string source)
        {
            var sp = source.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var list = new List<string>();
            for (var i = 0; i < sp.Length; i++)
            {
                if (string.IsNullOrEmpty(sp[i])) continue;
                if (sp[i][0] == ';') continue;
                list.Add(sp[i]);
            }
    
            if (list.Count == 0) return null;
            var rlist = new List<string>();
            while (list.Count > 0)
            {
                var index = Helpers.Dice.Make(list.Count);
                rlist.Add(list[index]);
                list.RemoveAt(index);
            }

            return rlist.ToArray();
        }

        internal static List<List<string>> ParseJsString(string str)
        {
            // "var fight_ty = [1,300,10,0,2,\"\",\"\",\"1\",\"1948222238\",[],[800817,\"5b5ce67a064cd39446d488a22b366525\",\"БИЗОНИУС\",1387692418]];"

            if (string.IsNullOrEmpty(str) || str.Length < 2)
                return null;

            var result = new List<List<string>>();

            var p1 = 0;
            var p2 = -1;

            while (p1 < str.Length)
            {
                if (str[p1] != '[')
                {
                    p2 = str.IndexOf(',', p1 + 1);
                }
                else
                {
                    p2 = str.IndexOf(']', p1 + 1);
                    if (p2 != -1)
                        p2++;
                }
                
                if (p2 == -1)
                    p2 = str.Length;

                var s = str.Substring(p1, p2 - p1);
                var arg = new List<string>();
                if (s.Length > 0)
                {
                    if (s[0] != '[')
                    {
                        s = s.Trim(new[] {' ', '"', '\''});
                        arg.Add(s);
                    }
                    else
                    {
                        s = s.Trim(new[] { ' ', '[', ']' });
                        var sarg = s.Split(',');
                        foreach (var ss in sarg)
                        {
                            var s1 = ss.Trim(new[] { ' ', '"', '\'' });
                            arg.Add(s1);
                        }
                    }
                }

                result.Add(arg);

                p1 = p2 + 1;
            }


            return result;
        }
    }
}