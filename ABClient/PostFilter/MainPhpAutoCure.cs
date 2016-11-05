using System;
using System.Text;

namespace ABClient.PostFilter
{
    using MyHelpers;

    internal static partial class Filter
    {
        private static int[] GetPoisonAndWounds(string html)
        {
            if (!MainPhpIsPerc(html) && !MainPhpIsInv(html))
                return null;

            if (html.IndexOf("<a href=\"?wfo=1\">", StringComparison.CurrentCultureIgnoreCase) == -1)
                return null;

            var poisonAndWounds = new int[4];

            // <script>var cureff = [[77,528413],[25,29589],[25,959],[56,15359],[76,9903],[83,9913],[71,9925],[73,9956],[46,6367],[2,28790]]; effects_view();</script>
            // <script>var cureff = [[77,415601]]; effects_view();</script>

            var streff = HelperStrings.SubString(html, "<script>var cureff = [[", "]]; effects_view();</script>");
            if (string.IsNullOrEmpty(streff))
                return poisonAndWounds;
            
            var par = streff.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
            if (par.Length == 0)
                return poisonAndWounds;

            foreach (var elem in par)
            {
                var pair = elem.Split(',');
                if (pair.Length != 2)
                    continue;

                switch (pair[0])
                {
                    case "2":
                        poisonAndWounds[3]++; // тяжелые
                        break;
                    case "3":
                        poisonAndWounds[2]++; // средние
                        break;
                    case "4":
                        poisonAndWounds[1]++; // легкие
                        break;
                    case "24":
                        poisonAndWounds[0]++;
                        break;
                }
            }

            return poisonAndWounds;
        }

        private static string MainPhpRemovePoison(string html)
        {
            // magicreform('74717180','Черный','Зелье Лечения Отравлений','73f8b8aa2932d667a6f37b843aa407e6')

            var namepotion = '\'' + "Зелье Лечения Отравлений" + '\'';
            var p0 = html.IndexOf(namepotion, StringComparison.OrdinalIgnoreCase);
            if (p0 == -1) return null;
            var ps = html.LastIndexOf('<', p0);
            if (ps == -1) return null;
            ps++;
            var pe = html.IndexOf('>', p0);
            if (pe == -1) return null;
            var args = html.Substring(ps, pe - ps);
            if (args.IndexOf("magicreform(", StringComparison.Ordinal) == -1)
                return null;

            args = HelperStrings.SubString(args, "magicreform('", "')");
            if (string.IsNullOrEmpty(args)) return null;
            var arg = args.Split('\'');
            if (arg.Length < 7) return null;
            var wuid = arg[0];
            var wmcode = arg[6];

            var sb = new StringBuilder(
                HelperErrors.Head() +
                "Используем ");
            sb.Append("Зелье Лечения Отравлений");
            sb.Append(" на себя...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=magicrestart type=hidden value=""");
            sb.Append(1);
            sb.Append(@""">");

            sb.Append(@"<input name=magicreuid type=hidden value=""");
            sb.Append(wuid);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(wmcode);
            sb.Append(@""">");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(46);
            sb.Append(@""">");

            sb.Append(@"<input name=fornickname type=hidden value=""");
            sb.Append(AppVars.Profile.UserNick);
            sb.Append(@""">");

            sb.Append(
                @"</form>" +
                @"<script language=""JavaScript"">" +
                @"document.ff.submit();" +
                @"</script></body></html>");

            return sb.ToString();
        }
    }
}
