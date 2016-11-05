using System;
using ABClient.ABForms;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] RouletteAjaxPhp(byte[] array)
        {
            // OK@Поздравляем, Вы выиграли 50 NV.@1@3@4@2.4@1@14785.5@1600.34@1
            // 

            var html = AppVars.Codepage.GetString(array);
            var args = html.Split('@');
            if ((args.Length > 2) && (args[0].Equals("OK")))

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateChatDelegate(AppVars.MainForm.UpdateChat),
                        new object[] { "Рулетка: " + args[1] });
                }
            }
            catch (InvalidOperationException)
            {
            }

            /*
           var rand = Dice.Make(100);
           var fileTemp = string.Format("alchemyajax{0:00}.txt", rand);
           var fullFileTemp = Path.Combine(DataManager.DirTemp, fileTemp);
           File.WriteAllText(fullFileTemp, html);
           */

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new SetMainTopInvokeDelegate(AppVars.MainForm.SetMainTopInvoke),
                        new object[] { "http://www.neverlands.ru/main.php?mselect=15" });
                }
            }
            catch (InvalidOperationException)
            {
            }

            return array;
        }
    }
}