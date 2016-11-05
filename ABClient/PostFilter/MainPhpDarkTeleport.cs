using System.Text;
using ABClient.Helpers;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static string MainPhpDarkTeleport(string html)
        {
            // abil_1(3,'5526edcf95299cde84cc07c5fb7d630b')

            var vcode = HelperStrings.SubString(html, "abil_1(3,'", "'");
            if (string.IsNullOrEmpty(vcode))
                return null;

            /*
              <input type=hidden name=useaction value="addon-action">
              <input type=hidden name=addid value="1">
              <input type=hidden name=post_id value="31">
              <input type=hidden name=vcode value="'+vcode+'">
              <SELECT name=wtelid class=zayavki> 1-12
             */

            var sb = new StringBuilder();
            sb.Append(
                HelperErrors.Head() +
                "Используем сумеречный телепорт...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=useaction type=hidden value=""");
            sb.Append("addon-action");
            sb.Append(@""">");

            sb.Append(@"<input name=addid type=hidden value=""");
            sb.Append(1);
            sb.Append(@""">");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(31);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(vcode);
            sb.Append(@""">");

            int wtelid = Dice.Make(12) + 1;

            sb.Append(@"<input name=wtelid type=hidden value=""");
            sb.Append(wtelid);
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