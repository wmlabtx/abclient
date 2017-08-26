namespace ABClient
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using ABForms;
    using ExtMap;

    [ComVisible(true)]
    public sealed class ScriptManager
    {
        public bool DoHideMiniMap()
        {
            return !AppVars.Profile.MapShowMiniMap;
        }

        public string MapText()
        {
            return FormMain.MapText();
        }

        public void SetFishNoCaptchaReady()
        {
            AppVars.FishNoCaptchaReady = true;
        }

        public void FishOverload()
        {
            FormMain.FishOverload();
        }

        public bool IsAutoFish()
        {
            return AppVars.Profile.FishAuto;
        }

        public string InsertGuaDiv(string code)
        {
            return FormMain.InsertGuaDiv(code);
        }

        public void SetAutoFishMassa(string massa)
        {
            AppVars.AutoFishMassa = massa;
        }

        public string CheckPri(string namePri, int myst)
        {
            return FormMain.CheckPri(namePri, myst);
        }

        public string CellDivText(int x, int y, int scale, string link, bool showmove, bool isframe)
        {
            var html = Map.CellDivText(x, y, scale, link, showmove, isframe);
            return html;
        }

        public string CellAltText(int x, int y, int scale)
        {
            var text = Map.CellAltText(x, y, scale);
            return text;
        }

        public bool IsCellExists(int x, int y)
        {
            var result = FormMain.IsCellExists(x, y);
            return result;
        }

        public bool IsCellInPath(int x, int y)
        {
            var result = Map.IsCellInPath(x, y);
            return result;
        }

        public string GetCellLabel(int x, int y)
        {
            var result = FormMain.GetCellLabel(x, y);
            return result;
        }

        public string GetRegionBorders(string framelabel, int x, int y)
        {
            var result = FormMain.GetRegionBorders(framelabel, x, y);
            return result;
        }

        public string GenMoveLink(int x, int y)
        {
            return FormMain.GenMoveLink(x, y);
        }

        public void MakeVisit(int x, int y)
        {
            FormMain.MakeVisit(x, y);
        }

        public int GetHalfMapWidth()
        {
            return (AppVars.Profile.MapBigWidth - 1) / 2;
        }

        public int GetHalfMapHeight()
        {
            return (AppVars.Profile.MapBigHeight - 1) / 2;
        }

        public int GetMapScale()
        {
            return AppVars.Profile.MapBigScale;
        }

        public void ChangeChatSize(int size)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.ChangeChatSize(size);
            }
        }

        public void ChangeChatSpeed(int delay)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.ChangeChatSpeed(delay);
            }
        }

        public void ChangeChatMode(int mode)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.ChangeChatMode(mode);
            }
        }

        public void AutoSelect()
        {
            if (AppVars.MainForm != null)
                AppVars.MainForm.AutoSelect();
        }

        public void AutoTurn()
        {
            if (AppVars.MainForm != null)
                AppVars.MainForm.AutoTurn();
        }

        public void AutoBoi()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.AutoBoi();
            }
        }

        public void MoveTo(string dest)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.MoveTo(dest);
            }
        }

        public void ResetLastBoiTimer()
        {
            AppVars.LastBoiTimer = DateTime.Now;
        }

        public void ResetCure()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.ResetCure();
            }
        }

        public void TraceDrinkPotion(string wnickname, string wnametxt)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.TraceDrinkPotion(wnickname, wnametxt);
            }
        }

        public void ShowMiniMap(bool show)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.ShowMiniMap(show);
            }
        }

        public void ChatUpdated()
        {
            if (AppVars.MainForm != null)
            {
                FormMain.ChatUpdated();
            }
        }

        public void ShowSmiles(int index)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.ShowSmiles(index);
            }
        }

        public void ShowFishTip()
        {
            if (AppVars.MainForm != null)
            {
                FormMain.ShowFishTip();
            }
        }

        public string ChatFilter(string message)
        {
            return FormMain.ChatFilter(message);
        }

        public string XodButtonElapsedTime()
        {
            return string.Format(CultureInfo.InvariantCulture, " ход {0} ", MyHelpers.HelperConverters.TimeSpanToString(DateTime.Now.Subtract(AppVars.LastBoiTimer)));            
        }

        public string InfoToolTip(string img, string alt)
        {
            return AppVars.MainForm != null ? FormMain.InfoToolTip(img, alt) : string.Empty;
        }

        public string CheckQuick(string nick, string str)
        {
            return nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase) ? string.Empty : str;
        }

        public void Quick(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.Quick(nick);
            }
        }

        public void FastAttack(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttack(nick);
            }
        }

        public string CheckFastAttack(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttack ? str : string.Empty;
        }

        public void FastAttackBlood(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackBlood(nick);
            }
        }

        public string CheckFastAttackBlood(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackBlood ? str : string.Empty;
        }

        public void FastAttackUltimate(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackUltimate(nick);
            }
        }

        public void FastAttackClosedUltimate(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackClosedUltimate(nick);
            }
        }

        public string CheckFastAttackUltimate(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackUltimate ? str : string.Empty;
        }

        public string CheckFastAttackClosedUltimate(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackClosedUltimate ? str : string.Empty;
        }

        public void FastAttackClosed(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackClosed(nick);
            }
        }

        public string CheckFastAttackClosed(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackClosed ? str : string.Empty;
        }

        public void FastAttackFist(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackFist(nick);
            }
        }

        public string CheckFastAttackFist(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackFist ? str : string.Empty;
        }

        public void FastAttackClosedFist(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackClosedFist(nick);
            }
        }

        public string CheckFastAttackClosedFist(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackClosedFist ? str : string.Empty;
        }

        public void FastAttackOpenNevid()
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.FastAttackOpenNevid();
            }
        }

        public string CheckFastAttackOpenNevid(string str)
        {
            return AppVars.Profile.DoShowFastAttackOpenNevid ? str : string.Empty;
        }

        public string CheckFastAttackPoison(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackPoison ? str : string.Empty;
        }

        public string CheckFastAttackStrong(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackStrong ? str : string.Empty;
        }

        public string CheckFastAttackNevid(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackNevid ? str : string.Empty;
        }

        public string CheckFastAttackFog(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackFog ? str : string.Empty;
        }

        public string CheckFastAttackZas(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackZas ? str : string.Empty;
        }

        public string CheckFastAttackTotem(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }

            return AppVars.Profile.DoShowFastAttackTotem ? str : string.Empty;
        }

        public void FastAttackPoison(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackPoison(nick);
            }
        }

        public void FastAttackStrong(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackStrong(nick);
            }
        }

        public void FastAttackNevid(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackNevidPot(nick);
            }
        }

        public void FastAttackFog(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackFog(nick);
            }
        }

        public void FastAttackZas(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackZas(nick);
            }
        }

        public void FastAttackTotem(string nick)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.FastAttackTotem(nick);
            }
        }

        public int GetClassIdOfContact(string nick)
        {
            return ContactsManager.GetClassIdOfContact(nick);
        }

        public void HerbsList(string list)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.HerbsList(list);
            }
        }

        public bool IsHerbAutoCut(string herb)
        {
            if (AppVars.MainForm != null)
            {
                var result = FormMain.IsHerbAutoCut(herb);
                return result;
            }

            return false;
        }

        public void HerbCut(string name)
        {
            if (AppVars.MainForm != null)
            {
                FormMain.HerbCut(name);
            }
        }

        public bool DoHerbAutoCut()
        {
            return AppVars.MainForm != null && FormMain.DoHerbAutoCut();
        }

        public string UsersOnline()
        {
            if (!string.IsNullOrEmpty(AppVars.UsersOnline))
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "<td rowspan=3><div><img src=http://image.neverlands.ru/1x1.gif width=8 height=1><font class=hpfont>[<font color=#ACAAA3>&nbsp;<b>{0}</b>&nbsp;</font>]</font></div></td>", 
                    AppVars.UsersOnline);
            }

            return string.Empty;
        }

        public void TraceCut(string herb)
        {
            FormMain.TraceCut(herb);
        }

        public void TraceCutID(string herbid)
        {
            FormMain.TraceCutID(herbid);
        }

        public bool ShowOverWarning()
        {
            return AppVars.Profile != null && AppVars.Profile.ShowOverWarning;
        }

        public void StartBulkSell(string thing, string price, string link)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.StartBulkSell(thing, price, link);
            }
        }

        public void StartBulkOldSell(string name, string price)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.StartBulkOldSell(name, price);
            }
        }

        public void StartBulkDrop(string thing, string price)
        {
            if (AppVars.MainForm != null)
            {
                AppVars.MainForm.StartBulkDrop(thing, price);
            }
        }

        public void SetNeverTimer(int msec)
        {
            AppVars.NeverTimer = DateTime.Now.AddMilliseconds(msec);
        }

        public string ShowHpMaTimers(string inner, double curHP, int maxHP, double intHP, double curMA, int maxMA, double intMA)
        {
            return FormMain.ShowHpMaTimers(inner, curHP, maxHP, intHP, curMA, maxMA, intMA);
        }

        public int BulkSellOldArg1()
        {
            if (string.IsNullOrEmpty(AppVars.BulkSellOldName))
                return 0;

            var pars = AppVars.BulkSellOldScript.Split(',');
            var a1 = pars[0].Trim(' ');
            int result;

            if (int.TryParse(a1, out result))
            {
                if (AppVars.MainForm != null)
                    AppVars.MainForm.WriteChatMsgSafe($"Сдача {AppVars.BulkSellOldName} (ID:{a1}) за {AppVars.BulkSellOldPrice}NV...");

                return result;
            }

            return 0;
        }

        public string BulkSellOldArg2()
        {
            if (string.IsNullOrEmpty(AppVars.BulkSellOldName))
                return string.Empty;

            var pars = AppVars.BulkSellOldScript.Split(',');
            var a2 = pars[1].Trim(' ', '\'');
            return a2;
        }

        public void FastAttackPortal(string nick)
        {
            if (AppVars.MainForm == null)
                return;

            FormMain.FastAttackPortal(nick);
        }

        public string CheckFastAttackPortal(string nick, string str)
        {
            if (nick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase) || !AppVars.Profile.DoShowFastAttackPortal)
                return string.Empty;
            return str;
        }
    }
}
