using System.Threading;
using System;

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
            var userInfo = NeverApi.GetAll(AppVars.Profile.UserNick);
            if (userInfo == null)
                return;

            // var effects = [[1,'Боевая травма (x9) (еще 23:06:17)'],[2,'Тяжелая травма (x2) (еще 07:01:22)'],[17,'Молчанка (еще 00:00:05)']];
            // var effects = [[24,'<b>Яд</b> (x1) (еще 04:59:46)'],[77,'<b>Новогодний бонус</b> (x1) (еще 352:52:16)']];
            // var effects = [[2,'<b>Тяжелая травма</b> (x1) (еще 12:20:22)'],[4,'<b>Легкая травма</b> (x1) (еще 01:40:48)'],[77,'<b>Новогодний бонус </ b > (x1)(еще 369:35:45)']];

            if (userInfo.EffectsCodes.Length > 0)
            {
                Array.Clear(AppVars.PoisonAndWounds, 0, AppVars.PoisonAndWounds.Length);
                for (var k = 0; k < userInfo.EffectsCodes.Length; k++)
                {
                    var effcode = userInfo.EffectsCodes[k];
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
                        AppVars.MainForm.WriteChatMsgSafe(
                            "У вас отравление. Почему бы не включить автолечение в настройках?");
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
                            AppVars.MainForm.WriteChatMsgSafe(
                                "У вас небоевая травма. Почему бы не включить автолечение в настройках?");
                        }
                    }
                }
            }

            var location = userInfo.Location;
            if (!string.IsNullOrEmpty(location))
            {
                var tied = userInfo.Tied;
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTiedDelegate(AppVars.MainForm.UpdateTied), tied);
                    }
                }
                catch (InvalidOperationException)
                {
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
                            new UpdateTiedDelegate(AppVars.MainForm.UpdateTied), tied);
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }
        }
    }
}