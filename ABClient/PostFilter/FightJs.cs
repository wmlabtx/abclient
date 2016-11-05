namespace ABClient.PostFilter
{
    using System.Text;

    /// <summary>
    /// Подмена fight.js.
    /// </summary>
    internal static partial class Filter
    {
        private static byte[] FightJs(byte[] array)
        {
            var sb = new StringBuilder(Helpers.Russian.Codepage.GetString(array));

            if (AppVars.Profile.DoGuamod)
            {
                sb.Replace(
                    @"code.php?'+fexp[4]+'"" width=134 height=60></TD>",
                    @"code.php?'+fexp[4]+'"" width=134 height=60><br><img src=http://image.neverlands.ru/1x1.gif width=1 height=8><br><span id=guamod3><font class=nickname><font color=#004A7F><b>* * * *</b></font></font></span></TD>");
            }

            /*var fightshow = (AppVars.Autoboi == AutoboiState.AutoboiOn) ? @"if (secs > " + Helpers.Dice.Make(5) + @") { AutoSelect(0); }" : string.Empty;*/
            sb.Replace(
                @"<input type=button value="" xoд "" name=""btx0"" class=fbut onclick=""javascript: StartAct()""> " +
                @"<input type=button value=сбросить name=""bt2"" class=fbut onclick=""javascript: RefreshF()"">",

                @"<input type=button value="" ход (0:00)"" name=""btx0"" class=fbut onclick=""javascript: myStartAct()""> " +
                @"<input type=button value=""автовыбор"" name=""btav"" title=""Предложить ход"" class=fbut onclick=""javascript: AutoSelect()""> " +
                @"<input type=button value=""автоход"" name=""btav"" title=""Один ход"" class=fbut onclick=""javascript: AutoTurn()""> " +
                @"<input type=button value=""автобой"" name=""btab"" title=""Полный автобой"" class=fbut onclick=""javascript: AutoBoi()""> " +
                @"<input type=button value=""сбросить"" name=""bt2"" class=fbut onclick=""javascript: RefreshF()""> " +
                @" <style type=""text/css"">" +
                @" .fbutred {" +
                @"  BACKGROUND: #ffcccc;" +
                @"  BORDER: solid 1px #dea6a6" +
                @"  COLOR: #333333;" +
                @"  CURSOR: hand;" +
                @"  FONT: 11px Tahoma, Verdana, Arial;" +
                @"  FONT-WEIGHT: bold;" +
                @" }" +
                @" </style>" +
                @" <SCRIPT language=""JavaScript"">" +
                @"  document.all(""btx0"").value = window.external.XodButtonElapsedTime();" +
                /*
                @" var mins = 0;" +
                @" var secs = 0;" +
                 */ 
                @"  var curTimeInt = setInterval(""xodtimerproc()"",1000);" +
                @"  function xodtimerproc(){ " +
                @"   document.all(""btx0"").value = window.external.XodButtonElapsedTime(); }" +
                /*
                @"   document.all(""btx0"").value = \' ход (\' + mins + \':\' + ((secs < 10) ? \'0\' + secs : secs) + \') \'; secs = secs + 1; if(secs == 60){ mins = mins + 1; secs = 0; }" +
                /*
                @"   if (secs == 29 || secs == 59) { window.location = ""main.php""; } " +
                @"   if (mins == 4 && secs == 35) { window.location = ""main.php""; } " +
                 */ 
                /*fightshow +
                @"}" +*/
                @" function myStartAct(){ " +
                @"   window.external.ResetLastBoiTimer();" +
                @"   StartAct();" +
                @" }" +
                @" </SCRIPT>");
            sb.Replace(
                @"<input type=button class=fbut value=""Завершить"" onclick=""location",
                @"<input type=button class=fbut value=""Завершить"" onclick=""ResetCure(); location");
            sb.Replace(
                @"<input type=submit class=fbut value=""Завершить",
                @"<input type=button class=fbut onclick=""javascript: ResetCure(); document.forms[\'FEND\'].submit();"" value=""Завершить");
            sb.AppendLine();
            sb.AppendLine(
                "function AutoSubmit(result)" +
                "{" +
                @" var ss = result.split(""|"");" +
                "  if (ss.length > 8)" +
                "  {" +
                "    var form_node = d.getElementById('form_main');" +
                "    form_node.appendChild(AddElement('post_id','7'));" +
                "    form_node.appendChild(AddElement('vcode',ss[0]));" +
                "    form_node.appendChild(AddElement('enemy',ss[1]));" +
                "    form_node.appendChild(AddElement('group',ss[2]));" +
                "    form_node.appendChild(AddElement('inf_bot',ss[3]));" +
                "    form_node.appendChild(AddElement('lev_bot',ss[4]));" +
                "    form_node.appendChild(AddElement('ftr',ss[5]));" +
                "    form_node.appendChild(AddElement('inu',ss[6]));" +
                "    form_node.appendChild(AddElement('inb',ss[7]));" +
                "    form_node.appendChild(AddElement('ina',ss[8]));" +
                "    fight_f.submit();" +
                "  }" +
                "}");
            sb.AppendLine(
                "function AutoSelect()" +
                "{" +
                "  window.external.AutoSelect();" +
                "}");
            sb.AppendLine(
                "function AutoTurn()" +
                "{" +
                "  window.external.AutoTurn();" +
                "}");
            sb.AppendLine(
                "function AutoUd()" +
                "{" +
                "  window.external.AutoUd();" +
                "  AutoSelect();" +
                "}");
            sb.AppendLine(
                "function AutoBoi()" +
                "{" +
                "  window.external.AutoBoi();" +
                "  AutoSelect();" +
                "}");
            sb.AppendLine(
                "function ResetCure()" +
                "{" +
                "  window.external.ResetCure();" +
                "}");
            return Helpers.Russian.Codepage.GetBytes(sb.ToString());
        }
    }
}