using ABClient.Helpers;

namespace ABClient.PostFilter
{

    internal static partial class Filter
    {
        private static byte[] PinfoJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            //html = Resources.nl_pinfo_v04;
            
            /*
            html = html.Replace(".addEventListener(", ".attachEvent(");
            html = html.Replace(".getElementsByClassName(", ".querySelectorAll('.'+");
             */ 
            //html = "alert(navigator.appVersion);" + html; 
            
            /*
            sb.Replace(
                @"<font color=#cc0000>Причина: '+pr_i+'</font></b><br><br>';",
                @"<font color=#cc0000>Причина: ' + pr_i + '</font></b><br><br>';" +
                @"}else if (pr_i != ''){" +
                @"return '<br><b><font class=nickname><font color=#0000cc>Персонаж бывал в тюрьме<br>Выпущен с причиной: '+pr_i+'</font></font></b><br><br>';");
            sb.Replace(
                @"d.write('<table cellpadding=0 cellspacing=0 border=0 width=62>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[0]+' width=62 height=65 alt=""'+left_alt_arr[0]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[1]+' width=62 height=35 alt=""'+left_alt_arr[1]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[2]+' width=62 height=91 alt=""'+left_alt_arr[2]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[3]+' width=62 height=30 alt=""'+left_alt_arr[3]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[4]+' width=20 height=20 alt=""'+left_alt_arr[4]+'"">" +
                @"<img src=http://image.neverlands.ru/weapon/slots/1x1gr.gif width=1 height=20>" +
                @"<img src=http://image.neverlands.ru/weapon/'+left_slo_arr[5]+' width=20 height=20 alt=""'+left_alt_arr[5]+'"">" +
                @"<img src=http://image.neverlands.ru/weapon/slots/1x1gr.gif width=1 height=20>" +
                @"<img src=http://image.neverlands.ru/weapon/'+left_slo_arr[6]+' width=20 height=20 alt=""'+left_alt_arr[6]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[7]+' width=62 height=63 alt=""'+left_alt_arr[7]+'""></td></tr></table>');",

                @"d.write('<table cellpadding=0 cellspacing=0 border=0 width=62>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[0]+' width=62 height=65 onmousemove=""showslo(event, 0)"" onmouseout=""hideslo(0)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[1]+' width=62 height=35 onmousemove=""showslo(event, 1)"" onmouseout=""hideslo(1)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[2]+' width=62 height=91 onmousemove=""showslo(event, 2)"" onmouseout=""hideslo(2)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[3]+' width=62 height=30 onmousemove=""showslo(event, 3)"" onmouseout=""hideslo(3)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[4]+' width=20 height=20 onmousemove=""showslo(event, 4)"" onmouseout=""hideslo(4)"">" +
                @"<img src=http://image.neverlands.ru/weapon/slots/1x1gr.gif width=1 height=20>" +
                @"<img src=http://image.neverlands.ru/weapon/'+left_slo_arr[5]+' width=20 height=20 onmousemove=""showslo(event, 5)"" onmouseout=""hideslo(5)"">" +
                @"<img src=http://image.neverlands.ru/weapon/slots/1x1gr.gif width=1 height=20>" +
                @"<img src=http://image.neverlands.ru/weapon/'+left_slo_arr[6]+' width=20 height=20 onmousemove=""showslo(event, 6)"" onmouseout=""hideslo(6)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+left_slo_arr[7]+' width=62 height=63 onmousemove=""showslo(event, 7)"" onmouseout=""hideslo(7)""></td></tr></table>');");

            sb.Replace(
                @"d.write('<table cellpadding=0 cellspacing=0 border=0 width=62>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[0]+' width=20 height=20 alt=""'+right_alt_arr[0]+'"">" +
                @"<img src=http://image.neverlands.ru/weapon/'+right_slo_arr[1]+' width=42 height=20 alt=""'+right_alt_arr[1]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[2]+' width=62 height=40 alt=""'+right_alt_arr[2]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[3]+' width=62 height=40 alt=""'+right_alt_arr[3]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[4]+' width=62 height=91 alt=""'+right_alt_arr[4]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[5]+' width=31 height=31 alt=""'+right_alt_arr[5]+'"">" +
                @"<img src=http://image.neverlands.ru/weapon/'+right_slo_arr[6]+' width=31 height=31 alt=""'+right_alt_arr[6]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[7]+' width=62 height=83 alt=""'+right_alt_arr[7]+'""></td></tr></table>');",

                @"d.write('<table cellpadding=0 cellspacing=0 border=0 width=62>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[0]+' width=20 height=20 alt=""'+right_alt_arr[0]+'"">" +
                @"<img src=http://image.neverlands.ru/weapon/'+right_slo_arr[1]+' width=42 height=20 alt=""'+right_alt_arr[1]+'""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[2]+' width=62 height=40 onmousemove=""showslo(event, 10)"" onmouseout=""hideslo(10)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[3]+' width=62 height=40 onmousemove=""showslo(event, 11)"" onmouseout=""hideslo(11)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[4]+' width=62 height=91 onmousemove=""showslo(event, 12)"" onmouseout=""hideslo(12)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[5]+' width=31 height=31 onmousemove=""showslo(event, 13)"" onmouseout=""hideslo(13)"">" +
                @"<img src=http://image.neverlands.ru/weapon/'+right_slo_arr[6]+' width=31 height=31 onmousemove=""showslo(event, 14)"" onmouseout=""hideslo(14)""></td></tr>" +
                @"<tr><td><img src=http://image.neverlands.ru/weapon/'+right_slo_arr[7]+' width=62 height=83 onmousemove=""showslo(event, 15)"" onmouseout=""hideslo(15)""></td></tr></table>');");
             */ 
            /*
            var strOldObrazHead = "d.write('<HTML><HEAD><TITLE>NeverLands.Ru [ Информация: '+nickname+' ]</TITLE>');";
            var strNewObrazHead =
                "var newimage = window.external.ObrazSubstitute(nickname, image); " +
                "var obrazsuffix; " +
                "if (newimage.indexOf('abclient.1gb.ru') == -1) " +
                "obrazsuffix = '</td>'; " +
                @"else " +
                @"obrazsuffix = '<br><table cellpadding=0 cellspacing=1 border=0><tr><td bgcolor=white><table cellpadding=0 cellspacing=1 border=0><tr><td bgcolor=#BFBFBF width=255 align=center title=""Установка индивидуального образа - $10. Свяжитесь с автором ABClient (ICQ: 92138051)."" style=""cursor:hand; font-family:Verdana; font-size:9px; font-weight:bold; color:white"">ABClient</td></tr></table></td></tr></table></td>'; " +
                "d.write('<HTML><HEAD><TITLE>NeverLands.Ru [ Информация: '+nickname+' ]</TITLE>');";
            sb.Replace(strOldObrazHead, strNewObrazHead);
                 
            var strOldObraz = @"<img src=http://image.neverlands.ru/1x1.gif width=1 height=23><br><img src=http://image.neverlands.ru/obrazy/'+image+' border=0 width=115 height=255 alt=""'+nickname+'""></td>";
            var strNewObraz = @"<img src=http://image.neverlands.ru/1x1.gif width=1 height=23><br><img src='+newimage+' border=0 width=115 height=255 alt=""'+nickname+'"">'+obrazsuffix+'";
            sb.Replace(strOldObraz, strNewObraz);
            */
            return Russian.Codepage.GetBytes(html);
        }
    }
}