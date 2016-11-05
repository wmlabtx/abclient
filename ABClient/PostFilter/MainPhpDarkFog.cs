using System.Text;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static string MainPhpDarkFog(string html)
        {
            // abil_2(3,'29396edee4f3a980ee244816a7b8a46d')

            var vcode = HelperStrings.SubString(html, "abil_2(3,'", "'");
            if (string.IsNullOrEmpty(vcode))
                return null;
            /*
             * <input type=hidden name=useaction value="addon-action">
             * <input type=hidden name=addid value="1">
             * <input type=hidden name=post_id value="32">
             * <input type=hidden name=vcode value="'+vcode+'">
             * <INPUT TYPE="text" name=pnick class=zayavki maxlength=25>
             */

            var sb = new StringBuilder();
            sb.Append(
                HelperErrors.Head() +
                "Используем сумеречный туман...");
            sb.Append("<form action=main.php method=POST name=ff>");

            sb.Append(@"<input name=useaction type=hidden value=""");
            sb.Append("addon-action");
            sb.Append(@""">");

            sb.Append(@"<input name=addid type=hidden value=""");
            sb.Append(1);
            sb.Append(@""">");

            sb.Append(@"<input name=post_id type=hidden value=""");
            sb.Append(32);
            sb.Append(@""">");

            sb.Append(@"<input name=vcode type=hidden value=""");
            sb.Append(vcode);
            sb.Append(@""">");

            sb.Append(@"<INPUT name=pnick type=hidden value=""");
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