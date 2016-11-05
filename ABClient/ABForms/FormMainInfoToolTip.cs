namespace ABClient.ABForms
{
    using System;
    using System.Text;

    internal sealed partial class FormMain
    {
        internal static string InfoToolTip(string img, string alt)
        {
            // "<b>Серп собирателя</b> (Любимый декапитатор)<br>Удар: 2-5"
            //  (Нейтральные Сумерки (Шандор))<br>Удар: 6-13<br>Пробой брони: +17<br>MP: +40
            //  (Любимый декапитатор)<br>Удар: 2-5
            //  (С Нами теплее)
            string name = string.Empty;
            string raw = string.Empty;
            string grav = string.Empty;
            var pos = alt.IndexOf("</b>", StringComparison.OrdinalIgnoreCase);
            if (pos > 3)
            {
                name = alt.Substring(3, pos - 3);
                raw = alt.Substring(pos + 4);
                if (raw.StartsWith(" (", StringComparison.Ordinal))
                {
                    pos = raw.IndexOf("<br>", StringComparison.OrdinalIgnoreCase);
                    if (pos == -1)
                    {
                        grav = raw.Substring(2, raw.Length - 3);
                        raw = string.Empty;
                    }
                    else
                    {
                        grav = raw.Substring(2, pos - 3);
                        raw = raw.Substring(pos + 4);
                    }
                }
            }

            var sb = new StringBuilder();
            sb.Append(@"<div>");

            sb.Append("<table cellpadding=0 cellspacing=0 border=0 width=1%>");
            sb.Append("<tr><td class=invback>");

            sb.Append("<table cellpadding=3 cellspacing=1 border=0 width=100%>");
            sb.Append("<tr>");

            if (img.StartsWith("sl_", StringComparison.OrdinalIgnoreCase))
            {
                sb.Append("<td class=invwhite width=99% valign=top>");

                sb.Append("<table cellpadding=0 cellspacing=0 border=0 width=100%>");
                sb.Append("<tr><td class=invname nowrap width=100%><span class=thing>" + name + "</span><br>");
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
                sb.Append("<tr><td class=invname nowrap width=100%><span class=thing>" + name + "</span><br>");
                if (!string.IsNullOrEmpty(grav))
                {
                    sb.Append("<span class=thingsub>" + grav + "</span><br>");
                }

                if (!Things.ThingsDb.FindPart(img, raw))
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

                    var salt = raw.Split(new[] { "<br>" }, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < salt.Length; i++)
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

            return sb.ToString();
        }
    }
}