namespace ABClient.PostFilter
{
    using System;
    using System.Text;
    using MyHelpers;

    internal static partial class Filter
    {
        private static string MainPhpCure(string html)
        {
            if (string.IsNullOrEmpty(html)) return null;

            // <input type=button class=invbut onclick="doctorform('29442375','00adf70b4369200af717408637365b7e','1','0','9')" value="Лечить лёгкую травму">
            // <input type=button class=invbut onclick="doctorform('29850516','0f3205e857a3141a1dd22545c1aa0049','1','1','12')" value="Лечить среднюю травму">
            // <input type=button class=invbut onclick="doctorform('29434480','5c675ad197ffd77e48f3bcd933ad3c7f','1','2','6')" value="Лечить тяжелую травму">

            /*
            function doctorform(duid,vcode,dprice,dtype,dcurs)
            {
                    var add_txt = '';
                    switch(dtype)
                    {
                            case '0': add_txt = 'легкая травма'; break;
                            case '1': add_txt = 'средняя травма'; break;
                            case '2': add_txt = 'тяжёлая травма'; break;
                            case '4': add_txt = 'боевая травма'; break;
                    }
                    top.frames['ch_buttons'].document.FBT.text.focus();
                    document.all("doctordiv").innerHTML = '
             *      
             * <form action=main.php method=POST>
             *      <input type=hidden name=useaction value="addon-action">
             *      <input type=hidden name=dtype value="'+dtype+'">
             *      <input type=hidden name=addid value=2>
             *      <input type=hidden name=post_id value=3>
             *      <input type=hidden name=dprice value="'+dprice+'">
             *      <input type=hidden name=dcurs value="'+dcurs+'">
             *      <input type=hidden name=duid value='+duid+'>
             *      <input type=hidden name=vcode value='+vcode+'>
             *      <INPUT TYPE="text" name=fnick class=LogintextBox  maxlength=25> 
             *      <input type=submit value="лечить" class=lbut>
             * </FORM>';
                    document.all("doctordiv").style.visibility = "visible";
                    document.all("fnick").focus();
                    ActionFormUse='fnick';
            }
             */

            string dtext;
            switch(AppVars.CureTravm)
            {
                case "1":
                    dtext = "0";
                    break;
                case "2":
                    dtext = "1";
                    break;
                case "3":
                    dtext = "2";
                    break;
                case "4":
                    dtext = "4";
                    break;
                default:
                    goto failed;
            }

            const string patternDoctorForm = "doctorform(";
            int p1 = 0;
            while (p1 != -1)
            {
                p1 = html.IndexOf(patternDoctorForm, p1, StringComparison.OrdinalIgnoreCase);
                if (p1 == -1)
                    break;

                p1 += patternDoctorForm.Length;
                var p2 = html.IndexOf(")", p1, StringComparison.OrdinalIgnoreCase);
                if (p2 == -1)
                    continue;

                var args = html.Substring(p1, p2 - p1);
                if (string.IsNullOrEmpty(args))
                    continue;

                var arg = args.Split(',');
                if (arg.Length < 5)
                    continue;

                var duid = arg[0].Trim(new[] {'\''});
                var vcode = arg[1].Trim(new[] {'\''});
                var dprice = arg[2].Trim(new[] {'\''});
                var dtype = arg[3].Trim(new[] {'\''});
                var dcurs = arg[4].Trim(new[] {'\''});

                if (!dtype.Equals(dtext, StringComparison.OrdinalIgnoreCase))
                    continue;

                var sb = new StringBuilder();
                sb.Append(
                    HelperErrors.Head() +
                    "Используем аптечку на ");
                sb.Append(AppVars.CureNick);
                sb.Append("...");
                sb.Append("<form action=main.php method=POST name=ff>");

                /*
                sb.Append(@"<input name=useaction type=hidden value=""");
                sb.Append(@"addon-action");
                sb.Append(@""">");
                 */ 

                sb.Append(@"<input name=dtype type=hidden value=""");
                sb.Append(dtype);
                sb.Append(@""">");

                sb.Append(@"<input name=addid type=hidden value=""");
                sb.Append(2);
                sb.Append(@""">");

                sb.Append(@"<input name=post_id type=hidden value=""");
                sb.Append(3);
                sb.Append(@""">");

                sb.Append(@"<input name=dprice type=hidden value=""");
                sb.Append(dprice);
                sb.Append(@""">");

                sb.Append(@"<input name=dcurs type=hidden value=""");
                sb.Append(dcurs);
                sb.Append(@""">");

                sb.Append(@"<input name=duid type=hidden value=""");
                sb.Append(duid);
                sb.Append(@""">");

                sb.Append(@"<input name=vcode type=hidden value=""");
                sb.Append(vcode);
                sb.Append(@""">");

                sb.Append(@"<input name=fnick type=hidden value=""");
                sb.Append(AppVars.CureNick);
                sb.Append(@""">");

                sb.Append(
                    @"</form>" +
                    @"<script language=""JavaScript"">" +
                    @"document.ff.submit();" +
                    @"</script></body></html>");

                return sb.ToString();
            }

            failed:
            return null;
        }
    }
}
          