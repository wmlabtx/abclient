using System.Windows.Forms;

namespace ABClient.ABForms
{
    using AppControls;

    internal sealed partial class FormMain
    {
        private CollapsibleSplitter collapsibleSplitter;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.TabControl tabControlLeft;
        private System.Windows.Forms.TabPage tabPageGame;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuitemGame;
        private System.Windows.Forms.ToolStripMenuItem menuitemGameExit;
        private System.Windows.Forms.ToolStrip toolbarGame;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Враги");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Клан/семья");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Друзья");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Доктора");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Торговцы");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Оружейники");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Дилеры");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Другие");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Администрация");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.CmPerson = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmtsClassNeutral = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsClassFoe = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsClassFriend = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.cmtsToolId0 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsToolId1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsToolId2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsToolId3 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsToolId4 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsToolId5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.cmtsDeleteContact = new System.Windows.Forms.ToolStripMenuItem();
            this.cmtsContactPrivate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.cmtsContactQuick = new System.Windows.Forms.ToolStripMenuItem();
            this.panelRight = new System.Windows.Forms.Panel();
            this.tabControlRight = new System.Windows.Forms.TabControl();
            this.tabPageContacts = new System.Windows.Forms.TabPage();
            this.treeContacts = new ABClient.AppControls.TreeViewEx();
            this.ImageListContacts = new System.Windows.Forms.ImageList(this.components);
            this.collapsibleSplitterContacts = new ABClient.AppControls.CollapsibleSplitter();
            this.tbContactDetails = new System.Windows.Forms.TextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.tsContactTrace = new System.Windows.Forms.ToolStripButton();
            this.tsBossTrace = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDeleteContact = new System.Windows.Forms.ToolStripButton();
            this.tsContactPrivate = new System.Windows.Forms.ToolStripButton();
            this.tabPageTextLog = new System.Windows.Forms.TabPage();
            this.panelTexLog = new System.Windows.Forms.Panel();
            this.textboxTexLog = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonDoTexLog = new System.Windows.Forms.ToolStripButton();
            this.buttonShowPerformance = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonClearTexLog = new System.Windows.Forms.ToolStripButton();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.checkDoLog = new System.Windows.Forms.CheckBox();
            this.tabPageLocation = new System.Windows.Forms.TabPage();
            this.listBoxLocation = new System.Windows.Forms.ListBox();
            this.tabPageDrinkSets = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FortBuffsCollect = new System.Windows.Forms.Button();
            this.FortBuffsSave = new System.Windows.Forms.Button();
            this.FortBuffsCells = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DrinkSetSave = new System.Windows.Forms.Button();
            this.DrinkSetDelete = new System.Windows.Forms.Button();
            this.DrinkSetUse = new System.Windows.Forms.Button();
            this.DrinkSetComposition = new System.Windows.Forms.TextBox();
            this.DrinkSetsNames = new System.Windows.Forms.ListBox();
            this.DrinkSetItemAddButton = new System.Windows.Forms.Button();
            this.DrinkSetItemUsesAmount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.DrinkSetItemsMenu = new System.Windows.Forms.ComboBox();
            this.DrinkSetAddButton = new System.Windows.Forms.Button();
            this.DrinkSetName = new System.Windows.Forms.TextBox();
            this.tabControlLeft = new System.Windows.Forms.TabControl();
            this.tabPageGame = new System.Windows.Forms.TabPage();
            this.panelGame = new System.Windows.Forms.Panel();
            this.browserGame = new ABClient.AppControls.ExtendedWebBrowser();
            this.toolbarGame = new System.Windows.Forms.ToolStrip();
            this.buttonAutoboi = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonFury = new System.Windows.Forms.ToolStripButton();
            this.buttonAutoRefresh = new System.Windows.Forms.ToolStripButton();
            this.buttonWaitOpen = new System.Windows.Forms.ToolStripButton();
            this.buttonPerenap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonWalkers = new System.Windows.Forms.ToolStripButton();
            this.buttonOpenNevid = new System.Windows.Forms.ToolStripButton();
            this.buttonSelfNevid = new System.Windows.Forms.ToolStripButton();
            this.buttonAutoAttack = new System.Windows.Forms.ToolStripSplitButton();
            this.miAutoAttack0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miAutoAttack1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miAutoAttack2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miAutoAttack3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miAutoAttack4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miAutoAttack5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonNavigator = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSilence = new System.Windows.Forms.ToolStripButton();
            this.buttonAutoAnswer = new System.Windows.Forms.ToolStripButton();
            this.buttonAutoAdv = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonGameLogOn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonGameScreen = new System.Windows.Forms.ToolStripButton();
            this.ic6x16 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuitemGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemSettingsAb = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemSettingsGeneral = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemGameLogOn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemGameExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemOpenTab = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemOpenForum = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemOpenTodayChat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemOpenNotepad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemShowCookies = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemDoSearchBox = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemDoResetVisitedCells = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemTurotor_Top = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuitemCheckCell = new System.Windows.Forms.ToolStripMenuItem();
            this.сканированиеКартыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemReloadTopFrame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemCacheRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemCommands = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemClanPrivate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemRekPrivate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemMinimize = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.miFastEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastTeleport = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastDarkTeleport = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastSviRass = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastSviSelfRass = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastF3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastF4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastElxVosst = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastSvitFog = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastDarkFog = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastF9 = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastF10 = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastF12 = new System.Windows.Forms.ToolStripMenuItem();
            this.miFastCtrlF12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.miWearAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.miQuick = new System.Windows.Forms.ToolStripMenuItem();
            this.miQuickCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.imagelistDownload = new System.Windows.Forms.ImageList(this.components);
            this.saveFileScreen = new System.Windows.Forms.SaveFileDialog();
            this.timerCrap = new System.Windows.Forms.Timer(this.components);
            this.trayImages = new System.Windows.Forms.ImageList(this.components);
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuitemRestoreWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitemTrayQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerTray = new System.Windows.Forms.Timer(this.components);
            this.CmGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miSetGroupNeutral = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetGroupFoe = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetGroupFriend = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.miSetGroupToolId0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetGroupToolId1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetGroupToolId2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetGroupToolId3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetGroupToolId4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetGroupToolId5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.miRemoveGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.timerCheckInfo = new System.Windows.Forms.Timer(this.components);
            this.timer30 = new System.Windows.Forms.Timer(this.components);
            this.statuslabelLocation = new System.Windows.Forms.ToolStripStatusLabel();
            this.dropdownPv = new System.Windows.Forms.ToolStripDropDownButton();
            this.statuslabelTied = new System.Windows.Forms.ToolStripStatusLabel();
            this.dropdownTravm = new System.Windows.Forms.ToolStripDropDownButton();
            this.человек3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.человек3ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.лечитьЛегкуюТравмуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.лечитьСреднююТравмуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.лечитьТяжелуюТравмуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.лечитьБоевуюТравмуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.отправитьРекламуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьИнфуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statuslabelClock = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dropdownTimers = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuitemNewTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.labelAddress = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuitemStatEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.collapsibleSplitter = new ABClient.AppControls.CollapsibleSplitter();
            this.teleportExtendedBar = new System.Windows.Forms.ToolStripDropDownButton();
            this.tpLocation1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation6 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation7 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation8 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation9 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation10 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation11 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpLocation12 = new System.Windows.Forms.ToolStripMenuItem();
            this.CmPerson.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.tabControlRight.SuspendLayout();
            this.tabPageContacts.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPageTextLog.SuspendLayout();
            this.panelTexLog.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.tabPageLocation.SuspendLayout();
            this.tabPageDrinkSets.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkSetItemUsesAmount)).BeginInit();
            this.tabControlLeft.SuspendLayout();
            this.tabPageGame.SuspendLayout();
            this.panelGame.SuspendLayout();
            this.toolbarGame.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.cmTray.SuspendLayout();
            this.CmGroup.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // CmPerson
            // 
            this.CmPerson.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmPerson.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmtsClassNeutral,
            this.cmtsClassFoe,
            this.cmtsClassFriend,
            this.toolStripSeparator18,
            this.cmtsToolId0,
            this.cmtsToolId1,
            this.cmtsToolId2,
            this.cmtsToolId3,
            this.cmtsToolId4,
            this.cmtsToolId5,
            this.toolStripSeparator9,
            this.cmtsDeleteContact,
            this.cmtsContactPrivate,
            this.toolStripSeparator19,
            this.cmtsContactQuick});
            this.CmPerson.Name = "CmPerson";
            this.CmPerson.Size = new System.Drawing.Size(222, 286);
            // 
            // cmtsClassNeutral
            // 
            this.cmtsClassNeutral.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmtsClassNeutral.Name = "cmtsClassNeutral";
            this.cmtsClassNeutral.Size = new System.Drawing.Size(221, 22);
            this.cmtsClassNeutral.Text = "Нейтрал";
            this.cmtsClassNeutral.Click += new System.EventHandler(this.CmtsClassNeutralClick);
            // 
            // cmtsClassFoe
            // 
            this.cmtsClassFoe.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmtsClassFoe.ForeColor = System.Drawing.Color.DarkRed;
            this.cmtsClassFoe.Name = "cmtsClassFoe";
            this.cmtsClassFoe.Size = new System.Drawing.Size(221, 22);
            this.cmtsClassFoe.Text = "Враг";
            this.cmtsClassFoe.Click += new System.EventHandler(this.CmtsClassFoeClick);
            // 
            // cmtsClassFriend
            // 
            this.cmtsClassFriend.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmtsClassFriend.ForeColor = System.Drawing.Color.DarkGreen;
            this.cmtsClassFriend.Name = "cmtsClassFriend";
            this.cmtsClassFriend.Size = new System.Drawing.Size(221, 22);
            this.cmtsClassFriend.Text = "Друг";
            this.cmtsClassFriend.Click += new System.EventHandler(this.CmtsClassFriendClick);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(218, 6);
            // 
            // cmtsToolId0
            // 
            this.cmtsToolId0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmtsToolId0.Name = "cmtsToolId0";
            this.cmtsToolId0.Size = new System.Drawing.Size(221, 22);
            this.cmtsToolId0.Text = "Как на тулбаре";
            this.cmtsToolId0.Click += new System.EventHandler(this.CmtsToolId0Click);
            // 
            // cmtsToolId1
            // 
            this.cmtsToolId1.Name = "cmtsToolId1";
            this.cmtsToolId1.Size = new System.Drawing.Size(221, 22);
            this.cmtsToolId1.Text = "Боевая нападалка";
            this.cmtsToolId1.Click += new System.EventHandler(this.CmtsToolId1Click);
            // 
            // cmtsToolId2
            // 
            this.cmtsToolId2.Name = "cmtsToolId2";
            this.cmtsToolId2.Size = new System.Drawing.Size(221, 22);
            this.cmtsToolId2.Text = "Закрытая боевая нападалка";
            this.cmtsToolId2.Click += new System.EventHandler(this.CmtsToolId2Click);
            // 
            // cmtsToolId3
            // 
            this.cmtsToolId3.Name = "cmtsToolId3";
            this.cmtsToolId3.Size = new System.Drawing.Size(221, 22);
            this.cmtsToolId3.Text = "Кулачка";
            this.cmtsToolId3.Click += new System.EventHandler(this.CmtsToolId3Click);
            // 
            // cmtsToolId4
            // 
            this.cmtsToolId4.Name = "cmtsToolId4";
            this.cmtsToolId4.Size = new System.Drawing.Size(221, 22);
            this.cmtsToolId4.Text = "Закрытая кулачка";
            this.cmtsToolId4.Click += new System.EventHandler(this.CmtsToolId4Click);
            // 
            // cmtsToolId5
            // 
            this.cmtsToolId5.Name = "cmtsToolId5";
            this.cmtsToolId5.Size = new System.Drawing.Size(221, 22);
            this.cmtsToolId5.Text = "Портал";
            this.cmtsToolId5.Click += new System.EventHandler(this.CmtsToolId5Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(218, 6);
            // 
            // cmtsDeleteContact
            // 
            this.cmtsDeleteContact.Name = "cmtsDeleteContact";
            this.cmtsDeleteContact.Size = new System.Drawing.Size(221, 22);
            this.cmtsDeleteContact.Text = "Удалить контакт";
            this.cmtsDeleteContact.Click += new System.EventHandler(this.OnCmtsDeleteContactClick);
            // 
            // cmtsContactPrivate
            // 
            this.cmtsContactPrivate.Name = "cmtsContactPrivate";
            this.cmtsContactPrivate.Size = new System.Drawing.Size(221, 22);
            this.cmtsContactPrivate.Text = "Написать в приват";
            this.cmtsContactPrivate.Click += new System.EventHandler(this.OnCmtsContactPrivateClick);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(218, 6);
            // 
            // cmtsContactQuick
            // 
            this.cmtsContactQuick.Name = "cmtsContactQuick";
            this.cmtsContactQuick.Size = new System.Drawing.Size(221, 22);
            this.cmtsContactQuick.Text = "Быстрые действия...";
            this.cmtsContactQuick.Click += new System.EventHandler(this.CmtsContactQuickClick);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.tabControlRight);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(827, 24);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(250, 603);
            this.panelRight.TabIndex = 9;
            // 
            // tabControlRight
            // 
            this.tabControlRight.Controls.Add(this.tabPageContacts);
            this.tabControlRight.Controls.Add(this.tabPageTextLog);
            this.tabControlRight.Controls.Add(this.tabPageLog);
            this.tabControlRight.Controls.Add(this.tabPageLocation);
            this.tabControlRight.Controls.Add(this.tabPageDrinkSets);
            this.tabControlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRight.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlRight.Location = new System.Drawing.Point(0, 0);
            this.tabControlRight.Multiline = true;
            this.tabControlRight.Name = "tabControlRight";
            this.tabControlRight.SelectedIndex = 0;
            this.tabControlRight.ShowToolTips = true;
            this.tabControlRight.Size = new System.Drawing.Size(250, 603);
            this.tabControlRight.TabIndex = 0;
            // 
            // tabPageContacts
            // 
            this.tabPageContacts.Controls.Add(this.treeContacts);
            this.tabPageContacts.Controls.Add(this.collapsibleSplitterContacts);
            this.tabPageContacts.Controls.Add(this.tbContactDetails);
            this.tabPageContacts.Controls.Add(this.toolStrip3);
            this.tabPageContacts.Location = new System.Drawing.Point(4, 40);
            this.tabPageContacts.Name = "tabPageContacts";
            this.tabPageContacts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContacts.Size = new System.Drawing.Size(242, 559);
            this.tabPageContacts.TabIndex = 2;
            this.tabPageContacts.Text = "Контакты";
            // 
            // treeContacts
            // 
            this.treeContacts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeContacts.CheckBoxes = true;
            this.treeContacts.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeContacts.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.treeContacts.FullRowSelect = true;
            this.treeContacts.HideSelection = false;
            this.treeContacts.ImageIndex = 0;
            this.treeContacts.ImageList = this.ImageListContacts;
            this.treeContacts.Location = new System.Drawing.Point(3, 28);
            this.treeContacts.Name = "treeContacts";
            treeNode1.Checked = true;
            treeNode1.ContextMenuStrip = this.CmPerson;
            treeNode1.ForeColor = System.Drawing.Color.Indigo;
            treeNode1.ImageKey = "enemies";
            treeNode1.Name = "enemies";
            treeNode1.SelectedImageKey = "enemies";
            treeNode1.Text = "Враги";
            treeNode2.Checked = true;
            treeNode2.ContextMenuStrip = this.CmPerson;
            treeNode2.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode2.ImageKey = "clan";
            treeNode2.Name = "clan";
            treeNode2.SelectedImageKey = "clan";
            treeNode2.Text = "Клан/семья";
            treeNode3.Checked = true;
            treeNode3.ContextMenuStrip = this.CmPerson;
            treeNode3.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode3.ImageKey = "friends";
            treeNode3.Name = "friends";
            treeNode3.SelectedImageKey = "friends";
            treeNode3.Text = "Друзья";
            treeNode4.Checked = true;
            treeNode4.ContextMenuStrip = this.CmPerson;
            treeNode4.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode4.ImageKey = "doctors";
            treeNode4.Name = "doctors";
            treeNode4.SelectedImageKey = "doctors";
            treeNode4.Text = "Доктора";
            treeNode5.ContextMenuStrip = this.CmPerson;
            treeNode5.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode5.ImageKey = "nego";
            treeNode5.Name = "nego";
            treeNode5.SelectedImageKey = "nego";
            treeNode5.Text = "Торговцы";
            treeNode6.ContextMenuStrip = this.CmPerson;
            treeNode6.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode6.ImageKey = "weapon";
            treeNode6.Name = "weapon";
            treeNode6.SelectedImageKey = "weapon";
            treeNode6.Text = "Оружейники";
            treeNode7.ContextMenuStrip = this.CmPerson;
            treeNode7.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode7.ImageKey = "dealers";
            treeNode7.Name = "dealers";
            treeNode7.SelectedImageKey = "dealers";
            treeNode7.Text = "Дилеры";
            treeNode8.ContextMenuStrip = this.CmPerson;
            treeNode8.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode8.ImageKey = "neutral";
            treeNode8.Name = "neutral";
            treeNode8.SelectedImageKey = "neutral";
            treeNode8.Text = "Другие";
            treeNode9.Checked = true;
            treeNode9.ContextMenuStrip = this.CmPerson;
            treeNode9.ForeColor = System.Drawing.Color.DarkCyan;
            treeNode9.ImageKey = "gods";
            treeNode9.Name = "gods";
            treeNode9.SelectedImageKey = "gods";
            treeNode9.Text = "Администрация";
            this.treeContacts.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
            this.treeContacts.SelectedImageIndex = 0;
            this.treeContacts.ShowNodeToolTips = true;
            this.treeContacts.Size = new System.Drawing.Size(236, 399);
            this.treeContacts.TabIndex = 3;
            this.treeContacts.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.TreeContactsAfterCheck);
            this.treeContacts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeContactsAfterSelect);
            this.treeContacts.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeContactsNodeMouseClick);
            this.treeContacts.DoubleClick += new System.EventHandler(this.TreeContactsDoubleClick);
            // 
            // ImageListContacts
            // 
            this.ImageListContacts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageListContacts.ImageStream")));
            this.ImageListContacts.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageListContacts.Images.SetKeyName(0, "enemies");
            this.ImageListContacts.Images.SetKeyName(1, "clan");
            this.ImageListContacts.Images.SetKeyName(2, "friends");
            this.ImageListContacts.Images.SetKeyName(3, "doctors");
            this.ImageListContacts.Images.SetKeyName(4, "gods");
            this.ImageListContacts.Images.SetKeyName(5, "weapon");
            this.ImageListContacts.Images.SetKeyName(6, "nego");
            this.ImageListContacts.Images.SetKeyName(7, "neutral");
            this.ImageListContacts.Images.SetKeyName(8, "none");
            this.ImageListContacts.Images.SetKeyName(9, "molch");
            this.ImageListContacts.Images.SetKeyName(10, "alch");
            this.ImageListContacts.Images.SetKeyName(11, "rent");
            this.ImageListContacts.Images.SetKeyName(12, "dealers");
            this.ImageListContacts.Images.SetKeyName(13, "pv");
            this.ImageListContacts.Images.SetKeyName(14, "injury0");
            this.ImageListContacts.Images.SetKeyName(15, "injury1");
            this.ImageListContacts.Images.SetKeyName(16, "injury2");
            this.ImageListContacts.Images.SetKeyName(17, "injury3");
            this.ImageListContacts.Images.SetKeyName(18, "injury4");
            // 
            // collapsibleSplitterContacts
            // 
            this.collapsibleSplitterContacts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.collapsibleSplitterContacts.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitterContacts.ControlToHide = this.tbContactDetails;
            this.collapsibleSplitterContacts.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.collapsibleSplitterContacts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleSplitterContacts.ExpandParentForm = false;
            this.collapsibleSplitterContacts.Location = new System.Drawing.Point(3, 427);
            this.collapsibleSplitterContacts.Name = "collapsibleSplitterContacts";
            this.collapsibleSplitterContacts.Size = new System.Drawing.Size(236, 8);
            this.collapsibleSplitterContacts.TabIndex = 4;
            this.collapsibleSplitterContacts.TabStop = false;
            this.collapsibleSplitterContacts.VisualStyle = ABClient.AppControls.SplitterVisualStyle.XP;
            // 
            // tbContactDetails
            // 
            this.tbContactDetails.BackColor = System.Drawing.Color.AliceBlue;
            this.tbContactDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbContactDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbContactDetails.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbContactDetails.Location = new System.Drawing.Point(3, 435);
            this.tbContactDetails.Multiline = true;
            this.tbContactDetails.Name = "tbContactDetails";
            this.tbContactDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbContactDetails.Size = new System.Drawing.Size(236, 121);
            this.tbContactDetails.TabIndex = 2;
            this.tbContactDetails.TextChanged += new System.EventHandler(this.TbContactDetailsTextChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsContactTrace,
            this.tsBossTrace,
            this.toolStripSeparator8,
            this.tsDeleteContact,
            this.tsContactPrivate});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(236, 25);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // tsContactTrace
            // 
            this.tsContactTrace.CheckOnClick = true;
            this.tsContactTrace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsContactTrace.Image = global::ABClient.Properties.Resources._16x16_walkers;
            this.tsContactTrace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsContactTrace.Name = "tsContactTrace";
            this.tsContactTrace.Size = new System.Drawing.Size(23, 22);
            this.tsContactTrace.Tag = "";
            this.tsContactTrace.ToolTipText = "Слежение за контактами";
            this.tsContactTrace.Click += new System.EventHandler(this.tsContactTrace_Click);
            // 
            // tsBossTrace
            // 
            this.tsBossTrace.CheckOnClick = true;
            this.tsBossTrace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBossTrace.Image = global::ABClient.Properties.Resources._16x16_wwalkers;
            this.tsBossTrace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBossTrace.Name = "tsBossTrace";
            this.tsBossTrace.Size = new System.Drawing.Size(23, 22);
            this.tsBossTrace.Tag = "";
            this.tsBossTrace.ToolTipText = "Слежение за боссами";
            this.tsBossTrace.Click += new System.EventHandler(this.tsBossTrace_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // tsDeleteContact
            // 
            this.tsDeleteContact.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDeleteContact.Image = ((System.Drawing.Image)(resources.GetObject("tsDeleteContact.Image")));
            this.tsDeleteContact.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDeleteContact.Name = "tsDeleteContact";
            this.tsDeleteContact.Size = new System.Drawing.Size(23, 22);
            this.tsDeleteContact.ToolTipText = "Удалить контакт";
            this.tsDeleteContact.Click += new System.EventHandler(this.OnTsDeleteContactClick);
            // 
            // tsContactPrivate
            // 
            this.tsContactPrivate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsContactPrivate.Image = global::ABClient.Properties.Resources._16x16_private;
            this.tsContactPrivate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsContactPrivate.Name = "tsContactPrivate";
            this.tsContactPrivate.Size = new System.Drawing.Size(23, 22);
            this.tsContactPrivate.ToolTipText = "Написать в приват";
            this.tsContactPrivate.Click += new System.EventHandler(this.TsContactPrivateClick);
            // 
            // tabPageTextLog
            // 
            this.tabPageTextLog.Controls.Add(this.panelTexLog);
            this.tabPageTextLog.Controls.Add(this.toolStrip1);
            this.tabPageTextLog.Location = new System.Drawing.Point(4, 40);
            this.tabPageTextLog.Name = "tabPageTextLog";
            this.tabPageTextLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTextLog.Size = new System.Drawing.Size(242, 559);
            this.tabPageTextLog.TabIndex = 1;
            this.tabPageTextLog.Text = "Тех.лог";
            this.tabPageTextLog.UseVisualStyleBackColor = true;
            // 
            // panelTexLog
            // 
            this.panelTexLog.Controls.Add(this.textboxTexLog);
            this.panelTexLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTexLog.Location = new System.Drawing.Point(3, 28);
            this.panelTexLog.Name = "panelTexLog";
            this.panelTexLog.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panelTexLog.Size = new System.Drawing.Size(236, 528);
            this.panelTexLog.TabIndex = 1;
            // 
            // textboxTexLog
            // 
            this.textboxTexLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxTexLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textboxTexLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxTexLog.Location = new System.Drawing.Point(0, 3);
            this.textboxTexLog.Multiline = true;
            this.textboxTexLog.Name = "textboxTexLog";
            this.textboxTexLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxTexLog.Size = new System.Drawing.Size(236, 525);
            this.textboxTexLog.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonDoTexLog,
            this.buttonShowPerformance,
            this.toolStripSeparator1,
            this.buttonClearTexLog});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(236, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonDoTexLog
            // 
            this.buttonDoTexLog.CheckOnClick = true;
            this.buttonDoTexLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonDoTexLog.Image = ((System.Drawing.Image)(resources.GetObject("buttonDoTexLog.Image")));
            this.buttonDoTexLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDoTexLog.Name = "buttonDoTexLog";
            this.buttonDoTexLog.Size = new System.Drawing.Size(23, 22);
            this.buttonDoTexLog.ToolTipText = "Выводить события";
            this.buttonDoTexLog.Click += new System.EventHandler(this.buttonDoTexLog_Click);
            // 
            // buttonShowPerformance
            // 
            this.buttonShowPerformance.CheckOnClick = true;
            this.buttonShowPerformance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonShowPerformance.Image = ((System.Drawing.Image)(resources.GetObject("buttonShowPerformance.Image")));
            this.buttonShowPerformance.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonShowPerformance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonShowPerformance.Name = "buttonShowPerformance";
            this.buttonShowPerformance.Size = new System.Drawing.Size(23, 22);
            this.buttonShowPerformance.ToolTipText = "Выводить счетчики производительности";
            this.buttonShowPerformance.Click += new System.EventHandler(this.buttonShowPerformance_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonClearTexLog
            // 
            this.buttonClearTexLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonClearTexLog.Image = ((System.Drawing.Image)(resources.GetObject("buttonClearTexLog.Image")));
            this.buttonClearTexLog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonClearTexLog.Name = "buttonClearTexLog";
            this.buttonClearTexLog.Size = new System.Drawing.Size(23, 22);
            this.buttonClearTexLog.ToolTipText = "Очистить журнал событий";
            this.buttonClearTexLog.Click += new System.EventHandler(this.buttonClearTexLog_Click);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.checkDoLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 40);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(242, 559);
            this.tabPageLog.TabIndex = 3;
            this.tabPageLog.Text = "Отладка";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // checkDoLog
            // 
            this.checkDoLog.AutoSize = true;
            this.checkDoLog.Location = new System.Drawing.Point(13, 12);
            this.checkDoLog.Name = "checkDoLog";
            this.checkDoLog.Size = new System.Drawing.Size(133, 17);
            this.checkDoLog.TabIndex = 0;
            this.checkDoLog.Text = "Отладочный файл";
            this.checkDoLog.UseVisualStyleBackColor = true;
            // 
            // tabPageLocation
            // 
            this.tabPageLocation.Controls.Add(this.listBoxLocation);
            this.tabPageLocation.Location = new System.Drawing.Point(4, 40);
            this.tabPageLocation.Name = "tabPageLocation";
            this.tabPageLocation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLocation.Size = new System.Drawing.Size(242, 559);
            this.tabPageLocation.TabIndex = 4;
            this.tabPageLocation.Text = "Локация";
            this.tabPageLocation.UseVisualStyleBackColor = true;
            // 
            // listBoxLocation
            // 
            this.listBoxLocation.FormattingEnabled = true;
            this.listBoxLocation.Location = new System.Drawing.Point(4, 4);
            this.listBoxLocation.Name = "listBoxLocation";
            this.listBoxLocation.Size = new System.Drawing.Size(235, 472);
            this.listBoxLocation.TabIndex = 0;
            // 
            // tabPageDrinkSets
            // 
            this.tabPageDrinkSets.Controls.Add(this.groupBox1);
            this.tabPageDrinkSets.Controls.Add(this.groupBox3);
            this.tabPageDrinkSets.Location = new System.Drawing.Point(4, 40);
            this.tabPageDrinkSets.Name = "tabPageDrinkSets";
            this.tabPageDrinkSets.Size = new System.Drawing.Size(242, 559);
            this.tabPageDrinkSets.TabIndex = 5;
            this.tabPageDrinkSets.Text = "Упивка";
            this.tabPageDrinkSets.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FortBuffsCollect);
            this.groupBox1.Controls.Add(this.FortBuffsSave);
            this.groupBox1.Controls.Add(this.FortBuffsCells);
            this.groupBox1.Location = new System.Drawing.Point(12, 445);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Форты";
            // 
            // FortBuffsCollect
            // 
            this.FortBuffsCollect.Location = new System.Drawing.Point(7, 72);
            this.FortBuffsCollect.Name = "FortBuffsCollect";
            this.FortBuffsCollect.Size = new System.Drawing.Size(214, 23);
            this.FortBuffsCollect.TabIndex = 1028;
            this.FortBuffsCollect.Text = "Собрать бафы";
            this.FortBuffsCollect.UseVisualStyleBackColor = true;
            this.FortBuffsCollect.Click += new System.EventHandler(this.FortBuffsCollect_Click);
            // 
            // FortBuffsSave
            // 
            this.FortBuffsSave.Location = new System.Drawing.Point(7, 47);
            this.FortBuffsSave.Name = "FortBuffsSave";
            this.FortBuffsSave.Size = new System.Drawing.Size(214, 23);
            this.FortBuffsSave.TabIndex = 1027;
            this.FortBuffsSave.Text = "Сохранить";
            this.FortBuffsSave.UseVisualStyleBackColor = true;
            this.FortBuffsSave.Click += new System.EventHandler(this.FortBuffsSave_Click);
            // 
            // FortBuffsCells
            // 
            this.FortBuffsCells.Location = new System.Drawing.Point(7, 20);
            this.FortBuffsCells.Name = "FortBuffsCells";
            this.FortBuffsCells.Size = new System.Drawing.Size(214, 21);
            this.FortBuffsCells.TabIndex = 1027;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.DrinkSetSave);
            this.groupBox3.Controls.Add(this.DrinkSetDelete);
            this.groupBox3.Controls.Add(this.DrinkSetUse);
            this.groupBox3.Controls.Add(this.DrinkSetComposition);
            this.groupBox3.Controls.Add(this.DrinkSetsNames);
            this.groupBox3.Controls.Add(this.DrinkSetItemAddButton);
            this.groupBox3.Controls.Add(this.DrinkSetItemUsesAmount);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.DrinkSetItemsMenu);
            this.groupBox3.Controls.Add(this.DrinkSetAddButton);
            this.groupBox3.Controls.Add(this.DrinkSetName);
            this.groupBox3.Location = new System.Drawing.Point(12, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 435);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Сеты упивки";
            // 
            // DrinkSetSave
            // 
            this.DrinkSetSave.Location = new System.Drawing.Point(9, 406);
            this.DrinkSetSave.Name = "DrinkSetSave";
            this.DrinkSetSave.Size = new System.Drawing.Size(131, 23);
            this.DrinkSetSave.TabIndex = 1026;
            this.DrinkSetSave.Text = "Сохранить";
            this.DrinkSetSave.UseVisualStyleBackColor = true;
            this.DrinkSetSave.Click += new System.EventHandler(this.DrinkSetSave_Click);
            // 
            // DrinkSetDelete
            // 
            this.DrinkSetDelete.Location = new System.Drawing.Point(146, 406);
            this.DrinkSetDelete.Name = "DrinkSetDelete";
            this.DrinkSetDelete.Size = new System.Drawing.Size(75, 23);
            this.DrinkSetDelete.TabIndex = 1025;
            this.DrinkSetDelete.Text = "Удалить";
            this.DrinkSetDelete.UseVisualStyleBackColor = true;
            this.DrinkSetDelete.Click += new System.EventHandler(this.DrinkSetDelete_Click);
            // 
            // DrinkSetUse
            // 
            this.DrinkSetUse.Location = new System.Drawing.Point(9, 377);
            this.DrinkSetUse.Name = "DrinkSetUse";
            this.DrinkSetUse.Size = new System.Drawing.Size(212, 23);
            this.DrinkSetUse.TabIndex = 1024;
            this.DrinkSetUse.Text = "Использовать";
            this.DrinkSetUse.UseVisualStyleBackColor = true;
            this.DrinkSetUse.Click += new System.EventHandler(this.DrinkSetUse_Click);
            // 
            // DrinkSetComposition
            // 
            this.DrinkSetComposition.Location = new System.Drawing.Point(9, 237);
            this.DrinkSetComposition.Multiline = true;
            this.DrinkSetComposition.Name = "DrinkSetComposition";
            this.DrinkSetComposition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DrinkSetComposition.Size = new System.Drawing.Size(212, 133);
            this.DrinkSetComposition.TabIndex = 1023;
            // 
            // DrinkSetsNames
            // 
            this.DrinkSetsNames.FormattingEnabled = true;
            this.DrinkSetsNames.Location = new System.Drawing.Point(9, 161);
            this.DrinkSetsNames.Name = "DrinkSetsNames";
            this.DrinkSetsNames.ScrollAlwaysVisible = true;
            this.DrinkSetsNames.Size = new System.Drawing.Size(212, 69);
            this.DrinkSetsNames.TabIndex = 1022;
            this.DrinkSetsNames.SelectedIndexChanged += new System.EventHandler(this.DrinkSetsNames_SelectedIndexChanged);
            // 
            // DrinkSetItemAddButton
            // 
            this.DrinkSetItemAddButton.Location = new System.Drawing.Point(7, 131);
            this.DrinkSetItemAddButton.Name = "DrinkSetItemAddButton";
            this.DrinkSetItemAddButton.Size = new System.Drawing.Size(214, 23);
            this.DrinkSetItemAddButton.TabIndex = 1021;
            this.DrinkSetItemAddButton.Text = "Добавить новый элемент";
            this.DrinkSetItemAddButton.UseVisualStyleBackColor = true;
            this.DrinkSetItemAddButton.Click += new System.EventHandler(this.DrinkSetItemAddButton_Click);
            // 
            // DrinkSetItemUsesAmount
            // 
            this.DrinkSetItemUsesAmount.Location = new System.Drawing.Point(177, 104);
            this.DrinkSetItemUsesAmount.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.DrinkSetItemUsesAmount.Name = "DrinkSetItemUsesAmount";
            this.DrinkSetItemUsesAmount.Size = new System.Drawing.Size(44, 21);
            this.DrinkSetItemUsesAmount.TabIndex = 1020;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 442);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Капча";
            // 
            // DrinkSetItemsMenu
            // 
            this.DrinkSetItemsMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DrinkSetItemsMenu.FormattingEnabled = true;
            this.DrinkSetItemsMenu.Items.AddRange(new object[] {
            "",
            "------------ЕДА------------",
            "Рыбная похлебка",
            "Рыбный суп-пюре",
            "Запеченная рыба",
            "Рыбные палочки",
            "Рыбная солянка",
            "Суши",
            "Рыбный салат",
            "Жареная рыба с красной икрой",
            "Вяленая рыба с белым соусом",
            "Паштет с овощами",
            "Рыбная паста по-Кентарийски",
            "Паста из раковых шеек с мясом",
            "Бифштекс от элементаля",
            "Тушеное мясо с гарниром",
            "Жаркое по-Кардиффски",
            "Филе по-Альвийски",
            "Фаршированная форель",
            "--------АЛКОГОЛЬ--------",
            "Самогон",
            "Анисовая водка",
            "Баалгорский травяной настой",
            "Фейданский бренди",
            "Жихорийский шнапс",
            "Коньяк Дубовый",
            "Цветочный пунш",
            "Фаросское вино",
            "--------ЭЛИКСИРЫ--------",
            "Пирог с бананами",
            "Яблочный пирог",
            "Молодильное яблочко",
            "Чашу Айрис",
            "Дар Иланы",
            "Эликсир Быстроты",
            "Зелье Кровожадности",
            "Эликсир из Подснежника",
            "Ледяной эликсир I",
            "Ледяной эликсир II",
            "----------СВИТКИ----------",
            "Свиток Величия",
            "Свиток Каменной Кожи",
            "Свиток Слеза Создателя",
            "Свиток Гнев Локара",
            "Свиток Берсерка",
            "Свиток Благословения",
            "Свиток Проклятия",
            "Свиток Магии Воды",
            "Свиток Магии Воздуха",
            "Свиток Магии Земли",
            "Свиток Магии Огня",
            "Свиток Сопротивления Огню",
            "Свиток Сопротивления Земле",
            "Свиток Сопротивления Воздуху",
            "Свиток Сопротивления Воде",
            "-----------ЗЕЛЬЯ-----------",
            "Зелье Метаболизма",
            "Зелье Блаженства",
            "Зелье Сильной Спины",
            "Зелье Просветления",
            "Зелье Сокрушительных Ударов",
            "Зелье Стойкости",
            "Зелье Недосягаемости",
            "Зелье Точного Попадания",
            "Зелье Ловких Ударов",
            "Зелье Мужества",
            "Зелье Жизни",
            "Зелье Лечения",
            "Зелье Восстановления Маны",
            "Зелье Энергии",
            "Зелье Удачи",
            "Зелье Силы",
            "Зелье Ловкости",
            "Зелье Гения",
            "Зелье Боевой Славы",
            "Зелье Невидимости",
            "Зелье Секрет Волшебника",
            "Зелье Медитации",
            "Зелье Иммунитета",
            "Зелье Лечения Отравлений",
            "Зелье Огненного Ореола",
            "Зелье Колкости",
            "Зелье Загрубелой Кожи",
            "Зелье Панциря",
            "Зелье Человек-гора",
            "Зелье Скорости",
            "Зелье подвижности",
            "Зелье Соколиный взор",
            "Секретное Зелье"});
            this.DrinkSetItemsMenu.Location = new System.Drawing.Point(7, 78);
            this.DrinkSetItemsMenu.Name = "DrinkSetItemsMenu";
            this.DrinkSetItemsMenu.Size = new System.Drawing.Size(214, 21);
            this.DrinkSetItemsMenu.TabIndex = 1018;
            // 
            // DrinkSetAddButton
            // 
            this.DrinkSetAddButton.Location = new System.Drawing.Point(7, 49);
            this.DrinkSetAddButton.Name = "DrinkSetAddButton";
            this.DrinkSetAddButton.Size = new System.Drawing.Size(214, 23);
            this.DrinkSetAddButton.TabIndex = 1;
            this.DrinkSetAddButton.Text = "Добавить новый сет";
            this.DrinkSetAddButton.UseVisualStyleBackColor = true;
            this.DrinkSetAddButton.Click += new System.EventHandler(this.DrinkSetAddButton_Click);
            // 
            // DrinkSetName
            // 
            this.DrinkSetName.Location = new System.Drawing.Point(7, 21);
            this.DrinkSetName.Name = "DrinkSetName";
            this.DrinkSetName.Size = new System.Drawing.Size(214, 21);
            this.DrinkSetName.TabIndex = 0;
            // 
            // tabControlLeft
            // 
            this.tabControlLeft.Controls.Add(this.tabPageGame);
            this.tabControlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlLeft.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlLeft.ImageList = this.ic6x16;
            this.tabControlLeft.Location = new System.Drawing.Point(0, 24);
            this.tabControlLeft.Multiline = true;
            this.tabControlLeft.Name = "tabControlLeft";
            this.tabControlLeft.SelectedIndex = 0;
            this.tabControlLeft.Size = new System.Drawing.Size(819, 603);
            this.tabControlLeft.TabIndex = 10;
            this.tabControlLeft.SelectedIndexChanged += new System.EventHandler(this.tabControlLeft_SelectedIndexChanged);
            this.tabControlLeft.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControlLeft_MouseDoubleClick);
            // 
            // tabPageGame
            // 
            this.tabPageGame.Controls.Add(this.panelGame);
            this.tabPageGame.Controls.Add(this.toolbarGame);
            this.tabPageGame.Location = new System.Drawing.Point(4, 23);
            this.tabPageGame.Name = "tabPageGame";
            this.tabPageGame.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGame.Size = new System.Drawing.Size(811, 576);
            this.tabPageGame.TabIndex = 0;
            this.tabPageGame.Text = "Ник";
            this.tabPageGame.UseVisualStyleBackColor = true;
            // 
            // panelGame
            // 
            this.panelGame.Controls.Add(this.browserGame);
            this.panelGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGame.Location = new System.Drawing.Point(3, 31);
            this.panelGame.Name = "panelGame";
            this.panelGame.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panelGame.Size = new System.Drawing.Size(805, 542);
            this.panelGame.TabIndex = 1;
            // 
            // browserGame
            // 
            this.browserGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserGame.Location = new System.Drawing.Point(0, 3);
            this.browserGame.MinimumSize = new System.Drawing.Size(20, 20);
            this.browserGame.Name = "browserGame";
            this.browserGame.Size = new System.Drawing.Size(805, 539);
            this.browserGame.TabIndex = 0;
            this.browserGame.WebBrowserShortcutsEnabled = false;
            // 
            // toolbarGame
            // 
            this.toolbarGame.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolbarGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonAutoboi,
            this.toolStripSeparator2,
            this.buttonFury,
            this.buttonAutoRefresh,
            this.buttonWaitOpen,
            this.buttonPerenap,
            this.toolStripSeparator4,
            this.buttonWalkers,
            this.buttonOpenNevid,
            this.buttonSelfNevid,
            this.buttonAutoAttack,
            this.toolStripSeparator23,
            this.buttonNavigator,
            this.teleportExtendedBar,
            this.toolStripSeparator15,
            this.buttonSilence,
            this.buttonAutoAnswer,
            this.buttonAutoAdv,
            this.toolStripSeparator6,
            this.buttonGameLogOn,
            this.toolStripSeparator5,
            this.buttonGameScreen});
            this.toolbarGame.Location = new System.Drawing.Point(3, 3);
            this.toolbarGame.Name = "toolbarGame";
            this.toolbarGame.Size = new System.Drawing.Size(805, 28);
            this.toolbarGame.TabIndex = 0;
            this.toolbarGame.Text = "toolStrip1";
            // 
            // buttonAutoboi
            // 
            this.buttonAutoboi.AutoSize = false;
            this.buttonAutoboi.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonAutoboi.Image = ((System.Drawing.Image)(resources.GetObject("buttonAutoboi.Image")));
            this.buttonAutoboi.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAutoboi.Name = "buttonAutoboi";
            this.buttonAutoboi.Size = new System.Drawing.Size(150, 22);
            this.buttonAutoboi.Text = "Останов лечения (0:00:00)";
            this.buttonAutoboi.Click += new System.EventHandler(this.buttonAutoboi_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // buttonFury
            // 
            this.buttonFury.CheckOnClick = true;
            this.buttonFury.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonFury.Image = global::ABClient.Properties.Resources._16x16_fury;
            this.buttonFury.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonFury.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonFury.Name = "buttonFury";
            this.buttonFury.Size = new System.Drawing.Size(23, 25);
            this.buttonFury.ToolTipText = "Снежок или ярость (первый удар на осаде)";
            this.buttonFury.Click += new System.EventHandler(this.buttonFury_Click);
            // 
            // buttonAutoRefresh
            // 
            this.buttonAutoRefresh.CheckOnClick = true;
            this.buttonAutoRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAutoRefresh.Image = global::ABClient.Properties.Resources._16x16_marinad1;
            this.buttonAutoRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAutoRefresh.Name = "buttonAutoRefresh";
            this.buttonAutoRefresh.Size = new System.Drawing.Size(23, 25);
            this.buttonAutoRefresh.ToolTipText = "Автообновление боя при ожидании хода противника";
            this.buttonAutoRefresh.Click += new System.EventHandler(this.ButtonAutoRefreshClick);
            // 
            // buttonWaitOpen
            // 
            this.buttonWaitOpen.CheckOnClick = true;
            this.buttonWaitOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonWaitOpen.Image = global::ABClient.Properties.Resources._16x16_waitopen;
            this.buttonWaitOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonWaitOpen.Name = "buttonWaitOpen";
            this.buttonWaitOpen.Size = new System.Drawing.Size(23, 25);
            this.buttonWaitOpen.Text = "toolStripButton2";
            this.buttonWaitOpen.ToolTipText = "Ожидание окончания открытого боя";
            this.buttonWaitOpen.Click += new System.EventHandler(this.ButtonWaitOpenClick);
            // 
            // buttonPerenap
            // 
            this.buttonPerenap.CheckOnClick = true;
            this.buttonPerenap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPerenap.Image = global::ABClient.Properties.Resources._16x16_perenap;
            this.buttonPerenap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPerenap.Name = "buttonPerenap";
            this.buttonPerenap.Size = new System.Drawing.Size(23, 25);
            this.buttonPerenap.ToolTipText = "Перенападение/автоприманка";
            this.buttonPerenap.Click += new System.EventHandler(this.ButtonPerenapClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // buttonWalkers
            // 
            this.buttonWalkers.CheckOnClick = true;
            this.buttonWalkers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonWalkers.Image = global::ABClient.Properties.Resources._16x16_walkers;
            this.buttonWalkers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonWalkers.Name = "buttonWalkers";
            this.buttonWalkers.Size = new System.Drawing.Size(23, 25);
            this.buttonWalkers.ToolTipText = "Слежение за составом локации";
            this.buttonWalkers.Click += new System.EventHandler(this.ButtonWalkersClick);
            // 
            // buttonOpenNevid
            // 
            this.buttonOpenNevid.CheckOnClick = true;
            this.buttonOpenNevid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonOpenNevid.Image = global::ABClient.Properties.Resources._16x16_opennevid;
            this.buttonOpenNevid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonOpenNevid.Name = "buttonOpenNevid";
            this.buttonOpenNevid.Size = new System.Drawing.Size(23, 25);
            this.buttonOpenNevid.Text = "toolStripButton2";
            this.buttonOpenNevid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOpenNevid.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonOpenNevid.ToolTipText = "Автоматическое развеивание невидимки";
            this.buttonOpenNevid.Click += new System.EventHandler(this.ButtonOpenNevidClick);
            // 
            // buttonSelfNevid
            // 
            this.buttonSelfNevid.CheckOnClick = true;
            this.buttonSelfNevid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSelfNevid.Image = global::ABClient.Properties.Resources._16x16_shading;
            this.buttonSelfNevid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSelfNevid.Name = "buttonSelfNevid";
            this.buttonSelfNevid.Size = new System.Drawing.Size(23, 25);
            this.buttonSelfNevid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSelfNevid.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.buttonSelfNevid.ToolTipText = "Автоматическое наложение невида на себя";
            this.buttonSelfNevid.Click += new System.EventHandler(this.ButtonSelfNevidClick);
            // 
            // buttonAutoAttack
            // 
            this.buttonAutoAttack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAutoAttack.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAutoAttack0,
            this.miAutoAttack1,
            this.miAutoAttack2,
            this.miAutoAttack3,
            this.miAutoAttack4,
            this.miAutoAttack5});
            this.buttonAutoAttack.Image = global::ABClient.Properties.Resources.i_svi_000;
            this.buttonAutoAttack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonAutoAttack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAutoAttack.Name = "buttonAutoAttack";
            this.buttonAutoAttack.Size = new System.Drawing.Size(58, 25);
            this.buttonAutoAttack.Text = "Автонападение отключено";
            // 
            // miAutoAttack0
            // 
            this.miAutoAttack0.Image = global::ABClient.Properties.Resources.i_svi_000;
            this.miAutoAttack0.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miAutoAttack0.Name = "miAutoAttack0";
            this.miAutoAttack0.Size = new System.Drawing.Size(270, 28);
            this.miAutoAttack0.Text = "Автонападение отключено";
            this.miAutoAttack0.ToolTipText = "Автонападение отключено";
            this.miAutoAttack0.Click += new System.EventHandler(this.MiAutoAttackClick);
            // 
            // miAutoAttack1
            // 
            this.miAutoAttack1.Image = global::ABClient.Properties.Resources.i_w28_26;
            this.miAutoAttack1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miAutoAttack1.Name = "miAutoAttack1";
            this.miAutoAttack1.Size = new System.Drawing.Size(270, 28);
            this.miAutoAttack1.Tag = "1";
            this.miAutoAttack1.Text = "Использовать боевые";
            this.miAutoAttack1.ToolTipText = "Использовать боевые";
            this.miAutoAttack1.Click += new System.EventHandler(this.MiAutoAttackClick);
            // 
            // miAutoAttack2
            // 
            this.miAutoAttack2.Image = global::ABClient.Properties.Resources.i_w28_26x1;
            this.miAutoAttack2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miAutoAttack2.Name = "miAutoAttack2";
            this.miAutoAttack2.Size = new System.Drawing.Size(270, 28);
            this.miAutoAttack2.Tag = "2";
            this.miAutoAttack2.Text = "Использовать закрытые боевые";
            this.miAutoAttack2.ToolTipText = "Использовать закрытые боевые";
            this.miAutoAttack2.Click += new System.EventHandler(this.MiAutoAttackClick);
            // 
            // miAutoAttack3
            // 
            this.miAutoAttack3.Image = global::ABClient.Properties.Resources.i_w28_24;
            this.miAutoAttack3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miAutoAttack3.Name = "miAutoAttack3";
            this.miAutoAttack3.Size = new System.Drawing.Size(270, 28);
            this.miAutoAttack3.Tag = "3";
            this.miAutoAttack3.Text = "Использовать кулачки";
            this.miAutoAttack3.ToolTipText = "Использовать кулачки";
            this.miAutoAttack3.Click += new System.EventHandler(this.MiAutoAttackClick);
            // 
            // miAutoAttack4
            // 
            this.miAutoAttack4.Image = global::ABClient.Properties.Resources.i_w28_25;
            this.miAutoAttack4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miAutoAttack4.Name = "miAutoAttack4";
            this.miAutoAttack4.Size = new System.Drawing.Size(270, 28);
            this.miAutoAttack4.Tag = "4";
            this.miAutoAttack4.Text = "Использовать закрытые кулачки";
            this.miAutoAttack4.ToolTipText = "Использовать закрытые кулачки";
            this.miAutoAttack4.Click += new System.EventHandler(this.MiAutoAttackClick);
            // 
            // miAutoAttack5
            // 
            this.miAutoAttack5.Image = global::ABClient.Properties.Resources.i_w28_86;
            this.miAutoAttack5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.miAutoAttack5.Name = "miAutoAttack5";
            this.miAutoAttack5.Size = new System.Drawing.Size(270, 28);
            this.miAutoAttack5.Tag = "5";
            this.miAutoAttack5.Text = "Использовать портал";
            this.miAutoAttack5.ToolTipText = "Использовать портал";
            this.miAutoAttack5.Click += new System.EventHandler(this.MiAutoAttackClick);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(6, 28);
            // 
            // buttonNavigator
            // 
            this.buttonNavigator.Image = global::ABClient.Properties.Resources._16x16_navigator;
            this.buttonNavigator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonNavigator.Name = "buttonNavigator";
            this.buttonNavigator.Size = new System.Drawing.Size(81, 25);
            this.buttonNavigator.Text = "Навигатор";
            this.buttonNavigator.ToolTipText = "Навигатор по природе";
            this.buttonNavigator.Click += new System.EventHandler(this.buttonNavigator_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 28);
            // 
            // buttonSilence
            // 
            this.buttonSilence.CheckOnClick = true;
            this.buttonSilence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonSilence.Image = global::ABClient.Properties.Resources._16x16_silence;
            this.buttonSilence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSilence.Name = "buttonSilence";
            this.buttonSilence.Size = new System.Drawing.Size(23, 25);
            this.buttonSilence.ToolTipText = "Запрет звуков в клиенте";
            this.buttonSilence.Click += new System.EventHandler(this.buttonSilence_Click);
            // 
            // buttonAutoAnswer
            // 
            this.buttonAutoAnswer.CheckOnClick = true;
            this.buttonAutoAnswer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAutoAnswer.Image = global::ABClient.Properties.Resources._16x16_autoanswer;
            this.buttonAutoAnswer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAutoAnswer.Name = "buttonAutoAnswer";
            this.buttonAutoAnswer.Size = new System.Drawing.Size(23, 25);
            this.buttonAutoAnswer.ToolTipText = "Автоответчик";
            this.buttonAutoAnswer.Click += new System.EventHandler(this.ButtonAutoAnswer_Click);
            // 
            // buttonAutoAdv
            // 
            this.buttonAutoAdv.CheckOnClick = true;
            this.buttonAutoAdv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonAutoAdv.Image = global::ABClient.Properties.Resources._16x16_private;
            this.buttonAutoAdv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonAutoAdv.Name = "buttonAutoAdv";
            this.buttonAutoAdv.Size = new System.Drawing.Size(23, 25);
            this.buttonAutoAdv.ToolTipText = "Автореклама в чат";
            this.buttonAutoAdv.Click += new System.EventHandler(this.buttonAutoAdv_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 28);
            // 
            // buttonGameLogOn
            // 
            this.buttonGameLogOn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonGameLogOn.Image = global::ABClient.Properties.Resources._16x16_refresh;
            this.buttonGameLogOn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonGameLogOn.Name = "buttonGameLogOn";
            this.buttonGameLogOn.Size = new System.Drawing.Size(23, 25);
            this.buttonGameLogOn.Text = "Перезаход";
            this.buttonGameLogOn.ToolTipText = "Перезаход в игру";
            this.buttonGameLogOn.Click += new System.EventHandler(this.buttonGameLogOn_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // buttonGameScreen
            // 
            this.buttonGameScreen.Image = global::ABClient.Properties.Resources._16x16_camera;
            this.buttonGameScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonGameScreen.Name = "buttonGameScreen";
            this.buttonGameScreen.Size = new System.Drawing.Size(23, 25);
            this.buttonGameScreen.ToolTipText = "Снимок игрового экрана";
            this.buttonGameScreen.Click += new System.EventHandler(this.buttonForumScreen_Click);
            // 
            // ic6x16
            // 
            this.ic6x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ic6x16.ImageStream")));
            this.ic6x16.TransparentColor = System.Drawing.Color.Transparent;
            this.ic6x16.Images.SetKeyName(0, "16x6-info.gif");
            this.ic6x16.Images.SetKeyName(1, "16x6-notepad.png");
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "123";
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemGame,
            this.menuitemTabs,
            this.menuitemTools,
            this.menuitemCommands});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1077, 24);
            this.menuStrip.TabIndex = 11;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuitemGame
            // 
            this.menuitemGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemSettingsAb,
            this.menuitemSettingsGeneral,
            this.toolStripSeparator17,
            this.menuitemGameLogOn,
            this.menuitemGameExit});
            this.menuitemGame.Name = "menuitemGame";
            this.menuitemGame.Size = new System.Drawing.Size(43, 20);
            this.menuitemGame.Text = "Игра";
            // 
            // menuitemSettingsAb
            // 
            this.menuitemSettingsAb.Name = "menuitemSettingsAb";
            this.menuitemSettingsAb.Size = new System.Drawing.Size(185, 22);
            this.menuitemSettingsAb.Text = "Настройки автобоя...";
            this.menuitemSettingsAb.Click += new System.EventHandler(this.menuitemSettingsAb_Click);
            // 
            // menuitemSettingsGeneral
            // 
            this.menuitemSettingsGeneral.Image = ((System.Drawing.Image)(resources.GetObject("menuitemSettingsGeneral.Image")));
            this.menuitemSettingsGeneral.Name = "menuitemSettingsGeneral";
            this.menuitemSettingsGeneral.Size = new System.Drawing.Size(185, 22);
            this.menuitemSettingsGeneral.Text = "Настройки игры...";
            this.menuitemSettingsGeneral.Click += new System.EventHandler(this.OnMenuitemSettingsGeneralClick);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(182, 6);
            // 
            // menuitemGameLogOn
            // 
            this.menuitemGameLogOn.Image = global::ABClient.Properties.Resources._16x16_refresh;
            this.menuitemGameLogOn.Name = "menuitemGameLogOn";
            this.menuitemGameLogOn.Size = new System.Drawing.Size(185, 22);
            this.menuitemGameLogOn.Text = "Перезаход в игру";
            this.menuitemGameLogOn.Click += new System.EventHandler(this.menuitemGameLogOn_Click);
            // 
            // menuitemGameExit
            // 
            this.menuitemGameExit.Image = ((System.Drawing.Image)(resources.GetObject("menuitemGameExit.Image")));
            this.menuitemGameExit.Name = "menuitemGameExit";
            this.menuitemGameExit.Size = new System.Drawing.Size(185, 22);
            this.menuitemGameExit.Text = "Выход из игры";
            this.menuitemGameExit.Click += new System.EventHandler(this.menuitemGameExit_Click);
            // 
            // menuitemTabs
            // 
            this.menuitemTabs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemOpenTab,
            this.menuitemOpenForum,
            this.menuitemOpenTodayChat,
            this.menuitemOpenNotepad,
            this.toolStripSeparator16});
            this.menuitemTabs.Name = "menuitemTabs";
            this.menuitemTabs.Size = new System.Drawing.Size(68, 20);
            this.menuitemTabs.Text = "Закладки";
            // 
            // menuitemOpenTab
            // 
            this.menuitemOpenTab.Name = "menuitemOpenTab";
            this.menuitemOpenTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.menuitemOpenTab.Size = new System.Drawing.Size(211, 22);
            this.menuitemOpenTab.Text = "Открыть новую...";
            this.menuitemOpenTab.Click += new System.EventHandler(this.OnMenuitemOpenTabClick);
            // 
            // menuitemOpenForum
            // 
            this.menuitemOpenForum.Name = "menuitemOpenForum";
            this.menuitemOpenForum.Size = new System.Drawing.Size(211, 22);
            this.menuitemOpenForum.Text = "Открыть форум";
            this.menuitemOpenForum.Click += new System.EventHandler(this.MenuitemOpenForumClick);
            // 
            // menuitemOpenTodayChat
            // 
            this.menuitemOpenTodayChat.Name = "menuitemOpenTodayChat";
            this.menuitemOpenTodayChat.Size = new System.Drawing.Size(211, 22);
            this.menuitemOpenTodayChat.Text = "Открыть сегодняшний чат";
            this.menuitemOpenTodayChat.Click += new System.EventHandler(this.MenuitemOpenTodayChatClick);
            // 
            // menuitemOpenNotepad
            // 
            this.menuitemOpenNotepad.Name = "menuitemOpenNotepad";
            this.menuitemOpenNotepad.Size = new System.Drawing.Size(211, 22);
            this.menuitemOpenNotepad.Text = "Открыть блокнот";
            this.menuitemOpenNotepad.Click += new System.EventHandler(this.MenuitemOpenNotepadClick);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(208, 6);
            // 
            // menuitemTools
            // 
            this.menuitemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemShowCookies,
            this.toolStripSeparator25,
            this.menuitemDoSearchBox,
            this.menuitemDoResetVisitedCells,
            this.toolStripSeparator7,
            this.menuitemTurotor_Top,
            this.toolStripSeparator3,
            this.menuitemCheckCell,
            this.сканированиеКартыToolStripMenuItem,
            this.menuitemReloadTopFrame,
            this.menuitemCacheRefresh});
            this.menuitemTools.Name = "menuitemTools";
            this.menuitemTools.Size = new System.Drawing.Size(87, 20);
            this.menuitemTools.Text = "Инструменты";
            // 
            // menuitemShowCookies
            // 
            this.menuitemShowCookies.Name = "menuitemShowCookies";
            this.menuitemShowCookies.Size = new System.Drawing.Size(223, 22);
            this.menuitemShowCookies.Text = "Куки игры...";
            this.menuitemShowCookies.Click += new System.EventHandler(this.MenuitemShowCookiesClick);
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(220, 6);
            // 
            // menuitemDoSearchBox
            // 
            this.menuitemDoSearchBox.CheckOnClick = true;
            this.menuitemDoSearchBox.Name = "menuitemDoSearchBox";
            this.menuitemDoSearchBox.Size = new System.Drawing.Size(223, 22);
            this.menuitemDoSearchBox.Text = "Ходим, ищем клад";
            this.menuitemDoSearchBox.Click += new System.EventHandler(this.MenuitemDoSearchBoxClick);
            // 
            // menuitemDoResetVisitedCells
            // 
            this.menuitemDoResetVisitedCells.Name = "menuitemDoResetVisitedCells";
            this.menuitemDoResetVisitedCells.Size = new System.Drawing.Size(223, 22);
            this.menuitemDoResetVisitedCells.Text = "Сброс посещенных локаций";
            this.menuitemDoResetVisitedCells.Click += new System.EventHandler(this.MenuitemDoResetVisitedCellsClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(220, 6);
            // 
            // menuitemTurotor_Top
            // 
            this.menuitemTurotor_Top.CheckOnClick = true;
            this.menuitemTurotor_Top.Name = "menuitemTurotor_Top";
            this.menuitemTurotor_Top.Size = new System.Drawing.Size(223, 22);
            this.menuitemTurotor_Top.Text = "Остров Туротор/Гиблая Топь";
            this.menuitemTurotor_Top.Click += new System.EventHandler(this.menuitemTurotor_Top_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(220, 6);
            // 
            // menuitemCheckCell
            // 
            this.menuitemCheckCell.Name = "menuitemCheckCell";
            this.menuitemCheckCell.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
            this.menuitemCheckCell.Size = new System.Drawing.Size(223, 22);
            this.menuitemCheckCell.Text = "Проверка клетки";
            // 
            // сканированиеКартыToolStripMenuItem
            // 
            this.сканированиеКартыToolStripMenuItem.Name = "сканированиеКартыToolStripMenuItem";
            this.сканированиеКартыToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.сканированиеКартыToolStripMenuItem.Text = "Сканирование карты";
            this.сканированиеКартыToolStripMenuItem.Click += new System.EventHandler(this.ScanMapToolStripMenuItem_Click);
            // 
            // menuitemReloadTopFrame
            // 
            this.menuitemReloadTopFrame.Name = "menuitemReloadTopFrame";
            this.menuitemReloadTopFrame.Size = new System.Drawing.Size(223, 22);
            this.menuitemReloadTopFrame.Text = "Обновление фрейма";
            this.menuitemReloadTopFrame.Click += new System.EventHandler(this.menuitemReloadTopFrame_Click);
            // 
            // menuitemCacheRefresh
            // 
            this.menuitemCacheRefresh.CheckOnClick = true;
            this.menuitemCacheRefresh.Name = "menuitemCacheRefresh";
            this.menuitemCacheRefresh.Size = new System.Drawing.Size(223, 22);
            this.menuitemCacheRefresh.Text = "Режим обновления кеша";
            this.menuitemCacheRefresh.ToolTipText = "Имеет смысл включить ненадолго при изменениях на сервере игры, чтобы клиент обнов" +
    "ил закешированные файлы.";
            // 
            // menuitemCommands
            // 
            this.menuitemCommands.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemClanPrivate,
            this.menuitemRekPrivate,
            this.menuitemMinimize,
            this.toolStripSeparator12,
            this.miFastEnabled,
            this.miFastTeleport,
            this.miFastDarkTeleport,
            this.miFastSviRass,
            this.miFastSviSelfRass,
            this.miFastF3,
            this.miFastF4,
            this.miFastElxVosst,
            this.miFastSvitFog,
            this.miFastDarkFog,
            this.miFastF9,
            this.miFastF10,
            this.miFastF12,
            this.miFastCtrlF12,
            this.toolStripSeparator11,
            this.miWearAfter,
            this.toolStripSeparator20,
            this.miQuick,
            this.miQuickCancel});
            this.menuitemCommands.Name = "menuitemCommands";
            this.menuitemCommands.Size = new System.Drawing.Size(65, 20);
            this.menuitemCommands.Text = "Команды";
            // 
            // menuitemClanPrivate
            // 
            this.menuitemClanPrivate.Name = "menuitemClanPrivate";
            this.menuitemClanPrivate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuitemClanPrivate.Size = new System.Drawing.Size(265, 22);
            this.menuitemClanPrivate.Text = "Клан-приват";
            this.menuitemClanPrivate.Click += new System.EventHandler(this.OnMenuitemClanPrivateClick);
            // 
            // menuitemRekPrivate
            // 
            this.menuitemRekPrivate.Name = "menuitemRekPrivate";
            this.menuitemRekPrivate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.menuitemRekPrivate.Size = new System.Drawing.Size(265, 22);
            this.menuitemRekPrivate.Text = "Рекрут-приват";
            this.menuitemRekPrivate.Click += new System.EventHandler(this.OnMenuitemRekPrivateClick);
            // 
            // menuitemMinimize
            // 
            this.menuitemMinimize.Name = "menuitemMinimize";
            this.menuitemMinimize.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.menuitemMinimize.Size = new System.Drawing.Size(265, 22);
            this.menuitemMinimize.Text = "Свернуть";
            this.menuitemMinimize.Click += new System.EventHandler(this.OnMenuitemMinimizeClick);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(262, 6);
            // 
            // miFastEnabled
            // 
            this.miFastEnabled.Checked = true;
            this.miFastEnabled.CheckOnClick = true;
            this.miFastEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miFastEnabled.Name = "miFastEnabled";
            this.miFastEnabled.Size = new System.Drawing.Size(265, 22);
            this.miFastEnabled.Text = "Быстрые команды разрешены";
            this.miFastEnabled.CheckStateChanged += new System.EventHandler(this.MiFastEnabledCheckStateChanged);
            // 
            // miFastTeleport
            // 
            this.miFastTeleport.Name = "miFastTeleport";
            this.miFastTeleport.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.miFastTeleport.Size = new System.Drawing.Size(265, 22);
            this.miFastTeleport.Text = "Телепорт";
            this.miFastTeleport.Click += new System.EventHandler(this.MiFastTeleportClick);
            // 
            // miFastDarkTeleport
            // 
            this.miFastDarkTeleport.Name = "miFastDarkTeleport";
            this.miFastDarkTeleport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.miFastDarkTeleport.Size = new System.Drawing.Size(265, 22);
            this.miFastDarkTeleport.Text = "Сумеречный телепорт";
            this.miFastDarkTeleport.Click += new System.EventHandler(this.MiFastDarkTeleportClick);
            // 
            // miFastSviRass
            // 
            this.miFastSviRass.Name = "miFastSviRass";
            this.miFastSviRass.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.miFastSviRass.Size = new System.Drawing.Size(265, 22);
            this.miFastSviRass.Text = "Свиток обнаружения";
            this.miFastSviRass.Click += new System.EventHandler(this.MiFastSviRassClick);
            // 
            // miFastSviSelfRass
            // 
            this.miFastSviSelfRass.Name = "miFastSviSelfRass";
            this.miFastSviSelfRass.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.miFastSviSelfRass.Size = new System.Drawing.Size(265, 22);
            this.miFastSviSelfRass.Text = "Свиток рассеивания";
            this.miFastSviSelfRass.Click += new System.EventHandler(this.MiFastSviSelfRassClick);
            // 
            // miFastF3
            // 
            this.miFastF3.Name = "miFastF3";
            this.miFastF3.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.miFastF3.Size = new System.Drawing.Size(265, 22);
            this.miFastF3.Text = "Эликсир мгновенного исцеления";
            this.miFastF3.Click += new System.EventHandler(this.MiFastMomentCure);
            // 
            // miFastF4
            // 
            this.miFastF4.Name = "miFastF4";
            this.miFastF4.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.miFastF4.Size = new System.Drawing.Size(265, 22);
            this.miFastF4.Text = "Приманка для ботов";
            this.miFastF4.Click += new System.EventHandler(this.MiFastPrimankaClick);
            // 
            // miFastElxVosst
            // 
            this.miFastElxVosst.Name = "miFastElxVosst";
            this.miFastElxVosst.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.miFastElxVosst.Size = new System.Drawing.Size(265, 22);
            this.miFastElxVosst.Text = "Эликсир восстановления";
            this.miFastElxVosst.Click += new System.EventHandler(this.MiFastMomentRestoreClick);
            // 
            // miFastSvitFog
            // 
            this.miFastSvitFog.Name = "miFastSvitFog";
            this.miFastSvitFog.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.miFastSvitFog.Size = new System.Drawing.Size(265, 22);
            this.miFastSvitFog.Text = "Свиток тумана";
            this.miFastSvitFog.Click += new System.EventHandler(this.MiFastFogClick);
            // 
            // miFastDarkFog
            // 
            this.miFastDarkFog.Name = "miFastDarkFog";
            this.miFastDarkFog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F8)));
            this.miFastDarkFog.Size = new System.Drawing.Size(265, 22);
            this.miFastDarkFog.Text = "Сумеречный туман";
            this.miFastDarkFog.Click += new System.EventHandler(this.MiFastDarkFogClick);
            // 
            // miFastF9
            // 
            this.miFastF9.Name = "miFastF9";
            this.miFastF9.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.miFastF9.Size = new System.Drawing.Size(265, 22);
            this.miFastF9.Text = "Зелье блаженства";
            this.miFastF9.Click += new System.EventHandler(this.MiFastBlazPotClick);
            // 
            // miFastF10
            // 
            this.miFastF10.Name = "miFastF10";
            this.miFastF10.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.miFastF10.Size = new System.Drawing.Size(265, 22);
            this.miFastF10.Text = "Эликсир блаженства";
            this.miFastF10.Click += new System.EventHandler(this.MiFastBlazElexirClick);
            // 
            // miFastF12
            // 
            this.miFastF12.Name = "miFastF12";
            this.miFastF12.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.miFastF12.Size = new System.Drawing.Size(265, 22);
            this.miFastF12.Text = "Зелье Невидимости";
            this.miFastF12.Click += new System.EventHandler(this.MiFastNevidPotClick);
            // 
            // miFastCtrlF12
            // 
            this.miFastCtrlF12.Name = "miFastCtrlF12";
            this.miFastCtrlF12.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F12)));
            this.miFastCtrlF12.Size = new System.Drawing.Size(265, 22);
            this.miFastCtrlF12.Text = "Телепорт (Остров Туротор)";
            this.miFastCtrlF12.Click += new System.EventHandler(this.MiFastIslandPotClick);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(262, 6);
            // 
            // miWearAfter
            // 
            this.miWearAfter.Name = "miWearAfter";
            this.miWearAfter.Size = new System.Drawing.Size(265, 22);
            this.miWearAfter.Text = "Одеть комплект после боя...";
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(262, 6);
            // 
            // miQuick
            // 
            this.miQuick.Name = "miQuick";
            this.miQuick.Size = new System.Drawing.Size(265, 22);
            this.miQuick.Text = "Быстрые действия...";
            this.miQuick.Click += new System.EventHandler(this.MiQuickClick);
            // 
            // miQuickCancel
            // 
            this.miQuickCancel.Name = "miQuickCancel";
            this.miQuickCancel.Size = new System.Drawing.Size(265, 22);
            this.miQuickCancel.Text = "Отмена";
            this.miQuickCancel.Click += new System.EventHandler(this.MiQuickCancelClick);
            // 
            // imagelistDownload
            // 
            this.imagelistDownload.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imagelistDownload.ImageStream")));
            this.imagelistDownload.TransparentColor = System.Drawing.Color.Transparent;
            this.imagelistDownload.Images.SetKeyName(0, "16x16-icoff.gif");
            this.imagelistDownload.Images.SetKeyName(1, "16x16-icon.gif");
            // 
            // saveFileScreen
            // 
            this.saveFileScreen.DefaultExt = "jpg";
            this.saveFileScreen.Filter = "Снимки экрана|*.jpg|Все файлы|*.*";
            this.saveFileScreen.RestoreDirectory = true;
            this.saveFileScreen.Title = "Сохранение снимка экрана";
            // 
            // timerCrap
            // 
            this.timerCrap.Enabled = true;
            this.timerCrap.Interval = 10000;
            this.timerCrap.Tick += new System.EventHandler(this.OnTimerCrapTick);
            // 
            // trayImages
            // 
            this.trayImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("trayImages.ImageStream")));
            this.trayImages.TransparentColor = System.Drawing.Color.Transparent;
            this.trayImages.Images.SetKeyName(0, "icontray1.gif");
            this.trayImages.Images.SetKeyName(1, "icontray2.gif");
            // 
            // trayIcon
            // 
            this.trayIcon.ContextMenuStrip = this.cmTray;
            this.trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnTrayIconMouseDoubleClick);
            // 
            // cmTray
            // 
            this.cmTray.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemRestoreWindow,
            this.menuitemTrayQuit});
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(173, 48);
            // 
            // menuitemRestoreWindow
            // 
            this.menuitemRestoreWindow.Name = "menuitemRestoreWindow";
            this.menuitemRestoreWindow.Size = new System.Drawing.Size(172, 22);
            this.menuitemRestoreWindow.Text = "Развернуть клиент";
            this.menuitemRestoreWindow.Click += new System.EventHandler(this.OnMenuitemRestoreWindowClick);
            // 
            // menuitemTrayQuit
            // 
            this.menuitemTrayQuit.Image = global::ABClient.Properties.Resources._16x16_close;
            this.menuitemTrayQuit.Name = "menuitemTrayQuit";
            this.menuitemTrayQuit.Size = new System.Drawing.Size(172, 22);
            this.menuitemTrayQuit.Text = "Завершить работу";
            this.menuitemTrayQuit.Click += new System.EventHandler(this.OnMenuitemTrayQuitClick);
            // 
            // timerTray
            // 
            this.timerTray.Interval = 300;
            this.timerTray.Tick += new System.EventHandler(this.OnTimerTrayTick);
            // 
            // CmGroup
            // 
            this.CmGroup.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CmGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSetGroupNeutral,
            this.miSetGroupFoe,
            this.miSetGroupFriend,
            this.toolStripSeparator21,
            this.miSetGroupToolId0,
            this.miSetGroupToolId1,
            this.miSetGroupToolId2,
            this.miSetGroupToolId3,
            this.miSetGroupToolId4,
            this.miSetGroupToolId5,
            this.toolStripSeparator24,
            this.miRemoveGroup});
            this.CmGroup.Name = "CmGroup";
            this.CmGroup.Size = new System.Drawing.Size(304, 236);
            // 
            // miSetGroupNeutral
            // 
            this.miSetGroupNeutral.Name = "miSetGroupNeutral";
            this.miSetGroupNeutral.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupNeutral.Text = "Сделать группу нейтральной";
            this.miSetGroupNeutral.Click += new System.EventHandler(this.MiSetGroupNeutralClick);
            // 
            // miSetGroupFoe
            // 
            this.miSetGroupFoe.Name = "miSetGroupFoe";
            this.miSetGroupFoe.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupFoe.Text = "Сделать группу вражеской";
            this.miSetGroupFoe.Click += new System.EventHandler(this.MiSetGroupFoeClick);
            // 
            // miSetGroupFriend
            // 
            this.miSetGroupFriend.Name = "miSetGroupFriend";
            this.miSetGroupFriend.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupFriend.Text = "Сделать группу дружественной";
            this.miSetGroupFriend.Click += new System.EventHandler(this.MiSetGroupFriendClick);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(300, 6);
            // 
            // miSetGroupToolId0
            // 
            this.miSetGroupToolId0.Name = "miSetGroupToolId0";
            this.miSetGroupToolId0.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupToolId0.Text = "Применять ко всем нападалку по умолчанию";
            this.miSetGroupToolId0.Click += new System.EventHandler(this.MiSetGroupToolId0Click);
            // 
            // miSetGroupToolId1
            // 
            this.miSetGroupToolId1.Name = "miSetGroupToolId1";
            this.miSetGroupToolId1.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupToolId1.Text = "Применять ко всем боевое";
            this.miSetGroupToolId1.Click += new System.EventHandler(this.MiSetGroupToolId1Click);
            // 
            // miSetGroupToolId2
            // 
            this.miSetGroupToolId2.Name = "miSetGroupToolId2";
            this.miSetGroupToolId2.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupToolId2.Text = "Применять ко всем закрытое боевое";
            this.miSetGroupToolId2.Click += new System.EventHandler(this.MiSetGroupToolId2Click);
            // 
            // miSetGroupToolId3
            // 
            this.miSetGroupToolId3.Name = "miSetGroupToolId3";
            this.miSetGroupToolId3.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupToolId3.Text = "Применять ко всем кулачку";
            this.miSetGroupToolId3.Click += new System.EventHandler(this.MiSetGroupToolId3Click);
            // 
            // miSetGroupToolId4
            // 
            this.miSetGroupToolId4.Name = "miSetGroupToolId4";
            this.miSetGroupToolId4.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupToolId4.Text = "Применять ко всем закрытую кулачку";
            this.miSetGroupToolId4.Click += new System.EventHandler(this.MiSetGroupToolId4Click);
            // 
            // miSetGroupToolId5
            // 
            this.miSetGroupToolId5.Name = "miSetGroupToolId5";
            this.miSetGroupToolId5.Size = new System.Drawing.Size(303, 22);
            this.miSetGroupToolId5.Text = "Применять ко всем портал";
            this.miSetGroupToolId5.Click += new System.EventHandler(this.MiSetGroupToolId5Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(300, 6);
            // 
            // miRemoveGroup
            // 
            this.miRemoveGroup.Name = "miRemoveGroup";
            this.miRemoveGroup.Size = new System.Drawing.Size(303, 22);
            this.miRemoveGroup.Text = "Удалить группу";
            this.miRemoveGroup.Click += new System.EventHandler(this.OnMiRemoveGroupClick);
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.OnTimerClockTick);
            // 
            // timerCheckInfo
            // 
            this.timerCheckInfo.Enabled = true;
            this.timerCheckInfo.Interval = 60000;
            this.timerCheckInfo.Tick += new System.EventHandler(this.TimerCheckInfoTick);
            // 
            // timer30
            // 
            this.timer30.Enabled = true;
            this.timer30.Interval = 34000;
            this.timer30.Tick += new System.EventHandler(this.timer30_Tick);
            // 
            // statuslabelLocation
            // 
            this.statuslabelLocation.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statuslabelLocation.Image = ((System.Drawing.Image)(resources.GetObject("statuslabelLocation.Image")));
            this.statuslabelLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.statuslabelLocation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.statuslabelLocation.Name = "statuslabelLocation";
            this.statuslabelLocation.Size = new System.Drawing.Size(55, 20);
            this.statuslabelLocation.Text = "0-000";
            this.statuslabelLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dropdownPv
            // 
            this.dropdownPv.Enabled = false;
            this.dropdownPv.Image = ((System.Drawing.Image)(resources.GetObject("dropdownPv.Image")));
            this.dropdownPv.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dropdownPv.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.dropdownPv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropdownPv.Name = "dropdownPv";
            this.dropdownPv.Size = new System.Drawing.Size(28, 23);
            this.dropdownPv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statuslabelTied
            // 
            this.statuslabelTied.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.statuslabelTied.Name = "statuslabelTied";
            this.statuslabelTied.Size = new System.Drawing.Size(88, 20);
            this.statuslabelTied.Text = "Усталость: 0%";
            this.statuslabelTied.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dropdownTravm
            // 
            this.dropdownTravm.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.человек3ToolStripMenuItem});
            this.dropdownTravm.Enabled = false;
            this.dropdownTravm.Image = global::ABClient.Properties.Resources._15x12_tr3;
            this.dropdownTravm.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dropdownTravm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.dropdownTravm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropdownTravm.Name = "dropdownTravm";
            this.dropdownTravm.Size = new System.Drawing.Size(28, 23);
            this.dropdownTravm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // человек3ToolStripMenuItem
            // 
            this.человек3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.человек3ToolStripMenuItem1,
            this.toolStripSeparator13,
            this.лечитьЛегкуюТравмуToolStripMenuItem,
            this.лечитьСреднююТравмуToolStripMenuItem,
            this.лечитьТяжелуюТравмуToolStripMenuItem,
            this.лечитьБоевуюТравмуToolStripMenuItem,
            this.toolStripSeparator14,
            this.отправитьРекламуToolStripMenuItem,
            this.открытьИнфуToolStripMenuItem});
            this.человек3ToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.человек3ToolStripMenuItem.Image = global::ABClient.Properties.Resources._15x12_tr1;
            this.человек3ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.человек3ToolStripMenuItem.Name = "человек3ToolStripMenuItem";
            this.человек3ToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.человек3ToolStripMenuItem.Text = "Человек [3]: 1/-/-/-";
            // 
            // человек3ToolStripMenuItem1
            // 
            this.человек3ToolStripMenuItem1.Image = global::ABClient.Properties.Resources._16x16_private;
            this.человек3ToolStripMenuItem1.Name = "человек3ToolStripMenuItem1";
            this.человек3ToolStripMenuItem1.Size = new System.Drawing.Size(201, 22);
            this.человек3ToolStripMenuItem1.Text = "Человек [3]";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(198, 6);
            // 
            // лечитьЛегкуюТравмуToolStripMenuItem
            // 
            this.лечитьЛегкуюТравмуToolStripMenuItem.Image = global::ABClient.Properties.Resources._15x12_tr1;
            this.лечитьЛегкуюТравмуToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.лечитьЛегкуюТравмуToolStripMenuItem.Name = "лечитьЛегкуюТравмуToolStripMenuItem";
            this.лечитьЛегкуюТравмуToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.лечитьЛегкуюТравмуToolStripMenuItem.Text = "Лечить легкую травму";
            // 
            // лечитьСреднююТравмуToolStripMenuItem
            // 
            this.лечитьСреднююТравмуToolStripMenuItem.Image = global::ABClient.Properties.Resources._15x12_tr2;
            this.лечитьСреднююТравмуToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.лечитьСреднююТравмуToolStripMenuItem.Name = "лечитьСреднююТравмуToolStripMenuItem";
            this.лечитьСреднююТравмуToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.лечитьСреднююТравмуToolStripMenuItem.Text = "Лечить среднюю травму";
            // 
            // лечитьТяжелуюТравмуToolStripMenuItem
            // 
            this.лечитьТяжелуюТравмуToolStripMenuItem.Image = global::ABClient.Properties.Resources._15x12_tr3;
            this.лечитьТяжелуюТравмуToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.лечитьТяжелуюТравмуToolStripMenuItem.Name = "лечитьТяжелуюТравмуToolStripMenuItem";
            this.лечитьТяжелуюТравмуToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.лечитьТяжелуюТравмуToolStripMenuItem.Text = "Лечить тяжелую травму";
            // 
            // лечитьБоевуюТравмуToolStripMenuItem
            // 
            this.лечитьБоевуюТравмуToolStripMenuItem.Image = global::ABClient.Properties.Resources._15x12_tr4;
            this.лечитьБоевуюТравмуToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.лечитьБоевуюТравмуToolStripMenuItem.Name = "лечитьБоевуюТравмуToolStripMenuItem";
            this.лечитьБоевуюТравмуToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.лечитьБоевуюТравмуToolStripMenuItem.Text = "Лечить боевую травму";
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(198, 6);
            // 
            // отправитьРекламуToolStripMenuItem
            // 
            this.отправитьРекламуToolStripMenuItem.Name = "отправитьРекламуToolStripMenuItem";
            this.отправитьРекламуToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.отправитьРекламуToolStripMenuItem.Text = "Отправить рекламу";
            // 
            // открытьИнфуToolStripMenuItem
            // 
            this.открытьИнфуToolStripMenuItem.Image = global::ABClient.Properties.Resources._16x16_info;
            this.открытьИнфуToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.открытьИнфуToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.открытьИнфуToolStripMenuItem.Name = "открытьИнфуToolStripMenuItem";
            this.открытьИнфуToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.открытьИнфуToolStripMenuItem.Text = "Открыть инфу";
            // 
            // statuslabelClock
            // 
            this.statuslabelClock.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statuslabelClock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statuslabelClock.Name = "statuslabelClock";
            this.statuslabelClock.Size = new System.Drawing.Size(55, 20);
            this.statuslabelClock.Text = "00:00:00";
            this.statuslabelClock.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.statuslabelClock.ToolTipText = "Точное серверное время";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel1.Image")));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 20);
            this.toolStripStatusLabel1.Text = "Таймеры";
            // 
            // dropdownTimers
            // 
            this.dropdownTimers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.dropdownTimers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemNewTimer});
            this.dropdownTimers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dropdownTimers.Name = "dropdownTimers";
            this.dropdownTimers.Size = new System.Drawing.Size(148, 23);
            this.dropdownTimers.Text = "Загружаются таймеры...";
            this.dropdownTimers.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // menuitemNewTimer
            // 
            this.menuitemNewTimer.Image = ((System.Drawing.Image)(resources.GetObject("menuitemNewTimer.Image")));
            this.menuitemNewTimer.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuitemNewTimer.Name = "menuitemNewTimer";
            this.menuitemNewTimer.Size = new System.Drawing.Size(158, 22);
            this.menuitemNewTimer.Text = "Новый таймер...";
            this.menuitemNewTimer.Click += new System.EventHandler(this.OnMenuitemNewTimerClick);
            // 
            // labelAddress
            // 
            this.labelAddress.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.labelAddress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(69, 20);
            this.labelAddress.Text = "Загрузка...";
            this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuitemStatEdit,
            this.statuslabelLocation,
            this.dropdownPv,
            this.statuslabelTied,
            this.dropdownTravm,
            this.statuslabelClock,
            this.toolStripStatusLabel1,
            this.dropdownTimers,
            this.labelAddress});
            this.statusStrip.Location = new System.Drawing.Point(0, 627);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.Size = new System.Drawing.Size(1077, 25);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // menuitemStatEdit
            // 
            this.menuitemStatEdit.Name = "menuitemStatEdit";
            this.menuitemStatEdit.Size = new System.Drawing.Size(165, 25);
            this.menuitemStatEdit.Text = "Вывод и сброс статистики...";
            this.menuitemStatEdit.Click += new System.EventHandler(this.menuitemStatEdit_Click);
            // 
            // collapsibleSplitter
            // 
            this.collapsibleSplitter.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter.ControlToHide = this.panelRight;
            this.collapsibleSplitter.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleSplitter.ExpandParentForm = false;
            this.collapsibleSplitter.Location = new System.Drawing.Point(819, 24);
            this.collapsibleSplitter.Name = "collapsibleSplitter";
            this.collapsibleSplitter.Size = new System.Drawing.Size(8, 603);
            this.collapsibleSplitter.TabIndex = 8;
            this.collapsibleSplitter.TabStop = false;
            this.collapsibleSplitter.VisualStyle = ABClient.AppControls.SplitterVisualStyle.XP;
            this.collapsibleSplitter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.collapsibleSplitter_MouseClick);
            // 
            // teleportExtendedBar
            // 
            this.teleportExtendedBar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.teleportExtendedBar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tpLocation1,
            this.tpLocation2,
            this.tpLocation3,
            this.tpLocation4,
            this.tpLocation5,
            this.tpLocation6,
            this.tpLocation7,
            this.tpLocation8,
            this.tpLocation9,
            this.tpLocation10,
            this.tpLocation11,
            this.tpLocation12});
            this.teleportExtendedBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.teleportExtendedBar.Name = "teleportExtendedBar";
            this.teleportExtendedBar.Size = new System.Drawing.Size(33, 25);
            this.teleportExtendedBar.Text = "ТП";
            this.teleportExtendedBar.ToolTipText = "Телепортация в определённую локацию";
            // 
            // tpLocation1
            // 
            this.tpLocation1.Name = "tpLocation1";
            this.tpLocation1.Size = new System.Drawing.Size(253, 22);
            this.tpLocation1.Tag = "1";
            this.tpLocation1.Text = "Город Форпост";
            this.tpLocation1.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation2
            // 
            this.tpLocation2.Name = "tpLocation2";
            this.tpLocation2.Size = new System.Drawing.Size(253, 22);
            this.tpLocation2.Tag = "2";
            this.tpLocation2.Text = "Город Октал";
            this.tpLocation2.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation3
            // 
            this.tpLocation3.Name = "tpLocation3";
            this.tpLocation3.Size = new System.Drawing.Size(253, 22);
            this.tpLocation3.Tag = "3";
            this.tpLocation3.Text = "Деревня Подгорная";
            this.tpLocation3.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation4
            // 
            this.tpLocation4.Name = "tpLocation4";
            this.tpLocation4.Size = new System.Drawing.Size(253, 22);
            this.tpLocation4.Tag = "4";
            this.tpLocation4.Text = "Окрестность Фейдана, Телепорт";
            this.tpLocation4.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation5
            // 
            this.tpLocation5.Name = "tpLocation5";
            this.tpLocation5.Size = new System.Drawing.Size(253, 22);
            this.tpLocation5.Tag = "5";
            this.tpLocation5.Text = "Окрестность Октала, Телепорт";
            this.tpLocation5.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation6
            // 
            this.tpLocation6.Name = "tpLocation6";
            this.tpLocation6.Size = new System.Drawing.Size(253, 22);
            this.tpLocation6.Tag = "6";
            this.tpLocation6.Text = "Окрестности Эринграда, Телепорт";
            this.tpLocation6.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation7
            // 
            this.tpLocation7.Name = "tpLocation7";
            this.tpLocation7.Size = new System.Drawing.Size(253, 22);
            this.tpLocation7.Tag = "7";
            this.tpLocation7.Text = "Окрестность Форпоста, Телепорт";
            this.tpLocation7.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation8
            // 
            this.tpLocation8.Name = "tpLocation8";
            this.tpLocation8.Size = new System.Drawing.Size(253, 22);
            this.tpLocation8.Tag = "8";
            this.tpLocation8.Text = "Пустыня Самум-Бейт, Телепорт";
            this.tpLocation8.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation9
            // 
            this.tpLocation9.Name = "tpLocation9";
            this.tpLocation9.Size = new System.Drawing.Size(253, 22);
            this.tpLocation9.Tag = "9";
            this.tpLocation9.Text = "Северский Тракт, Телепорт";
            this.tpLocation9.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation10
            // 
            this.tpLocation10.Name = "tpLocation10";
            this.tpLocation10.Size = new System.Drawing.Size(253, 22);
            this.tpLocation10.Tag = "10";
            this.tpLocation10.Text = "Восточные Леса, Телепорт";
            this.tpLocation10.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation11
            // 
            this.tpLocation11.Name = "tpLocation11";
            this.tpLocation11.Size = new System.Drawing.Size(253, 22);
            this.tpLocation11.Tag = "11";
            this.tpLocation11.Text = "Окрестности Кенджии, Телепорт";
            this.tpLocation11.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            // 
            // tpLocation12
            // 
            this.tpLocation12.Name = "tpLocation12";
            this.tpLocation12.Size = new System.Drawing.Size(253, 22);
            this.tpLocation12.Tag = "12";
            this.tpLocation12.Text = "Ущелье Эль-Тэр, Телепорт";
            this.tpLocation12.Click += new global::System.EventHandler(this.TeleportExtended_Click);
            //
            // FastTeleport
            //
            //this.miFastTeleport.Name = "miFastTeleport";
            //this.miFastTeleport.ShortcutKeys = global::System.Windows.Forms.Keys.F1;
            //this.miFastTeleport.Size = new global::System.Drawing.Size(258, 22);
            //this.miFastTeleport.Text = "Телепорт";
            //this.miFastTeleport.Click += new global::System.EventHandler(this.MiFastTeleportClick);
            // 
            // FormMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1077, 652);
            this.Controls.Add(this.tabControlLeft);
            this.Controls.Add(this.collapsibleSplitter);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = global::ABClient.Properties.Resources.ABClientIcon;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormMainFormClosing);
            this.Load += new System.EventHandler(this.OnFormMainLoad);
            this.CmPerson.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.tabControlRight.ResumeLayout(false);
            this.tabPageContacts.ResumeLayout(false);
            this.tabPageContacts.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPageTextLog.ResumeLayout(false);
            this.tabPageTextLog.PerformLayout();
            this.panelTexLog.ResumeLayout(false);
            this.panelTexLog.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.tabPageLog.PerformLayout();
            this.tabPageLocation.ResumeLayout(false);
            this.tabPageDrinkSets.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DrinkSetItemUsesAmount)).EndInit();
            this.tabControlLeft.ResumeLayout(false);
            this.tabPageGame.ResumeLayout(false);
            this.tabPageGame.PerformLayout();
            this.panelGame.ResumeLayout(false);
            this.toolbarGame.ResumeLayout(false);
            this.toolbarGame.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.cmTray.ResumeLayout(false);
            this.CmGroup.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imagelistDownload;
        private System.Windows.Forms.SaveFileDialog saveFileScreen;
        private System.Windows.Forms.ImageList ic6x16;
        private System.Windows.Forms.ToolStripMenuItem menuitemTabs;
        private System.Windows.Forms.ToolStripMenuItem menuitemOpenForum;
        private System.Windows.Forms.ToolStripMenuItem menuitemOpenTab;
        private System.Windows.Forms.ToolStripMenuItem menuitemOpenNotepad;
        private System.Windows.Forms.ToolStripButton buttonGameScreen;
        private System.Windows.Forms.ToolStripButton buttonGameLogOn;
        private System.Windows.Forms.ToolStripMenuItem menuitemGameLogOn;
        private System.Windows.Forms.Timer timerCrap;
        private System.Windows.Forms.ToolStripButton buttonAutoboi;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem menuitemCommands;
        private System.Windows.Forms.ToolStripMenuItem menuitemClanPrivate;
        private System.Windows.Forms.ToolStripMenuItem menuitemRekPrivate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton buttonNavigator;
        private System.Windows.Forms.ImageList trayImages;
        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.Timer timerTray;
        private System.Windows.Forms.ContextMenuStrip cmTray;
        private System.Windows.Forms.ToolStripMenuItem menuitemRestoreWindow;
        private System.Windows.Forms.ToolStripMenuItem menuitemTrayQuit;
        private System.Windows.Forms.ToolStripButton buttonWalkers;
        private System.Windows.Forms.ToolStripMenuItem miRemoveGroup;
        private System.Windows.Forms.ToolStripMenuItem cmtsDeleteContact;
        private System.Windows.Forms.ToolStripMenuItem cmtsContactPrivate;
        internal System.Windows.Forms.ContextMenuStrip CmPerson;
        internal System.Windows.Forms.ImageList ImageListContacts;
        internal System.Windows.Forms.ContextMenuStrip CmGroup;
        private System.Windows.Forms.ToolStripButton buttonAutoAdv;
        private System.Windows.Forms.ToolStripMenuItem menuitemMinimize;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripButton buttonAutoAnswer;
        private System.Windows.Forms.ToolStripMenuItem menuitemTools;
        private System.Windows.Forms.ToolStripButton buttonSilence;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Panel panelGame;
        private ExtendedWebBrowser browserGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem miFastEnabled;
        private System.Windows.Forms.ToolStripMenuItem miFastTeleport;
        private System.Windows.Forms.ToolStripMenuItem miFastElxVosst;
        private System.Windows.Forms.ToolStripMenuItem miFastF3;
        private System.Windows.Forms.ToolStripMenuItem miFastF4;
        private System.Windows.Forms.ToolStripMenuItem miFastCtrlF12;
        private System.Windows.Forms.ToolStripMenuItem miFastSvitFog;
        private System.Windows.Forms.ToolStripMenuItem miFastF9;
        private System.Windows.Forms.ToolStripMenuItem miFastF10;
        private System.Windows.Forms.ToolStripMenuItem miFastF12;
        private System.Windows.Forms.ToolStripMenuItem menuitemSettingsGeneral;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem miQuick;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem cmtsContactQuick;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem miWearAfter;
        private System.Windows.Forms.ToolStripMenuItem miFastSviRass;
        private System.Windows.Forms.ToolStripMenuItem miQuickCancel;
        private System.Windows.Forms.ToolStripMenuItem cmtsClassNeutral;
        private System.Windows.Forms.ToolStripMenuItem cmtsClassFoe;
        private System.Windows.Forms.ToolStripMenuItem cmtsClassFriend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupNeutral;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupFoe;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupFriend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripButton buttonAutoRefresh;
        private System.Windows.Forms.ToolStripButton buttonWaitOpen;
        private System.Windows.Forms.ToolStripButton buttonOpenNevid;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem cmtsToolId0;
        private System.Windows.Forms.ToolStripMenuItem cmtsToolId1;
        private System.Windows.Forms.ToolStripMenuItem cmtsToolId2;
        private System.Windows.Forms.ToolStripMenuItem cmtsToolId3;
        private System.Windows.Forms.ToolStripMenuItem cmtsToolId4;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupToolId0;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupToolId1;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupToolId2;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupToolId3;
        private System.Windows.Forms.ToolStripMenuItem miSetGroupToolId4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripSplitButton buttonAutoAttack;
        private System.Windows.Forms.ToolStripMenuItem miAutoAttack0;
        private System.Windows.Forms.ToolStripMenuItem miAutoAttack1;
        private System.Windows.Forms.ToolStripMenuItem miAutoAttack2;
        private System.Windows.Forms.ToolStripMenuItem miAutoAttack3;
        private System.Windows.Forms.ToolStripMenuItem miAutoAttack4;
        private ToolStripMenuItem menuitemShowCookies;
        private ToolStripSeparator toolStripSeparator25;
        private Timer timerCheckInfo;
        private ToolStripMenuItem menuitemOpenTodayChat;
        private ToolStripMenuItem miFastDarkTeleport;
        private ToolStripMenuItem miFastDarkFog;
        private ToolStripButton buttonPerenap;
        private ToolStripButton buttonSelfNevid;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuitemDoSearchBox;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem menuitemDoResetVisitedCells;
        private Timer timer30;
        private ToolStripMenuItem miFastSviSelfRass;
        private ToolStripButton buttonFury;
        private ToolStripMenuItem menuitemSettingsAb;
        public TabControl tabControlRight;
        private TabPage tabPageContacts;
        private TreeViewEx treeContacts;
        private CollapsibleSplitter collapsibleSplitterContacts;
        private TextBox tbContactDetails;
        private ToolStrip toolStrip3;
        private ToolStripButton tsDeleteContact;
        private ToolStripButton tsContactPrivate;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripButton tsBossTrace;
        private TabPage tabPageTextLog;
        private Panel panelTexLog;
        private TextBox textboxTexLog;
        private ToolStrip toolStrip1;
        private ToolStripButton buttonDoTexLog;
        private ToolStripButton buttonShowPerformance;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton buttonClearTexLog;
        private ToolStripButton tsContactTrace;
        private TabPage tabPageLog;
        private CheckBox checkDoLog;
        private ToolStripMenuItem сканированиеКартыToolStripMenuItem;
        private ToolStripMenuItem menuitemReloadTopFrame;
        private ToolStripMenuItem miAutoAttack5;
        private ToolStripMenuItem miSetGroupToolId5;
        private ToolStripMenuItem cmtsToolId5;
        private ToolStripStatusLabel statuslabelLocation;
        private ToolStripDropDownButton dropdownPv;
        private ToolStripStatusLabel statuslabelTied;
        private ToolStripDropDownButton dropdownTravm;
        private ToolStripMenuItem человек3ToolStripMenuItem;
        private ToolStripMenuItem человек3ToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripMenuItem лечитьЛегкуюТравмуToolStripMenuItem;
        private ToolStripMenuItem лечитьСреднююТравмуToolStripMenuItem;
        private ToolStripMenuItem лечитьТяжелуюТравмуToolStripMenuItem;
        private ToolStripMenuItem лечитьБоевуюТравмуToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripMenuItem отправитьРекламуToolStripMenuItem;
        private ToolStripMenuItem открытьИнфуToolStripMenuItem;
        private ToolStripStatusLabel statuslabelClock;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripDropDownButton dropdownTimers;
        private ToolStripMenuItem menuitemNewTimer;
        private ToolStripStatusLabel labelAddress;
        private StatusStrip statusStrip;
        private ToolStripMenuItem menuitemCheckCell;
        private ToolStripMenuItem menuitemCacheRefresh;
        private TabPage tabPageLocation;
        public ListBox listBoxLocation;
        private ToolStripMenuItem menuitemTurotor_Top;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuitemStatEdit;
        private TabPage tabPageDrinkSets;
        private GroupBox groupBox3;
        private Button DrinkSetSave;
        private Button DrinkSetDelete;
        private Button DrinkSetUse;
        private TextBox DrinkSetComposition;
        private ListBox DrinkSetsNames;
        private Button DrinkSetItemAddButton;
        private NumericUpDown DrinkSetItemUsesAmount;
        private Label label2;
        private ComboBox DrinkSetItemsMenu;
        private Button DrinkSetAddButton;
        private TextBox DrinkSetName;
        private GroupBox groupBox1;
        private Button FortBuffsCollect;
        private Button FortBuffsSave;
        private TextBox FortBuffsCells;
        private ToolStripDropDownButton teleportExtendedBar;
        internal ToolStripMenuItem tpLocation1;
        internal ToolStripMenuItem tpLocation2;
        private ToolStripMenuItem tpLocation3;
        private ToolStripMenuItem tpLocation4;
        private ToolStripMenuItem tpLocation5;
        private ToolStripMenuItem tpLocation6;
        private ToolStripMenuItem tpLocation7;
        private ToolStripMenuItem tpLocation8;
        private ToolStripMenuItem tpLocation9;
        private ToolStripMenuItem tpLocation10;
        private ToolStripMenuItem tpLocation11;
        private ToolStripMenuItem tpLocation12;
    }
}