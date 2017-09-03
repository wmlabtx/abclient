using ABClient.MyProfile;

namespace ABClient.MyForms
{
    using System;
    using System.Globalization;
    using System.Windows.Forms;

    internal partial class FormSettingsGeneral : Form
    {
        internal FormSettingsGeneral()
        {
            InitializeComponent();

            checkBoxDoAutoDrinkBlaz.Checked = AppVars.Profile.DoAutoDrinkBlaz;
            textBoxAutoDrinkBlazTied.Text = AppVars.Profile.AutoDrinkBlazTied.ToString(CultureInfo.InvariantCulture);
            groupBoxDoAutoDrinkBlaz.Text = @"Автопитье блажа";
            checkBoxDoAutoDrinkBlaz.Enabled = true;
            textBoxAutoDrinkBlazTied.Enabled = true;

            checkboxDoPromptExit.Checked = AppVars.Profile.DoPromptExit;
            checkboxDoTray.Checked = AppVars.Profile.DoTray;
            checkboxShowTrayBaloons.Checked = AppVars.Profile.ShowTrayBaloons;

            checkboxDoKeepChatMoving.Checked = AppVars.Profile.ChatKeepMoving;
            checkboxDoKeepChatGame.Checked = AppVars.Profile.ChatKeepGame;
            checkboxDoKeepChatLog.Checked = AppVars.Profile.ChatKeepLog;
            linkChatSizeLog.Tag = AppVars.Profile.ChatSizeLog;
            linkChatSizeLog.Text = "Размер чата: " + AppVars.Profile.ChatSizeLog + "Кб";
            checkDoChatLevels.Checked = AppVars.Profile.DoChatLevels;

            checkStatReset.Checked = AppVars.Profile.Stat.Reset;

            checkboxRazdChatReport.Checked = AppVars.Profile.RazdChatReport;

            numCureNV1.Value = AppVars.Profile.CureNV[0];
            numCureNV2.Value = AppVars.Profile.CureNV[1];
            numCureNV3.Value = AppVars.Profile.CureNV[2];
            numCureNV4.Value = AppVars.Profile.CureNV[3];
            textCureAsk1.Text = AppVars.Profile.CureAsk[0];
            textCureAsk2.Text = AppVars.Profile.CureAsk[1];
            textCureAsk3.Text = AppVars.Profile.CureAsk[2];
            textCureAsk4.Text = AppVars.Profile.CureAsk[3];
            textCureAdv.Text = AppVars.Profile.CureAdv;
            textCureAfter.Text = AppVars.Profile.CureAfter;
            textCureBoi.Text = AppVars.Profile.CureBoi;
            checkE1.Checked = AppVars.Profile.CureEnabled[0];
            checkE2.Checked = AppVars.Profile.CureEnabled[1];
            checkE3.Checked = AppVars.Profile.CureEnabled[2];
            checkE4.Checked = AppVars.Profile.CureEnabled[3];
            checkD04.Checked = AppVars.Profile.CureDisabledLowLevels;

            checkBoxDoExtendMap.Checked = AppVars.Profile.MapShowExtend;
            
            numBigMapWidth.Maximum = AppConsts.MapBigWidthMax;
            numBigMapWidth.Minimum = AppConsts.MapBigWidthMin;
            numBigMapWidth.Value = AppVars.Profile.MapBigWidth;
            
            numBigMapHeight.Maximum = AppConsts.MapBigHeightMax;
            numBigMapHeight.Minimum = AppConsts.MapBigHeightMin;
            numBigMapHeight.Value = AppVars.Profile.MapBigHeight;
            
            numBigMapScale.Maximum = AppConsts.MapBigScaleMax;
            numBigMapScale.Minimum = AppConsts.MapBigScaleMin;
            numBigMapScale.Value = AppVars.Profile.MapBigScale;
            
            numBigMapTransparency.Maximum = AppConsts.MapBigTransparencyMax;
            numBigMapTransparency.Minimum = AppConsts.MapBigTransparencyMin;
            numBigMapTransparency.Value = AppVars.Profile.MapBigTransparency;
            
            checkBoxBigMapBackColorWhite.Checked = AppVars.Profile.MapShowBackColorWhite;
            checkBoxMapDrawRegion.Checked = AppVars.Profile.MapDrawRegion;

            numMiniMapWidth.Maximum = AppConsts.MapMiniWidthMax;
            numMiniMapWidth.Minimum = AppConsts.MapMiniWidthMin;
            numMiniMapWidth.Value = AppVars.Profile.MapMiniWidth;

            numMiniMapHeight.Maximum = AppConsts.MapMiniHeightMax;
            numMiniMapHeight.Minimum = AppConsts.MapMiniHeightMin;
            numMiniMapHeight.Value = AppVars.Profile.MapMiniHeight;

            numMiniMapScale.Maximum = AppConsts.MapMiniScaleMax;
            numMiniMapScale.Minimum = AppConsts.MapMiniScaleMin;
            numMiniMapScale.Value = AppVars.Profile.MapMiniScale;

            checkShowMiniMap.Checked = AppVars.Profile.MapShowMiniMap;

            numFishTiedHigh.Value = AppVars.Profile.FishTiedHigh;

            checkFishTiedZero.Checked = AppVars.Profile.FishTiedZero;
            
            checkboxStopOverW.Checked = AppVars.Profile.FishStopOverWeight;

            checkUseSounds.Checked = AppVars.Profile.Sound.Enabled;
            checkDoPlayDigits.Checked = AppVars.Profile.Sound.DoPlayDigits;
            checkDoPlayAttack.Checked = AppVars.Profile.Sound.DoPlayAttack;
            checkDoPlaySndMsg.Checked = AppVars.Profile.Sound.DoPlaySndMsg;
            checkDoPlayRefresh.Checked = AppVars.Profile.Sound.DoPlayRefresh;
            checkDoPlayAlarm.Checked = AppVars.Profile.Sound.DoPlayAlarm;
            checkDoPlayTimer.Checked = AppVars.Profile.Sound.DoPlayTimer;

            numAdvMin.Value = (int)((decimal)AppVars.Profile.AutoAdv.Sec / 60);
            numAdvSec.Value = AppVars.Profile.AutoAdv.Sec % 60;
            textPhraz.Text = AppVars.Profile.AutoAdv.Phraz;

            checkFishAutoWear.Checked = AppVars.Profile.FishAutoWear;
            for (var i = 0; i < comboFishHand1.Items.Count; i++)
            {
                if (!AppVars.Profile.FishHandOne.Equals((string)comboFishHand1.Items[i], StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                comboFishHand1.SelectedIndex = i;
                break;
            }

            if (comboFishHand1.SelectedIndex == -1)
            {
                comboFishHand1.SelectedIndex = 0;
            }

            for (var i = 0; i < comboFishHand2.Items.Count; i++)
            {
                if (!AppVars.Profile.FishHandTwo.Equals((string)comboFishHand2.Items[i], StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                comboFishHand2.SelectedIndex = i;
                break;
            }

            if (comboFishHand2.SelectedIndex == -1)
            {
                comboFishHand2.SelectedIndex = 0;
            }

            checkPrimBread.Checked = (AppVars.Profile.FishEnabledPrims & Prims.Bread) != 0;
            checkPrimWorm.Checked = (AppVars.Profile.FishEnabledPrims & Prims.Worm) != 0;
            checkPrimBigWorm.Checked = (AppVars.Profile.FishEnabledPrims & Prims.BigWorm) != 0;
            checkPrimStink.Checked = (AppVars.Profile.FishEnabledPrims & Prims.Stink) != 0;
            checkPrimFly.Checked = (AppVars.Profile.FishEnabledPrims & Prims.Fly) != 0;
            checkPrimLight.Checked = (AppVars.Profile.FishEnabledPrims & Prims.Light) != 0;
            checkPrimMorm.Checked = (AppVars.Profile.FishEnabledPrims & Prims.Morm) != 0;
            checkPrimHiFlight.Checked = (AppVars.Profile.FishEnabledPrims & Prims.HiFlight) != 0;
            checkPrimDonka.Checked = (AppVars.Profile.FishEnabledPrims & Prims.Donka) != 0;

            checkboxFishChatReport.Checked = AppVars.Profile.FishChatReport;
            checkboxFishChatReportColor.Checked = AppVars.Profile.FishChatReportColor;

            checkAutoAnswer.Checked = AppVars.Profile.DoAutoAnswer;
            textAutoAnswer.Text = AppVars.Profile.AutoAnswer.Replace(AppConsts.Br, Environment.NewLine);

            checkLightForum.Checked = AppVars.Profile.LightForum;

            textTorgTable.Text = AppVars.Profile.TorgTabl;
            textTorgMessageAdv.Text = AppVars.Profile.TorgMessageAdv;
            textTorgAdvTime.Text = AppVars.Profile.TorgAdvTime.ToString(CultureInfo.InvariantCulture);
            textTorgMessageNoMoney.Text = AppVars.Profile.TorgMessageNoMoney;
            textTorgMessageTooExp.Text = AppVars.Profile.TorgMessageTooExp;
            textTorgMessageThanks.Text = AppVars.Profile.TorgMessageThanks;
            textTorgMessageLess90.Text = AppVars.Profile.TorgMessageLess90;
            checkTorgSliv.Checked = AppVars.Profile.TorgSliv;
            textTorgMinLevel.Text = AppVars.Profile.TorgMinLevel.ToString(CultureInfo.InvariantCulture);
            textTorgEx.Text = AppVars.Profile.TorgEx;
            textTorgDeny.Text = AppVars.Profile.TorgDeny;

            checkDoInvPack.Checked = AppVars.Profile.DoInvPack;
            checkDoInvPackDolg.Checked = AppVars.Profile.DoInvPackDolg;
            checkDoInvSort.Checked = AppVars.Profile.DoInvSort;

            checkDoShowFastAttack.Checked = AppVars.Profile.DoShowFastAttack;
            checkDoShowFastAttackBlood.Checked = AppVars.Profile.DoShowFastAttackBlood;
            checkDoShowFastAttackUltimate.Checked = AppVars.Profile.DoShowFastAttackUltimate;
            checkDoShowFastAttackClosedUltimate.Checked = AppVars.Profile.DoShowFastAttackClosedUltimate;
            checkDoShowFastAttackClosed.Checked = AppVars.Profile.DoShowFastAttackClosed;
            checkDoShowFastAttackFist.Checked = AppVars.Profile.DoShowFastAttackFist;
            checkDoShowFastAttackClosedFist.Checked = AppVars.Profile.DoShowFastAttackClosedFist;
            checkDoShowFastAttackOpenNevid.Checked = AppVars.Profile.DoShowFastAttackOpenNevid;
            checkDoShowFastAttackPoison.Checked = AppVars.Profile.DoShowFastAttackPoison;
            checkDoShowFastAttackStrong.Checked = AppVars.Profile.DoShowFastAttackStrong;
            checkDoShowFastAttackNevid.Checked = AppVars.Profile.DoShowFastAttackNevid;
            checkDoShowFastAttackFog.Checked = AppVars.Profile.DoShowFastAttackFog;
            checkDoShowFastAttackZas.Checked = AppVars.Profile.DoShowFastAttackZas;
            checkDoShowFastAttackTotem.Checked = AppVars.Profile.DoShowFastAttackTotem;
            checkDoShowFastAttackPortal.Checked = AppVars.Profile.DoShowFastAttackPortal;

            checkShowOverWarning.Checked = AppVars.Profile.ShowOverWarning;
            checkDoStopOnDig.Checked = AppVars.Profile.DoStopOnDig;

            checkDoRob.Checked = AppVars.Profile.DoRob;
            checkDoAutoCure.Checked = AppVars.Profile.DoAutoCure;
            textAutoWearComplect.Text = AppVars.Profile.AutoWearComplect ?? string.Empty;

            comboBoxDoAutoDrinkBlaz.SelectedIndex = AppVars.Profile.AutoDrinkBlazOrder;

            switch (AppVars.Profile.BossSay)
            {
                case LezSayType.No:
                    radioSayNo.Checked = true;
                    break;

                case LezSayType.Chat:
                    radioSayChat.Checked = true;
                    break;

                case LezSayType.Clan:
                    radioSayClan.Checked = true;
                    break;

                case LezSayType.Pair:
                    radioSayPair.Checked = true;
                    break;
            }
        }

        private void OnButtonOkClick(object sender, EventArgs e)
        {
            AppVars.Profile.DoAutoDrinkBlaz = checkBoxDoAutoDrinkBlaz.Checked;
            int autoDrinkBlazTied;
            if (!int.TryParse(textBoxAutoDrinkBlazTied.Text, out autoDrinkBlazTied))
            {
                autoDrinkBlazTied = 84;
            }

            AppVars.Profile.AutoDrinkBlazTied = autoDrinkBlazTied;

            AppVars.Profile.DoPromptExit = checkboxDoPromptExit.Checked;
            AppVars.Profile.DoTray = checkboxDoTray.Checked;
            AppVars.Profile.ShowTrayBaloons = checkboxShowTrayBaloons.Checked;

            AppVars.Profile.ChatKeepMoving = checkboxDoKeepChatMoving.Checked;
            AppVars.Profile.ChatKeepGame = checkboxDoKeepChatGame.Checked;
            AppVars.Profile.ChatKeepLog = checkboxDoKeepChatLog.Checked;
            AppVars.Profile.ChatSizeLog = (int) linkChatSizeLog.Tag;
            AppVars.Profile.DoChatLevels = checkDoChatLevels.Checked;
            
            AppVars.Profile.DoRob = checkDoRob.Checked;

            if (!AppVars.Profile.Stat.Reset && checkStatReset.Checked)
            {
                AppVars.Profile.Stat.LastUpdateDay = DateTime.Now.DayOfYear;
            }

            AppVars.Profile.Stat.Reset = checkStatReset.Checked;

            AppVars.Profile.RazdChatReport = checkboxRazdChatReport.Checked;

            AppVars.Profile.CureNV[0] = (int)numCureNV1.Value;
            AppVars.Profile.CureNV[1] = (int)numCureNV2.Value;
            AppVars.Profile.CureNV[2] = (int)numCureNV3.Value;
            AppVars.Profile.CureNV[3] = (int)numCureNV4.Value;
            AppVars.Profile.CureAsk[0] = textCureAsk1.Text;
            AppVars.Profile.CureAsk[1] = textCureAsk2.Text;
            AppVars.Profile.CureAsk[2] = textCureAsk3.Text;
            AppVars.Profile.CureAsk[3] = textCureAsk4.Text;
            AppVars.Profile.CureAdv = textCureAdv.Text;
            AppVars.Profile.CureAfter = textCureAfter.Text;
            AppVars.Profile.CureBoi = textCureBoi.Text;
            AppVars.Profile.CureEnabled[0] = checkE1.Checked;
            AppVars.Profile.CureEnabled[1] = checkE2.Checked;
            AppVars.Profile.CureEnabled[2] = checkE3.Checked;
            AppVars.Profile.CureEnabled[3] = checkE4.Checked;
            AppVars.Profile.CureDisabledLowLevels = checkD04.Checked;
           
            AppVars.Profile.MapShowExtend = checkBoxDoExtendMap.Checked;
            AppVars.Profile.MapBigWidth = (int)numBigMapWidth.Value;
            AppVars.Profile.MapBigHeight = (int)numBigMapHeight.Value;
            AppVars.Profile.MapBigScale = (int)numBigMapScale.Value;
            AppVars.Profile.MapBigTransparency = (int)numBigMapTransparency.Value;
            AppVars.Profile.MapShowBackColorWhite = checkBoxBigMapBackColorWhite.Checked;
            AppVars.Profile.MapDrawRegion = checkBoxMapDrawRegion.Checked;

            AppVars.Profile.MapMiniWidth = (int)numMiniMapWidth.Value;
            AppVars.Profile.MapMiniHeight = (int)numMiniMapHeight.Value;
            AppVars.Profile.MapMiniScale = (int)numMiniMapScale.Value;
            AppVars.Profile.MapShowMiniMap = checkShowMiniMap.Checked;

            AppVars.Profile.FishStopOverWeight = checkboxStopOverW.Checked;

            AppVars.Profile.Sound.Enabled = checkUseSounds.Checked;
            AppVars.Profile.Sound.DoPlayDigits = checkDoPlayDigits.Checked;
            AppVars.Profile.Sound.DoPlayAttack = checkDoPlayAttack.Checked;
            AppVars.Profile.Sound.DoPlaySndMsg = checkDoPlaySndMsg.Checked;
            AppVars.Profile.Sound.DoPlayRefresh = checkDoPlayRefresh.Checked;
            AppVars.Profile.Sound.DoPlayAlarm = checkDoPlayAlarm.Checked;
            AppVars.Profile.Sound.DoPlayTimer = checkDoPlayTimer.Checked;

            AppVars.Profile.AutoAdv.Phraz = textPhraz.Text;
            AppVars.Profile.AutoAdv.Sec = (int)((numAdvMin.Value * 60) + numAdvSec.Value);
            if (AppVars.Profile.AutoAdv.Sec < 30)
            {
                AppVars.Profile.AutoAdv.Sec = 600;
            }

            AppVars.Profile.FishAutoWear = checkFishAutoWear.Checked;
            AppVars.Profile.FishHandOne = (string)comboFishHand1.SelectedItem;
            AppVars.Profile.FishHandTwo = (string)comboFishHand2.SelectedItem;

            AppVars.Profile.FishTiedHigh = (int)numFishTiedHigh.Value;
            AppVars.Profile.FishTiedZero = checkFishTiedZero.Checked;

            AppVars.Profile.FishEnabledPrims = 0;
            if (checkPrimBread.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.Bread;
            }

            if (checkPrimWorm.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.Worm;
            }

            if (checkPrimBigWorm.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.BigWorm;
            }

            if (checkPrimStink.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.Stink;
            }

            if (checkPrimFly.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.Fly;
            }

            if (checkPrimLight.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.Light;
            }

            if (checkPrimDonka.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.Donka;
            }

            if (checkPrimMorm.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.Morm;
            }

            if (checkPrimHiFlight.Checked)
            {
                AppVars.Profile.FishEnabledPrims += (int) Prims.HiFlight;
            }

            AppVars.Profile.FishChatReport = checkboxFishChatReport.Checked;
            AppVars.Profile.FishChatReportColor = checkboxFishChatReportColor.Checked;

            AppVars.Profile.DoAutoAnswer = checkAutoAnswer.Checked;
            AppVars.Profile.AutoAnswer = textAutoAnswer.Text.Trim().Replace(Environment.NewLine, AppConsts.Br);
            AutoAnswerMachine.SetAnswers(AppVars.Profile.AutoAnswer);

            AppVars.Profile.LightForum = checkLightForum.Checked;

            AppVars.Profile.TorgTabl = textTorgTable.Text;
            TorgList.Parse(textTorgTable.Text);
            AppVars.Profile.TorgMessageAdv = textTorgMessageAdv.Text;

            int advTime;
            if (int.TryParse(textTorgAdvTime.Text, out advTime))
            {
                AppVars.Profile.TorgAdvTime = advTime;
            }

            AppVars.Profile.TorgMessageNoMoney = textTorgMessageNoMoney.Text;
            AppVars.Profile.TorgMessageTooExp = textTorgMessageTooExp.Text;
            AppVars.Profile.TorgMessageThanks = textTorgMessageThanks.Text;
            AppVars.Profile.TorgMessageLess90 = textTorgMessageLess90.Text;
            AppVars.Profile.TorgSliv = checkTorgSliv.Checked;

            int minlevel;
            if (int.TryParse(textTorgMinLevel.Text, out minlevel))
            {
                AppVars.Profile.TorgMinLevel = minlevel;
            }

            AppVars.Profile.TorgEx = textTorgEx.Text;
            AppVars.Profile.TorgDeny = textTorgDeny.Text;

            AppVars.Profile.DoInvPack = checkDoInvPack.Checked;
            AppVars.Profile.DoInvPackDolg = checkDoInvPackDolg.Checked;
            AppVars.Profile.DoInvSort = checkDoInvSort.Checked;

            AppVars.Profile.DoShowFastAttack = checkDoShowFastAttack.Checked;
            AppVars.Profile.DoShowFastAttackBlood = checkDoShowFastAttackBlood.Checked; 
            AppVars.Profile.DoShowFastAttackUltimate = checkDoShowFastAttackUltimate.Checked;
            AppVars.Profile.DoShowFastAttackClosedUltimate = checkDoShowFastAttackClosedUltimate.Checked;
            AppVars.Profile.DoShowFastAttackClosed = checkDoShowFastAttackClosed.Checked;
            AppVars.Profile.DoShowFastAttackFist = checkDoShowFastAttackFist.Checked;
            AppVars.Profile.DoShowFastAttackClosedFist = checkDoShowFastAttackClosedFist.Checked;
            AppVars.Profile.DoShowFastAttackOpenNevid = checkDoShowFastAttackOpenNevid.Checked;
            AppVars.Profile.DoShowFastAttackPoison = checkDoShowFastAttackPoison.Checked;
            AppVars.Profile.DoShowFastAttackStrong = checkDoShowFastAttackStrong.Checked;
            AppVars.Profile.DoShowFastAttackNevid = checkDoShowFastAttackNevid.Checked;
            AppVars.Profile.DoShowFastAttackFog = checkDoShowFastAttackFog.Checked;
            AppVars.Profile.DoShowFastAttackZas = checkDoShowFastAttackZas.Checked;
            AppVars.Profile.DoShowFastAttackTotem = checkDoShowFastAttackTotem.Checked;
            AppVars.Profile.DoShowFastAttackPortal = checkDoShowFastAttackPortal.Checked;

            AppVars.Profile.ShowOverWarning = checkShowOverWarning.Checked;
            AppVars.Profile.DoStopOnDig = checkDoStopOnDig.Checked;

            AppVars.Profile.DoAutoCure = checkDoAutoCure.Checked;
            AppVars.Profile.AutoWearComplect = textAutoWearComplect.Text;

            AppVars.Profile.AutoDrinkBlazOrder = comboBoxDoAutoDrinkBlaz.SelectedIndex;

            if (radioSayNo.Checked)
                AppVars.Profile.BossSay = LezSayType.No;

            if (radioSayChat.Checked)
                AppVars.Profile.BossSay = LezSayType.Chat;

            if (radioSayClan.Checked)
                AppVars.Profile.BossSay = LezSayType.Clan;

            if (radioSayPair.Checked)
                AppVars.Profile.BossSay = LezSayType.Pair;

            AppVars.Profile.Save();
        }

        private void OnLinkChatSizeLogLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var ff = new FormEnterInt("Размер чата", (int)((LinkLabel)sender).Tag, 8, 128))
            {
                if (ff.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                linkChatSizeLog.Tag = ff.Val;
                linkChatSizeLog.Text = "Размер чата: " + ff.Val + "Кб";
            }
        }

        private void textTorgTable_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (TorgList.Parse(textTorgTable.Text))
            {
                return;
            }

            e.Cancel = true;
            textTorgTable.Text = AppVars.Profile.TorgTabl;
            errorTorg.SetError(textTorgTable, "Ошибка в таблице торга");
        }

        private void textTorgTable_Validated(object sender, EventArgs e)
        {
            errorTorg.SetError(textTorgTable, string.Empty);
        }

        private void textTorgAdvTime_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int advTime;
            if (int.TryParse(textTorgAdvTime.Text, out advTime))
            {
                if (advTime >= AppConsts.TorgAdvTimeMin && advTime <= AppConsts.TorgAdvTimeMax)
                {
                    return;
                }
            }

            e.Cancel = true;
            textTorgAdvTime.SelectAll();
            errorTorg.SetError(textTorgAdvTime, "Должно быть целое число минут, например, 10");
        }

        private void textTorgAdvTime_Validated(object sender, EventArgs e)
        {
            errorTorg.SetError(textTorgAdvTime, string.Empty);
        }

        private void textTorgMinLevel_Validated(object sender, EventArgs e)
        {
            errorTorg.SetError(textTorgMinLevel, string.Empty);
        }

        private void textTorgMinLevel_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int minlevel;
            if (int.TryParse(textTorgMinLevel.Text, out minlevel))
            {
                if (minlevel >= 1 && minlevel <= 33)
                {
                    return;
                }
            }

            e.Cancel = true;
            textTorgMinLevel.SelectAll();
            errorTorg.SetError(textTorgMinLevel, "Уровень вещи - это целое число от 1 до 34, например, 10");
        }
    }
}