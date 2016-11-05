namespace ABClient.PostFilter
{
    using System.Text;
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] ChMsgJs(byte[] array)
        {
            var sb = new StringBuilder(Russian.Codepage.GetString(array));
            sb.Replace(
                @"s += txt + ""<BR>"";",
                @"s += window.external.ChatFilter(txt) + ""<BR>"";");
            sb.Replace(
                ",65000);",
                ", 65000); window.external.ChatUpdated()");

            //sb.Replace("var o = e.target || e.srcElement;", "var o = e.target || e.srcElement; alert('o.tagName = ' + o.tagName);");
            //sb.Replace("if (o.alt != null && o.alt.length>0) login=o.alt;", "if (o.alt != null && o.alt.length>0) login=o.alt; alert('o.alt = ' + o.alt + 'login = ' + login);");
            
            /*
            sb.Replace(
                "if(user2 != '') msgp[1] = msgp[1].replace('<SPAN>','<SPAN alt=\"%'+user2+'\">');",
                "if(user2 != '') { if (txt.indexOf('%<clan>') >= 0) { msgp[1] = msgp[1].replace('<SPAN>','<SPAN alt=\"%%'+user2+'\">'); } " +
                    "else { if (txt.indexOf('%<pair>') >= 0) { msgp[1] = msgp[1].replace('<SPAN>','<SPAN alt=\"%%%'+user2+'\">'); } else " + 
                    "{ msgp[1] = msgp[1].replace('<SPAN>', '<SPAN alt=\"%'+user2+'\">'); } } } ");
             */
            /*
            sb.Replace(
                "if(user2 != '') msgp[2] = msgp[2].replace(' '+user,' <SPAN alt=\"'+user2+'\">'+user+'</SPAN>');",
                "if(user2 != '') { if (txt.indexOf('%<clan>') >= 0) if(user2 != '') msgp[2] = msgp[2].replace(' '+user,' <SPAN alt=\"%%'+user+'\">'+user+'</SPAN>'); " +
                    "else if (txt.indexOf('%<pair>') >= 0) if(user2 != '') msgp[2] = msgp[2].replace(' '+user,' <SPAN alt=\"%%%'+user+'\">'+user+'</SPAN>'); else " +
                    "if(user2 != '') msgp[2] = msgp[2].replace(' ' + user,' <SPAN alt=\"%'+user+'\">'+user+'</SPAN>'); }");
             */

            sb.Replace(
                "msgp[2].replace(user,'<SPAN alt=\"%'+user2+'\">'+user+'</SPAN>');",
                "msgp[2].replace(user,'<SPAN alt=\"%'+user+'\">'+user+'</SPAN>');");

            sb.Replace(
                "msgp[2] = msgp[2].replace(' '+user,' <SPAN alt=\"'+user2+'\">'+user+'</SPAN>');",
                "msgp[2] = msgp[2].replace(' '+user,' <SPAN alt=\"'+user+'\">'+user+'</SPAN>');");

            sb.Replace(
                "login = login.replace ('%', '');",
                "top.frames['ch_buttons'].document.FBT.text.focus(); " +
                "var prompt = top.frames['ch_buttons'].document.FBT.text.value; " +
                "if (prompt.indexOf('%clan%') == 0 || prompt.indexOf('%pair%') == 0) { login = login.replace('%',''); login = login.replace('%',''); login = login.replace('%',''); top.frames['ch_buttons'].document.FBT.text.value = prompt + '%<' + login + '> '; return false; } else {" +
                "if (login.charAt(2) == '%'){ login = login.substr(3); top.frames['ch_buttons'].document.FBT.text.value = '%pair%%<' + login + '> ' + prompt; return false; } else " +
                "if (login.charAt(1) == '%'){ login = login.substr(2); top.frames['ch_buttons'].document.FBT.text.value = '%clan%%<' + login + '> ' + prompt; return false; } else login = login.substr(1); }");

            sb.Replace("alt=","title=");
            sb.Replace(".alt", ".title");

            //sb.Replace(" + document.documentElement.scrollLeft + document.body.scrollLeft", "");
            //sb.Replace(" + document.documentElement.scrollTop + document.body.scrollTop", "");

            sb.Replace(" + document.body.scrollLeft", "");
            sb.Replace(" + document.body.scrollTop", "");


            //sb.Replace("y -= e.clientY + 72 > document.body.clientHeight ? 70 : 2;", "y -= e.clientY + 72 > document.body.clientHeight ? 70 : 2; alert('e.clientX=' +e.clientX+ ' e.clientY=' +e.clientY+ ' x=' +x+ ' y='+y); x = e.clientX; y = e.clientY;");

            //sb.Replace("y -= e.clientY + 72 > document.body.clientHeight ? 70 : 2;", "");

            return Russian.Codepage.GetBytes(sb.ToString());
        }
    }
}