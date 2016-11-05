using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ABClient.ABProxy;
using ABClient.MyHelpers;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        internal void WaitForTurnSafe()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(WaitForTurnSafe));
                return;
            }

            if (AppVars.Profile.ShowTrayBaloons)
                UpdateTrayBaloon("Ожидаем хода противника");

            if ((AppVars.ThreadWaitForTurn == null) ||
                ((AppVars.ThreadWaitForTurn != null) && (!AppVars.ThreadWaitForTurn.IsAlive)))
            {
                AppVars.ThreadWaitForTurn = new Thread(WaitForTurnAsync);
                AppVars.ThreadWaitForTurn.Start();
            }
        }

        private static void WaitForTurnStop()
        {
            if (AppVars.ThreadWaitForTurn == null)
                return;

            AppVars.AutoRefresh = false;
            while ((AppVars.ThreadWaitForTurn != null) && AppVars.ThreadWaitForTurn.IsAlive)
            {
                AppVars.ThreadWaitForTurn.Join(50);
            }

            AppVars.ThreadWaitForTurn = null;
        }

        private static void WaitForTurnAsync(object stateInfo)
        {
            var timeStart = DateTime.Now;
            int lastSeconds = -1;

            while (AppVars.AutoRefresh)
            {
                var timeDiff = DateTime.Now.Subtract(timeStart);
                if ((timeDiff.Seconds % 30) == 0 && (lastSeconds != timeDiff.Seconds))
                {
                    lastSeconds = timeDiff.Seconds;
                    if (AppVars.MainForm != null)
                        AppVars.MainForm.WriteChatMsgSafe(
                            $"Ожидаем хода противника... <b>{timeDiff.Minutes}:{timeDiff.Seconds:00}</b>");
                }

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.neverlands.ru/main.php");
                httpWebRequest.Method = "GET";
                httpWebRequest.Proxy = AppVars.LocalProxy;
                var cookies = CookiesManager.Obtain("www.neverlands.ru");
                httpWebRequest.Headers.Add("Cookie", cookies);
                string html = null;
                try
                {
                    IdleManager.AddActivity();
                    var resp = httpWebRequest.GetResponse();
                    var webstream = resp.GetResponseStream();
                    if (webstream != null)
                    {
                        var reader = new StreamReader(webstream, AppVars.Codepage);
                        html = reader.ReadToEnd();
                    }
                }
                catch
                {
                    html = null;
                }
                finally
                {
                    IdleManager.RemoveActivity();
                }

                if (string.IsNullOrEmpty(html))
                    break;

                if (!AreWaitingForTurn(html))
                    break;
            }

            if (AppVars.AutoRefresh && (AppVars.MainForm != null))
            {
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

            AppVars.ThreadWaitForTurn = null;
        }

        private static string[] ParseString(string html, string sarg, int mina)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            if (sarg == null)
            {
                throw new ArgumentNullException("sarg");
            }

            if (html.IndexOf(sarg, StringComparison.OrdinalIgnoreCase) == -1)
            {
                return null;
            }

            var args = HelperStrings.SubString(html, sarg, "]");
            if (args == null)
            {
                return null;
            }

            var pars = args.Split(',');
            return pars.Length < mina ? null : pars;
        }

        private static bool AreWaitingForTurn(string html)
        {
            var fightty = ParseString(html, @"var fight_ty = [", 0);
            if (fightty == null)
                return false;

            var isBoi = (fightty[3].Length >= 1) && (fightty[3][0] == '1');
            if (isBoi)
                return false;

            if (!fightty[4].Equals("3"))
                return false;

            if (fightty[6].Length > 2)
                return false;

            return true;
        }
    }
}
