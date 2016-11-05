using System;
using System.Net;
using System.Threading;
using ABClient.Helpers;
using ABClient.MyHelpers;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        private static void CheckInfo()
        {
            ThreadPool.QueueUserWorkItem(CheckInfoAsync, null);
        }

        private static int[] GetPoisonAndWounds(string html)
        {
            var poisonAndWounds = new int[4];

            // var effects = [[3,'<b>Средняя травма</b> (x1) (еще 02:16:49)'],[77,'<b>Новогодний бонус</b> (x1) (еще 94:24:18)']];

            var streff = HelperStrings.SubString(html, "var effects = [[", "]];");
            if (string.IsNullOrEmpty(streff))
                return poisonAndWounds;
            
            var par = streff.Split(new[] { "],[" }, StringSplitOptions.RemoveEmptyEntries);
            if (par.Length == 0)
                return poisonAndWounds;

            foreach (var elem in par)
            {
                var pair = elem.Split(',');
                if (pair.Length != 2)
                    continue;

                switch (pair[0])
                {
                    case "2":
                        poisonAndWounds[3]++; // тяжелые
                        break;
                    case "3":
                        poisonAndWounds[2]++; // средние
                        break;
                    case "4":
                        poisonAndWounds[1]++; // легкие
                        break;
                    case "24":
                        poisonAndWounds[0]++;
                        break;
                }
            }

            return poisonAndWounds;
        }

        private static void CheckInfoAsync(object state)
        {
            if (string.IsNullOrEmpty(AppVars.Profile.UserNick))
                return;

            var textdata = NeverInfo.GetPInfo(AppVars.Profile.UserNick);
            if (string.IsNullOrEmpty(textdata)) 
                return;
            
            var poisonAndWounds = GetPoisonAndWounds(textdata);
            if (
                (poisonAndWounds[0] > AppVars.PoisonAndWounds[0]) ||
                (poisonAndWounds[1] > AppVars.PoisonAndWounds[1]) ||
                (poisonAndWounds[2] > AppVars.PoisonAndWounds[2]) ||
                (poisonAndWounds[3] > AppVars.PoisonAndWounds[3])
                )
            {
                AppVars.PoisonAndWounds = poisonAndWounds;
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                            new object[] { });
                    }
                }
                catch (InvalidOperationException)
                {
                }                    
            }
        }
    }
}