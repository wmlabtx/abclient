using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ABClient.MyHelpers;
using ABClient.Lez;

namespace ABClient.MyProfile
{
    internal sealed partial class UserConfig
    {
        private void InternalSave()
        {
            ConfigLastSaved = DateTime.Now.Ticks;
            var xmlWriterSettings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
            XmlWriter xmlWriter = null;
            try
            {
                if (string.IsNullOrEmpty(_configFileName))
                {
                    do
                    {
                        _configFileName = Path.Combine(
                            Application.StartupPath,
                            Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + AppConsts.ProfileExtension);
                    }
                    while (File.Exists(_configFileName));
                }

                if (string.IsNullOrEmpty(_configFileNameTemp))
                {
                    _configFileNameTemp = Path.ChangeExtension(_configFileName, ConstProfileExtensionTemp);
                }

                xmlWriter = XmlWriter.Create(_configFileNameTemp, xmlWriterSettings);
                if (true)
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteComment(ConstCommentOne);
                    xmlWriter.WriteComment(ConstCommentTwo);
                    xmlWriter.WriteStartElement(ConstProfile);

                    WriteUser(xmlWriter);
                    WriteProxy(xmlWriter);
                    WriteMap(xmlWriter);
                    WriteCure(xmlWriter);
                    WriteAutoAnswer(xmlWriter);
                    WriteFish(xmlWriter);
                    WriteChat(xmlWriter);
                    WriteForum(xmlWriter);
                    WriteTorg(xmlWriter);

                    xmlWriter.WriteStartElement("window");
                    var windowState = Window.State.ToString();
                    xmlWriter.WriteStartAttribute("state");
                    xmlWriter.WriteString(windowState);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("left");
                    xmlWriter.WriteValue(Window.Left);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("top");
                    xmlWriter.WriteValue(Window.Top);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("width");
                    xmlWriter.WriteValue(Window.Width);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("height");
                    xmlWriter.WriteValue(Window.Height);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("stat");
                    xmlWriter.WriteStartAttribute("show");
                    xmlWriter.WriteValue(Stat.Show);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("lastreset");
                    xmlWriter.WriteValue(Stat.LastReset);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("lastupdateday");
                    xmlWriter.WriteValue(Stat.LastUpdateDay);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("reset");
                    xmlWriter.WriteValue(Stat.Reset);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("traffic");
                    xmlWriter.WriteValue(Stat.Traffic);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("savedtraffic");
                    xmlWriter.WriteValue(Stat.SavedTraffic);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("drop");
                    xmlWriter.WriteString(Stat.Drop ?? string.Empty);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("xp");
                    xmlWriter.WriteValue(Stat.XP);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("nv");
                    xmlWriter.WriteValue(Stat.NV);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("fishnv");
                    xmlWriter.WriteValue(Stat.FishNV);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("itemdrops");
                    for (var i = 0; i < Stat.ItemDrop.Count; i++)
                    {
                        xmlWriter.WriteStartElement("itemdrop");
                        xmlWriter.WriteStartAttribute("name");
                        xmlWriter.WriteString(Stat.ItemDrop[i].Name);
                        xmlWriter.WriteEndAttribute();
                        if (Stat.ItemDrop[i].Count > 1)
                        {
                            xmlWriter.WriteStartAttribute("count");
                            xmlWriter.WriteValue(Stat.ItemDrop[i].Count);
                            xmlWriter.WriteEndAttribute();
                        }

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("splitter");
                    xmlWriter.WriteStartAttribute("width");
                    xmlWriter.WriteValue(Splitter.Width);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("collapsed");
                    xmlWriter.WriteValue(Splitter.Collapsed);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("dopromptexit");
                    xmlWriter.WriteValue(DoPromptExit);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("showoverwarning");
                    xmlWriter.WriteValue(ShowOverWarning);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("dohttplog");
                    xmlWriter.WriteValue(DoHttpLog);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("dotexlog");
                    xmlWriter.WriteValue(DoTexLog);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("showperformance");
                    xmlWriter.WriteValue(ShowPerformance);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("selectedrightpanel");
                    xmlWriter.WriteValue(SelectedRightPanel);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("notepad");
                    xmlWriter.WriteString(HelperPacks.PackString(Notepad ?? string.Empty));
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("tabs");
                    for (var i = 0; i < Tabs.Length; i++)
                    {
                        xmlWriter.WriteStartElement("tab");
                        xmlWriter.WriteString(Tabs[i] ?? string.Empty);
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("favlocations");
                    for (var i = 0; i < FavLocations.Length; i++)
                    {
                        xmlWriter.WriteStartElement("favlocation");
                        xmlWriter.WriteString(FavLocations[i] ?? string.Empty);
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("dotray");
                    xmlWriter.WriteValue(DoTray);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("showtraybaloons");
                    xmlWriter.WriteValue(ShowTrayBaloons);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("nextcheckversion");
                    xmlWriter.WriteValue(NextCheckVersion.ToBinary());
                    xmlWriter.WriteEndElement();

                    /*
                    xmlWriter.WriteStartElement("servdiff");
                    xmlWriter.WriteString(ServDiff.ToString());
                    xmlWriter.WriteEndElement();
                    */

                    xmlWriter.WriteStartElement("doconvertrussian");
                    xmlWriter.WriteValue(DoConvertRussian);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("doautocutwritechat");
                    xmlWriter.WriteValue(DoAutoCutWriteChat);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("complects");
                    xmlWriter.WriteString(Complects);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("pers");
                    xmlWriter.WriteStartAttribute("guamod");
                    xmlWriter.WriteValue(Pers.Guamod);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("inthp");
                    xmlWriter.WriteValue(Pers.IntHP);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("intma");
                    xmlWriter.WriteValue(Pers.IntMA);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("ready");
                    xmlWriter.WriteValue(Pers.Ready);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("logready");
                    xmlWriter.WriteString(Pers.LogReady ?? string.Empty);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("autodrinkblaz");
                    xmlWriter.WriteStartAttribute("do");
                    xmlWriter.WriteValue(DoAutoDrinkBlaz);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("tied");
                    xmlWriter.WriteValue(AutoDrinkBlazTied);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("navigator");
                    xmlWriter.WriteStartAttribute("allowteleports");
                    xmlWriter.WriteValue(Navigator.AllowTeleports);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    if (HerbCells.Count > 0)
                    {
                        xmlWriter.WriteStartElement("herbcells");
                        var kvp = new KeyValuePair<string, HerbCell>[HerbCells.Count];
                        HerbCells.CopyTo(kvp, 0);
                        for (var index = 0; index < kvp.Length; index++)
                        {
                            xmlWriter.WriteStartElement("herbcell");

                            xmlWriter.WriteStartAttribute("location");
                            xmlWriter.WriteString(kvp[index].Value.RegNum);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("herbs");
                            xmlWriter.WriteString(kvp[index].Value.Herbs);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute("lastview");
                            xmlWriter.WriteValue(kvp[index].Value.UpdatedInTicks);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteEndElement();
                        }

                        xmlWriter.WriteEndElement();
                    }

                    if (HerbsAutoCut.Count > 0)
                    {
                        xmlWriter.WriteStartElement("herbsautocut");
                        for (var i = 0; i < HerbsAutoCut.Count; i++)
                        {
                            xmlWriter.WriteStartElement("herbautocut");
                            xmlWriter.WriteString(HerbsAutoCut[i] ?? string.Empty);
                            xmlWriter.WriteEndElement();
                        }

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteStartElement("dorob");
                    xmlWriter.WriteValue(DoRob);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("doautocure");
                    xmlWriter.WriteValue(DoAutoCure);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("autowearcomplect");
                    xmlWriter.WriteString(AutoWearComplect);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("dostopondig");
                    xmlWriter.WriteValue(DoStopOnDig);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("autodrinkblazorder");
                    xmlWriter.WriteValue(AutoDrinkBlazOrder);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("contacts");
                    try
                    {
                        ContactsManager.Rwl.AcquireWriterLock(5000);
                        try
                        {
                            if (Contacts == null)
                            {
                                Contacts = new SortedList<string, Contact>();
                            }

                            foreach (var contact in Contacts)
                            {
                                xmlWriter.WriteStartElement("contactentry");

                                xmlWriter.WriteStartAttribute("name");
                                xmlWriter.WriteString(contact.Value.Name ?? string.Empty);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("classid");
                                xmlWriter.WriteValue(contact.Value.ClassId);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("toolid");
                                xmlWriter.WriteValue(contact.Value.ToolId);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("sign");
                                xmlWriter.WriteString(contact.Value.Sign ?? string.Empty);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("clan");
                                xmlWriter.WriteString(contact.Value.Clan ?? string.Empty);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("align");
                                xmlWriter.WriteString(contact.Value.Align ?? string.Empty);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("comments");
                                xmlWriter.WriteString(HelperPacks.PackString(contact.Value.Comments ?? string.Empty));
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("tracing");
                                xmlWriter.WriteValue(contact.Value.Tracing);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteStartAttribute("level");
                                xmlWriter.WriteString(contact.Value.Level ?? string.Empty);
                                xmlWriter.WriteEndAttribute();

                                xmlWriter.WriteEndElement();
                            }
                        }
                        finally
                        {
                            ContactsManager.Rwl.ReleaseWriterLock();
                        }
                    }
                    catch (ApplicationException)
                    {
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("apptimers");
                    var arrayAppTimers = AppTimerManager.GetTimers();
                    foreach (var appTimer in arrayAppTimers)
                    {
                        xmlWriter.WriteStartElement("apptimer");

                        xmlWriter.WriteStartAttribute("triggertime");
                        xmlWriter.WriteValue(appTimer.TriggerTime.ToBinary());
                        xmlWriter.WriteEndAttribute();
                        if (!string.IsNullOrEmpty(appTimer.Description))
                        {
                            xmlWriter.WriteStartAttribute("description");
                            xmlWriter.WriteString(appTimer.Description);
                            xmlWriter.WriteEndAttribute();
                        }

                        if (!string.IsNullOrEmpty(appTimer.Complect))
                        {
                            xmlWriter.WriteStartAttribute("complect");
                            xmlWriter.WriteString(appTimer.Complect);
                            xmlWriter.WriteEndAttribute();
                        }

                        if (!string.IsNullOrEmpty(appTimer.Potion))
                        {
                            xmlWriter.WriteStartAttribute("potion");
                            xmlWriter.WriteString(appTimer.Potion);
                            xmlWriter.WriteEndAttribute();
                        }

                        if (appTimer.DrinkCount > 0)
                        {
                            xmlWriter.WriteStartAttribute("drinkcount");
                            xmlWriter.WriteValue(appTimer.DrinkCount);
                            xmlWriter.WriteEndAttribute();
                        }

                        if (appTimer.IsRecur)
                        {
                            xmlWriter.WriteStartAttribute("isrecur");
                            xmlWriter.WriteValue(appTimer.IsRecur);
                            xmlWriter.WriteEndAttribute();
                        }

                        if (appTimer.EveryMinutes > 0)
                        {
                            xmlWriter.WriteStartAttribute("everyminutes");
                            xmlWriter.WriteValue(appTimer.EveryMinutes);
                            xmlWriter.WriteEndAttribute();
                        }

                        if (!string.IsNullOrEmpty(appTimer.Destination))
                        {
                            xmlWriter.WriteStartAttribute("destination");
                            xmlWriter.WriteString(appTimer.Destination);
                            xmlWriter.WriteEndAttribute();
                        }

                        if (appTimer.IsHerb)
                        {
                            xmlWriter.WriteStartAttribute("isherb");
                            xmlWriter.WriteValue(appTimer.IsHerb);
                            xmlWriter.WriteEndAttribute();
                        }

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("sound");
                    xmlWriter.WriteStartAttribute("enabled");
                    xmlWriter.WriteValue(Sound.Enabled);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("alarm");
                    xmlWriter.WriteValue(Sound.DoPlayAlarm);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("attack");
                    xmlWriter.WriteValue(Sound.DoPlayAttack);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("digits");
                    xmlWriter.WriteValue(Sound.DoPlayDigits);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("refresh");
                    xmlWriter.WriteValue(Sound.DoPlayRefresh);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("sndmsg");
                    xmlWriter.WriteValue(Sound.DoPlaySndMsg);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("timer");
                    xmlWriter.WriteValue(Sound.DoPlayTimer);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("autoadv");
                    xmlWriter.WriteStartAttribute("sec");
                    xmlWriter.WriteValue(AutoAdv.Sec);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("phraz");
                    xmlWriter.WriteString(HelperPacks.PackString(AutoAdv.Phraz ?? string.Empty));
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("inv");
                    xmlWriter.WriteStartAttribute("doInvSort");
                    xmlWriter.WriteValue(DoInvSort);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("doInvPack");
                    xmlWriter.WriteValue(DoInvPack);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("doInvPackDolg");
                    xmlWriter.WriteValue(DoInvPackDolg);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("fastactions");
                    xmlWriter.WriteStartAttribute("simple");
                    xmlWriter.WriteValue(DoShowFastAttack);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("blood");
                    xmlWriter.WriteValue(DoShowFastAttackBlood);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("ultimate");
                    xmlWriter.WriteValue(DoShowFastAttackUltimate);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("closedultimate");
                    xmlWriter.WriteValue(DoShowFastAttackClosedUltimate);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("closed");
                    xmlWriter.WriteValue(DoShowFastAttackClosed);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("fist");
                    xmlWriter.WriteValue(DoShowFastAttackFist);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("closedfist");
                    xmlWriter.WriteValue(DoShowFastAttackClosedFist);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("opennevid");
                    xmlWriter.WriteValue(DoShowFastAttackOpenNevid);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("poison");
                    xmlWriter.WriteValue(DoShowFastAttackPoison);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("strong");
                    xmlWriter.WriteValue(DoShowFastAttackStrong);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("nevid");
                    xmlWriter.WriteValue(DoShowFastAttackNevid);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("fog");
                    xmlWriter.WriteValue(DoShowFastAttackFog);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("zas");
                    xmlWriter.WriteValue(DoShowFastAttackZas);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("totem");
                    xmlWriter.WriteValue(DoShowFastAttackTotem);
                    xmlWriter.WriteEndAttribute();
                    xmlWriter.WriteStartAttribute("portal");
                    xmlWriter.WriteValue(DoShowFastAttackPortal);
                    xmlWriter.WriteEndAttribute();

                    xmlWriter.WriteEndElement();

                    // Lez Autoboi

                    xmlWriter.WriteStartElement(AppConsts.TagLezDoAutoboi);
                    xmlWriter.WriteValue(LezDoAutoboi);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezDoWaitHp);
                    xmlWriter.WriteValue(LezDoWaitHp);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezDoWaitMa);
                    xmlWriter.WriteValue(LezDoWaitMa);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezWaitHp);
                    xmlWriter.WriteValue(LezWaitHp);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezWaitMa);
                    xmlWriter.WriteValue(LezWaitMa);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezDoDrinkHp);
                    xmlWriter.WriteValue(LezDoDrinkHp);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezDoDrinkMa);
                    xmlWriter.WriteValue(LezDoDrinkMa);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezDrinkHp);
                    xmlWriter.WriteValue(LezDrinkHp);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezDrinkMa);
                    xmlWriter.WriteValue(LezDrinkMa);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezDoWinTimeout);
                    xmlWriter.WriteValue(LezDoWinTimeout);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagLezSay);
                    xmlWriter.WriteString(LezSay.ToString());
                    xmlWriter.WriteEndElement();

                    if (AppVars.Profile != null && AppVars.Profile.LezGroups != null)
                    {
                        xmlWriter.WriteStartElement(AppConsts.TagLezBotsGroups);
                        foreach (var group in AppVars.Profile.LezGroups)
                        {
                            xmlWriter.WriteStartElement(AppConsts.TagLezBotsGroup);

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsGroupId);
                            xmlWriter.WriteValue(group.Id);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsMinimalLevelId);
                            xmlWriter.WriteValue(group.MinimalLevel);
                            xmlWriter.WriteEndAttribute();


                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoRestoreHp);
                            xmlWriter.WriteValue(group.DoRestoreHp);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoRestoreMa);
                            xmlWriter.WriteValue(group.DoRestoreMa);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsRestoreHp);
                            xmlWriter.WriteValue(group.RestoreHp);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsRestoreMa);
                            xmlWriter.WriteValue(group.RestoreMa);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoAbilBlocks);
                            xmlWriter.WriteValue(group.DoAbilBlocks);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoAbilHits);
                            xmlWriter.WriteValue(group.DoAbilHits);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoMagHits);
                            xmlWriter.WriteValue(group.DoMagHits);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsMagHits);
                            xmlWriter.WriteValue(group.MagHits);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoMagBlocks);
                            xmlWriter.WriteValue(group.DoMagBlocks);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoHits);
                            xmlWriter.WriteValue(group.DoHits);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoBlocks);
                            xmlWriter.WriteValue(group.DoBlocks);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoMiscAbils);
                            xmlWriter.WriteValue(group.DoMiscAbils);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsSpellsHits);
                            xmlWriter.WriteValue(LezSpellCollection.SpellsToString(group.SpellsHits));
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsSpellsBlocks);
                            xmlWriter.WriteValue(LezSpellCollection.SpellsToString(group.SpellsBlocks));
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsSpellsRestoreHp);
                            xmlWriter.WriteValue(LezSpellCollection.SpellsToString(group.SpellsRestoreHp));
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsSpellsRestoreMa);
                            xmlWriter.WriteValue(LezSpellCollection.SpellsToString(group.SpellsRestoreMa));
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsSpellsMisc);
                            xmlWriter.WriteValue(LezSpellCollection.SpellsToString(group.SpellsMisc));
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoStopNow);
                            xmlWriter.WriteValue(group.DoStopNow);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoStopLowHp);
                            xmlWriter.WriteValue(group.DoStopLowHp);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoStopLowMa);
                            xmlWriter.WriteValue(group.DoStopLowMa);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsStopLowHp);
                            xmlWriter.WriteValue(group.StopLowHp);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsStopLowMa);
                            xmlWriter.WriteValue(group.StopLowMa);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoExit);
                            xmlWriter.WriteValue(group.DoExit);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteStartAttribute(AppConsts.AttrLezBotsDoExitRisky);
                            xmlWriter.WriteValue(group.DoExitRisky);
                            xmlWriter.WriteEndAttribute();

                            xmlWriter.WriteEndElement();
                        }

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteStartElement(AppConsts.TagDoContactTrace);
                    xmlWriter.WriteValue(DoContactTrace);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagDoBossTrace);
                    xmlWriter.WriteValue(DoBossTrace);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagBossSay);
                    xmlWriter.WriteString(BossSay.ToString());
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement(AppConsts.TagSkinAuto);
                    xmlWriter.WriteValue(SkinAuto);
                    xmlWriter.WriteEndElement();

                    // Запись хвоста конфигурации

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush();
                    xmlWriter.Close();
                    xmlWriter = null;

                    if (File.Exists(_configFileName))
                    {
                        File.Delete(_configFileName);
                    }

                    File.Move(_configFileNameTemp, _configFileName);
                }
            }
            catch (IOException ex)
            {
                ConfigSaveError(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                ConfigSaveError(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                ConfigSaveError(ex.Message);
            }
            catch (ArgumentException ex)
            {
                ConfigSaveError(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                ConfigSaveError(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                ConfigSaveError(ex.Message);
            }
            finally
            {
                if (xmlWriter != null)
                {
                    xmlWriter.Close();
                }
            }          
        }

        /// <summary>
        /// Сохраняет файл конфирурации
        /// </summary>
        internal void Save()
        {
            try
            {
                UserConfigLock.AcquireWriterLock(5000);
                try
                {
                    InternalSave();
                }
                finally
                {
                    UserConfigLock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        private static void ConfigSaveError(string message)
        {
            MessageBox.Show(
                message,
                ConstErrorConfigSaveTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);            
        }

        private void WriteUser(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagUser);
            if (!string.IsNullOrEmpty(UserNick))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteUserNick);
                xmlWriter.WriteString(UserNick);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(UserKey))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteUserKey);
                xmlWriter.WriteString(UserKey);
                xmlWriter.WriteEndAttribute();
            }

            if (string.IsNullOrEmpty(ConfigHash))
            {
                // Данные в случае открытых паролей
                 
                if (!string.IsNullOrEmpty(UserPassword))
                {
                    xmlWriter.WriteStartAttribute(ConstAttibuteUserPassword);
                    xmlWriter.WriteString(UserPassword);
                    xmlWriter.WriteEndAttribute();
                }

                if (!string.IsNullOrEmpty(UserPasswordFlash))
                {
                    xmlWriter.WriteStartAttribute(ConstAttibuteUserPasswordFlash);
                    xmlWriter.WriteString(UserPasswordFlash);
                    xmlWriter.WriteEndAttribute();
                }
            }
            else
            {
                // Данные в случае шифрованных паролей
                 
                xmlWriter.WriteStartAttribute(ConstAttibuteConfigHash);
                xmlWriter.WriteString(ConfigHash);
                xmlWriter.WriteEndAttribute();

                if (!string.IsNullOrEmpty(EncryptedUserPassword))
                {
                    xmlWriter.WriteStartAttribute(ConstAttibuteEncryptedUserPassword);
                    xmlWriter.WriteString(EncryptedUserPassword);
                    xmlWriter.WriteEndAttribute();
                }

                if (!string.IsNullOrEmpty(EncryptedUserPasswordFlash))
                {
                    xmlWriter.WriteStartAttribute(ConstEncryptedUserPasswordFlash);
                    xmlWriter.WriteString(EncryptedUserPasswordFlash);
                    xmlWriter.WriteEndAttribute();
                }
            }

            if (UserAutoLogon != ConstUserAutoLogonDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteUserAutoLogon);
                xmlWriter.WriteValue(UserAutoLogon);
                xmlWriter.WriteEndAttribute();
            }

            if (DoPromptExit != ConstDoPromptExitDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteDoPromptExit);
                xmlWriter.WriteValue(DoPromptExit);
                xmlWriter.WriteEndAttribute();
            }

            if (DoGuamod != ConstDoGuamodDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteDoGuamod);
                xmlWriter.WriteValue(DoGuamod);
                xmlWriter.WriteEndAttribute();
            }

            xmlWriter.WriteStartAttribute(ConstAttibuteConfigLastSaved);
            xmlWriter.WriteValue(ConfigLastSaved);
            xmlWriter.WriteEndAttribute();
            xmlWriter.WriteEndElement();
        }

        private void WriteProxy(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagProxy);

            if (DoProxy != ConstDoProxyDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteDoProxy);
                xmlWriter.WriteValue(DoProxy);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(ProxyAddress))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteProxyAddress);
                xmlWriter.WriteString(ProxyAddress);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(ProxyUserName))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteProxyUserName);
                xmlWriter.WriteString(ProxyUserName);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(ProxyPassword))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteProxyPassword);
                xmlWriter.WriteString(ProxyPassword);
                xmlWriter.WriteEndAttribute();
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteMap(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagMap);

            if (MapShowExtend != ConstMapShowExtendDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapShowExtend);
                xmlWriter.WriteValue(MapShowExtend);
                xmlWriter.WriteEndAttribute();
            }

            if (MapBigWidth != ConstMapBigWidthDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapBigWidth);
                xmlWriter.WriteValue(MapBigWidth);
                xmlWriter.WriteEndAttribute();
            }

            if (MapBigHeight != ConstMapBigHeightDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapBigHeight);
                xmlWriter.WriteValue(MapBigHeight);
                xmlWriter.WriteEndAttribute();
            }

            if (MapBigScale != ConstMapBigScaleDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapBigScale);
                xmlWriter.WriteValue(MapBigScale);
                xmlWriter.WriteEndAttribute();
            }

            if (MapBigTransparency != ConstMapBigTransparencyDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapBigTransparency);
                xmlWriter.WriteValue(MapBigTransparency);
                xmlWriter.WriteEndAttribute();
            }

            if (MapShowBackColorWhite != ConstMapShowBackColorWhiteDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapShowBackColorWhite);
                xmlWriter.WriteValue(MapShowBackColorWhite);
                xmlWriter.WriteEndAttribute();
            }

            if (MapShowMiniMap != ConstMapShowMiniMapDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapShowMiniMap);
                xmlWriter.WriteValue(MapShowMiniMap);
                xmlWriter.WriteEndAttribute();
            }

            if (MapMiniWidth != ConstMapMiniWidthDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapMiniWidth);
                xmlWriter.WriteValue(MapMiniWidth);
                xmlWriter.WriteEndAttribute();
            }

            if (MapMiniHeight != ConstMapMiniHeightDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapMiniHeight);
                xmlWriter.WriteValue(MapMiniHeight);
                xmlWriter.WriteEndAttribute();
            }

            if (MapMiniScale != ConstMapMiniScaleDedault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapMiniScale);
                xmlWriter.WriteValue(MapMiniScale);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(MapLocation))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapLocation);
                xmlWriter.WriteString(MapLocation);
                xmlWriter.WriteEndAttribute();
            }

            if (MapDrawRegion != ConstMapDrawRegionDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteMapDrawRegion);
                xmlWriter.WriteValue(MapDrawRegion);
                xmlWriter.WriteEndAttribute();
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteCure(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagCure);

            if (CureNV[0] != ConstCureNVOneDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureNVOne);
                xmlWriter.WriteValue(CureNV[0]);
                xmlWriter.WriteEndAttribute();
            }

            if (CureNV[1] != ConstCureNVTwoDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureNVTwo);
                xmlWriter.WriteValue(CureNV[1]);
                xmlWriter.WriteEndAttribute();
            }

            if (CureNV[2] != ConstCureNVThreeDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureNVThree);
                xmlWriter.WriteValue(CureNV[2]);
                xmlWriter.WriteEndAttribute();
            }

            if (CureNV[3] != ConstCureNVFourDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureNVFour);
                xmlWriter.WriteValue(CureNV[3]);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(CureAsk[0]))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureAskOne);
                xmlWriter.WriteString(CureAsk[0]);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(CureAsk[1]))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureAskTwo);
                xmlWriter.WriteString(CureAsk[1]);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(CureAsk[2]))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureAskThree);
                xmlWriter.WriteString(CureAsk[2]);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(CureAsk[3]))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureAskFour);
                xmlWriter.WriteString(CureAsk[3]);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(CureAdv))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureAdv);
                xmlWriter.WriteString(CureAdv);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(CureAfter))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureAfter);
                xmlWriter.WriteString(CureAfter);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(CureBoi))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureBoi);
                xmlWriter.WriteString(CureBoi);
                xmlWriter.WriteEndAttribute();
            }

            if (CureEnabled[0] != ConstCureEnabledOneDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureEnabledOne);
                xmlWriter.WriteValue(CureEnabled[0]);
                xmlWriter.WriteEndAttribute();
            }

            if (CureEnabled[1] != ConstCureEnabledTwoDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureEnabledTwo);
                xmlWriter.WriteValue(CureEnabled[1]);
                xmlWriter.WriteEndAttribute();
            }

            if (CureEnabled[2] != ConstCureEnabledThreeDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureEnabledThree);
                xmlWriter.WriteValue(CureEnabled[2]);
                xmlWriter.WriteEndAttribute();
            }

            if (CureEnabled[3] != ConstCureEnabledFourDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureEnabledFour);
                xmlWriter.WriteValue(CureEnabled[3]);
                xmlWriter.WriteEndAttribute();
            }

            if (CureDisabledLowLevels != ConstCureDisabledLowLevelsDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteCureDisabledLowLevels);
                xmlWriter.WriteValue(CureDisabledLowLevels);
                xmlWriter.WriteEndAttribute();
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteAutoAnswer(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagAutoAnswer);

            xmlWriter.WriteStartAttribute(ConstAttibuteDoAutoAnswer);
            xmlWriter.WriteValue(DoAutoAnswer);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteAutoAnswer);
            xmlWriter.WriteString(AutoAnswer);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteEndElement();
        }

        private void WriteFish(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagFish);

            xmlWriter.WriteStartAttribute(ConstAttibuteFishTiedHigh);
            xmlWriter.WriteValue(FishTiedHigh);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteFishTiedZero);
            xmlWriter.WriteValue(FishTiedZero);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteFishStopOverWeight);
            xmlWriter.WriteValue(FishStopOverWeight);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteFishAutoWear);
            xmlWriter.WriteValue(FishAutoWear);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteFishHandOne);
            xmlWriter.WriteString(FishHandOne);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteFishHandTwo);
            xmlWriter.WriteString(FishHandTwo);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteFishEnabledPrims);
            xmlWriter.WriteValue((int)FishEnabledPrims);
            xmlWriter.WriteEndAttribute();

            xmlWriter.WriteStartAttribute(ConstAttibuteFishUm);
            xmlWriter.WriteValue(FishUm);
            xmlWriter.WriteEndAttribute();

            if (FishMaxLevelBots != ConstFishMaxLevelBotsDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteFishMaxLevelBots);
                xmlWriter.WriteValue(FishMaxLevelBots);
                xmlWriter.WriteEndAttribute();
            }

            if (FishChatReport != ConstFishChatReportDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteFishChatReport);
                xmlWriter.WriteValue(FishChatReport);
                xmlWriter.WriteEndAttribute();
            }

            if (FishChatReportColor != ConstFishChatReportColorDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteFishChatReportColor);
                xmlWriter.WriteValue(FishChatReportColor);
                xmlWriter.WriteEndAttribute();
            }

            if (FishAuto != ConstFishAutoDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteFishAuto);
                xmlWriter.WriteValue(FishAuto);
                xmlWriter.WriteEndAttribute();
            }

            if (RazdChatReport != ConstRazdChatReportDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteRazdChatReport);
                xmlWriter.WriteValue(RazdChatReport);
                xmlWriter.WriteEndAttribute();
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteChat(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagChat);

            if (ChatKeepGame != ConstChatKeepGameDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteChatKeepGame);
                xmlWriter.WriteValue(ChatKeepGame);
                xmlWriter.WriteEndAttribute();
            }

            if (ChatKeepMoving != ConstChatKeepMovingDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteChatKeepMoving);
                xmlWriter.WriteValue(ChatKeepMoving);
                xmlWriter.WriteEndAttribute();
            }

            if (ChatKeepLog != ConstChatKeepLogDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteChatKeepLog);
                xmlWriter.WriteValue(ChatKeepLog);
                xmlWriter.WriteEndAttribute();
            }

            if (ChatSizeLog != ConstChatSizeLogDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteChatSizeLog);
                xmlWriter.WriteValue(ChatSizeLog);
                xmlWriter.WriteEndAttribute();
            }

            if (ChatHeight != ConstChatHeightDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteChatHeight);
                xmlWriter.WriteValue(ChatHeight);
                xmlWriter.WriteEndAttribute();
            }

            if (ChatDelay != ConstChatDelayDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteChatDelay);
                xmlWriter.WriteValue(ChatDelay);
                xmlWriter.WriteEndAttribute();
            }

            if (ChatMode != ConstChatModeDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteChatMode);
                xmlWriter.WriteValue(ChatMode);
                xmlWriter.WriteEndAttribute();
            }

            if (DoChatLevels != ConstDoChatLevelsDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteDoChatLevels);
                xmlWriter.WriteValue(DoChatLevels);
                xmlWriter.WriteEndAttribute();
            }

            xmlWriter.WriteEndElement();
        }

        private void WriteForum(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagLightForum);
            xmlWriter.WriteValue(LightForum);
            xmlWriter.WriteEndElement();
        }

        private void WriteTorg(XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(ConstTagTorg);

            if (TorgActive != ConstTorgActiveDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgActive);
                xmlWriter.WriteValue(TorgActive);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(TorgTabl))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgTabl);
                xmlWriter.WriteString(TorgTabl);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgMessageTooExp != null)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgMsgTooExp);
                xmlWriter.WriteString(TorgMessageTooExp);
                xmlWriter.WriteEndAttribute();
            }

            if (!string.IsNullOrEmpty(TorgMessageAdv))
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgMsgAdv);
                xmlWriter.WriteString(TorgMessageAdv);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgAdvTime != AppConsts.TorgAdvTimeDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgAdvTime);
                xmlWriter.WriteValue(TorgAdvTime);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgMessageThanks != null)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgMsgThanks);
                xmlWriter.WriteString(TorgMessageThanks);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgMessageNoMoney != null)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgMsgNoMoney);
                xmlWriter.WriteString(TorgMessageNoMoney);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgMessageLess90 != null)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgMsgLess90);
                xmlWriter.WriteString(TorgMessageLess90);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgSliv != ConstTorgSlivDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgSliv);
                xmlWriter.WriteValue(TorgSliv);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgMinLevel != ConstTorgMinLevelDefault)
            {
                xmlWriter.WriteStartAttribute(ConstAttibuteTorgMinLevel);
                xmlWriter.WriteValue(TorgMinLevel);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgEx != null)
            {
                xmlWriter.WriteStartAttribute(ConstAttributeTorgEx);
                xmlWriter.WriteString(TorgEx);
                xmlWriter.WriteEndAttribute();
            }

            if (TorgDeny != null)
            {
                xmlWriter.WriteStartAttribute(ConstAttributeTorgDeny);
                xmlWriter.WriteString(TorgDeny);
                xmlWriter.WriteEndAttribute();
            }

            xmlWriter.WriteEndElement();
        }
    }
}