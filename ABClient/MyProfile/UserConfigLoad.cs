using System.Collections.Generic;
using ABClient.Lez;

namespace ABClient.MyProfile
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using MyHelpers;

    internal sealed partial class UserConfig
    {
        //private readonly List<Contact> _contacts = new List<Contact>();

        /// <summary>
        /// Загружает файл конфигурации
        /// </summary>
        /// <param name="fileName">Полный путь к файлу конфигурации</param>
        /// <returns>Успешна ли загрузка</returns>
        internal bool Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            _configFileName = fileName;
            if (!File.Exists(_configFileName))
            {
                return false;
            }

            var xmlReaderSettings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true,
                ConformanceLevel = ConformanceLevel.Auto
            };

            LezGroups.Clear();

            XmlReader xmlReader = null;
            try
            {
                xmlReader = XmlReader.Create(_configFileName, xmlReaderSettings);
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        ReadElement(xmlReader);
                    }
                }
            }
            catch (IOException ex)
            {
                ConfigLoadError(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                ConfigLoadError(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ConfigLoadError(ex.Message);
            }
            catch (ArgumentException ex)
            {
                ConfigLoadError(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                ConfigLoadError(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                ConfigLoadError(ex.Message);
            }
            catch (XmlException ex)
            {
                ConfigLoadError(ex.Message);
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
            }

            Tabs = _listtabs.ToArray();
            _listtabs.Clear();

            FavLocations = _listfavlocations.ToArray();

            if (Contacts == null)
            {
                Contacts = new SortedList<string, Contact>();
            }

            if (LezGroups.Count == 0)
                LezGroups.Add(new LezBotsGroup(001, 0));

            return true;
        }

        private static void ConfigLoadError(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            MessageBox.Show(
                message,
                ConstErrorConfigLoadTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void ReadElement(XmlReader xmlReader)
        {
            switch (xmlReader.Name)
            {
                case ConstTagUser:
                    ReadUser(xmlReader);
                    break;
                case ConstTagProxy:
                    ReadProxy(xmlReader);
                    break;
                case ConstTagMap:
                    ReadMap(xmlReader);
                    break;
                case ConstTagCure:
                    ReadCure(xmlReader);
                    break;
                case ConstTagAutoAnswer:
                    ReadAutoAnswer(xmlReader);
                    break;
                case ConstTagFish:
                    ReadFish(xmlReader);
                    break;
                case ConstTagChat:
                    ReadChat(xmlReader);
                    break;
                case ConstTagLightForum:
                    ReadForum(xmlReader);
                    break;
                case ConstTagTorg:
                    ReadTorg(xmlReader);
                    break;

                case "window":
                    Window.State = FormWindowState.Normal;
                    var windowState = xmlReader["state"] ?? string.Empty;
                    try
                    {
                        Window.State = (FormWindowState)Enum.Parse(typeof(FormWindowState), windowState);
                    }
                    catch (ArgumentException)
                    {
                    }

                    Window.Left = (xmlReader["left"] == null) ? 0 : Convert.ToInt32(xmlReader["left"], CultureInfo.InvariantCulture);
                    Window.Top = (xmlReader["top"] == null) ? 0 : Convert.ToInt32(xmlReader["top"], CultureInfo.InvariantCulture);
                    Window.Width = (xmlReader["width"] == null) ? 0 : Convert.ToInt32(xmlReader["width"], CultureInfo.InvariantCulture);
                    Window.Height = (xmlReader["height"] == null) ? 0 : Convert.ToInt32(xmlReader["height"], CultureInfo.InvariantCulture);
                    break;

                case "stat":
                    Stat.Drop = xmlReader["drop"] ?? string.Empty;
                    Stat.LastReset = (xmlReader["lastreset"] == null) ? 0 : Convert.ToInt64(xmlReader["lastreset"]);
                    Stat.LastUpdateDay = (xmlReader["lastupdateday"] == null) ? DateTime.Now.DayOfYear : Convert.ToInt32(xmlReader["lastupdateday"]);
                    Stat.Reset = (xmlReader["reset"] != null) && Convert.ToBoolean(xmlReader["reset"]);
                    Stat.SavedTraffic = (xmlReader["savedtraffic"] == null) ? 0 : Convert.ToInt64(xmlReader["savedtraffic"]);
                    Stat.Show = (xmlReader["show"] == null) ? 0 : Convert.ToInt32(xmlReader["show"]);
                    Stat.Traffic = (xmlReader["traffic"] == null) ? 0 : Convert.ToInt64(xmlReader["traffic"]);
                    Stat.XP = (xmlReader["xp"] == null) ? 0 : Convert.ToInt64(xmlReader["xp"]);
                    Stat.NV = (xmlReader["nv"] == null) ? 0 : Convert.ToInt32(xmlReader["nv"]);
                    Stat.FishNV = (xmlReader["fishnv"] == null) ? 0 : Convert.ToInt32(xmlReader["fishnv"]);
                    break;

                case "itemdrop":
                    var itemdrop = new TypeItemDrop
                                       {
                                           Name = xmlReader["name"] ?? string.Empty,
                                           Count = (xmlReader["count"] == null) ? 1 : Convert.ToInt32(xmlReader["count"])
                                       };
                    Stat.ItemDrop.Add(itemdrop);
                    break;

                case "splitter":
                    Splitter.Collapsed = (xmlReader["collapsed"] != null) && Convert.ToBoolean(xmlReader["collapsed"], CultureInfo.InvariantCulture);
                    Splitter.Width = (xmlReader["width"] == null) ? 200 : Convert.ToInt32(xmlReader["width"], CultureInfo.InvariantCulture);
                    break;

                case "inv":
                    DoInvPack = (xmlReader["doInvPack"] == null) || Convert.ToBoolean(xmlReader["doInvPack"]);
                    DoInvPackDolg = (xmlReader["doInvPackDolg"] == null) || Convert.ToBoolean(xmlReader["doInvPackDolg"]);
                    DoInvSort = (xmlReader["doInvSort"] == null) || Convert.ToBoolean(xmlReader["doInvSort"]);
                    break;

                case "dopromptexit":
                    xmlReader.Read();
                    DoPromptExit = xmlReader.ReadContentAsBoolean();
                    break;

                case "dostopondig":
                    xmlReader.Read();
                    DoStopOnDig = xmlReader.ReadContentAsBoolean();
                    break;

                case "dohttplog":
                    xmlReader.Read();
                    DoHttpLog = xmlReader.ReadContentAsBoolean();
                    break;

                case "dotexlog":
                    xmlReader.Read();
                    DoTexLog = xmlReader.ReadContentAsBoolean();
                    break;

                case "doautocutwritechat":
                    xmlReader.Read();
                    DoAutoCutWriteChat = xmlReader.ReadContentAsBoolean();
                    break;

                case "showperformance":
                    xmlReader.Read();
                    ShowPerformance = xmlReader.ReadContentAsBoolean();
                    break;

                case "showoverwarning":
                    xmlReader.Read();
                    ShowOverWarning = xmlReader.ReadContentAsBoolean();
                    break;

                case "selectedrightpanel":
                    xmlReader.Read();
                    SelectedRightPanel = xmlReader.ReadContentAsInt();
                    break;

                case "notepad":
                    xmlReader.Read();
                    Notepad = HelperPacks.UnpackString(xmlReader.ReadContentAsString());
                    break;

                case "nextcheckversion":
                    xmlReader.Read();
                    var binaryNextCheckVersion = xmlReader.ReadContentAsLong();
                    NextCheckVersion = DateTime.FromBinary(binaryNextCheckVersion);
                    break;

                case "tab":
                    xmlReader.Read();
                    _listtabs.Add(xmlReader.ReadContentAsString());
                    break;

                case "favlocation":
                    xmlReader.Read();
                    _listfavlocations.Add(xmlReader.ReadContentAsString());
                    break;

                case "herbautocut":
                    xmlReader.Read();
                    HerbsAutoCut.Add(xmlReader.ReadContentAsString());
                    break;

                case "complects":
                    xmlReader.Read();
                    Complects = xmlReader.ReadContentAsString();
                    break;

                case "dotray":
                    xmlReader.Read();
                    DoTray = xmlReader.ReadContentAsBoolean();
                    break;

                case "showtraybaloons":
                    xmlReader.Read();
                    ShowTrayBaloons = xmlReader.ReadContentAsBoolean();
                    break;
/*
                case "servdiff":
                    xmlReader.Read();
                    var servdiff = xmlReader.ReadContentAsString();
                    TimeSpan val;
                    if (!TimeSpan.TryParse(servdiff, out val))
                    {
                        val = TimeSpan.MinValue;
                    }

                    ServDiff = val;
                    break;
                    */

                case "doconvertrussian":
                    xmlReader.Read();
                    DoConvertRussian = xmlReader.ReadContentAsBoolean();
                    break;

                case "apptimer":
                    var appTimer = new AppTimer();
                    var triggertime = xmlReader["triggertime"];
                    if (triggertime != null)
                    {
                        long binary;
                        if (long.TryParse(triggertime, out binary))
                        {
                            appTimer.TriggerTime = DateTime.FromBinary(binary);
                        }
                    }

                    appTimer.Description = xmlReader["description"] ?? string.Empty;
                    appTimer.Complect = xmlReader["complect"] ?? string.Empty;
                    appTimer.Potion = xmlReader["potion"] ?? string.Empty;
                    var xmldrinkcount = xmlReader["drinkcount"];
                    if (xmldrinkcount != null)
                    {
                        int drinkcount;
                        if (int.TryParse(xmldrinkcount, out drinkcount))
                        {
                            appTimer.DrinkCount = drinkcount;
                        }
                    }

                    var xmlisrecur =  xmlReader["isrecur"];
                    if (xmlisrecur != null)
                    {
                        bool isrecur;
                        if (bool.TryParse(xmlisrecur, out isrecur))
                        {
                            appTimer.IsRecur = isrecur;
                        }
                    }

                    var xmlisherb = xmlReader["isherb"];
                    if (xmlisherb != null)
                    {
                        bool isherb;
                        if (bool.TryParse(xmlisherb, out isherb))
                        {
                            appTimer.IsHerb = isherb;
                        }
                    }

                    var xmleveryminutes = xmlReader["everyminutes"];
                    if (xmleveryminutes != null)
                    {
                        int everyMinutes;
                        if (int.TryParse(xmleveryminutes, out everyMinutes))
                        {
                            appTimer.EveryMinutes = everyMinutes;
                        }
                    }

                    appTimer.Destination = xmlReader["destination"] ?? string.Empty;
                    AppConfigTimers.Add(appTimer);
                    break;
                case "pers":
                    Pers.Guamod = (xmlReader["guamod"] == null) || Convert.ToBoolean(xmlReader["guamod"], CultureInfo.InvariantCulture);
                    Pers.IntHP = (xmlReader["inthp"] != null) ? Convert.ToDouble(xmlReader["inthp"], CultureInfo.InvariantCulture) : 2000;
                    Pers.IntMA = (xmlReader["intma"] != null) ? Convert.ToDouble(xmlReader["intma"], CultureInfo.InvariantCulture) : 9000;
                    Pers.Ready = (xmlReader["ready"] != null) ? Convert.ToInt64(xmlReader["ready"], CultureInfo.InvariantCulture) : 0;
                    Pers.LogReady = xmlReader["logready"] ?? string.Empty;
                    break;
                case "navigator":
                    Navigator.AllowTeleports = (xmlReader["allowteleports"] == null) || Convert.ToBoolean(xmlReader["allowteleports"], CultureInfo.InvariantCulture);
                    break;

                case "contactentry":
                    var strclassid = xmlReader["classid"] ?? string.Empty;
                    int classid;
                    if (!int.TryParse(strclassid, out classid))
                    {
                        classid = 0;
                    }

                    var strtoolid = xmlReader["toolid"] ?? string.Empty;
                    int toolid;
                    if (!int.TryParse(strtoolid, out toolid))
                    {
                        toolid = 0;
                    }

                    var contact = new Contact(
                        xmlReader["name"] ?? string.Empty,
                        classid,
                        toolid,
                        xmlReader["sign"] ?? string.Empty,
                        xmlReader["clan"] ?? string.Empty,
                        xmlReader["align"] ?? string.Empty,
                        HelperPacks.UnpackString(xmlReader["comments"] ?? string.Empty),
                        xmlReader["tracing"] == null || Convert.ToBoolean(xmlReader["tracing"], CultureInfo.InvariantCulture),                        
                        xmlReader["level"] ?? string.Empty,
                        true);

                    if (Contacts == null)
                    {
                        Contacts = new SortedList<string, Contact>();
                    }

                    if (!Contacts.ContainsKey(contact.Name.ToLower()))
                    {
                        Contacts.Add(contact.Name.ToLower(), contact);
                    }

                    break;

                case "herbcell":
                    var herbCell = new HerbCell();
                    var location = xmlReader["location"] ?? string.Empty;
                    if (!string.IsNullOrEmpty(location))
                    {
                        herbCell.RegNum = location;
                        var herbs = xmlReader["herbs"] ?? string.Empty;
                        if (!string.IsNullOrEmpty(location))
                        {
                            herbCell.Herbs = herbs;
                        }

                        var lastViewString = xmlReader["lastview"] ?? string.Empty;
                        if (!string.IsNullOrEmpty(location))
                        {
                            long updatedInTicks;
                            if (long.TryParse(lastViewString, out updatedInTicks))
                            {
                                if ((ServDiff != TimeSpan.MinValue) && (updatedInTicks < DateTime.Now.Subtract(ServDiff).Ticks))
                                {
                                    herbCell.UpdatedInTicks = updatedInTicks;
                                    var timediff = TimeSpan.FromTicks(DateTime.Now.Subtract(ServDiff).Ticks - updatedInTicks);
                                    if (timediff.TotalHours < 6)
                                    {
                                        if (!HerbCells.ContainsKey(location))
                                        {
                                            HerbCells.Add(location, herbCell);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;

                case "dorob":
                    xmlReader.Read();
                    DoRob = xmlReader.ReadContentAsBoolean();
                    break;
                case "doautocure":
                    xmlReader.Read();
                    DoAutoCure = xmlReader.ReadContentAsBoolean();
                    break;
                case "autowearcomplect":
                    xmlReader.Read();
                    AutoWearComplect = xmlReader.ReadContentAsString();
                    break;

                case "sound":
                    Sound.Enabled = (xmlReader["enabled"] == null) || Convert.ToBoolean(xmlReader["enabled"], CultureInfo.InvariantCulture);
                    Sound.DoPlayAlarm = (xmlReader["alarm"] == null) || Convert.ToBoolean(xmlReader["alarm"], CultureInfo.InvariantCulture);
                    Sound.DoPlayAttack = (xmlReader["attack"] == null) || Convert.ToBoolean(xmlReader["attack"], CultureInfo.InvariantCulture);
                    Sound.DoPlayDigits = (xmlReader["digits"] == null) || Convert.ToBoolean(xmlReader["digits"], CultureInfo.InvariantCulture);
                    Sound.DoPlayRefresh = (xmlReader["refresh"] == null) || Convert.ToBoolean(xmlReader["refresh"], CultureInfo.InvariantCulture);
                    Sound.DoPlaySndMsg = (xmlReader["sndmsg"] == null) || Convert.ToBoolean(xmlReader["sndmsg"], CultureInfo.InvariantCulture);
                    Sound.DoPlayTimer = (xmlReader["timer"] == null) || Convert.ToBoolean(xmlReader["timer"], CultureInfo.InvariantCulture);
                    break;

                case "autoadv":
                    AutoAdv.Sec = (xmlReader["sec"] != null) ? Convert.ToInt32(xmlReader["sec"], CultureInfo.InvariantCulture) : 600;
                    AutoAdv.Phraz = HelperPacks.UnpackString(xmlReader["phraz"] ?? string.Empty);
                    break;

                case "autodrinkblaz":
                    DoAutoDrinkBlaz = (xmlReader["do"] != null) && Convert.ToBoolean(xmlReader["do"], CultureInfo.InvariantCulture);
                    AutoDrinkBlazTied = (xmlReader["tied"] != null) ? Convert.ToInt32(xmlReader["tied"], CultureInfo.InvariantCulture) : 84;
                    break;

                case "autodrinkblazorder":
                    xmlReader.Read();
                    AutoDrinkBlazOrder = xmlReader.ReadContentAsInt();
                    if ((AutoDrinkBlazOrder < 0) || (AutoDrinkBlazOrder > 1))
                        AutoDrinkBlazOrder = 0;
                    break;

                case "fastactions":
                    DoShowFastAttack = (xmlReader["simple"] != null) && Convert.ToBoolean(xmlReader["simple"], CultureInfo.InvariantCulture);
                    DoShowFastAttackBlood = (xmlReader["blood"] == null) || Convert.ToBoolean(xmlReader["blood"], CultureInfo.InvariantCulture);
                    DoShowFastAttackUltimate = (xmlReader["ultimate"] == null) || Convert.ToBoolean(xmlReader["ultimate"], CultureInfo.InvariantCulture);
                    DoShowFastAttackClosedUltimate = (xmlReader["closedultimate"] == null) || Convert.ToBoolean(xmlReader["closedultimate"], CultureInfo.InvariantCulture);
                    DoShowFastAttackClosed = (xmlReader["closed"] == null) || Convert.ToBoolean(xmlReader["closed"], CultureInfo.InvariantCulture);
                    DoShowFastAttackFist = (xmlReader["fist"] != null) && Convert.ToBoolean(xmlReader["fist"], CultureInfo.InvariantCulture);
                    DoShowFastAttackClosedFist = (xmlReader["closedfist"] == null) || Convert.ToBoolean(xmlReader["closedfist"], CultureInfo.InvariantCulture);
                    DoShowFastAttackOpenNevid = (xmlReader["opennevid"] == null) || Convert.ToBoolean(xmlReader["opennevid"], CultureInfo.InvariantCulture);
                    DoShowFastAttackPoison = (xmlReader["poison"] == null) || Convert.ToBoolean(xmlReader["poison"], CultureInfo.InvariantCulture);
                    DoShowFastAttackStrong = (xmlReader["strong"] == null) || Convert.ToBoolean(xmlReader["strong"], CultureInfo.InvariantCulture);
                    DoShowFastAttackNevid = (xmlReader["nevid"] == null) || Convert.ToBoolean(xmlReader["nevid"], CultureInfo.InvariantCulture);
                    DoShowFastAttackFog = (xmlReader["fog"] == null) || Convert.ToBoolean(xmlReader["fog"], CultureInfo.InvariantCulture);
                    DoShowFastAttackZas = (xmlReader["zas"] == null) || Convert.ToBoolean(xmlReader["zas"], CultureInfo.InvariantCulture);
                    DoShowFastAttackTotem = (xmlReader["totem"] == null) || Convert.ToBoolean(xmlReader["totem"], CultureInfo.InvariantCulture);
                    DoShowFastAttackPortal = (xmlReader["portal"] == null) || Convert.ToBoolean(xmlReader["portal"], CultureInfo.InvariantCulture);
                    break;

                case AppConsts.TagLezDoAutoboi:
                    xmlReader.Read();
                    LezDoAutoboi = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagLezDoWaitHp:
                    xmlReader.Read();
                    LezDoWaitHp = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagLezDoWaitMa:
                    xmlReader.Read();
                    LezDoWaitMa = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagLezWaitHp:
                    xmlReader.Read();
                    LezWaitHp = xmlReader.ReadContentAsInt();
                    break;

                case AppConsts.TagLezWaitMa:
                    xmlReader.Read();
                    LezWaitMa = xmlReader.ReadContentAsInt();
                    break;

                case AppConsts.TagLezDoDrinkHp:
                    xmlReader.Read();
                    LezDoDrinkHp = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagLezDoDrinkMa:
                    xmlReader.Read();
                    LezDoDrinkMa = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagLezDrinkHp:
                    xmlReader.Read();
                    LezDrinkHp = xmlReader.ReadContentAsInt();
                    break;

                case AppConsts.TagLezDrinkMa:
                    xmlReader.Read();
                    LezDrinkMa = xmlReader.ReadContentAsInt();
                    break;

                case AppConsts.TagLezDoWinTimeout:
                    xmlReader.Read();
                    LezDoWinTimeout = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagLezSay:
                    xmlReader.Read();
                    LezSay = (LezSayType)Enum.Parse(typeof(LezSayType), xmlReader.ReadContentAsString());
                    break;

                case AppConsts.TagLezBotsGroup:
                    var group = new LezBotsGroup(001, 0);
                    int.TryParse(xmlReader[AppConsts.AttrLezBotsGroupId] ?? "0", out group.Id);
                    int.TryParse(xmlReader[AppConsts.AttrLezBotsMinimalLevelId] ?? "0", out group.MinimalLevel);

                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoRestoreHp] ?? "true", out group.DoRestoreHp);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoRestoreMa] ?? "true", out group.DoRestoreMa);
                    int.TryParse(xmlReader[AppConsts.AttrLezBotsRestoreHp] ?? "50", out group.RestoreHp);
                    int.TryParse(xmlReader[AppConsts.AttrLezBotsRestoreMa] ?? "50", out group.RestoreMa);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoAbilBlocks] ?? "true", out group.DoAbilBlocks);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoAbilHits] ?? "true", out group.DoAbilHits);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoMagHits] ?? "true", out group.DoMagHits);
                    int.TryParse(xmlReader[AppConsts.AttrLezBotsMagHits] ?? "5", out group.MagHits);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoMagBlocks] ?? "false", out group.DoMagBlocks);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoHits] ?? "true", out group.DoHits);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoBlocks] ?? "true", out group.DoBlocks);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoMiscAbils] ?? "true", out group.DoMiscAbils);

                    group.SpellsHits = LezSpellCollection.SpellsFromString(xmlReader[AppConsts.AttrLezBotsSpellsHits] ?? "");
                    group.SpellsBlocks = LezSpellCollection.SpellsFromString(xmlReader[AppConsts.AttrLezBotsSpellsBlocks] ?? "");
                    group.SpellsRestoreHp = LezSpellCollection.SpellsFromString(xmlReader[AppConsts.AttrLezBotsSpellsRestoreHp] ?? "");
                    group.SpellsRestoreMa = LezSpellCollection.SpellsFromString(xmlReader[AppConsts.AttrLezBotsSpellsRestoreMa] ?? "");
                    group.SpellsMisc = LezSpellCollection.SpellsFromString(xmlReader[AppConsts.AttrLezBotsSpellsMisc] ?? "");

                    if (group.SpellsHits.Length == 0)
                        group.SpellsHits = LezSpellCollection.Hits;

                    if (group.SpellsBlocks.Length == 0)
                        group.SpellsBlocks = LezSpellCollection.Blocks;

                    if (group.SpellsRestoreHp.Length == 0)
                        group.SpellsRestoreHp = LezSpellCollection.RestoreHp;

                    if (group.SpellsRestoreMa.Length == 0)
                        group.SpellsRestoreMa = LezSpellCollection.RestoreMa;

                    if (group.SpellsMisc.Length == 0)
                        group.SpellsMisc = LezSpellCollection.Misc;

                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoStopNow] ?? "false", out group.DoStopNow);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoStopLowHp] ?? "false", out group.DoStopLowHp);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoStopLowMa] ?? "false", out group.DoStopLowMa);
                    int.TryParse(xmlReader[AppConsts.AttrLezBotsStopLowHp] ?? "25", out group.StopLowHp);
                    int.TryParse(xmlReader[AppConsts.AttrLezBotsStopLowMa] ?? "25", out group.StopLowMa);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoExit] ?? "false", out group.DoExit);
                    bool.TryParse(xmlReader[AppConsts.AttrLezBotsDoExitRisky] ?? "true", out group.DoExitRisky);

                    LezGroups.Add(group);
                    break;

                case AppConsts.TagDoContactTrace:
                    xmlReader.Read();
                    DoContactTrace = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagDoBossTrace:
                    xmlReader.Read();
                    DoBossTrace = xmlReader.ReadContentAsBoolean();
                    break;

                case AppConsts.TagBossSay:
                    xmlReader.Read();
                    BossSay = (LezSayType)Enum.Parse(typeof(LezSayType), xmlReader.ReadContentAsString());
                    break;

                case AppConsts.TagSkinAuto:
                    xmlReader.Read();
                    SkinAuto = xmlReader.ReadContentAsBoolean();
                    break;
            }
        }

        private void ReadUser(XmlReader xmlReader)
        {
            UserNick = xmlReader[ConstAttibuteUserNick] ?? string.Empty;
            UserPassword = xmlReader[ConstAttibuteUserPassword] ?? string.Empty;
            UserKey = xmlReader[ConstAttibuteUserKey] ?? string.Empty;
            UserPasswordFlash = xmlReader[ConstAttibuteUserPasswordFlash] ?? string.Empty;
            EncryptedUserPassword = xmlReader[ConstAttibuteEncryptedUserPassword] ?? string.Empty;
            EncryptedUserPasswordFlash = xmlReader[ConstEncryptedUserPasswordFlash] ?? string.Empty;
            ConfigHash = xmlReader[ConstAttibuteConfigHash] ?? string.Empty;

            long lastlogon;
            if (!long.TryParse(xmlReader[ConstAttibuteConfigLastSaved], out lastlogon))
            {
                lastlogon = DateTime.Now.Ticks;
            }

            ConfigLastSaved = lastlogon;

            bool autologon;
            if (!bool.TryParse(xmlReader[ConstAttibuteUserAutoLogon], out autologon))
            {
                autologon = ConstUserAutoLogonDefault;
            }

            UserAutoLogon = autologon;

            bool dopromptexit;
            if (!bool.TryParse(xmlReader[ConstAttibuteDoPromptExit], out dopromptexit))
            {
                dopromptexit = ConstDoPromptExitDefault;
            }

            DoPromptExit = dopromptexit;

            bool guamod;
            if (!bool.TryParse(xmlReader[ConstAttibuteDoGuamod], out guamod))
            {
                guamod = ConstDoGuamodDefault;
            }

            DoGuamod = guamod;
        }

        private void ReadProxy(XmlReader xmlReader)
        {
            bool active;
            if (!bool.TryParse(xmlReader[ConstAttibuteDoProxy], out active))
            {
                active = ConstDoProxyDefault;
            }

            DoProxy = active;

            ProxyAddress = xmlReader[ConstAttibuteProxyAddress] ?? string.Empty;
            ProxyUserName = xmlReader[ConstAttibuteProxyUserName] ?? string.Empty;
            ProxyPassword = xmlReader[ConstAttibuteProxyPassword] ?? string.Empty;            
        }

        private void ReadMap(XmlReader xmlReader)
        {
            bool showextend;
            if (!bool.TryParse(xmlReader[ConstAttibuteMapShowExtend], out showextend))
            {
                showextend = ConstMapShowExtendDefault;
            }

            MapShowExtend = showextend;

            int bigwidth;
            if (!int.TryParse(xmlReader[ConstAttibuteMapBigWidth], out bigwidth))
            {
                bigwidth = ConstMapBigWidthDedault;
            }

            if ((bigwidth % 2) == 0)
            {
                bigwidth = ConstMapBigWidthDedault;    
            }

            MapBigWidth = bigwidth < AppConsts.MapBigWidthMin || bigwidth > AppConsts.MapBigWidthMax ? ConstMapBigWidthDedault : bigwidth;

            int bigheight;
            if (!int.TryParse(xmlReader[ConstAttibuteMapBigHeight], out bigheight))
            {
                bigheight = ConstMapBigHeightDedault;
            }

            if ((bigheight % 2) == 0)
            {
                bigheight = ConstMapBigHeightDedault;
            }

            MapBigHeight = bigheight < AppConsts.MapBigHeightMin || bigheight > AppConsts.MapBigHeightMax ? ConstMapBigHeightDedault : bigheight;

            int bigscale;
            if (!int.TryParse(xmlReader[ConstAttibuteMapBigScale], out bigscale))
            {
                bigscale = ConstMapBigScaleDedault;
            }

            if ((bigscale % 10) != 0)
            {
                bigscale = ConstMapBigScaleDedault;
            }

            MapBigScale = bigscale < AppConsts.MapBigScaleMin || bigscale > AppConsts.MapBigScaleMax ? ConstMapBigScaleDedault : bigscale;

            int bigtransparency;
            if (!int.TryParse(xmlReader[ConstAttibuteMapBigTransparency], out bigtransparency))
            {
                bigtransparency = ConstMapBigTransparencyDedault;
            }

            if ((bigtransparency % 5) != 0)
            {
                bigtransparency = ConstMapBigTransparencyDedault;
            }

            MapBigTransparency = bigtransparency < AppConsts.MapBigTransparencyMin || bigtransparency > AppConsts.MapBigTransparencyMax ? ConstMapBigTransparencyDedault : bigtransparency;

            bool showbackcolorwhite;
            if (!bool.TryParse(xmlReader[ConstAttibuteMapShowBackColorWhite], out showbackcolorwhite))
            {
                showbackcolorwhite = ConstMapShowBackColorWhiteDedault;
            }

            MapShowBackColorWhite = showbackcolorwhite;

            bool showminimap;
            if (!bool.TryParse(xmlReader[ConstAttibuteMapShowMiniMap], out showminimap))
            {
                showminimap = ConstMapShowMiniMapDedault;
            }

            MapShowMiniMap = showminimap;

            int miniwidth;
            if (!int.TryParse(xmlReader[ConstAttibuteMapMiniWidth], out miniwidth))
            {
                miniwidth = ConstMapMiniWidthDedault;
            }

            if ((miniwidth % 2) == 0)
            {
                miniwidth = ConstMapMiniWidthDedault;
            }

            MapMiniWidth = miniwidth < AppConsts.MapMiniWidthMin || miniwidth > AppConsts.MapMiniWidthMax ? ConstMapMiniWidthDedault : miniwidth;

            int miniheight;
            if (!int.TryParse(xmlReader[ConstAttibuteMapMiniHeight], out miniheight))
            {
                miniheight = ConstMapMiniHeightDedault;
            }

            if ((miniheight % 2) == 0)
            {
                miniheight = ConstMapMiniHeightDedault;
            }

            MapMiniHeight = miniheight < AppConsts.MapMiniHeightMin || miniheight > AppConsts.MapMiniHeightMax ? ConstMapMiniHeightDedault : miniheight;

            int miniscale;
            if (!int.TryParse(xmlReader[ConstAttibuteMapMiniScale], out miniscale))
            {
                miniscale = ConstMapMiniScaleDedault;
            }

            if ((miniscale % 10) != 0)
            {
                miniscale = ConstMapMiniScaleDedault;
            }

            MapMiniScale = miniscale < AppConsts.MapMiniScaleMin || miniscale > AppConsts.MapMiniScaleMax ? ConstMapMiniScaleDedault : miniscale;

            bool drawRegion;
            if (!bool.TryParse(xmlReader[ConstAttibuteMapDrawRegion], out drawRegion))
            {
                drawRegion = ConstMapDrawRegionDefault;
            }

            MapDrawRegion = drawRegion;

            MapLocation = xmlReader[ConstAttibuteMapLocation] ?? string.Empty;
        }

        private void ReadCure(XmlReader xmlReader)
        {
            int nv1;
            if (!int.TryParse(xmlReader[ConstAttibuteCureNVOne], out nv1))
            {
                nv1 = ConstCureNVOneDefault;
            }

            if (nv1 < ConstCureNVOneMin || nv1 > ConstCureNVOneMax)
            {
                nv1 = ConstCureNVOneDefault;
            }

            CureNV[0] = nv1;

            int nv2;
            if (!int.TryParse(xmlReader[ConstAttibuteCureNVTwo], out nv2))
            {
                nv2 = ConstCureNVOneDefault;
            }

            if (nv2 < ConstCureNVTwoMin || nv2 > ConstCureNVTwoMax)
            {
                nv2 = ConstCureNVTwoDefault;
            }

            CureNV[1] = nv2;

            int nv3;
            if (!int.TryParse(xmlReader[ConstAttibuteCureNVThree], out nv3))
            {
                nv3 = ConstCureNVThreeDefault;
            }

            if (nv3 < ConstCureNVThreeMin || nv3 > ConstCureNVThreeMax)
            {
                nv3 = ConstCureNVThreeDefault;
            }

            CureNV[2] = nv3;

            int nv4;
            if (!int.TryParse(xmlReader[ConstAttibuteCureNVFour], out nv4))
            {
                nv4 = ConstCureNVFourDefault;
            }

            if (nv4 < ConstCureNVFourMin || nv4 > ConstCureNVFourMax)
            {
                nv4 = ConstCureNVFourDefault;
            }

            CureNV[3] = nv4;

            CureAsk[0] = xmlReader[ConstAttibuteCureAskOne] ?? ConstCureAskOneDefault;
            CureAsk[1] = xmlReader[ConstAttibuteCureAskTwo] ?? ConstCureAskTwoDefault;
            CureAsk[2] = xmlReader[ConstAttibuteCureAskThree] ?? ConstCureAskThreeDefault;
            CureAsk[3] = xmlReader[ConstAttibuteCureAskFour] ?? ConstCureAskFourDefault;

            bool e1;
            if (!bool.TryParse(xmlReader[ConstAttibuteCureEnabledOne], out e1))
            {
                e1 = ConstCureEnabledOneDefault;
            }

            CureEnabled[0] = e1;

            bool e2;
            if (!bool.TryParse(xmlReader[ConstAttibuteCureEnabledTwo], out e2))
            {
                e2 = ConstCureEnabledTwoDefault;
            }

            CureEnabled[1] = e2;

            bool e3;
            if (!bool.TryParse(xmlReader[ConstAttibuteCureEnabledThree], out e3))
            {
                e3 = ConstCureEnabledThreeDefault;
            }

            CureEnabled[2] = e3;

            bool e4;
            if (!bool.TryParse(xmlReader[ConstAttibuteCureEnabledFour], out e4))
            {
                e4 = ConstCureEnabledFourDefault;
            }

            CureEnabled[3] = e4;

            CureAdv = xmlReader[ConstAttibuteCureAdv] ?? ConstCureAdvDefault;
            CureAfter = xmlReader[ConstAttibuteCureAfter] ?? ConstCureAfterDefault;
            CureBoi = xmlReader[ConstAttibuteCureBoi] ?? ConstCureBoiDefault;

            bool disabled04;
            if (!bool.TryParse(xmlReader[ConstAttibuteCureDisabledLowLevels], out disabled04))
            {
                disabled04 = ConstCureDisabledLowLevelsDefault;
            }

            CureDisabledLowLevels = disabled04;
        }

        private void ReadAutoAnswer(XmlReader xmlReader)
        {
            bool active;
            if (!bool.TryParse(xmlReader[ConstAttibuteDoAutoAnswer], out active))
            {
                active = ConstDoAutoAnswerDefault;
            }

            DoAutoAnswer = active;
            if (xmlReader[ConstAttibuteAutoAnswer] != null)
            {
                AutoAnswer = xmlReader[ConstAttibuteAutoAnswer];
            }

            AutoAnswerMachine.SetAnswers(AutoAnswer);
        }

        private void ReadFish(XmlReader xmlReader)
        {
            int tiedHigh;
            if (!int.TryParse(xmlReader[ConstAttibuteFishTiedHigh], out tiedHigh))
            {
                tiedHigh = ConstFishTiedHighDefault;
            }

            FishTiedHigh = tiedHigh;

            bool tiedZero;
            if (!bool.TryParse(xmlReader[ConstAttibuteFishTiedZero], out tiedZero))
            {
                tiedZero = ConstFishTiedZeroDefault;
            }

            FishTiedZero = tiedZero;

            bool stopOverWeight;
            if (!bool.TryParse(xmlReader[ConstAttibuteFishStopOverWeight], out stopOverWeight))
            {
                stopOverWeight = ConstFishStopOverWeightDefault;
            }

            FishStopOverWeight = stopOverWeight;

            bool autoWear;
            if (!bool.TryParse(xmlReader[ConstAttibuteFishAutoWear], out autoWear))
            {
                autoWear = ConstFishAutoWearDefault;
            }

            FishAutoWear = autoWear;

            FishHandOne = xmlReader[ConstAttibuteFishHandOne] ?? ConstFishHandOneDefault;
            FishHandTwo = xmlReader[ConstAttibuteFishHandTwo] ?? string.Empty;

            int enabledPrims;
            FishEnabledPrims = !int.TryParse(xmlReader[ConstAttibuteFishEnabledPrims], out enabledPrims)
                                   ? ConstFishEnabledPrimsDefault
                                   : (Prims) enabledPrims;

            int fishUm;
            if (!int.TryParse(xmlReader[ConstAttibuteFishUm], out fishUm))
            {
                fishUm = ConstFishUmDefault;    
            }

            FishUm = fishUm;

            int fishMaxLevelBots;
            if (!int.TryParse(xmlReader[ConstAttibuteFishMaxLevelBots], out fishMaxLevelBots))
            {
                fishMaxLevelBots = ConstFishMaxLevelBotsDefault;
            }

            FishMaxLevelBots = fishMaxLevelBots;

            bool fishChatReport;
            if (!bool.TryParse(xmlReader[ConstAttibuteFishChatReport], out fishChatReport))
            {
                fishChatReport = ConstFishChatReportDefault;
            }

            FishChatReport = fishChatReport;

            bool fishChatReportColor;
            if (!bool.TryParse(xmlReader[ConstAttibuteFishChatReportColor], out fishChatReportColor))
            {
                fishChatReportColor = ConstFishChatReportColorDefault;
            }

            FishChatReportColor = fishChatReportColor;

            bool fishAuto;
            if (!bool.TryParse(xmlReader[ConstAttibuteFishAuto], out fishAuto))
            {
                fishAuto = ConstFishAutoDefault;
            }

            FishAuto = fishAuto;

            bool razdChatReport;
            if (!bool.TryParse(xmlReader[ConstAttibuteRazdChatReport], out razdChatReport))
            {
                razdChatReport = ConstRazdChatReportDefault;
            }

            RazdChatReport = razdChatReport;
        }

        private void ReadChat(XmlReader xmlReader)
        {
            bool chatKeepLog;
            if (!bool.TryParse(xmlReader[ConstAttibuteChatKeepGame], out chatKeepLog))
            {
                chatKeepLog = ConstChatKeepGameDefault;
            }

            ChatKeepLog = chatKeepLog;
            ChatKeepLog = (xmlReader["keeplog"] == null) || Convert.ToBoolean(xmlReader["keeplog"], CultureInfo.InvariantCulture);
            ChatKeepGame = (xmlReader["keepgame"] == null) || Convert.ToBoolean(xmlReader["keepgame"], CultureInfo.InvariantCulture);
            ChatKeepMoving = (xmlReader["keepmoving"] == null) || Convert.ToBoolean(xmlReader["keepmoving"], CultureInfo.InvariantCulture);
            ChatSizeLog = (xmlReader["sizelog"] == null) ? 16 : Convert.ToInt32(xmlReader["sizelog"]);
            ChatHeight = (xmlReader["height"] == null) ? 240 : Convert.ToInt32(xmlReader["height"]);
            ChatDelay = (xmlReader["delay"] == null) ? 10 : Convert.ToInt32(xmlReader["delay"]);
            ChatMode = (xmlReader["mode"] == null) ? 0 : Convert.ToInt32(xmlReader["mode"]);

            bool doChatLevels;
            if (!bool.TryParse(xmlReader[ConstAttibuteDoChatLevels], out doChatLevels))
            {
                doChatLevels = ConstDoChatLevelsDefault;
            }

            DoChatLevels = doChatLevels;
        }

        private void ReadForum(XmlReader xmlReader)
        {
            xmlReader.Read();
            LightForum = xmlReader.ReadContentAsBoolean();
        }

        private void ReadTorg(XmlReader xmlReader)
        {
            bool active;
            if (!bool.TryParse(xmlReader[ConstAttibuteTorgActive], out active))
            {
                active = ConstTorgActiveDefault;
            }

            TorgActive = active;

            TorgTabl = xmlReader[ConstAttibuteTorgTabl] ?? ConstTorgTablDefault;
            if (!TorgList.Parse(TorgTabl))
            {
                TorgTabl = ConstTorgTablDefault;
                TorgList.Parse(TorgTabl);
            }

            if (xmlReader[ConstAttibuteTorgMsgAdv] != null)
            {
                TorgMessageAdv = xmlReader[ConstAttibuteTorgMsgAdv];
                TorgList.Parse(TorgMessageAdv);
            }

            int advTime;
            if (!int.TryParse(xmlReader[ConstAttibuteTorgAdvTime], out advTime))
            {
                advTime = AppConsts.TorgAdvTimeDefault;
            }

            if (advTime < AppConsts.TorgAdvTimeMin || advTime > AppConsts.TorgAdvTimeMax)
            {
                advTime = AppConsts.TorgAdvTimeDefault;
            }

            TorgAdvTime = advTime;

            if (xmlReader[ConstAttibuteTorgMsgTooExp] != null)
            {
                TorgMessageTooExp = xmlReader[ConstAttibuteTorgMsgTooExp];
            }

            if (xmlReader[ConstAttibuteTorgMsgThanks] != null)
            {
                TorgMessageThanks = xmlReader[ConstAttibuteTorgMsgThanks];
            }

            if (xmlReader[ConstAttibuteTorgMsgNoMoney] != null)
            {
                TorgMessageNoMoney = xmlReader[ConstAttibuteTorgMsgNoMoney];
            }

            if (xmlReader[ConstAttibuteTorgMsgLess90] != null)
            {
                TorgMessageLess90 = xmlReader[ConstAttibuteTorgMsgLess90];
            }

            bool sliv;
            if (!bool.TryParse(xmlReader[ConstAttibuteTorgSliv], out sliv))
            {
                sliv = ConstTorgSlivDefault;
            }

            TorgSliv = sliv;

            int minlevel;
            if (!int.TryParse(xmlReader[ConstAttibuteTorgMinLevel], out minlevel))
            {
                minlevel = ConstTorgMinLevelDefault;
            }

            TorgMinLevel = minlevel;

            if (xmlReader[ConstAttributeTorgEx] != null)
            {
                TorgEx = xmlReader[ConstAttributeTorgEx];
            }

            if (xmlReader[ConstAttributeTorgDeny] != null)
            {
                TorgDeny = xmlReader[ConstAttributeTorgDeny];
            }
        }
    }
}
