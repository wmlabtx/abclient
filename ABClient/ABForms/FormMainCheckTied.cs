using System.Threading;
using System;
using System.Text;
using ABClient.MyHelpers;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        private static void CheckTied()
        {
            ThreadPool.QueueUserWorkItem(CheckTiedAsync, null);
        }

        private static void CheckTiedAsync(object state)
        {
            AppVars.LastTied = DateTime.Now;
            var html = NeverInfo.GetPInfo(AppVars.Profile.UserNick);

            if (string.IsNullOrEmpty(html))
                return;

            // var effects = [[1,'Боевая травма (x9) (еще 23:06:17)'],[2,'Тяжелая травма (x2) (еще 07:01:22)'],[17,'Молчанка (еще 00:00:05)']];
            // var effects = [[24,'<b>Яд</b> (x1) (еще 04:59:46)'],[77,'<b>Новогодний бонус</b> (x1) (еще 352:52:16)']];
            // var effects = [[2,'<b>Тяжелая травма</b> (x1) (еще 12:20:22)'],[4,'<b>Легкая травма</b> (x1) (еще 01:40:48)'],[77,'<b>Новогодний бонус </ b > (x1)(еще 369:35:45)']];

            var effects = HelperStrings.SubString(html, "var effects = [", "];");        
            if (!string.IsNullOrEmpty(effects))
            {
                Array.Clear(AppVars.PoisonAndWounds, 0, AppVars.PoisonAndWounds.Length);

                var seffects = effects.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
                for (var k = 0; k < seffects.Length; k++)
                {
                    var effk = seffects[k].Trim('[', ']');
                    var seffk = effk.Split(',');
                    if (seffk.Length <= 1)
                    {
                        continue;
                    }

                    var effcode = seffk[0];
                    // "2" - тяжелая
                    // "3" - средняя
                    // "4" - легкая
                    // "24" - яд

                    switch (effcode)
                    {
                        case "2":
                            AppVars.PoisonAndWounds[3]++; // тяжелые
                            break;
                        case "3":
                            AppVars.PoisonAndWounds[2]++; // средние
                            break;
                        case "4":
                            AppVars.PoisonAndWounds[1]++; // легкие
                            break;
                        case "24":
                            AppVars.PoisonAndWounds[0]++;
                            break;
                        default:
                            break;
                    }
                }

                if (AppVars.PoisonAndWounds[0] > 0)
                {
                    if (DateTime.Now.Subtract(AppVars.LastMessageAboutTraumaOrPoison).TotalMinutes > 10.0)
                    {
                        AppVars.LastMessageAboutTraumaOrPoison = DateTime.Now;
                        AppVars.MainForm.WriteChatMsgSafe("У вас отравление. Почему бы не включить автолечение в настройках?");
                    }
                }
                else
                {
                    if ((AppVars.PoisonAndWounds[1] > 0) || (AppVars.PoisonAndWounds[2] > 0) ||
                        (AppVars.PoisonAndWounds[3] > 0))
                    {
                        if (DateTime.Now.Subtract(AppVars.LastMessageAboutTraumaOrPoison).TotalMinutes > 10.0)
                        {
                            AppVars.LastMessageAboutTraumaOrPoison = DateTime.Now;
                            AppVars.MainForm.WriteChatMsgSafe("У вас небоевая травма. Почему бы не включить автолечение в настройках?");
                        }
                    }
                }
            }

            // var params = [['~Angry~',2,'nona.gif',17,'angry.gif','Форпост [Больница]',1,1394107847,'LightSoulS','Hungry and Angry','Форпост','25.12.2007'],
            var params0 = HelperStrings.SubString(html, "var params = [[", "],");
            if (!string.IsNullOrEmpty(params0))
            {
                var spar0 = HelperStrings.ParseArguments(params0);
                if (spar0.Length > 5)
                {
                    var location = spar0[5];
                    if (!string.IsNullOrEmpty(location))
                    {
                        // var hpmp = [1,525,0,0,100]
                        var hpmp = HelperStrings.SubString(html, "var hpmp = [", "]");
                        if (!string.IsNullOrEmpty(hpmp))
                        {
                            var spar1 = HelperStrings.ParseArguments(hpmp);
                            if (spar1.Length > 4)
                            {
                                var stied = spar1[4];
                                int tied;
                                if (!int.TryParse(stied, out tied)) 
                                    return;
                                
                                tied = 100 - tied;
                                try
                                {
                                    if (AppVars.MainForm != null)
                                    {
                                        AppVars.MainForm.BeginInvoke(
                                            new UpdateTiedDelegate(AppVars.MainForm.UpdateTied),
                                            new object[] { tied });
                                    }
                                }
                                catch (InvalidOperationException)
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        /*
                        AppVars.SwitchToPerc = true;
                        AppVars.SwitchToFlora = true;
                         */

                        var tied = AppVars.Tied + 2;
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateTiedDelegate(AppVars.MainForm.UpdateTied),
                                    new object[] { tied });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }
                }
            }
        }
    }
}