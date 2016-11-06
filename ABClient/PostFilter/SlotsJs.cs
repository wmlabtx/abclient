namespace ABClient.PostFilter
{
    using System.Text;
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] SlotsJs(byte[] array)
        {
            /*
            var sb = new StringBuilder(Russian.Codepage.GetString(array));
            var strOldObraz = @"return '<td width='+wsize+' valign=top><img src=http://image.neverlands.ru/1x1.gif width=1 height=23><br><img src=http://image.neverlands.ru/obrazy/'+image+' border=0 width='+wsize+' height=255 alt=""'+nick+'""></td>';";
            var strNewObraz =
                @"var newimage = window.external.ObrazSubstitute(nick, image); " +
                @"var htmlcode = '<td width='+wsize+' valign=top><img src=http://image.neverlands.ru/1x1.gif width=1 height=23><br><img src='+newimage+' border=0 width='+wsize+' height=255 alt=""'+nick+'"">'; " +
                @"if (newimage.indexOf('abclient.1gb.ru') == -1) " +
                "return htmlcode + '</td>'; " +
                @"else " +
                @"return htmlcode + '<br><table cellpadding=0 cellspacing=1 border=0><tr><td bgcolor=white><table cellpadding=0 cellspacing=1 border=0><tr><td bgcolor=#BFBFBF width=255 align=center title=""Установка индивидуального образа - $10. Свяжитесь с автором ABClient (ICQ: 92138051)."" style=""cursor:hand; font-family:Verdana; font-size:9px; font-weight:bold; color:white"">ABClient</td></tr></table></td></tr></table></td>';";
            sb.Replace(strOldObraz, strNewObraz);
            sb.Replace("alt=", "title=");
            return Russian.Codepage.GetBytes(sb.ToString());
            */

            return array;
        }
    }
}