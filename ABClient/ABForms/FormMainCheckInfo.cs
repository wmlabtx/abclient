using System;
using System.Threading;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        private static void CheckInfo()
        {
            ThreadPool.QueueUserWorkItem(CheckInfoAsync, null);
        }

        private static int[] GetPoisonAndWounds(UserInfo userInfo)
        {
            var poisonAndWounds = new int[4];
            if (userInfo.EffectsCodes.Length == 0)
                return poisonAndWounds;
            
            foreach (var elem in userInfo.EffectsCodes)
            {
                switch (elem)
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
                    default:
                        break;
                }
            }

            return poisonAndWounds;
        }

        private static void CheckInfoAsync(object state)
        {
            if (string.IsNullOrEmpty(AppVars.Profile.UserNick))
                return;

            var userInfo = NeverApi.GetAll(AppVars.Profile.UserNick);
            if (userInfo == null) 
                return;
            
            var poisonAndWounds = GetPoisonAndWounds(userInfo);
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