using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using ABClient.MyHelpers;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        private class AttackInfo
        {
            internal string TargetNick;
            internal string Weapon;
        }

        private void FastStartSafe(string id, string nick, int count = 1)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => FastStartSafe(id, nick, count)));
                return;
            }

            AppVars.FastNeed = true;
            AppVars.FastId = id;
            AppVars.FastNick = nick;
            AppVars.FastCount = count;
        }

        internal void FastCancelSafe()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(FastCancelSafe));
                return;
            }

            AppVars.FastNeed = false;
            AppVars.FastNick = null;
            AppVars.FastId = null;
            AppVars.FastCount = 0;
            AppVars.FastNeedAbilDarkTeleport = false;
            AppVars.FastNeedAbilDarkFog = false;

            if (AppVars.FastWaitEndOfBoiActive)
                AppVars.FastWaitEndOfBoiCancel = true;
        }

        private static void FastAttackAsync(object stateInfo)
        {
            var ai = (AttackInfo) stateInfo;
            var nick = StripItalic(ai.TargetNick);
            var userInfo = NeverApi.GetAll(nick);
            if (userInfo == null)
            {
                if (AppVars.MainForm != null)
                    AppVars.MainForm.WriteChatMsgSafe("Ошибка анализа инфы атакуемого.");

                return;
            }

            var flog = userInfo.FightLog;
            if (flog.Equals("0", StringComparison.Ordinal))
            {
                flog = string.Empty;
            }

            if (!string.IsNullOrEmpty(flog))
            {
                var scans = 0;
                var swatch = new Stopwatch();
                swatch.Start();
                AppVars.FastWaitEndOfBoiCancel = false;
                while (!AppVars.FastWaitEndOfBoiCancel)
                {
                    AppVars.FastWaitEndOfBoiActive = true;
                    var html = NeverApi.GetFlog(flog);
                    if (string.IsNullOrEmpty(html))
                        continue;

                    scans++;

                    // var off = 1;
                    // var off = 0;

                    var off = HelperStrings.SubString(html, "var off = ", ";");
                    if (string.IsNullOrEmpty(off))
                        continue;

                    if (off.Equals("1"))
                    {
                        break;
                    }
                    
                    if (
                        (html.IndexOf("нападение бота", StringComparison.CurrentCultureIgnoreCase) == -1) &&
                        (html.IndexOf("закрытый бой", StringComparison.CurrentCultureIgnoreCase) == -1) &&
                        (html.IndexOf("закрытое нападение", StringComparison.CurrentCultureIgnoreCase) == -1) &&
                        (html.IndexOf("закрытое кулачное нападение", StringComparison.CurrentCultureIgnoreCase) == -1) &&
                        (html.IndexOf("закрытое боевое нападение", StringComparison.CurrentCultureIgnoreCase) == -1)
                        )
                    {
                        if (!AppVars.WaitOpen)
                        {
                            break;
                        }
                    }

                    if (scans == 1)
                    {
                        if (AppVars.MainForm != null)
                            AppVars.MainForm.WriteChatMsgSafe("Ожидание окончания боя (отмена: меню/быстрые действия/отмена).");
                    }
                    else
                    {
                        if ((scans%100) == 0)
                        {
                            if (AppVars.MainForm != null)
                                AppVars.MainForm.WriteChatMsgSafe(
                                    $"Ожидание окончания боя (запросов: {scans}, средн: {swatch.ElapsedMilliseconds / scans}мс)");
                        }
                    }
                }
            }

            AppVars.FastWaitEndOfBoiActive = false;

            if (AppVars.FastWaitEndOfBoiCancel)
            {
                AppVars.FastWaitEndOfBoiCancel = false;
                if (AppVars.MainForm != null) 
                    AppVars.MainForm.WriteChatMsgSafe("Ожидание окончания боя прекращено.");
            }
            else
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.FastStartSafe(ai.Weapon, nick, AppVars.DoPerenap ? int.MaxValue : 1);
                    ReloadMainFrame();
                }
            }
        }

        internal static void FastAttack(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_svi_001.gif" });
        }

        internal static void FastAttackBlood(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_svi_002.gif" });
        }

        internal static void FastAttackUltimate(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_w28_26.gif" });
        }

        internal static void FastAttackUltimateAutoAttack(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_w28_26.gif", nick, AppVars.DoPerenap ? int.MaxValue : 1);
                ReloadMainFrame();
            }
        }

        internal static void FastAttackClosedUltimate(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_w28_26X.gif" });
        }

        internal static void FastAttackClosedUltimateAutoAttack(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_w28_26X.gif", nick, AppVars.DoPerenap ? int.MaxValue : 1);
                ReloadMainFrame();
            }
        }

        internal static void FastAttackClosed(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_svi_205.gif" });
        }

        internal static void FastAttackFist(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_w28_24.gif" });
        }

        internal static void FastAttackFistAutoAttack(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_w28_24.gif", nick, AppVars.DoPerenap ? int.MaxValue : 1);
                ReloadMainFrame();
            }
        }

        internal static void FastAttackClosedFist(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_w28_25.gif" });
        }

        internal static void FastAttackClosedFistAutoAttack(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_w28_25.gif", nick, AppVars.DoPerenap ? int.MaxValue : 1);
                ReloadMainFrame();
            }
        }

        internal static void FastAttackPortalAutoAttack(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "i_w28_86.gif" });
        }

        internal static void FastAttackFog(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_svi_213.gif", StripItalic(nick));
                ReloadMainFrame();
            }
        }

        internal static void FastAttackZas(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_w28_27.gif", StripItalic(nick));
                ReloadMainFrame();
            }
        }

        internal static void FastAttackTotem(string nick)
        {
            var threadAttack = new Thread(FastAttackAsync);
            threadAttack.Start(new AttackInfo { TargetNick = nick, Weapon = "Тотем" });

            /*
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Тотем", StripItalic(nick));
                ReloadMainFrame();
            }
             */ 
        }

        internal static void FastAttackPortal(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_w28_86.gif", StripItalic(nick));
                ReloadMainFrame();
            }
        }

        internal void FastAttackOpenNevid()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(FastAttackOpenNevid));
                return;
            }

            // <input type=button class=invbut onclick="w28_form('90e74a29c879926d22bd9fc89a4904e0','73493740',28,'66')" 

            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("i_w28_28.gif", "клетке");
                ReloadMainFrame();
            }
        }

        internal static void FastAttackPoison(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Яд", StripItalic(nick));
                ReloadMainFrame();
            }
        }

        internal static void FastAttackStrong(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Зелье Сильной Спины", StripItalic(nick), AppVars.DoPerenap ? 5 : 1);
                ReloadMainFrame();
            }
        }

        internal static void FastAttackNevidPot(string nick)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Зелье Невидимости", StripItalic(nick));
                ReloadMainFrame();
            }
        }

        private static void FastAttackIslandPot()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Телепорт (Остров Туротор)", AppVars.Profile.UserNick);
                ReloadMainFrame();
            }
        }

        private static void FastAttackBlazPot()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Зелье Блаженства", AppVars.Profile.UserNick);
                ReloadMainFrame();
            }
        }

        private static void FastAttackBlazElixir()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Эликсир Блаженства", AppVars.Profile.UserNick);
                ReloadMainFrame();
            }
        }

        private static void FastAttackMomentCureElixir()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Эликсир Мгновенного Исцеления", AppVars.Profile.UserNick);
                ReloadMainFrame();
            }
        }

        private static void FastAttackMomentRestoreElixir()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastStartSafe("Эликсир Восстановления", AppVars.Profile.UserNick);
                ReloadMainFrame();
            }
        }

        private static void FastAttackPrimankaElixir()
        {
            if (AppVars.MainForm != null)
                AppVars.MainForm.FastStartSafe(AppConsts.Bait, AppVars.Profile.UserNick, AppVars.DoPerenap? int.MaxValue : 1);

            AppVars.Profile.Pers.Ready = 0;
            if (AppVars.MainForm != null)
                AppVars.MainForm.ChangeAutoboiState(AutoboiState.AutoboiOn);
            
            ReloadMainFrame();
        }

        private static string StripItalic(string nick)
        {
            return nick.Replace("<i>", string.Empty).Replace("</i>", string.Empty).Trim();
        }
    }
}