using ABClient.MyForms;

namespace ABClient.ABForms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using ABProxy;
    using AppControls;
    using MyHelpers;
    using Properties;
    using Tabs;
    using MyChat;

    internal sealed partial class FormMain
    {
        private void InitGameTab()
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                        new object[] { "InitGameTab()" });
                }
            }
            catch (InvalidOperationException)
            {
            }

            if (tabControlLeft == null)
            {
                return;
            }

            if (tabControlLeft.TabPages.Count == 0)
            {
                return;
            }

            var tabPage = tabControlLeft.TabPages[0];
            tabPage.Text = AppVars.Profile.UserNick;
            TabClass tabClass;
            if (tabPage.Tag == null)
            {
                tabClass = new TabClass { MyType = TabType.Game, WB = browserGame };
                tabPage.Tag = tabClass;
            }

            browserGame.BeforeNewWindow -= wbTab_BeforeNewWindow;
            browserGame.BeforeNavigate -= browserGame_BeforeNavigate;
            browserGame.DocumentCompleted -= browserGame_DocumentCompleted;

            panelGame.Controls.Remove(browserGame);
            browserGame.Dispose();
            browserGame = null;
            CookiesManager.ClearGame();

            browserGame = new ExtendedWebBrowser
                              {
                                  Dock = DockStyle.Fill,
                                  Name = "browserGame",
                                  ScriptErrorsSuppressed = true,
                                  TabIndex = 0,
                                  ObjectForScripting = new ScriptManager()
                              };

            browserGame.BeforeNewWindow += wbTab_BeforeNewWindow;
            browserGame.BeforeNavigate += browserGame_BeforeNavigate;
            browserGame.DocumentCompleted += browserGame_DocumentCompleted;
          
            panelGame.Controls.Add(browserGame);
            tabClass = (TabClass)tabPage.Tag;
            tabClass.WB = browserGame;
            tabPage.Tag = tabClass;
            buttonGameScreen.Tag = browserGame;

            /*
            var encnick = HelperConverters.NickEncode("Мастер Создатель");
            var url = $"http://www.neverlands.ru/pinfo.cgi?{encnick}";
            browserGame.Navigate(url);
            while (browserGame != null && !browserGame.IsDisposed && browserGame.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();

            */

            if (browserGame != null && !browserGame.IsDisposed)
            {
                browserGame.Navigate(Resources.AddressNeverlands);

                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                            new object[] { $"browserGame.Navigate({Resources.AddressNeverlands})" });
                    }
                }
                catch (InvalidOperationException)
                {
                }

                while (browserGame != null && !browserGame.IsDisposed &&
                       browserGame.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();

                /*
                if (browserGame != null && !browserGame.IsDisposed)
                    browserGame.Navigate(Resources.AddressNeverlands);
                    */
            }
        }

        internal void BeforeNewWindow(string tourl)
        {
            if (string.IsNullOrEmpty(tourl))
            {
                return;
            }

            BeforeNewWindow(tourl, false);
        }

        private void BeforeNewWindow(string tourl, bool delayed)
        {
            if (tourl.Equals(Resources.AddressGame, StringComparison.OrdinalIgnoreCase) || 
                tourl.StartsWith(Resources.AddressGameRoot, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (tourl.StartsWith(Resources.AddressForum, StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.Forum, tourl, delayed);
                return;
            }

            if (tourl.StartsWith(Resources.AddressPInfo, StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.PInfo, tourl, delayed);
                return;
            }

            if (tourl.StartsWith(Resources.AddressPName, StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.PName, tourl, delayed);
                return;
            }

            if (tourl.StartsWith(Resources.AddressPBots, StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.PBots, tourl, delayed);
                return;
            }

            if (tourl.StartsWith(Resources.AddressFightLog, StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.Log, tourl, delayed);
                return;
            }

            if (tourl.StartsWith("http://www.neverlands.ru/ch.php?lo=1&r=", StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.Room, tourl, delayed);
                return;
            }

            if (tourl.Equals(Resources.AddressNotepad, StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.Notepad, tourl, delayed);
                return;
            }

            if (tourl.Equals(Resources.AddressTodayChat, StringComparison.OrdinalIgnoreCase))
            {
                CreateNewTab(TabType.TodayChat, tourl, delayed);
                return;
            }

            /*
            if (tourl.StartsWith("http://stat.alone.com.ua/userinfo/?name=", StringComparison.OrdinalIgnoreCase))
            {
                var newurl = Resources.AddressPInfo + tourl.Substring("http://stat.alone.com.ua/userinfo/?name=".Length);
                CreateNewTab(TabType.PInfo, newurl, delayed);
                return;
            }
            */

            CreateNewTab(TabType.Other, tourl, delayed);
        }

        private void CreateNewTab(TabType tt, string tourl, bool delayed)
        {
            if (!string.IsNullOrEmpty(tourl))
            {
                for (var index = 1; index < tabControlLeft.TabPages.Count; index++)
                {
                    if (!tourl.Equals(TabGetUrl(index), StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    if (!delayed)
                    {
                        tabControlLeft.SelectedIndex = index;
                    }

                    return;
                }
            }

            var tabc = new TabClass { MyType = tt, Address = tourl, AddressInit = tourl, Delayed = delayed };

            var tabpage = new TabPage();

            ExtendedWebBrowser browser = null;
            TextBox textbox = null;
            if (tabc.MyType != TabType.Notepad)
            {
                browser = new ExtendedWebBrowser { Dock = DockStyle.Fill, ScriptErrorsSuppressed = true, ObjectForScripting = new ScriptManager() }; // !!!
                browser.BeforeNewWindow += wbTab_BeforeNewWindow;
                tabc.WB = browser;
            }
            else
            {
                textbox = new TextBox
                              {
                                  BorderStyle = BorderStyle.None,
                                  Dock = DockStyle.Fill,
                                  Font = new Font("Courier New", 9F, FontStyle.Regular, GraphicsUnit.Point, 204),
                                  Multiline = true,
                                  ScrollBars = ScrollBars.Both,
                              };

                textbox.Leave += (sender, e) => AppVars.Profile.Notepad = ((TextBox)sender).Text;
                textbox.Text = AppVars.Profile.Notepad;
                textbox.SelectionStart = textbox.Text.Length;
                textbox.SelectionLength = 1;
                tabc.Note = textbox;                   
            }

            //  panel
            
            var panel = new Panel();

            if (browser != null)
            {
                panel.Controls.Add(browser);
            }
            else
            {
                panel.Controls.Add(textbox);
            }

            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(3, 28);
            panel.Name = Guid.NewGuid().ToString();
            panel.Padding = new Padding(0, 3, 0, 0);
            panel.Size = new Size(666, 461);

            // buttonback

            ToolStripButton buttonback = null;
            if (tabc.MyType == TabType.Forum || tabc.MyType == TabType.Log || tabc.MyType == TabType.Other)
            {
                buttonback = new ToolStripButton
                                 {
                                     Image = Resources._16x16_back,
                                     ImageTransparentColor = Color.Magenta,
                                     Name = Guid.NewGuid().ToString(),
                                     Size = new Size(58, 22),
                                     Text = @"Назад",
                                     ToolTipText = @"Возврат на предыдущую страницу",
                                     Tag = browser
                                 };
                buttonback.Click += buttonForumBack_Click;
            }

            // buttonrefresh

            ToolStripButton buttonrefresh = null;
            if ((tabc.MyType != TabType.Notepad) && (tabc.MyType != TabType.TodayChat))
            {
                buttonrefresh = new ToolStripButton
                                    {
                                        Image = Resources._16x16_refresh,
                                        ImageTransparentColor = Color.Magenta,
                                        Name = Guid.NewGuid().ToString(),
                                        Size = new Size(77, 22),
                                        Text = "Обновить",
                                        ToolTipText = "Обновление текущей страницы",
                                        Tag = browser
                                    };
                buttonrefresh.Click += buttonForumRefresh_Click;
            }

            // buttonreload

            ToolStripButton buttonreload = null;
            if (tabc.MyType == TabType.TodayChat)
            {
                buttonreload = new ToolStripButton
                {
                    Image = Resources._16x16_refresh,
                    ImageTransparentColor = Color.Magenta,
                    Name = Guid.NewGuid().ToString(),
                    Size = new Size(77, 22),
                    Text = "Перечитать",
                    ToolTipText = "Обновление сегодняшнего чата",
                    Tag = browser
                };
                buttonreload.Click += buttonReload_Click;
            }

            // buttonclose
            
            var buttonclose = new ToolStripButton
                                  {
                                      Image = Resources._16x16_close,
                                      ImageTransparentColor = Color.Magenta,
                                      Name = Guid.NewGuid().ToString(),
                                      Size = new Size(71, 22),
                                      Text = "Закрыть",
                                      ToolTipText = "Закрытие этой закладки",
                                      Tag = tabpage
                                  };
            buttonclose.Click += buttonForumClose_Click;

            // toolstripseparator1
            
            ToolStripSeparator toolstripseparator1 = null;
            if (tabc.MyType != TabType.Notepad)
            {
                toolstripseparator1 = new ToolStripSeparator { Name = Guid.NewGuid().ToString(), Size = new Size(6, 25) };
            }

            // buttoncompas
            
            ToolStripButton buttoncompas = null;
            if (tabc.MyType == TabType.PInfo || tabc.MyType == TabType.PName)
            {
                if (!string.IsNullOrEmpty(tourl))
                {
                    var nick = tabc.MyType == TabType.PInfo
                                   ?
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPInfo.Length))
                                   :
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPName.Length));

                    buttoncompas = new ToolStripButton
                    {
                        Image = Resources._16x16_compas,
                        ImageTransparentColor = Color.Magenta,
                        Name = Guid.NewGuid().ToString(),
                        Size = new Size(64, 22),
                        Text = @"Компас",
                        ToolTipText = @"Поиск положения на природе",
                        Tag = nick,
                        Enabled = true
                    };
                    buttoncompas.Click += buttonCompas_Click;
                }
            }
             

            // buttonprivate
             
            ToolStripButton buttonprivate = null;
            if (tabc.MyType == TabType.PInfo || tabc.MyType == TabType.PName)
            {
                if (!string.IsNullOrEmpty(tourl))
                {
                    var nick = tabc.MyType == TabType.PInfo
                                   ?
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPInfo.Length))
                                   :
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPName.Length));

                    buttonprivate = new ToolStripButton
                                        {
                                            Image = Resources._16x16_private,
                                            ImageTransparentColor = Color.Magenta,
                                            Name = Guid.NewGuid().ToString(),
                                            Size = new Size(64, 22),
                                            Text = "Приват",
                                            ToolTipText = "Обратиться в приват",
                                            Tag = nick
                                        };
                    buttonprivate.Click += buttonPrivate_Click;
                }
            }

            // buttoncontact
             
            ToolStripButton buttoncontact = null;
            if (tabc.MyType == TabType.PInfo || tabc.MyType == TabType.PName)
            {
                if (!string.IsNullOrEmpty(tourl))
                {
                    var nick = tabc.MyType == TabType.PInfo
                                   ?
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPInfo.Length))
                                   :
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPName.Length));

                    buttoncontact = new ToolStripButton
                                        {
                                            Image = Resources._16x16_person,
                                            ImageTransparentColor = Color.Magenta,
                                            Name = Guid.NewGuid().ToString(),
                                            Size = new Size(64, 22),
                                            Text = "В контакты",
                                            ToolTipText = "Добавить в контакты",
                                            Tag = nick
                                        };

                    buttoncontact.Click += buttonContact_Click;
                }
            }

            // buttonaddclan
            
            ToolStripButton buttonAddClan = null;
            if (tabc.MyType == TabType.PInfo || tabc.MyType == TabType.PName)
            {
                if (!string.IsNullOrEmpty(tourl))
                {
                    var nick = tabc.MyType == TabType.PInfo
                                   ?
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPInfo.Length))
                                   :
                                       HelperConverters.NickDecode(tourl.Substring(Resources.AddressPName.Length));

                    buttonAddClan = new ToolStripButton
                                        {
                                            Image = Resources._16x16_person,
                                            ImageTransparentColor = Color.Magenta,
                                            Name = Guid.NewGuid().ToString(),
                                            Size = new Size(64, 22),
                                            Text = @"Весь клан",
                                            ToolTipText = @"Добавление всего клана в контакты",
                                            Tag = nick,
                                            Enabled = true
                                        };
                    buttonAddClan.Click += buttonAddClan_Click;
                }
            }

            // buttonscreen

            ToolStripButton buttonscreen = null;
            if ((tabc.MyType != TabType.Notepad) && (tabc.MyType != TabType.TodayChat))
            {
                buttonscreen = new ToolStripButton
                                   {
                                       Image = Resources._16x16_camera,
                                       ImageTransparentColor = Color.Magenta,
                                       Name = Guid.NewGuid().ToString(),
                                       Size = new Size(64, 22),
                                       Text = "Снимок",
                                       ToolTipText = "Снимок текущей закладки",
                                       Tag = browser
                                   };
                buttonscreen.Click += buttonForumScreen_Click;
            }

            // separator

            ToolStripSeparator toolstripseparator2 = null;
            if ((tabc.MyType != TabType.Notepad) && (tabc.MyType != TabType.TodayChat))
            {
                toolstripseparator2 = new ToolStripSeparator { Name = Guid.NewGuid().ToString(), Size = new Size(6, 25) };
            }

            // buttonaddress

            ToolStripButton buttonaddress = null;
            if ((tabc.MyType != TabType.Notepad) && (tabc.MyType != TabType.TodayChat))
            {
                buttonaddress = new ToolStripButton
                                    {
                                        DisplayStyle = ToolStripItemDisplayStyle.Image,
                                        Image = Resources._16x16_copyurl,
                                        ImageTransparentColor = Color.Magenta,
                                        Name = Guid.NewGuid().ToString(),
                                        Size = new Size(23, 22),
                                        ToolTipText = "Копировать адрес в клипборд"
                                    };
                buttonaddress.Click += buttonForumCopyAddress_Click;
            }
             
            // labeladdress

            ToolStripStatusLabel labeladdress = null;
            if ((tabc.MyType != TabType.Notepad) && (tabc.MyType != TabType.TodayChat))
            {
                labeladdress = new ToolStripStatusLabel
                                   {
                                       Enabled = false,
                                       Name = Guid.NewGuid().ToString(),
                                       Size = new Size(65, 22),
                                       Text = tabc.AddressInit,
                                       TextAlign = ContentAlignment.MiddleLeft,
                                       DisplayStyle = ToolStripItemDisplayStyle.Text
                                   };
            }

            if (browser != null)
            {
                browser.Tag = labeladdress;
                browser.DocumentCompleted += wbTab_DocumentCompleted;
            }

            //
            //  toolstrip
            //

            var toolstrip = new ToolStrip();
            switch (tabc.MyType)
            {
                case TabType.Other:
                case TabType.Log:
                case TabType.Forum:
                    toolstrip.Items.AddRange(new ToolStripItem[]
                                                 {
                                                     buttonback,
                                                     buttonrefresh,
                                                     buttonclose,
                                                     toolstripseparator1,
                                                     buttonscreen,
                                                     toolstripseparator2,
                                                     buttonaddress,
                                                     labeladdress
                                                 });
                    break;
                case TabType.PBots:
                case TabType.Room:
                    toolstrip.Items.AddRange(new ToolStripItem[]
                                                 {
                                                     buttonrefresh,
                                                     buttonclose,
                                                     toolstripseparator1,
                                                     buttonscreen,
                                                     toolstripseparator2,
                                                     buttonaddress,
                                                     labeladdress
                                                 });
                    break;

                case TabType.PInfo:
                case TabType.PName:
                    toolstrip.Items.AddRange(new ToolStripItem[]
                                                 {
                                                     buttonrefresh,
                                                     buttonclose,
                                                     toolstripseparator1,
                                                     buttoncompas,
                                                     buttonprivate,
                                                     buttoncontact,
                                                     buttonAddClan,
                                                     buttonscreen,
                                                     toolstripseparator2,
                                                     buttonaddress,
                                                     labeladdress
                                                 });

                    break;

                case TabType.Notepad:
                    toolstrip.Items.AddRange(new ToolStripItem[]
                                                 {
                                                     buttonclose
                                                 });
                    break;

                case TabType.TodayChat:
                    toolstrip.Items.AddRange(new ToolStripItem[]
                                                 {
                                                     buttonreload,
                                                     buttonclose
                                                 });
                    break;
            }

            toolstrip.Location = new Point(3, 3);
            toolstrip.Name = Guid.NewGuid().ToString();
            toolstrip.Size = new Size(666, 25);

            tabpage.Controls.Add(panel);
            tabpage.Controls.Add(toolstrip);
            tabpage.Name = Guid.NewGuid().ToString();
            tabpage.Location = new Point(4, 23);
            tabpage.Size = new Size(672, 492);
            tabpage.Padding = new Padding(3);
            tabpage.UseVisualStyleBackColor = true;

            switch (tt)
            {
                case TabType.Forum:
                    tabpage.Text = "Форум";
                    break;
                case TabType.PInfo:
                    if (tourl != null)
                    {
                        tabpage.Text = HelperConverters.NickDecode(tourl.Substring(Resources.AddressPInfo.Length));
                        tourl = HelperConverters.AddressEncode(Resources.AddressPInfo + tabpage.Text);
                    }

                    tabpage.ImageIndex = 0;
                    break;
                case TabType.PName:
                    if (tourl != null)
                    {
                        tabpage.Text = HelperConverters.NickDecode(tourl.Substring(Resources.AddressPName.Length));
                        tourl = HelperConverters.AddressEncode(Resources.AddressPName + tabpage.Text);
                    }

                    tabpage.ImageIndex = 0;
                    break;
                case TabType.PBots:
                    if (tourl != null) tabpage.Text = "Бот " + tourl.Substring(Resources.AddressPBots.Length);
                    tabpage.ImageIndex = 0;
                    break;
                case TabType.Log:
                    if (tourl != null)
                    {
                        var slog = tourl.Substring(Resources.AddressFightLog.Length);
                        var pos = slog.IndexOf('&');
                        if (pos != -1)
                            slog = slog.Substring(0, pos);
                        tabpage.Text = "Бой " + slog;
                    }
                    break;
                case TabType.Room:
                    if (tourl != null)
                        tabpage.Text = "Комната " + tourl.Substring("http://www.neverlands.ru/ch.php?lo=1&r=".Length);
                    break;
                case TabType.Notepad:
                    tabpage.Text = "Блокнот";
                    tabpage.ImageIndex = 1;
                    break;
                case TabType.TodayChat:
                    tabpage.Text = "Сегодняшний чат";                   
                    break;
                case TabType.Other:
                    if (tourl != null) tabpage.Text = new Uri(tourl).DnsSafeHost;
                    break;
            }

            if ((tt != TabType.Game) && (tt != TabType.Notepad) && (tt != TabType.TodayChat))
            {
                if (!delayed)
                {
                    tabc.WB.Navigate(tourl);
                }
            }

            if (tt == TabType.TodayChat)
            {
                if (!delayed)
                {
                    TabReload(buttonreload);
                }                
            }

            tabc.Address = tourl;
            tabpage.Tag = tabc;

            if ((tt == TabType.PInfo) && (tabpage.Text.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase)))
            {
                tabControlLeft.TabPages.Insert(1, tabpage);
            }
            else
            {
                tabControlLeft.TabPages.Add(tabpage);
            }

            if (delayed)
            {
                return;
            }

            SaveTabs();
            tabControlLeft.SelectedTab = tabpage;
        }

        private void TabChanged()
        {
            if (tabControlLeft.SelectedIndex == -1)
            {
                return;
            }

            var obj = tabControlLeft.TabPages[tabControlLeft.SelectedIndex].Tag;
            if (obj == null)
            {
                return;
            }

            var myTabClass = (TabClass)obj;
            if (myTabClass.MyType == TabType.Game || myTabClass.MyType == TabType.Notepad)
            {
                return;
            }

            if (myTabClass.MyType == TabType.TodayChat)
            {
                TabTodayChatReload(myTabClass.WB);
            }

            if (!myTabClass.Delayed)
            {
                return;
            }            

            myTabClass.Delayed = false;
            tabControlLeft.TabPages[tabControlLeft.SelectedIndex].Tag = myTabClass;

            if (myTabClass.MyType != TabType.TodayChat)
            {
                myTabClass.WB.Navigate(myTabClass.Address);
            }           
        }

        private void TabCopyUrl()
        {
            var tourl = TabGetUrl(tabControlLeft.SelectedIndex);

            if (!string.IsNullOrEmpty(tourl))
            {
                try
                {
                    Clipboard.SetText(tourl);
                }
                catch (ExternalException)
                {
                }
            }
        }

        private string TabGetUrl(int index)
        {
            var myTabClass = (TabClass)tabControlLeft.TabPages[index].Tag;
            if (myTabClass.MyType == TabType.Notepad)
            {
                return Resources.AddressNotepad;
            }

            if (myTabClass.MyType == TabType.TodayChat)
            {
                return Resources.AddressTodayChat;
            }

            if (myTabClass.MyType == TabType.PInfo || myTabClass.MyType == TabType.PName)
            {
                return myTabClass.AddressInit;
            }

            var tourl = myTabClass.AddressInit;
            var wb = myTabClass.WB;
            if (wb != null && wb.Url != null)
            {
                return wb.Url.ToString();
            }

            return tourl;
        }

        private static void TabBack(object sender)
        {
            var button = (ToolStripButton)sender;
            var browser = (ExtendedWebBrowser)(button.Tag);
            browser.GoBack();            
        }

        private static void TabRefresh(object sender)
        {
            var button = (ToolStripButton)sender;
            var browser = (ExtendedWebBrowser)(button.Tag);
            browser.Refresh(WebBrowserRefreshOption.Completely);
        }

        private static void TabTodayChatReload(WebBrowser wb)
        {
            var fileLog = Chat.GetLogName();
            if (!File.Exists(fileLog))
                return;

            try
            {
                wb.Url = new Uri($"file:///{fileLog}");
                while (wb.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();
                if (wb.Document?.Window != null && wb.Document.Body != null)
                {
                    wb.Document.Window.ScrollTo(0, wb.Document.Body.ScrollRectangle.Height);
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private static void TabReload(object sender)
        {
            var button = (ToolStripButton)sender;
            var browser = (ExtendedWebBrowser)(button.Tag);
            TabTodayChatReload(browser);
        }

        private void TabClose(object sender)
        {
            var button = (ToolStripButton)sender;
            var tabpage = (TabPage)button.Tag;
            tabControlLeft.TabPages.Remove(tabpage);
            tabpage.Dispose();
            SaveTabs();
        }

        private static void TabChanged(object sender)
        {
            var browser = (ExtendedWebBrowser)sender;
            var label = (ToolStripLabel)browser.Tag;
            label.Text = browser.Url.AbsoluteUri;
        }

        private static void TabCompas(object sender)
        {
            var button = (ToolStripButton)sender;
            var nick = (string)button.Tag;
            if (AppVars.VipFormCompas != null)
            {
                FormCompas.SetNick(nick);
                AppVars.VipFormCompas.Focus();
                return;
            }

            AppVars.VipFormCompas = new FormCompas(nick);
            AppVars.VipFormCompas.Show();
        }        

        private static void TabAddClan(object sender)
        {
            var button = (ToolStripButton)sender;
            var nick = (string)button.Tag;
            if (AppVars.VipFormAddClan != null)
            {
                AppVars.VipFormAddClan.SetNick(nick);
                AppVars.VipFormAddClan.Focus();
                return;
            } 

            AppVars.VipFormAddClan = new FormAddClan(nick);
            AppVars.VipFormAddClan.Show();
        }

        private void TabPrivate(object sender)
        {
            var button = (ToolStripButton)sender;
            var nick = (string)(button.Tag);
            if (browserGame.Document == null) return;
            tabControlLeft.SelectedIndex = 0;
            WriteMessageToPrompt("%<" + nick + "> ");
        }

        private void TabAddContact(object sender)
        {
            var button = (ToolStripButton)sender;
            var nick = (string)(button.Tag);
            AddContact(nick);
            if (collapsibleSplitter.IsCollapsed)
            {
                collapsibleSplitter.ToggleState();    
            }

            tabControlRight.SelectedIndex = 0;
        }

        private void TabScreen(object sender)
        {
            var button = (ToolStripButton)sender;
            var browser = (ExtendedWebBrowser)(button.Tag);
            var rect = new Rectangle(0, 0, browser.ClientRectangle.Width, browser.ClientRectangle.Height);
            using (var bmp = new Bitmap(browser.ClientRectangle.Width, browser.ClientRectangle.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    var hdc = g.GetHdc();
                    var punk = Marshal.GetIUnknownForObject(browser.ActiveXInstance);
                    HelperExterns.OleDraw(punk, 1, hdc, ref rect);
                    Marshal.Release(punk);
                    g.ReleaseHdc(hdc);
                }
                var screenshotdir = Path.Combine(Application.StartupPath, @"screenshots\");
                if (!Directory.Exists(screenshotdir))
                {
                    Directory.CreateDirectory(screenshotdir);
                }

                saveFileScreen.InitialDirectory = screenshotdir;
                var dr = saveFileScreen.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    bmp.Save(saveFileScreen.FileName, ImageFormat.Jpeg);
                }
            }            
        }

        private void TabAddForum()
        {
            CreateNewTab(TabType.Forum, Resources.AddressForum, false);
        }

        private void TabAddTodayChat()
        {
            CreateNewTab(TabType.TodayChat, Resources.AddressTodayChat, false);
        }

        private void TabAddSite(string address)
        {
            CreateNewTab(TabType.Other, address, false);
        }

        private void TabAddNotepad()
        {
            CreateNewTab(TabType.Notepad, Resources.AddressNotepad, false);
        }

        private void TabAddNew()
        {
            using (var ff = new FormNewTab())
            {
                if (ff.ShowDialog() == DialogResult.OK)
                {
                    BeforeNewWindow(ff.GetAddress());
                }
            }
        }

        private void TabDoubleClick(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            var hti = new TcHitTestInfo(e.X, e.Y);
            var tab = HelperExterns.SendMessage(tabControlLeft.Handle, 0x130D, IntPtr.Zero, ref hti);
            if (tab.ToInt32() <= 0)
            {
                return;
            }

            if (tabControlLeft.TabPages.Count <= tab.ToInt32())
            {
                return;
            }

            var hotTab = tabControlLeft.TabPages[tab.ToInt32()];
            tabControlLeft.TabPages.Remove(hotTab);
            hotTab.Dispose();
            SaveTabs();
        }

        private void SaveTabs()
        {
            var tabs = new List<string>();
            for (var tab = 1; tab < tabControlLeft.TabCount; tab++)
            {
                tabs.Add(TabGetUrl(tab));
            }

            AppVars.Profile.Tabs = tabs.ToArray();
        }

        private void LoadTabs()
        {
            for (var tab = 0; tab < AppVars.Profile.Tabs.Length; tab++)
            {
                BeforeNewWindow(AppVars.Profile.Tabs[tab], true);
            }
        }
    }
}