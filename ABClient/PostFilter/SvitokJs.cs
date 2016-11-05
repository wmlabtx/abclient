namespace ABClient.PostFilter
{
    using Helpers;
    using System.Text;

    internal static partial class Filter
    {
        private static byte[] SvitokJs(byte[] array)
        {
            var sb = new StringBuilder(Russian.Codepage.GetString(array));
            sb.Replace(
                @"document.all(""transfer"").innerHTML = '" +
                @"<form action=main.php method=POST><input type=hidden name=magicrestart value=""1"">" +
                @"<input type=hidden name=magicreuid value='+wuid+'><input type=hidden name=vcode value='+wmcode+'>" +
                @"<input type=hidden name=post_id value=46><table cellpadding=0 cellspacing=0 border=0 width=100%>" +
                @"<tr><td bgcolor=#B9A05C><table cellpadding=3 cellspacing=1 border=0 width=100%><tr>" +
                @"<td width=100% bgcolor=#FCFAF3><font class=nickname><b>Использовать ""'+wnametxt+'"" сейчас?</b>" +
                @"</div></td></tr><tr><td bgcolor=#FCFAF3><font class=nickname><b>Кому:</b> " +
                @"<INPUT TYPE=""text"" name=fornickname class=LogintextBox value=""'+wnickname+'"" maxlength=25> " +
                @"<input type=submit value=""выполнить"" class=lbut> " +
                @"<input type=button class=lbut onclick=""closeform()"" value="" x ""></td></tr></table>" +
                @"</td></tr></table></FORM>';",
                @"document.all(""transfer"").innerHTML = '" +
                @"<form action=main.php method=POST><input type=hidden name=magicrestart value=""1"">" +
                @"<input type=hidden name=magicreuid value='+wuid+'><input type=hidden name=vcode value='+wmcode+'>" +
                @"<input type=hidden name=post_id value=46><table cellpadding=0 cellspacing=0 border=0 width=100%>" +
                @"<tr><td bgcolor=#B9A05C><table cellpadding=3 cellspacing=1 border=0 width=100%><tr>" +
                @"<td width=100% bgcolor=#FCFAF3><font class=nickname><b>Использовать ""'+wnametxt+'"" сейчас?</b>" +
                @"</div></td></tr><tr><td bgcolor=#FCFAF3><font class=nickname><b>Кому:</b> " +
                @"<INPUT TYPE=""text"" name=fornickname class=LogintextBox value=""'+wnickname+'"" maxlength=25> " +
                @"<input type=submit value=""выполнить"" class=lbut onclick=""window.external.TraceDrinkPotion(fornickname.value, \''+wnametxt+'\')""> " +
                @"<input type=button class=lbut onclick=""closeform()"" value="" x ""></td></tr></table>" +
                @"</td></tr></table></FORM>';");
            return Russian.Codepage.GetBytes(sb.ToString());

            /*
var ActionFormUse;
function closeform()
{
       document.all("transfer").style.visibility = "hidden";
       document.all("transfer").innerHTML = '<img src=image/1x1.gif width=1 height=1>';
       top.frames['ch_buttons'].document.FBT.text.focus();
       ActionFormUse = '';
}

function magicreform(wuid,wnickname,wnametxt,wmcode)
{
       top.frames['ch_buttons'].document.FBT.text.focus();
       document.all("transfer").innerHTML = '<form action=main.php method=POST><input type=hidden name=magicrestart value="1"><input type=hidden name=magicreuid value='+wuid+'><input type=hidden name=vcode value='+wmcode+'><input type=hidden name=post_id value=46><table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#B9A05C><table cellpadding=3 cellspacing=1 border=0 width=100%><tr><td width=100% bgcolor=#FCFAF3><font class=nickname><b>Использовать "'+wnametxt+'" сейчас?</b></div></td></tr><tr><td bgcolor=#FCFAF3><font class=nickname><b>Кому:</b> <INPUT TYPE="text" name=fornickname class=LogintextBox value="'+wnickname+'" maxlength=25> <input type=submit value="выполнить" class=lbut> <input type=button class=lbut onclick="closeform()" value=" x "></td></tr></table></td></tr></table></FORM>';
       document.all("transfer").style.visibility = "visible";
       document.all("fornickname").focus();
       ActionFormUse = 'fornickname';
}

function fightmagicform(wuid,wmid,wnametxt,wmcode,wmsolid)
{
       top.frames['ch_buttons'].document.FBT.text.focus();
       document.all("transfer").innerHTML = '<form action=main.php method=POST><input type=hidden name=fightmagicstart value="1"><input type=hidden name=post_a value=0><input type=hidden name=uid value='+wuid+'><input type=hidden name=mid value='+wmid+'><input type=hidden name=wmsolid value='+wmsolid+'><input type=hidden name=fmc value='+wmcode+'><table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#B9A05C><table cellpadding=3 cellspacing=1 border=0 width=100%><tr><td width=100% bgcolor=#FCFAF3><font class=nickname><b>Использовать "'+wnametxt+'" сейчас?</b></div></td></tr><tr><td bgcolor=#FCFAF3><font class=nickname><b>На кого:</b> <INPUT TYPE="text" name=fornickname class=LogintextBox maxlength=25> <input type=submit value="выполнить" class=lbut> <input type=button class=lbut onclick="closeform()" value=" x "></td></tr></table></td></tr></table></FORM>';
       document.all("transfer").style.visibility = "visible";
       document.all("fornickname").focus();
       ActionFormUse = 'fornickname';
}

function chatsleepform(wuid,wmid,wsolid,wcode,wnametxt)
{
       top.frames['ch_buttons'].document.FBT.text.focus();
       document.all("transfer").innerHTML = '<form action=main.php method=POST><input type=hidden name=post_id value=26><input type=hidden name=wuid value='+wuid+'><input type=hidden name=wmid value='+wmid+'><input type=hidden name=wsolid value='+wsolid+'><input type=hidden name=vcode value='+wcode+'><table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#B9A05C><table cellpadding=3 cellspacing=1 border=0 width=100%><tr><td width=100% bgcolor=#FCFAF3><font class=nickname><b>Использовать "'+wnametxt+'" сейчас?</b></td></tr><tr><td bgcolor=#FCFAF3><div align=center><font class=nickname><b>На кого:</b> <INPUT TYPE="text" name=fornickname class=LogintextBox maxlength=25> <input type=submit value="выполнить" class=lbut> <input type=button class=lbut onclick="closeform()" value=" x "></div></td></tr></table></td></tr></table></FORM>';
       document.all("transfer").style.visibility = "visible";
       document.all("fornickname").focus();
       ActionFormUse = 'fornickname';
}

function abil_svitok(wuid,wmid,wmsolid,wnametxt,wmcode)
{
       top.frames['ch_buttons'].document.FBT.text.focus();
       document.all("transfer").innerHTML = '<form action=main.php method=POST><input type=hidden name=post_id value=44><input type=hidden name=uid value='+wuid+'><input type=hidden name=mid value='+wmid+'><input type=hidden name=curs value='+wmsolid+'><input type=hidden name=vcode value='+wmcode+'><table cellpadding=0 cellspacing=0 border=0 width=100%><tr><td bgcolor=#B9A05C><table cellpadding=3 cellspacing=1 border=0 width=100%><tr><td width=100% bgcolor=#FCFAF3><font class=nickname><b>Использовать "'+wnametxt+'" сейчас?</b></div></td></tr><tr><td bgcolor=#FCFAF3><font class=nickname><b>На кого:</b> <INPUT TYPE="text" name=fnick class=LogintextBox maxlength=25> <input type=submit value="выполнить" class=lbut> <input type=button class=lbut onclick="closeform()" value=" x "></td></tr></table></td></tr></table></FORM>';
       document.all("transfer").style.visibility = "visible";
       document.all("fnick").focus();
       ActionFormUse = 'fnick';
}        
             */
             }
    }
}