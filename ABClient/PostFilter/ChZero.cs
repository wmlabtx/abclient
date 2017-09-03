namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] ChZero(byte[] array)
        {
            var html = Helpers.Russian.Codepage.GetString(array);
            
            /*
            html =
                @"<script>top.frames['chmain'].add_msg('<font class=massm>&nbsp;NeverLands.Ru&nbsp;</font> <font color=000000>" + 
                @"<B>Внимание! Акция!</B> Установка любого образа в Доме Дилеров в автоматическом режиме - <B>20 DNV</B>!  " + 
                @"Новый платный сервис - Отмена защитного кода (CAPTCHA). <font class=freemain>" + 
                @"<a href=""http://forum.neverlands.ru/1/1/125264/1/"" target=""_blank""><font color=#3564A5><B>Подробнее...</B></font></a></font></font><BR>'+'');top.set_lmid(8);</script>";

             */
            
            /*
            var fix = new StringBuilder();
            fix.AppendLine(@"<script>");
            fix.AppendLine(@"top.frames['chmain'].add_msg('<font class=prchattime>&nbsp;16:35:08&nbsp;</font> <SPL><SPAN>GoD Killer</SPAN> <SPL>%<Черный><SPL> <font color=6600FF>хорошо через 10 минут, кинь скайп</font> <BR>'+'');");
            fix.AppendLine(@"top.frames['chmain'].add_msg('<font class=clchattime>&nbsp;18:22:43&nbsp;</font> <SPL><SPAN>KIL</SPAN> <SPL>%<pair> %<-Exit-><SPL> <font color=FF0000> ок </font> <BR>'+'');");
            fix.AppendLine(@"top.frames['chmain'].add_msg('<font class=clchattime>&nbsp;18:21:42&nbsp;</font> <font color=9966CC><i><SPL><SPAN>Рыж@я Стерв@</SPAN> <SPL>%<clan> %<УбИйЦа С тОпОрОм><SPL> шошо? а я отписала( щас скажу шоб не стучал</i></font> <BR>'+'');");
            fix.AppendLine(@"top.set_lmid(8);");
            fix.AppendLine(@"</script>");
            html = fix.ToString();
            */

            /*
            html =
                @"<script>" + 
                "top.frames['chmain'].add_msg('top.frames['chmain'].add_msg('<font class=clchattime>&nbsp;18:21:42&nbsp;</font> <font color=9966CC><i><SPL><SPAN>Рыж@я Стерв@</SPAN> <SPL>%<clan> %<УбИйЦа С тОпОрОм><SPL> шошо? а я отписала( щас скажу шоб не стучал</i></font> <BR>'+'');" + 
                "top.frames['chmain'].add_msg('<font class=clchattime>&nbsp;18:22:43&nbsp;</font> <SPL><SPAN>KIL</SPAN> <SPL>%<pair> %<-Exit-><SPL> <font color=FF0000> ок </font> <BR>'+'');" +
                "top.frames['chmain'].add_msg('<font class=prchattime>&nbsp;16:35:08&nbsp;</font> <SPL><SPAN>GoD Killer</SPAN> <SPL>%<Черный><SPL> <font color=6600FF>хорошо через 10 минут, кинь скайп</font> <BR>'+'');" +
                "top.set_lmid(8);</script>";
             */ 

            /*
            html = "<script>" + 
                "top.frames['chmain'].add_msg('<font class=clchattime>&nbsp;18:21:42&nbsp;</font> <font color=9966CC><i><SPL><SPAN>Рыж@я Стерв@</SPAN> <SPL>%<clan> %<УбИйЦа С тОпОрОм><SPL> шошо? а я отписала( щас скажу шоб не стучал</i></font> <BR>'+'');" +
                "top.frames['chmain'].add_msg('<font class=clchattime>&nbsp;18:22:43&nbsp;</font> <SPL><SPAN>KIL</SPAN> <SPL>%<pair> %<-Exit-><SPL> <font color=FF0000> ок </font> <BR>'+'');" +
                "top.set_lmid(8);</script>";
                */

            /*
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                        new object[] { html });
                }
            }
            catch (InvalidOperationException)
            {
            }
             */ 

            /*
            if (AppVars.DoChatTip)
            {
                var pos1 = html.IndexOf("'<font class=massm>&nbsp;NeverLands.Ru", StringComparison.Ordinal);
                if (pos1 != -1)
                {
                    var pos2 = html.IndexOf("<BR>'", pos1, StringComparison.Ordinal);
                    if (pos2 != -1)
                    {
                        var message =
                            @"'<SPAN class=massm>&nbsp;" + "abclient.1gb.ru" + "&nbsp;</SPAN> " +
                            "<FONT color=#000000> " +
                            Tips.GetNextAnswer() +
                            "</FONT><BR>" +
                            @"'<SPAN class=massm>&nbsp;" + "abclient.1gb.ru" + "&nbsp;</SPAN> " +
                            "<FONT color=#000000> " +
                            "Дней до истечения лицензии: " +
                            "<FONT color=#008000><b>" +
                            (int)(Key.KeyFile.ExpirationDate.Subtract(AppVars.ServerDateTime).TotalDays) +
                            "</b></FONT><BR>" + 
                            "'";

                        pos2 += "<BR>'".Length;
                        html =
                            html.Substring(0, pos1) +
                            message +
                            html.Substring(pos2, html.Length - pos2);
                        AppVars.DoChatTip = false;
                    }
                }
            }
             */

            return Helpers.Russian.Codepage.GetBytes(html);
        }
    }
}
