namespace ABClient.ABForms
{
    using System;
    using System.Windows.Forms;
    using AppControls;
    using Forms;

    internal sealed partial class FormMain
    {
        private void InitForm()
        {
            InitSize();
            ToolStripManager.Renderer = new Office2007Render();
        }

        private void FormLoad()
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                        new object[] {  $"{AppConsts.ApplicationName} has been started" });
                }
            }
            catch (InvalidOperationException)
            {
            }

            RestoreElements();
            Text = AppVars.AppVersion.NickProductShortVersion;

            foreach (var bookmark in Favorites.Bookmarks)
            {
                var menuitem = new ToolStripMenuItem
                                   {
                                       Name = Guid.NewGuid().ToString(),
                                       Size = new System.Drawing.Size(207, 22),
                                       Tag = bookmark.Url,
                                       Text = bookmark.Title,
                                       ImageScaling = ToolStripItemImageScaling.None,
                                       ToolTipText = bookmark.Url
                                   };

                if (bookmark.SmallIcon != null)
                {
                    menuitem.Image = bookmark.SmallIcon;
                }

                menuitem.Click += OnMenuitemSiteClick;
                menuitemTabs.DropDownItems.Add(menuitem);
            }

            if (!string.IsNullOrEmpty(AppVars.Profile.Complects))
            {
                var complects = AppVars.Profile.Complects.Split('|');
                UpdateComplects(complects);
            }

            //BossUsers.LoadUsers();
            BossMap.LoadMap();
            ContactsManager.LoadBossUsers();

            LogOn();
        }

        private void SaveElements()
        {
            if (tabControlRight != null)
            {
                AppVars.Profile.SelectedRightPanel = (byte) tabControlRight.SelectedIndex;
            }

            AppVars.Profile.Splitter.Collapsed = collapsibleSplitter.IsCollapsed;
            AppVars.Profile.Splitter.Width = panelRight.Width;
        }

        private void RestoreElements()
        {
            ChangeAutoboiState(AppVars.Profile.LezDoAutoboi ? AutoboiState.AutoboiOn : AutoboiState.AutoboiOff);

            AppVars.AutoRefresh = false;
            buttonAutoRefresh.Checked = false;

            buttonAutoAnswer.Checked = AppVars.Profile.DoAutoAnswer;
            tsBossTrace.Checked = false;
            
            buttonDoTexLog.Checked = AppVars.Profile.DoTexLog;
            buttonShowPerformance.Checked = AppVars.Profile.ShowPerformance;
            buttonAutoFish.Checked = AppVars.Profile.FishAuto;
            if (AppVars.Profile.FishAuto)
            {
                AppVars.SwitchToPerc = true;
                AppVars.SwitchToFlora = true;
            }

            buttonAutoSkin.Checked = AppVars.Profile.SkinAuto;
            if (AppVars.Profile.SkinAuto)
            {
                AppVars.SwitchToPerc = true;
                AppVars.SwitchToFlora = true;
                AppVars.AutoSkinCheckUm = true;
                AppVars.AutoSkinCheckRes = true;
                AppVars.SkinUm = 0;
                AppVars.AutoSkinCheckKnife = true;
                AppVars.AutoSkinArmedKnife = false;
            }

            buttonSilence.Checked = !AppVars.Profile.Sound.Enabled;
            statuslabelTorgAdv.Enabled = AppVars.Profile.TorgActive;
            if (AppVars.Profile.SelectedRightPanel < tabControlRight.TabCount)
            {
                tabControlRight.SelectedIndex = AppVars.Profile.SelectedRightPanel;
            }

            menuitemGuamod.Checked = AppVars.Profile.DoGuamod;

            UpdateStat();

            panelRight.Width = AppVars.Profile.Splitter.Width;
            if (AppVars.Profile.Splitter.Collapsed)
            {
                collapsibleSplitter.ToggleState();
            }

            LoadTabs();
            UpdateLocationSafe(AppVars.Profile.MapLocation);

            AppVars.Tied = 0;
            AppVars.LastTied = DateTime.MinValue;

            AppVars.LastChList = DateTime.Now;

            tsContactTrace.Checked = AppVars.Profile.DoContactTrace;
            tsBossTrace.Checked = AppVars.Profile.DoBossTrace;

            ContactsManager.Init(treeContacts);
            RoomManager.StartTracing();

            Things.ThingsDb.Load();

            AppVars.LastAdv = DateTime.Now;
            AppVars.LastTorgAdv = DateTime.Now;
        }

        private void LogOn()
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                        new object[] { "LogOn()" });
                }
            }
            catch (InvalidOperationException)
            {
            }

            AppVars.NextCheckNoConnection = DateTime.Now.AddSeconds(30);
            var chat = ReadChat();
            if (chat != null)
            {
                AppVars.Chat = chat;
            }

            SaveElements();
            AppVars.LastMainPhp = DateTime.Now;
            AppVars.LastInitForm = DateTime.Now;
            AppVars.IdleTimer = DateTime.Now;
            InitGameTab();
        }

        private bool CloseForm()
        {
            if (AppVars.DoPromptExit)
            {
                using (var ff = new FormPromptExit())
                {
                    if (ff.ShowDialog() == DialogResult.OK)
                    {
                        AppVars.Profile.DoPromptExit = !ff.SkipPromptExit;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            RoomManager.StopTracing();
            WaitForTurnStop();

            SaveSize();
            SaveElements();
            SaveTabs();
            ExtMap.Map.SaveAbcMap();
            ContactsManager.SaveBossUsers();
            AppVars.Profile.Save();            
            return true;
        }
    }
}