using ABClient.Properties;
using System;
using System.Text;
using ABClient.Helpers;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] Pinfo(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);

            /*
            html = html.Replace(
                "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//ENhttp://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">", 
                "<!DOCTYPE html>");
             */ 

            //html = html.Replace("<head>", "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">")
            

            //html = html.Replace(".addEventListener(", ".attachEvent(");
            //html = html.Replace(".getElementsByClassName(", ".querySelectorAll('.'+");

            /*
            <SCRIPT language="JavaScript">
            var presents = [];
            var hpmp = [540,595,823,1671,98];
            var params = [['Apo111o',4,'c247.gif',15,'male_7.gif','Окрестности Форпоста [Лесная дорога к Руднику]',1,0,'Чернокнижники','Казначей / Всезнайкин','Форпост','24.11.2006'],[['Сила',1,3],['Ловкость',20,14],['Удача',22,9],['Знания',75,73],['Здоровье',25,0],['Мудрость',1,0]],[['Класс брони',174],['Уловка',260],['Точность',100],['Сокрушение',105],['Стойкость',210],['Пробой брони',45]]];
            var slots = ['njs47ha6.gif:Колпак Ученого (ап):|0|0|27|0|0|105|55@axl50d3w.gif:Амулет Придворного Мага (ап):|0|0|15|0|30|45|70@81ahv7ok.gif:Жезл Стража:|25|32|0|40|90|50|120@vmegly20.gif:Пояс Скорпиона (ап):|0|0|32|0|45|60|75@i_mag_006.gif:Восстановление 250 MP:|0|0|0|0|0|0|5@sl_l_4.gif:Слот для содержимого пояса@sl_l_4.gif:Слот для содержимого пояса@fpnc14v9.gif:Тапки Чародея (ап):|0|0|3|0|40|50|75@sl_r_0.gif:Слот для кармана@sl_r_1.gif:Слот для содержимого кармана@uxr5ksln.gif:Морские Наручи (ап):|0|0|31|5|35|50|70@0ul6cdio.gif:Перчатки Шторма:|0|0|9|0|25|60|70@ke30ss2s.gif:Книга Заклинаний Воздуха:|0|0|0|0|0|0|100@o17mbhdt.gif:Резное Кольцо (ап):|0|0|6|0|25|50|70@o17mbhdt.gif:Резное Кольцо (ап):|0|0|6|0|25|50|70@aof42lbk.gif:Доспех Жреца:|0|0|35|0|45|45|90@'];
            var ability = [[5,'Час Орла']];
            var effects = [];
            var info = ['Andriy','Ukraine','Lutsk',0,'magicsclan.com','','','','','','',[],''];
            view_pinfo_top();
            </SCRIPT>
             */
            /*
            var params0 = HelperStrings.SubString(html, "var params = [[", "],");
            if (!string.IsNullOrEmpty(params0))
            {
                var spar0 = HelperStrings.ParseArguments(params0);
                if (spar0.Length >= 9)
                {
                    var nick = spar0[0].Trim();
                    var align = spar0[1];
                    var sign = spar0[2];
                    var level = spar0[3];
                    var clan = spar0[8];
                    var status = spar0[9];
                    var ali1 = string.Empty;
                    var ali2 = string.Empty;
                    switch (align)
                    {
                        case "1":
                            ali1 = "darks.gif";
                            ali2 = "Дети Тьмы";
                            break;
                        case "2":
                            ali1 = "lights.gif";
                            ali2 = "Дети Света";
                            break;
                        case "3":
                            ali1 = "sumers.gif";
                            ali2 = "Дети Сумерек";
                            break;
                        case "4":
                            ali1 = "chaoss.gif";
                            ali2 = "Дети Хаоса";
                            break;
                        case "5":
                            ali1 = "light.gif";
                            ali2 = "Истинный Свет";
                            break;
                        case "6":
                            ali1 = "dark.gif";
                            ali2 = "Истинная Тьма";
                            break;
                        case "7":
                            ali1 = "sumer.gif";
                            ali2 = "Нейтральные Сумерки";
                            break;
                        case "8":
                            ali1 = "chaos.gif";
                            ali2 = "Абсолютный Хаос";
                            break;
                        case "9":
                            ali1 = "angel.gif";
                            ali2 = "Ангел";
                            break;
                    }

                    if (!string.IsNullOrEmpty(status))
                    {
                        clan = clan + ", " + status;
                    }

                    ChatUserList.AddUser(new ChatUser(nick, level, sign, clan, ali1, ali2));
                }
            }

            var phead = html.IndexOf("</HEAD>", StringComparison.OrdinalIgnoreCase);
            if (phead != -1)
            {
                html = html.Insert(
                    phead, 
                    @"<style type=""text/css"">" +
                    @".invback { background-color: #E0E0E0; } " +
                    @".invhead { background-color: #F9F9F9; font-size: 11px; color: #222222; text-align: center; } " +
                    @".invwhite { background-color: #FFFFFF; } " +
                    @".invname { font: 12px Verdana; color: #000000; background-color: #FFFFFF; padding: 2px; } " +
                    @".invprop, .invpropb { font: 11px Tahoma; color: #F5F5F5; font-weight: bold; " +
                    @"  background-color: #D8CDAF; text-align: center; }" +
                    @".invpropb { border-right: #B9A05C 1px solid; } " +
                    @".invreq, .invreqb { font: 11px Tahoma; color: #333333; " +
                    @"  background-color: #FCFAF3; padding-left: 5px; padding-right: 5px; } " +
                    @".invreqb { border-right: #B9A05C 1px solid; } " +
                    @".thing { font: 12px Verdana; color: #000000; font-weight: bold; }" +
                    @".thingsub { font: 10px Tahoma; color: #cc9999; font-weight: bold; } " +
                    @".description { font: 11px Tahoma; color: #c00000; font-weight: bold; }" +
                    @".up { font: 11px Tahoma; color: #B9A05C; font-weight: bold; }" +
                    ".slobar {" +
                    "position: absolute;" +
                    "left: 0px;" +
                    "visibility: hidden; }" +
                    "</style>");
            }
             * */

            return Russian.Codepage.GetBytes(html);
        }

        /*
        private static void MakeThingPopup(StringBuilder sb, int index, string img, string alt)
        {
            var salt = alt.Split('=');

            sb.Append(@"<div id=""sloi[");
            sb.Append(index);
            sb.Append(@"]"" class=""slobar"">");

            sb.Append("<table cellpadding=0 cellspacing=0 border=0 width=1%>");
            sb.Append("<tr><td class=invback>");
            
                sb.Append("<table cellpadding=3 cellspacing=1 border=0 width=100%>");
                sb.Append("<tr>");

                if (img.StartsWith("slots/sl_", StringComparison.OrdinalIgnoreCase))
                {
                    sb.Append("<td class=invwhite width=99% valign=top>");

                    sb.Append("<table cellpadding=0 cellspacing=0 border=0 width=100%>");
                    sb.Append("<tr><td class=invname nowrap width=100%><span class=thing>" + salt[0] + "</span><br>");
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("</td>");
                }
                else
                {
                    sb.Append("<td class=invhead width=1%><div align=center><img src=http://image.neverlands.ru/weapon/");
                    sb.Append(img);
                    sb.Append(" border=0>");
                    sb.Append("<br><img src=http://image.neverlands.ru/1x1.gif width=62 height=1>");
                    sb.Append("<br><img src=http://image.neverlands.ru/solidst.gif width=62 height=2 border=0></div>");
                    sb.Append("</td>");
                    sb.Append("<td class=invwhite width=99% valign=top>");

                    sb.Append("<table cellpadding=0 cellspacing=0 border=0 width=100%>");
                    sb.Append("<tr><td class=invname nowrap width=100%><span class=thing>" + salt[0] + "</span><br>");
                    if (!Things.ThingsDb.FindPart(img, alt))
                    {
                        sb.Append("<span class=thingsub>Вещь не найдена в базе. Показаны видимые параметры.</span></td></tr>");
                        sb.Append("<tr>");
                        sb.Append("<td colspan=2 width=100%>");

                        sb.Append("<table cellpadding=1 cellspacing=0 border=0 width=100%>");
                        sb.Append("<tr>");
                        sb.Append("<td class=invpropb width=50%>свойства</td>");
                        sb.Append("<td class=invprop width=50%>требования</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class=invreqb nowrap valign=middle>");

                        for (var i = 1; i < salt.Length; i++)
                        {
                            if (salt[i].Contains(": "))
                            {
                                salt[i] = salt[i].Replace(": ", ":<b> ");
                                salt[i] = salt[i] + "</b>";
                            }

                            sb.Append(salt[i]);
                            sb.Append("<br>");
                        }

                        sb.Append("</td>");
                        sb.Append("<td class=invreq valign=middle>");

                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                    }
                    else
                    {
                        sb.Append("<tr>");
                        sb.Append("<td colspan=2 width=100%>");

                        sb.Append("<table cellpadding=1 cellspacing=0 border=0 width=100%>");
                        sb.Append("<tr>");
                        sb.Append("<td class=invpropb width=50%>&nbsp;свойства&nbsp;</td>");
                        sb.Append("<td class=invprop width=50%>&nbsp;требования&nbsp;</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td class=invreqb nowrap valign=middle>");
                        sb.Append(Things.ThingsDb.StrBon);
                        sb.Append("</td>");
                        sb.Append("<td class=invreq nowrap valign=middle>");
                        sb.Append(Things.ThingsDb.StrReq);
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("</table>");
                    }

                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    sb.Append("</td>");
                }

                sb.Append("</tr>");
                sb.Append("</table>");

            sb.Append("</td></tr>");
            sb.Append("</table>");
            sb.Append("</div>");
        }
         */ 
    }
}