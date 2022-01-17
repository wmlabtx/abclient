using System.IO;
using System.Net;
using System.Threading;
using ABClient.ExtMap;
using ABClient.MyHelpers;

namespace ABClient.ABForms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net.Http;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.UI;
    using System.Windows.Forms;
    using System.Xml;
    using ABClient.PostFilter;
    using ABProxy;
    using AppControls;
    using MyChat;
    using MyForms;
    using Newtonsoft.Json;
    using NLog;
    using Properties;
    using Tabs;

    internal sealed partial class FormMain : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short wYear;
            public short wMonth;
            public short wDayOfWeek;
            public short wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetSystemTime(ref SYSTEMTIME time);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetSystemTime(ref SYSTEMTIME time);
        private static readonly ReaderWriterLock LockStat = new ReaderWriterLock();
        private static readonly ReaderWriterLock LockAddressStatus = new ReaderWriterLock();
        internal static readonly ReaderWriterLock LockOb = new ReaderWriterLock();
        private static readonly ReaderWriterLock LockBaloon = new ReaderWriterLock();
        private FormWindowState _prevWindowState = FormWindowState.Normal;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        //private readonly Boss _boss1 = new Boss("2303103");
        //private readonly Boss _boss2 = new Boss("2304578");

        //// *** Инициализация ***

        internal FormMain()
        {
            InitializeComponent();
            SetRightTime();
            InitForm();
        }

        private async void SetRightTime() {
            SYSTEMTIME time = new SYSTEMTIME();
            //получаем текущее время
            GetSystemTime(ref time);

            
            string url = "http://worldtimeapi.org/api/timezone/Europe/moscow";
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                JsonTimeClass jtc = JsonConvert.DeserializeObject<JsonTimeClass>(json);
                var now = DateTime.Parse(jtc.datetime).ToUniversalTime();
                SYSTEMTIME stime = new SYSTEMTIME();
                //получаем текущее время
                GetSystemTime(ref stime);
                stime.wHour = (ushort)now.Hour;
                stime.wMinute = (ushort)now.Minute;
                stime.wSecond = (ushort)now.Second;
                //устанавливаем новые значения
                SetSystemTime(ref stime);
                

            }
            catch (Exception ex)
            {
                logger.Error($" Some " + ex.GetType() + " occured in " + ex.StackTrace + " message: " + ex.Message);
            }
        }

        internal string GetServerTime()
        {
            return statuslabelClock.Text;
        }

        internal static void ShowFishTip()
        {
            using (var formFishAdvisor = new FormFishAdvisor())
            {
                formFishAdvisor.ShowDialog();
            }
        }

        //// *** События ***

        private void OnFormMainLoad(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void OnFormMainFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CloseForm();
        }

        private void OnFormMainResize(object sender, EventArgs e)
        {
            MainFormResize();
        }

        private void OnTimerTrayTick(object sender, EventArgs e)
        {
            TrayIconTick();
        }

        private void OnTrayIconMouseDoubleClick(object sender, MouseEventArgs e)
        {
            TrayIconDoubleClick();
        }

        //// *** Нажатия кнопок ***

        /*
        private void buttonDoHttpLog_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonClearHttpLog_Click(object sender, EventArgs e)
        {
            
        }
         */

        private void buttonDoTexLog_Click(object sender, EventArgs e)
        {
            AppVars.Profile.DoTexLog = buttonDoTexLog.Checked;
        }

        private void buttonShowPerformance_Click(object sender, EventArgs e)
        {
            AppVars.Profile.ShowPerformance = buttonShowPerformance.Checked;
        }

        private void buttonClearTexLog_Click(object sender, EventArgs e)
        {
            textboxTexLog.Text = string.Empty;
        }

        //// *** Статистика ***

        private void menuitemStatItem_Click(object sender, EventArgs e)
        {
            ChangeShowStat((string) (((ToolStripItem) sender).Tag));
        }

        private void menuitemStatEdit_Click(object sender, EventArgs e)
        {
            ShowAndClearStat();
        }

        //// *** Cплиттер ***

        private void collapsibleSplitter_MouseClick(object sender, MouseEventArgs e)
        {
            tabControlLeft.Refresh();
            tabControlRight.Refresh();
        }

        //// *** Меню ***

        private void menuitemGameLogOn_Click(object sender, EventArgs e)
        {
            LogOn();
        }

        private void menuitemGameExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuitemOpenForumClick(object sender, EventArgs e)
        {
            TabAddForum();
        }

        private void MenuitemOpenTodayChatClick(object sender, EventArgs e)
        {
            TabAddTodayChat();
        }

        private void MenuitemOpenNotepadClick(object sender, EventArgs e)
        {
            TabAddNotepad();
        }

        private void OnMenuitemOpenTabClick(object sender, EventArgs e)
        {
            TabAddNew();
        }

        private void OnMenuitemSiteClick(object sender, EventArgs e)
        {
            TabAddSite(((string) ((ToolStripMenuItem) sender).Tag));
        }

        private void OnMenuitemSettingsGeneralClick(object sender, EventArgs e)
        {
            SettingsGeneral();
        }

        private void OnMenuitemClanPrivateClick(object sender, EventArgs e)
        {
            InsertMessageToPrompt("%clan%");
        }

        private void OnMenuitemRekPrivateClick(object sender, EventArgs e)
        {
            InsertMessageToPrompt("%pair%");
        }

        private void OnMenuitemMinimizeClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

     /*   private void OnMenuitemGuamodClick(object sender, EventArgs e)
        {
            AppVars.Profile.DoGuamod = menuitemGuamod.Checked;
        }*/

        private void menuitemCacheRefresh_Click(object sender, EventArgs e)
        {
            AppVars.CacheRefresh = menuitemCacheRefresh.Checked;
            if (AppVars.CacheRefresh)
            {
                Cache.Clear();
            }
        }

        private void menuitemCheckNewVersion_Click(object sender, EventArgs e)
        {
        }

        private void menuitemUpdateKey_Click(object sender, EventArgs e)
        {
            DownloadKey();
        }

        private void menuitemAbout_Click(object sender, EventArgs e)
        {
            //ExtMap.Map.UpdateJent();

            using (var ff = new FormAbout())
            {
                ff.ShowDialog();
            }
        }

        private void MenuitemFishAdvisor_Click(object sender, EventArgs e)
        {
            ShowFishTip();
        }

        // *** Табы ***

        private void tabControlLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabChanged();
        }

        private void tabControlLeft_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabDoubleClick(e);
        }

        // *** Закладки ***

        private static void browserGame_BeforeNavigate(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = GameBeforeNavigate(e.Address);
        }

        private void wbTab_BeforeNewWindow(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            BeforeNewWindow(e.Address);
            e.Cancel = true;
        }

        private void browserGame_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            DocumentCompleted();
        }

        private static void wbTab_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            TabChanged(sender);
        }

        // *** Тулбары закладок ***

        private void buttonAutoboi_Click(object sender, EventArgs e)
        {
            ChangeButtonAutoboiState();
        }

        private void buttonNavigator_Click(object sender, EventArgs e)
        {
            MoveToDialog(AppVars.Profile.MapLocation);
        }

        /*private void buttonDrink_Click(object sender, EventArgs e)
        {
            AppVars.AutoDrink = buttonDrink.Checked;
            if (!AppVars.AutoDrink)
                return;

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] {});
                }
            }
            catch (InvalidOperationException)
            {
            }
        }*/

        private void ButtonAutoAnswer_Click(object sender, EventArgs e)
        {
            AppVars.Profile.DoAutoAnswer = buttonAutoAnswer.Checked;
        }

       /* private void ButtonAutoFish_Click(object sender, EventArgs e)
        {
            AppVars.Profile.FishAuto = buttonAutoFish.Checked;
            AppVars.Profile.Save();
            if (!AppVars.Profile.FishAuto)
            {
                return;
            }

            AppVars.AutoFishCheckUd = true;
            AppVars.AutoFishWearUd = false;
            AppVars.AutoFishCheckUm = AppVars.Profile.FishUm == 0;
            AppVars.Profile.LezDoAutoboi = true;
            AppVars.AutoFishHand1 = string.Empty;
            AppVars.AutoFishHand1D = string.Empty;
            AppVars.AutoFishHand2 = string.Empty;
            AppVars.AutoFishHand2D = string.Empty;
            AppVars.AutoFishMassa = string.Empty;
            AppVars.AutoFishNV = 0;
            AppVars.AutoFishDrink = false;
            UpdateNavigatorOff();

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] {});
                }
            }
            catch (InvalidOperationException)
            {
            }
        }*/

      /*  private void buttonAutoSkin_Click(object sender, EventArgs e)
        {
            AppVars.Profile.SkinAuto = buttonAutoSkin.Checked;
            AppVars.Profile.Save();
            if (!AppVars.Profile.SkinAuto)
            {
                return;
            }

            AppVars.AutoSkinCheckUm = true;
            AppVars.AutoSkinCheckRes = true;
            AppVars.SkinUm = 0;
            AppVars.AutoSkinCheckKnife = true;
            AppVars.AutoSkinArmedKnife = false;

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
        }*/

        /*
        private void buttonAutoTorg_Click(object sender, EventArgs e)
        {
            AppVars.Profile.TorgActive = buttonAutoTorg.Checked;
            statuslabelTorgAdv.Enabled = buttonAutoTorg.Checked;
            if (buttonAutoTorg.Checked)
            {
                ChangeAutoboiState(AutoboiState.AutoboiOff);
                AppVars.Profile.Pers.Ready = 0;
                AppVars.Profile.Autoboi.Active = false;                    
            }

            AppVars.Profile.Save();

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
         */

        private void buttonAutoAdv_Click(object sender, EventArgs e)
        {
            AppVars.AdvActive = buttonAutoAdv.Checked;
            //statuslabelAutoAdv.Enabled = buttonAutoAdv.Checked;
        }

        private void buttonSilence_Click(object sender, EventArgs e)
        {
            AppVars.Profile.Sound.Enabled = !buttonSilence.Checked;
            AppVars.Profile.Save();
        }

        private void buttonGameLogOn_Click(object sender, EventArgs e)
        {
            LogOn();
        }

        private static void buttonForumBack_Click(object sender, EventArgs e)
        {
            TabBack(sender);
        }

        private static void buttonForumRefresh_Click(object sender, EventArgs e)
        {
            TabRefresh(sender);
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            TabReload(sender);
        }

        private void buttonForumClose_Click(object sender, EventArgs e)
        {
            TabClose(sender);
        }

        private void buttonPrivate_Click(object sender, EventArgs e)
        {
            TabPrivate(sender);
        }

        private void buttonCompas_Click(object sender, EventArgs e)
        {
            TabCompas(sender);
        }

        private void buttonAddClan_Click(object sender, EventArgs e)
        {
            TabAddClan(sender);
        }

        private void buttonContact_Click(object sender, EventArgs e)
        {
            TabAddContact(sender);
        }

        private void buttonForumScreen_Click(object sender, EventArgs e)
        {
            TabScreen(sender);
        }

        private void buttonForumCopyAddress_Click(object sender, EventArgs e)
        {
            TabCopyUrl();
        }

        // *** Контакты ***

        private void TreeContactsAfterCheck(object sender, TreeViewEventArgs e)
        {
            ContactsManager.AfterCheck(treeContacts, e.Node);
        }

        private void TreeContactsAfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectContact(e.Node);
        }

        private void TreeContactsDoubleClick(object sender, EventArgs e)
        {
            OpenContact();
        }

        private void TreeContactsNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeContacts.SelectedNode = e.Node;
        }

        private void TbContactDetailsTextChanged(object sender, EventArgs e)
        {
            CommentContact();
        }

        // *** Тулбар контактов

        private void OnTsDeleteContactClick(object sender, EventArgs e)
        {
            DeleteContact();
        }

        private void TsContactPrivateClick(object sender, EventArgs e)
        {
            WriteContactPrivate();
        }

        // *** Всплывающие меню контактов ***

        private void OnMiRemoveGroupClick(object sender, EventArgs e)
        {
            var groupName = treeContacts.SelectedNode.Name;
            ContactsManager.RemoveGroup(treeContacts, groupName);
        }

        private void OnCmtsDeleteContactClick(object sender, EventArgs e)
        {
            DeleteContact();
        }

        private void OnCmtsContactPrivateClick(object sender, EventArgs e)
        {
            WriteContactPrivate();
        }

        private void CmtsContactQuickClick(object sender, EventArgs e)
        {
            OpenQuickFromContact();
        }

        // *** Статусбар ***

        private void OnStatuslabelAutoAdvClick(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(
                @"Бросить рекламу в чат досрочно?",
                @"Автореклама",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                AppVars.LastAdv = DateTime.Now;
            }
        }

        private void statuslabelTorgAdv_Click(object sender, EventArgs e)
        {
            var dr = MessageBox.Show(
                @"Бросить рекламу торговли в чат досрочно?",
                @"Автореклама",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                AppVars.LastTorgAdv = DateTime.Now;
            }
        }

        // *** Таймеры ***

        private void OnTimerCrapTick(object sender, EventArgs e)
        {
            TimerCrap();
        }

        private void OnTimerClockTick(object sender, EventArgs e)
        {
            string time = TimerClock();
            statuslabelClock.Text = time;
        }

        // *** Меню таймеров

        private void OnMenuitemNewTimerClick(object sender, EventArgs e)
        {
            using (var ft = new FormNewTimer())
            {
                var da = ft.ShowDialog();
                if (da == DialogResult.OK)
                {
                    UpdateTimers();
                }
            }
        }

        private void OnMenuitemWorkingTimerClick(object sender, EventArgs e)
        {
            var index = (int) (((ToolStripMenuItem) sender).Tag);
            RemoveTimer(index);
        }

        // *** Меню привата с ПВ ***

        internal void OnPvFastToolStripMenuItemClick(object sender, EventArgs e)
        {
            var nick = ((ToolStripMenuItem) sender).Text;
            WriteMessageToPrompt("%<" + nick + "> ");
        }

        // *** Меню привата с травмированным

        internal void OnTravmFastToolStripMenuItemClick(object sender, EventArgs e)
        {
            var nick = (string) (((ToolStripMenuItem) sender).Tag);
            WriteMessageToPrompt("%<" + nick + "> ");
        }

        internal void OnTravmInfoToolStripMenuItemClick(object sender, EventArgs e)
        {
            var nick = (string) (((ToolStripMenuItem) sender).Tag);
            CreateNewTab(TabType.PInfo, Resources.AddressPInfo + nick, false);
        }

        internal static void OnTravmAdvToolStripMenuItemClick(object sender, EventArgs e)
        {
            var nick = (string) (((ToolStripMenuItem) sender).Tag);
            Chat.AddAnswer("%<" + nick + "> " + AppVars.Profile.CureConvert(AppVars.Profile.CureAdv));
        }

        internal static void OnTravmAdvAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            var nicklist = (string) (((ToolStripMenuItem) sender).Tag);
            var nlist = nicklist.Split(':');
            var sb = new StringBuilder();
            for (var i = 0; i < nlist.Length; i++)
            {
                sb.Append("%<");
                sb.Append(nlist[i]);
                sb.Append("> ");
            }

            sb.Append(AppVars.Profile.CureConvert(AppVars.Profile.CureAdv));
            Chat.AddAnswer(sb.ToString());
        }

        internal static void OnTravmChatToolStripMenuItemClick(object sender, EventArgs e)
        {
            Chat.AddAnswer(AppVars.Profile.CureConvert(AppVars.Profile.CureAdv));
        }

        internal static void OnTravmAskToolStripMenuItemClick(object sender, EventArgs e)
        {
            var nicklist = (string) (((ToolStripMenuItem) sender).Tag);
            var nlist = nicklist.Split(':');
            var sb = new StringBuilder();
            int t;
            if (!int.TryParse(nlist[0], out t))
            {
                t = 1;
            }

            for (var i = 1; i < nlist.Length; i++)
            {
                sb.Append("%<");
                sb.Append(nlist[i]);
                sb.Append("> ");
            }

            sb.Append(AppVars.Profile.CureConvert(AppVars.Profile.CureAsk[t - 1]));
            Chat.AddAnswer(sb.ToString());
        }

        internal static void OnTravmCureToolStripMenuItemClick(object sender, EventArgs e)
        {
            var nicktr = (string) (((ToolStripMenuItem) sender).Tag);
            var nlist = nicktr.Split(':');
            var strTravm = new[] {"легкую", "среднюю", "тяжелую", "боевую"};
            int typeTravm;
            if (!int.TryParse(nlist[1], out typeTravm))
            {
                return;
            }

            if (typeTravm < 1 || typeTravm > 4)
            {
                return;
            }

            var dr = MessageBox.Show(
                @"Лечить " + strTravm[typeTravm - 1] + @" у " + nlist[0] + @" ?",
                @"Быстрое лечение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            AppVars.CureNeed = true;
            AppVars.CureNick = nlist[0];
            AppVars.CureTravm = nlist[1];

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] {});
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        // *** Меню трея ***

        private void OnMenuitemRestoreWindowClick(object sender, EventArgs e)
        {
            TrayIconDoubleClick();
        }

        private void OnMenuitemTrayQuitClick(object sender, EventArgs e)
        {
            Close();
        }

        /*
        private void menuitemSettingsAutoCut_Click(object sender, EventArgs e)
        {
            using (var formSettingsAutoCut = new FormSettingsAutoCut())
            {
                formSettingsAutoCut.ShowDialog();
            }
        }
         */

        private void buttonHerbAutoCut_Click(object sender, EventArgs e)
        {
            /*
            AppVars.DoHerbAutoCut = buttonHerbAutoCut.Checked;
            if (!AppVars.DoHerbAutoCut) return;
            if (AppVars.Profile.HerbsAutoCut.Count == 0)
            {
                MessageBox.Show(
                    "Ни одна трава не выбрана!",
                    "Автоспил",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                using (var formSettingsAutoCut = new FormSettingsAutoCut())
                {
                    formSettingsAutoCut.ShowDialog();
                }
            }

            ReloadMainTop();
             */
        }

        private void menuitemChatAdvisor_Click(object sender, EventArgs e)
        {
            NextChatTip();
        }

        private static void NextChatTip()
        {
            var message = Tips.GetNextAnswer();
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateChatDelegate(AppVars.MainForm.WriteChatTip),
                        new object[] {message});
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void MiFastEnabledCheckStateChanged(object sender, EventArgs e)
        {
            MiFastEnabledChanged();
        }

        private void MiFastEnabledChanged()
        {
            miFastTeleport.Enabled = miFastEnabled.Checked;
            miFastElxVosst.Enabled = miFastEnabled.Checked;
            miFastF3.Enabled = miFastEnabled.Checked;
            miFastF4.Enabled = miFastEnabled.Checked;
            miFastCtrlF12.Enabled = miFastEnabled.Checked;
            miFastSvitFog.Enabled = miFastEnabled.Checked;
            miFastF9.Enabled = miFastEnabled.Checked;
            miFastF10.Enabled = miFastEnabled.Checked;
        }

        private void MiFastMomentRestoreClick(object sender, EventArgs e)
        {
            FastAttackMomentRestoreElixir();
        }

        private void MiFastMomentCure(object sender, EventArgs e)
        {
            FastAttackMomentCureElixir();
        }

        private void MiFastPrimankaClick(object sender, EventArgs e)
        {
            FastAttackPrimankaElixir();
        }

        private void MiFastNevidPotClick(object sender, EventArgs e)
        {
            FastAttackNevidPot(AppVars.Profile.UserNick);
        }

        private void MiFastIslandPotClick(object sender, EventArgs e)
        {
            FastAttackIslandPot();
        }

        private void MiFastBlazPotClick(object sender, EventArgs e)
        {
            FastAttackBlazPot();
        }

        private void MiFastBlazElexirClick(object sender, EventArgs e)
        {
            FastAttackBlazElixir();
        }

        private void MiQuickClick(object sender, EventArgs e)
        {
            Quick(AppVars.Profile.UserNick);
        }

        internal static void Quick(string nick)
        {
            var formQuick = new FormQuick(nick);
            formQuick.Show();
        }

        internal static void ReloadMainFrame()
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] {});
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void MiFastFogClick(object sender, EventArgs e)
        {
            AppVars.MainForm.FastStartSafe("i_svi_213.gif", AppVars.Profile.UserNick);
            ReloadMainFrame();
        }

        private void MiFastSviRassClick(object sender, EventArgs e)
        {
            FastStartSafe("i_w28_28.gif", "клетке");
            ReloadMainFrame();
        }

        private void MiFastSviSelfRassClick(object sender, EventArgs e)
        {
            FastStartSafe("i_w28_23.gif", "себя");
            ReloadMainFrame();
        }

        private void MiFastDarkTeleportClick(object sender, EventArgs e)
        {
            AppVars.FastNeedAbilDarkTeleport = true;
            WriteChatMsgSafe("Заказано применение сумеречного телепорта в случайную локацию");
            ReloadMainFrame();
        }

        private void MiFastDarkFogClick(object sender, EventArgs e)
        {
            AppVars.FastNeedAbilDarkFog = true;
            WriteChatMsgSafe("Заказано применение сумеречного тумана на себя");
            ReloadMainFrame();
        }

        private void MiFastTeleportClick(object sender, EventArgs e)
        {
            WriteChatMsgSafe("Заказано применение телепорта в случайную локацию");
            AppVars.MainForm.FastStartSafe("i_w28_22.gif", AppVars.Profile.UserNick);
            ReloadMainFrame();
        }

        private void MiQuickCancelClick(object sender, EventArgs e)
        {
            WriteChatMsgSafe("Быстрое действие отменено");
            FastCancelSafe();
        }

        private void ButtonAutoRefreshClick(object sender, EventArgs e)
        {
            AppVars.AutoRefresh = buttonAutoRefresh.Checked;

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] {});
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void MenuitemInvClick(object sender, EventArgs e)
        {
            /*
            string html = string.Empty;

            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.neverlands.ru/main.php?get_id=56&act=10&go=inv&vcode=46450ec4f3d238a9ca9ef5f58cf2f0d9");
                httpWebRequest.Method = "GET";
                httpWebRequest.Proxy = AppVars.LocalProxy;
                var cookies = CookiesManager.Obtain("www.neverlands.ru");
                httpWebRequest.Headers.Add("Cookie", cookies);
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
            }
             */
        }

        private void ButtonWaitOpenClick(object sender, EventArgs e)
        {
            AppVars.WaitOpen = buttonWaitOpen.Checked;
            ReloadMainFrame();
        }

        private void MenuitemShowCookiesClick(object sender, EventArgs e)
        {
            using (var formShowCookies = new FormShowCookies())
            {
                formShowCookies.ShowDialog();
            }
        }

        private void TimerCheckInfoTick(object sender, EventArgs e)
        {
            CheckInfo();
        }

        private void menuitemInvFilter_Click(object sender, EventArgs e)
        {
            //SetMainTopInvoke("http://www.neverlands.ru/main.php?im=0&wca=4");

            var f = GetFrame("main_top");
            if (f == null)
            {
                return;
            }

            try
            {
                if (f.Document == null)
                {
                    return;
                }

                var body = f.Document.Body;
                if (body != null)
                {
                    var b = body.InnerHtml;
                    File.WriteAllText("main_top.html", b);
                }
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch
                // ReSharper restore EmptyGeneralCatchClause
            {
            }

        }
        
        private void ButtonPerenapClick(object sender, EventArgs e)
        {
            AppVars.DoPerenap = buttonPerenap.Checked;
            AppVars.MainForm.WriteChatMsgSafe(AppVars.DoPerenap
                ? "Включен <b>режим перенападения</b>. В этом режиме любое совершенное нападение будет повторяться, приманка будет использоваться непрерывно, будет наливаться сразу 5 спин - до отмены через меню: Команды/Отмена"
                : "<b>Режим перенападения</b> отключен. Любое нападение или использование приманки будет одиночным.");
        }

        private void ButtonWalkers(bool check)
        {
            AppVars.DoShowWalkers = check;
            timerCrap.Interval = AppVars.DoShowWalkers ? 3000 : 10000;

            AppVars.MyCoordOld = string.Empty;
            AppVars.MyLocOld = string.Empty;
        }

        private void ButtonWalkersClick(object sender, EventArgs e)
        {
            ButtonWalkers(buttonWalkers.Checked);
        }

        private void ButtonOpenNevidClick(object sender, EventArgs e)
        {
            AppVars.AutoOpenNevid = buttonOpenNevid.Checked;
            if (buttonOpenNevid.Checked)
            {
                buttonWalkers.Checked = true;
                ButtonWalkers(true);
            }
        }

        private void ButtonSelfNevidClick(object sender, EventArgs e)
        {
            AppVars.DoSelfNevid = buttonSelfNevid.Checked;
            if (buttonSelfNevid.Checked)
            {
                buttonWalkers.Checked = true;
                ButtonWalkers(true);
            }

            ReloadMainFrame();
        }

        /*
        private void ButtonFastCancelClick(object sender, EventArgs e)
        {
            WriteChatMsgSafe("Быстрое действие отменено");
            FastCancelSafe();
        }
         */

        internal void SelfNevidOffSafe()
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker) (SelfNevidOffSafe));
                return;
            }

            buttonSelfNevid.Checked = false;
            AppVars.DoSelfNevid = false;

        }

        private void MenuitemDoSearchBoxClick(object sender, EventArgs e)
        {
            AppVars.DoSearchBox = menuitemDoSearchBox.Checked;
            if (AppVars.DoSearchBox)
            {
                ReloadMainFrame();
            }
        }

        private void menuitemBearRoar_Click(object sender, EventArgs e)
        {
            MySounds.EventSounds.PlayBear();
        }

        private void MenuitemDoResetVisitedCellsClick(object sender, EventArgs e)
        {
            Map.ResetAllVisitedCells();
            ReloadMainFrame();
        }

        private void timer30_Tick(object sender, EventArgs e)
        {
            timer30.Stop();
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        private void buttonFury_Click(object sender, EventArgs e)
        {
            AppVars.DoFury = buttonFury.Checked;
            AppVars.MainForm.WriteChatMsgSafe(AppVars.DoFury
                ? "Включен <b>режим свитка осады</b>. В этом режиме в любом бою первым ударом будет <b>Снежок</b> или <b>Свиток Удар Ярости</b>, после чего режим отключится сам и отключит автобой. <b><font color=red>Не забудьте одеть свиток!</font></b>"
                : "<b>Режим свитка осады</b> отключен.");

        }

        private void menuitemSettingsAb_Click(object sender, EventArgs e)
        {
            SettingsAb();
        }

        private void menuitemCrazy_Click(object sender, EventArgs e)
        {
            /*
            using (var form = new FormAuthTest())
            {
                form.ShowDialog();
            }
            */

            /*
            BrowserAuth.DocumentCompleted += Browser_DocumentCompleted;

            var key = Encoding.ASCII.GetBytes("p@ssw0rdDR0wSS@P6660juht");
            var iv = Encoding.ASCII.GetBytes("p@ssw0rd");
            var askCode = string.Format(
                CultureInfo.InvariantCulture,
                "{0}|{1}|{2}",
                AppVars.AppVersion.Product,
                AppVars.Profile.UserNick,
                DateTime.Now);
            var data = AppVars.Codepage.GetBytes(askCode);
            _tdes = TripleDES.Create();
            _tdes.IV = iv;
            _tdes.Key = key;
            _tdes.Mode = CipherMode.CBC;
            _tdes.Padding = PaddingMode.Zeros;
            var ict = _tdes.CreateEncryptor();
            var enc = ict.TransformFinalBlock(data, 0, data.Length);
            var ask = Convert.ToBase64String(enc);
            var html =
                "<HTML><HEAD></HEAD><BODY>" +
                "<form action=\"http://www.neverlands.ru/modules/abclient/auth.php\" method=\"post\">" +
                $"<input type=\"text\" name=\"ask\" value=\"{ask}\" />" +
                "<input type=\"submit\" id=\"submit\" name=\"submit\" value=\"submit\" />" +
                "</form>" +
                "</BODY></HTML>";

            BrowserAuth.DocumentText = html;
            */
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            /*
            var result = BrowserAuth.DocumentText;

            if (result.Contains("<HTML>"))
            {
                var htmlButton = BrowserAuth.Document?.GetElementById("submit");
                htmlButton?.InvokeMember("Click");
            }
            else
            {
                var encoded = HttpUtility.UrlDecode(result);

                var datad = Convert.FromBase64String(encoded);
                var ictd = _tdes.CreateDecryptor();
                var dec = ictd.TransformFinalBlock(datad, 0, datad.Length);
                var restr = AppVars.Codepage.GetString(dec);
                var par = restr.Split('|');
                if (par.Length < 2 ||
                    (!par[0].Equals("0", StringComparison.Ordinal) &&
                     !par[0].Equals("1", StringComparison.Ordinal)))
                {
                    MessageBox.Show(
                        "Ошибочный серверный ключ",
                        "Ошибка проверки лицензии",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    if (par[0].Equals("1", StringComparison.Ordinal))
                    {
                        MessageBox.Show(
                            "Все ок",
                            "Все ок",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Не куплен",
                            "Не куплен",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
            }
            */

            /*
            while (browser.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();

            if (browser.Document != null)
            {
                browser.Document.All["submit"].c;

                //var theform = browser.Document.GetElementsByTagName("form")[0];
                //theform.InvokeMember("Submit");

                while (browser.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();

                Thread.Sleep(1000);
            }

            if (browser.Document != null && browser.DocumentText != null)
            {
                var encoded = HttpUtility.UrlDecode(browser.DocumentText);
                var datad = Convert.FromBase64String(encoded);
                var ictd = tdes.CreateDecryptor();
                var dec = ictd.TransformFinalBlock(datad, 0, datad.Length);
                var restr = AppVars.Codepage.GetString(dec);
                var par = restr.Split('|');
                if (par.Length < 2 ||
                    (!par[0].Equals("0", StringComparison.Ordinal) &&
                     !par[0].Equals("1", StringComparison.Ordinal)))
                {
                    MessageBox.Show(
                        "Ошибочный серверный ключ",
                        "Ошибка проверки лицензии",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    if (par[0].Equals("1", StringComparison.Ordinal))
                    {
                        MessageBox.Show(
                            "Все ок",
                            "Все ок",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Не куплен",
                            "Не куплен",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }

            }
            */


            /*
            var key = Encoding.ASCII.GetBytes("p@ssw0rdDR0wSS@P6660juht");
            var iv = Encoding.ASCII.GetBytes("p@ssw0rd");
            var askCode = string.Format(
                CultureInfo.InvariantCulture, 
                "{0}|{1}|{2}", 
                AppVars.AppVersion.Product,
                AppVars.Profile.UserNick, 
                DateTime.Now);
            var data = AppVars.Codepage.GetBytes(askCode);
            var tdes = TripleDES.Create();
            tdes.IV = iv;
            tdes.Key = key;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.Zeros;
            var ict = tdes.CreateEncryptor();
            var enc = ict.TransformFinalBlock(data, 0, data.Length);
            var ask = Convert.ToBase64String(enc);
            var postData = string.Format(
                CultureInfo.InvariantCulture,
                "ask={0}",
                HttpUtility.UrlEncode(ask));


            using (var client = new WebClient())
            {
                var values = new NameValueCollection {["ask"] = ask};
                var response = client.UploadValues("http://www.neverlands.ru/modules/abclient/auth.php", "POST", values);
                var responseString = Encoding.Default.GetString(response);
            }
            */

            /*
            WebResponse response = null;
            try
            {
                var httpWebRequest =
                    (HttpWebRequest) WebRequest.Create("http://www.neverlands.ru/modules/abclient/auth.php");
                httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
                httpWebRequest.Method = "POST";
                httpWebRequest.UserAgent = "ABClient";
                httpWebRequest.Proxy = AppVars.LocalProxy;

                var byteArray = Encoding.ASCII.GetBytes(postData);
                httpWebRequest.ContentLength = byteArray.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                var postStream = httpWebRequest.GetRequestStream();
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();
                response = httpWebRequest.GetResponse();
                var receiveStream = response.GetResponseStream();
                if (receiveStream != null)
                {
                    using (var readStream = new StreamReader(receiveStream, Helpers.Russian.Codepage))
                    {
                        var result = readStream.ReadToEnd();
                        var encoded = HttpUtility.UrlDecode(result);
                        var datad = Convert.FromBase64String(encoded);
                        var ictd = tdes.CreateDecryptor();
                        var dec = ictd.TransformFinalBlock(datad, 0, datad.Length);
                        var restr = AppVars.Codepage.GetString(dec);
                        var par = restr.Split('|');
                        if (par.Length < 2 ||
                            (!par[0].Equals("0", StringComparison.Ordinal) &&
                             !par[0].Equals("1", StringComparison.Ordinal)))
                        {
                            MessageBox.Show(
                                "Ошибочный серверный ключ",
                                "Ошибка проверки лицензии",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (par[0].Equals("1", StringComparison.Ordinal))
                            {
                                MessageBox.Show(
                                    "Все ок",
                                    "Все ок",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show(
                                    "Не куплен",
                                    "Не куплен",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(
                    exception.Message,
                    "Ошибка проверки лицензии",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            */

            //Key.KeyFile.CheckExpiration(serverDateTime);



            //if (Key.KeyFile.Status != Key.KeyFileStatus.Ok)

            // <input name="topic_subj" class="inputTxt" id="MTITLE" style="width: 99%;" type="text" value="">
            // <textarea name="topic_mess" class="inputTxt" id="MESSAGE" style="width: 99%;" rows="5" cols="40"></textarea>
            // <input name="s" class="buttons" id="SUBBUT" type="submit" value="  Создать тему  ">



            /*
            var a1 = HelperConverters.NickDecode("%CA%E0%EA%E6%E5+%EC%E5%ED%E5+%F5%EE%F0%EE%F8%EE");
            var a2 = HelperConverters.NickDecode("%E4%FB%E0");
            var a3 = HelperConverters.NickDecode("%D1%EE%E7%E4%E0%F2%FC+%F2%E5%EC%F3");
            */

            /*
            using (var wc = new CookieAwareWebClient())
            {
                byte[] bufferLog = null;
                try
                {
                    bufferLog = wc.DownloadData("http://forum.neverlands.ru/31/1/");
                }
                catch (Exception)
                {
                }

                // var fmain = [31,1,0,1,1,3,"1584505072565068463b0ca",0];

                if (bufferLog == null)
                    return;

                var forum = Russian.Codepage.GetString(bufferLog);
                var args = HelperStrings.SubString(forum, "var fmain = [", "]");
                if (args == null)
                {
                    try
                    {
                        bufferLog = wc.DownloadData("http://forum.neverlands.ru/31/1/");
                    }
                    catch (Exception)
                    {
                    }

                    if (bufferLog == null)
                        return;

                    forum = Russian.Codepage.GetString(bufferLog);
                    args = HelperStrings.SubString(forum, "var fmain = [", "]");
                    if (args == null)
                        return;
                }

                var pars = args.Split(',');
                if (pars.Length < 7)
                    return;

                var id = pars[6].Trim('\"');

                WebResponse response = null;
                try
                {
                    var messageTitle = "Предлагаю";
                    var messageBody =
                        "Друзья!" + Environment.NewLine +
                        "Постоянное обеспечение нашей игры приводит к созданию соответствующих условий активизации." +
                        Environment.NewLine +
                        "Требуется уточнение модели развития. Наш негативный опыт требует от нас анализа перекошенной экономики игры." +
                        Environment.NewLine +
                        "К сожалению, совет игроков не в состоянии охватить все аспекты.Я предлагаю свою помощь." +
                        Environment.NewLine +
                        " " + Environment.NewLine +
                        "Skype: " + Environment.NewLine +
                        "xxx.xxx" + Environment.NewLine +
                        "Сайт: www.xxx.net" + Environment.NewLine +
                        " " + Environment.NewLine +
                        "Остерегайтесь мошенников!Я не я, корова не моя!;";

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(@"http://forum.neverlands.ru/action/");
                    httpWebRequest.Method = "POST";
                    httpWebRequest.Proxy = AppVars.LocalProxy;
                    CookiesManager.Obtain("forum.neverlands.ru");
                    var postData = string.Format(
                        CultureInfo.InvariantCulture,
                        "act=1&subact=1&f=31&p=1&messid={0}&topic_subj={1}&topic_mess={2}&&s=++%D1%EE%E7%E4%E0%F2%FC+%F2%E5%EC%F3++",
                        id,
                        HttpUtility.UrlEncode(messageTitle),
                        HttpUtility.UrlEncode(messageBody));
                    var byteArray = Encoding.ASCII.GetBytes(postData);
                    httpWebRequest.ContentLength = byteArray.Length;
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    var postStream = httpWebRequest.GetRequestStream();
                    postStream.Write(byteArray, 0, byteArray.Length);
                    postStream.Close();
                    response = httpWebRequest.GetResponse();
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                    {
                        using (var readStream = new StreamReader(receiveStream, Russian.Codepage))
                        {
                            var stringResult = readStream.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    Debug.Print(ex.Message);
                }
                finally
                {
                    response?.Close();
                }
            }
            */
        }

        public void FormMainClose(string message)
        {
            SetMainTopInvoke("http://www.neverlands.ru/exit.php");
            while (browserGame.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();

            UnhandledExceptionManager.RemoveHandler();
            AppVars.AccountError = message;
            AppVars.DoPromptExit = false;
            Close();
        }

        private void menuitemInfCookies_Click(object sender, EventArgs e)
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateWriteChatMsgDelegate(AppVars.MainForm.FormMainClose),
                        "Тест закрытия игры");
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        private void menuitemCheckCell_Click(object sender, EventArgs e)
        {
            var changed = false;
            var cell = Map.AbcCells[AppVars.LocationReal];
            cell.Verified = DateTime.Now;
            if (string.Compare(AppVars.LocationName, cell.Label, StringComparison.Ordinal) != 0)
            {
                WriteChatMsgSafe($"Название исправлено. Было: [{cell.Label}], стало [{AppVars.LocationName}]");
                cell.Label = AppVars.LocationName;
                changed = true;
            }

            var html = AppVars.ContentMainPhp;
            // var map = [[997,999,30,"day",[],""],[[998,999
            var mapstr = HelperStrings.SubString(html, "var map = [", "];");
            if (!string.IsNullOrEmpty(mapstr))
            {
                var sp = mapstr.Split(',');
                if (sp.Length >= 2)
                {
                    int cost;
                    int.TryParse(sp[2], out cost);
                    if (cost > 0 && cost != cell.Cost)
                    {
                        WriteChatMsgSafe($"Стоимость прохода исправлена. Было: [{cell.Cost}], стало [{cost}]");
                        cell.Cost = cost;
                        changed = true;
                    }
                }
            }

            if (changed)
            { 
                Map.SaveAbcMap();
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
            else
                WriteChatMsgSafe($"Клетка [{cell.RegNum}] проверена");
        }

        private void ScanMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(ScanMap);
        }

        private void ScanMap(object obj)
        {
            WriteChatMsgSafe("Сканирование карты начато...");

            foreach (var e in Map.Location)
            {
                var p = e.Value;
                var url = $"http://www.neverlands.ru/ch.php?lo=1&r=m_{p.X}_{p.Y}";
                try
                {
                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpWebRequest.Method = "GET";
                    httpWebRequest.Proxy = AppVars.LocalProxy;
                    var cookies = CookiesManager.Obtain("www.neverlands.ru");
                    httpWebRequest.Headers.Add("Cookie", cookies);
                    var resp = httpWebRequest.GetResponse();
                    var webstream = resp.GetResponseStream();
                    if (webstream != null)
                    {
                        var reader = new StreamReader(webstream, AppVars.Codepage);
                        var html = reader.ReadToEnd();

                        // <font class=placename><b>Форпост, Западные Ворота</b></font></a> [

                        var label = HelperStrings.SubString(
                            html, 
                            "<font class=placename><b>",
                            "</b></font></a>");
                        if (string.IsNullOrEmpty(label))
                        {
                            if (Map.AbcCells.ContainsKey(p.RegNum))
                            {
                                var cell = Map.AbcCells[p.RegNum];
                                WriteChatMsgSafe($"Клетка {p.RegNum} ({cell.Label}) удалена");
                                Map.AbcCells.Remove(p.RegNum);
                            }
                        }
                        else
                        {
                            label = label.Replace("<br>", " ");
                            if (Map.AbcCells.ContainsKey(p.RegNum))
                            {
                                var cell = Map.AbcCells[p.RegNum];
                                if (!cell.Label.Equals(label))
                                {
                                    WriteChatMsgSafe($"{p.RegNum}: {label} (было {cell.Label})");
                                    cell.Label = label;
                                }
                            }
                            else
                            {
                                WriteChatMsgSafe($"Клетка {p.RegNum} ({label}) добавлена");
                                var cell = new AbcCell
                                {
                                    RegNum = p.RegNum,
                                    Label = label,
                                    Cost = 30
                                };
                                Map.AbcCells.Add(cell.RegNum, cell);
                            }
                        }
                    }
                }
                catch
                {
                }
            }

            Map.SaveAbcMap();
            WriteChatMsgSafe("Сканирование карты закончено!");
        }

        private void menuitemFatalErrorTest_Click(object sender, EventArgs e)
        {
            var x = new int[1];
            var y = $"{x[2]}";
            WriteChatMsgSafe(y);
        }

        private void menuitemReloadTopFrame_Click(object sender, EventArgs e)
        {
            ReloadMainFrame();
        }

        private void menuitemTurotor_Top_Click(object sender, EventArgs e)
        {
            if (!this.menuitemTurotor_Top.Checked)
            {
                this.TurotorCancel();
                return;
            }
            using (FormTurotor formTurotor = new FormTurotor())
            {
                formTurotor.ShowDialog();
            }
        }

        internal void TurotorCancel()
        {
            this.menuitemTurotor_Top.Checked = false;
            AppVars.DoSentToIsland = false;
            AppVars.TurotorTopDestination1 = null;
            AppVars.TurotorTopDestination2 = null;
        }

        internal bool IsTurotorTopActive()
        {
            return this.menuitemTurotor_Top.Checked;
        }

        private void DrinkSetAddButton_Click(object sender, EventArgs e)
        {
            string text = this.DrinkSetName.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                WriteChatMsgSafe("Невозможно создать сет с пустым именем. Введите имя сета в поле над кнопкой.");
                return;
            }
            if (AppVars.DrinkSets.ContainsKey(text))
            {
                WriteChatMsgSafe("Сет с таким именем уже существует. Используйте другое имя.");
                return;
            }
            this.DrinkSetName.Clear();
            AppVars.DrinkSets.Add(text, string.Empty);
            this.DrinkSetsNames.DataSource = new List<string>(AppVars.DrinkSets.Keys);
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                MemoryStream memoryStream = new MemoryStream();
                XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("drinkSets");
                foreach (KeyValuePair<string, string> keyValuePair in AppVars.DrinkSets)
                {
                    xmlWriter.WriteStartElement("drinkSet");
                    xmlWriter.WriteStartAttribute("name");
                    xmlWriter.WriteString(keyValuePair.Key);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("composition");
                    xmlWriter.WriteString(keyValuePair.Value);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                FileStream fileStream = new FileStream("drinkSets.xml", FileMode.Create);
                memoryStream.WriteTo(fileStream);
                fileStream.Close();
                memoryStream.Close();
            }
            catch(Exception ex)
            {
                logger.Error($"Some error " + ex.GetType() + " with message - " + ex.Message + " occured in " + ex.StackTrace);
            }
        }

        private void DrinkSetsNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = this.DrinkSetsNames.SelectedItem.ToString();
            this.DrinkSetComposition.Text = (AppVars.DrinkSets.ContainsKey(key) ? AppVars.DrinkSets[key] : string.Empty);
        }

        private void DrinkSetItemAddButton_Click(object sender, EventArgs e)
        {
            string text = this.DrinkSetsNames.SelectedItem.ToString();
            if (string.IsNullOrEmpty(text))
            {
                WriteChatMsgSafe("Для добавления элемента в сет, выберите имя сета из поля ниже.");
                return;
            }
            string text2 = this.DrinkSetItemsMenu.Text;
            if (string.IsNullOrEmpty(text2))
            {
                WriteChatMsgSafe("Для добавления элемента в сет, выберите имя элемента из выпадающего списка.");
                return;
            }
            int num = (int)this.DrinkSetItemUsesAmount.Value;
            if (num < 1 || num > 99)
            {
                WriteChatMsgSafe("Для добавления элемента в сет, укажите количество использований от 1 до 99.");
                return;
            }
            string text3 = AppVars.DrinkSets[text];
            if (!string.IsNullOrEmpty(text3))
            {
                text3 += "|";
            }
            AppVars.DrinkSets[text] = text3 + string.Format("{0}:{1}", text2, num);
            string key = this.DrinkSetsNames.SelectedItem.ToString();
            this.DrinkSetComposition.Text = (AppVars.DrinkSets.ContainsKey(key) ? AppVars.DrinkSets[key] : string.Empty);
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                MemoryStream memoryStream = new MemoryStream();
                XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("drinkSets");
                foreach (KeyValuePair<string, string> keyValuePair in AppVars.DrinkSets)
                {
                    xmlWriter.WriteStartElement("drinkSet");
                    xmlWriter.WriteStartAttribute("name");
                    xmlWriter.WriteString(keyValuePair.Key);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("composition");
                    xmlWriter.WriteString(keyValuePair.Value);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                FileStream fileStream = new FileStream("drinkSets.xml", FileMode.Create);
                memoryStream.WriteTo(fileStream);
                fileStream.Close();
                memoryStream.Close();
            }
            catch (Exception ex)
            {
                logger.Error($"Some error " + ex.GetType() + " with message - " + ex.Message + " occured in " + ex.StackTrace);
            }
        }

        private void DrinkSetUse_Click(object sender, EventArgs e)
        {
            string text = this.DrinkSetsNames.SelectedItem.ToString();
            if (string.IsNullOrEmpty(text))
            {
                WriteChatMsgSafe("Выберите имя сета чтобы использовать.");
                return;
            }
            AppVars.DrinkSetName = AppVars.DrinkSets[text];
            FormMain.ReloadMainFrame();
        }

        private void DrinkSetSave_Click(object sender, EventArgs e)
        {
            string text = this.DrinkSetsNames.SelectedItem.ToString();
            if (string.IsNullOrEmpty(text))
            {
                WriteChatMsgSafe("Выберите имя сета чтобы сохранить список элементов.");
                return;
            }
            AppVars.DrinkSets[text] = this.DrinkSetComposition.Text;
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                MemoryStream memoryStream = new MemoryStream();
                XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("drinkSets");
                foreach (KeyValuePair<string, string> keyValuePair in AppVars.DrinkSets)
                {
                    xmlWriter.WriteStartElement("drinkSet");
                    xmlWriter.WriteStartAttribute("name");
                    xmlWriter.WriteString(keyValuePair.Key);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("composition");
                    xmlWriter.WriteString(keyValuePair.Value);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                FileStream fileStream = new FileStream("drinkSets.xml", FileMode.Create);
                memoryStream.WriteTo(fileStream);
                fileStream.Close();
                memoryStream.Close();
            }
            catch (Exception ex)
            {
                logger.Error($"Some error " + ex.GetType() + " with message - " + ex.Message + " occured in " + ex.StackTrace);
            }
        }

        private void DrinkSetDelete_Click(object sender, EventArgs e)
        {
            string text = this.DrinkSetsNames.SelectedItem.ToString();
            if (string.IsNullOrEmpty(text))
            {
                WriteChatMsgSafe("Выберите имя сета чтобы удалить.");
                return;
            }
            AppVars.DrinkSets.Remove(text);
            this.DrinkSetsNames.DataSource = new List<string>(AppVars.DrinkSets.Keys);
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                MemoryStream memoryStream = new MemoryStream();
                XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("drinkSets");
                foreach (KeyValuePair<string, string> keyValuePair in AppVars.DrinkSets)
                {
                    xmlWriter.WriteStartElement("drinkSet");
                    xmlWriter.WriteStartAttribute("name");
                    xmlWriter.WriteString(keyValuePair.Key);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("composition");
                    xmlWriter.WriteString(keyValuePair.Value);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                FileStream fileStream = new FileStream("drinkSets.xml", FileMode.Create);
                memoryStream.WriteTo(fileStream);
                fileStream.Close();
                memoryStream.Close();
            }
            catch (Exception ex)
            {
                logger.Error($"Some error " + ex.GetType() + " with message - " + ex.Message + " occured in " + ex.StackTrace);
            }
        }

        private void FortBuffsSave_Click(object sender, EventArgs e)
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true
                };
                MemoryStream memoryStream = new MemoryStream();
                XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings);
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("drinkSets");
                foreach (KeyValuePair<string, string> keyValuePair in Class72.dictionary_4)
                {
                    xmlWriter.WriteStartElement("drinkSet");
                    xmlWriter.WriteStartAttribute("name");
                    xmlWriter.WriteString(keyValuePair.Key);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("composition");
                    xmlWriter.WriteString(keyValuePair.Value);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteStartElement("fortBuffsCells");
                xmlWriter.WriteValue(this.FortBuffsCells.Text.Trim());
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
                FileStream fileStream = new FileStream("drinkSets.xml", FileMode.Create);
                memoryStream.WriteTo(fileStream);
                fileStream.Close();
                memoryStream.Close();
            }
            catch(Exception ex)
            {
                logger.Error($"Error " + ex.GetType() + " with message " + ex.Message + " occured in " + ex.StackTrace);
            }
        }

        private async void FortBuffsCollect_Click(object sender, EventArgs e)
        {
            string fortsCells = FortBuffsCells.Text;
            string[] forts = fortsCells.Split(',');
            foreach(string val in forts)
            {
                if (AppVars.LocationReal != val)
                {
                    if (AppVars.isInFort)
                    {
                        HtmlElement exitToMapButton = this.browserGame.Document.Window.Frames[0].Document.GetElementById("up");
                        exitToMapButton.RaiseEvent("onclick");
                        Thread.Sleep(1000);
                        ReloadMainPhpInvoke();
                        AppVars.isInFort = false;
                    }
                    await Task.Run(() => this.MoveToSafe(val));
                    Thread.Sleep(500);
                    bool result = AutoGettingBuffMoveCheck(val, AppVars.LocationReal);
                    do
                    {
                        
                        await Task.Run(() => this.UpdateLocationSafe(AppVars.LocationReal));
                        result = AutoGettingBuffMoveCheck(val, AppVars.LocationReal);
                    } while (!result);
                }
                if (AppVars.LocationReal == val)
                {
                    string html = AppVars.ContentMainPhp;
                    await Task.Run(() => this.UpdateLocationSafe(AppVars.LocationReal));
                    /*var enterFortButton = this.browserGame.Document.Window.Frames[0].Document.GetElementById("dep");
                    enterFortButton.RaiseEvent("onclick");*/
                    await Task.Run(() => EnterToFort(html));
                    await Task.Run(() => this.UpdateLocationSafe(AppVars.LocationReal));
                    Thread.Sleep(1000);
                    html = AppVars.ContentMainPhp;
                    var inOutButtons = this.browserGame.Document.GetElementsByTagName("input").GetElementsByName("outpost_enter");
                    var inButton = inOutButtons[1];
                    inButton.RaiseEvent("onclick");
                    Thread.Sleep(3000);
                    var outButton = inOutButtons[0];
                    try
                    {
                        html = AppVars.ContentMainPhp;
                        var effectButtons = this.browserGame.Document.GetElementsByTagName("input").GetElementsByName("apply_effect");
                        var effectButton = effectButtons.Count==0 ? null : effectButtons[0];
                        if (effectButton == null)
                        {
                            outButton.RaiseEvent("onclick");
                            Thread.Sleep(1000);
                            await Task.Run(() => ExitFromFort(html));
                            Thread.Sleep(2000);
                            continue;
                        }
                        effectButton.RaiseEvent("onclick");
                       
                        
                        outButton.RaiseEvent("onclick");
                        
                        await Task.Run(() => ExitFromFort(html));

                       /* HtmlElement exitToMapButton = this.browserGame.Document.GetElementById("up");
                        exitToMapButton.RaiseEvent("onclick");
                       
                        await Task.Run(() => this.browserGame.Navigate("http://www.neverlands.ru/main.php")); */
                        Thread.Sleep(1000);
                        var chatFrame = GetFrame("chmain");
                        if (chatFrame != null)
                        {
                            string message = String.Format("%clan% Я беру бафф в локации {0}. Снова будет доступен в {1}.", AppVars.Profile.MapLocation, DateTime.Now.AddMinutes(30.0));
                            this.WriteChatMsgSafe(message);
                            Thread.Sleep(2000);
                        }
                        HtmlElement exitToMapButton = this.browserGame.Document.Window.Frames[0].Document.GetElementById("up");
                        exitToMapButton.RaiseEvent("onclick");
                        AppVars.isInFort = false;
                        Thread.Sleep(1000);
                    }
                    catch (Exception ex) {
                        logger.Error(ex.Message);
                       // outButton.RaiseEvent("onclick");
                        Thread.Sleep(1000);
                        HtmlElement exitToMapButton = this.browserGame.Document.Window.Frames[0].Document.GetElementById("up");
                        exitToMapButton.RaiseEvent("onclick");
                        Thread.Sleep(1000);
                        AppVars.isInFort = false;
                        await Task.Run(() => this.UpdateLocationSafe(AppVars.LocationReal));
                        continue;
                    }
                    
                }
                
            }
        }
        private bool AutoGettingBuffMoveCheck(string destination, string curLocation)
        {
            bool result = false;
            if (destination == curLocation)
                result = true;

            return result;
        }

        private void EnterToFort(string html)
        {
            string query = Filter.MainPhpFindEnterLink(html);
            string url = "http://www.neverlands.ru/" + query;
            this.browserGame.Navigate(url);
            Thread.Sleep(1000);
            AppVars.isInFort = true;
        }

        private void ExitFromFort(string html)
        {
            string query = Filter.MainPhpFindExitLink(html);
            string url = "http://www.neverlands.ru/" + query;
            this.browserGame.Navigate(url);
            Thread.Sleep(10000);
            ReloadMainPhpInvoke();
            //this.browserGame.Navigate("http://www.neverlands.ru/main.php");
        }

        private void GetFortBuff(string html)
        {
            string vcode = "";
            string query = Filter.MainPhpFindApplyEffectLink(html, out vcode);
            string url = "http://www.neverlands.ru/" + query;
            this.browserGame.Navigate(url);
            Thread.Sleep(1000);
        }

        private void TeleportExtended_Click(object sender, EventArgs e)
        {
            AppVars.ExtendedTPTag = int.Parse((string)((ToolStripMenuItem)sender).Tag);
            AppVars.MainForm.FastStartSafe("i_w28_22.gif", AppVars.Profile.UserNick, 1);
            FormMain.ReloadMainFrame();
        }

    }
}
