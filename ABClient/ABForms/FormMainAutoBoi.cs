using System;
using ABClient.Lez;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        internal void AutoSelect()
        {
            if (!string.IsNullOrEmpty(AppVars.ContentMainPhp))
            {
                var fight = new LezFight(AppVars.ContentMainPhp);
                if (fight.IsValid)
                {                    
                    if (fight.LezCombinations.Count > 0 && fight.LezCombination != null)
                    {
                        //fight.PrintDebug();

                        var mainTop = GetFrame("main_top");
                        if (mainTop == null || mainTop.Document == null)
                            return;

                        for (var i = 0; i < fight.LezCombination.MagicFlags.Length; i++)
                        {
                            var m = string.Format($"m{i}");
                            var e = mainTop.Document.GetElementById(m);
                            if (e != null)
                            {
                                e.SetAttribute("bgColor", "#cccccc");
                            }
                        }

                        for (var i = 0; i < fight.LezCombination.MagicFlags.Length; i++)
                        {
                            var m = string.Format($"m{i}");
                            var e = mainTop.Document.GetElementById(m);
                            if (e != null)
                            {
                                if (fight.LezCombination.MagicFlags[i])
                                    e.SetAttribute("bgColor", "#cc0000");
                            }
                        }

                        for (var i = 0; i < 4; i++)
                        {
                            var u = string.Format($"u{i}");
                            var e = mainTop.Document.GetElementById(u);
                            if (e != null)
                            {
                                e.SetAttribute("SelectedIndex", fight.LezCombination.HitOps[i].ToString());
                                e.InvokeMember("onChange");
                            }

                            if (LezSpell.IsMagHit(fight.LezCombination.HitCodes[i]))
                            {
                                var mb = string.Format($"mbu{i}");
                                e = mainTop.Document.GetElementById(mb);
                                if (e != null)
                                {
                                    e.SetAttribute("Value", fight.FoeGroup.MagHits.ToString());
                                }
                            }
                        }

                        for (var i = 0; i < 4; i++)
                        {
                            var b = string.Format($"b{i}");
                            var e = mainTop.Document.GetElementById(b);
                            if (e != null)
                            {
                                e.SetAttribute("SelectedIndex", "0");
                                e.InvokeMember("onChange");
                            }
                        }

                        var bl = string.Format($"b{fight.LezCombination.BlockCombo}");
                        var el = mainTop.Document.GetElementById(bl);
                        if (el != null)
                        {
                            el.SetAttribute("SelectedIndex", fight.LezCombination.BlockOp.ToString());
                            el.InvokeMember("onChange");
                        }
                    }
                    else
                        WriteChatMsgSafe("Не выбрано ни одной правильной комбинации ударов/блоков/абилок. Измените настройки автобоя.");
                }
                else
                {
                    WriteChatMsgSafe("Фрейм боя содержит ошибки либо мы не в бою.");
                }
            }
        }

        internal void AutoTurn()
        {
            if (!string.IsNullOrEmpty(AppVars.ContentMainPhp))
            {
                var fight = new LezFight(AppVars.ContentMainPhp);
                if (fight.IsValid)
                {
                    if (fight.LezCombinations.Count > 0 && fight.LezCombination != null)
                    {
                        //fight.PrintDebug();
                        
                        var mainTop = GetFrame("main_top");
                        if (mainTop == null || mainTop.Document == null)
                            return;

                        AppVars.LastBoiTimer = DateTime.Now;
                        mainTop.Document.InvokeScript("AutoSubmit", new object[] { fight.Result });
                    }
                    else
                        WriteChatMsgSafe("Не выбрано ни одной правильной комбинации ударов/блоков/абилок. Измените настройки автобоя.");
                }
                else
                {
                    WriteChatMsgSafe("Фрейм боя содержит ошибки либо мы не в бою.");
                }
            }
        }

        internal void AutoBoi()
        {
            ChangeAutoboiState(AutoboiState.AutoboiOn);
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

        private void ChangeAutoboiState(AutoboiState state)
        {
            var previousState = AppVars.Autoboi;
            AppVars.Autoboi = state;
            var message = string.Empty;
            switch (state)
            {
                case AutoboiState.AutoboiOn:
                    message = "Отключение автобоя";
                    AppVars.Profile.LezDoAutoboi = true;
                    break;
                case AutoboiState.AutoboiOff:
                    message = "Включение автобоя";
                    AppVars.Profile.LezDoAutoboi = false;
                    break;
                case AutoboiState.Restoring:
                    var lech = TimeSpan.FromTicks(AppVars.Profile.Pers.Ready - DateTime.Now.Ticks);
                    message = "Останов лечения " + MyHelpers.HelperConverters.TimeSpanToString(lech);
                    break;
                case AutoboiState.Timeout:
                    var timeout = MyHelpers.HelperConverters.TimeSpanToString(TimeSpan.FromTicks(AppVars.Profile.Pers.Ready - DateTime.Now.Ticks));
                    message = "Останов ожидания " + timeout;
                    break;
                case AutoboiState.Guamod:
                    message = "Останов расчета";
                    if (state != previousState)
                    {
                        WriteMessageToGuamod("Идет распознавание...");
                    }

                    break;
            }

            buttonAutoboi.Text = message;
        }

        private void ChangeButtonAutoboiState()
        {
            switch (AppVars.Autoboi)
            {
                case AutoboiState.Guamod:
                case AutoboiState.Restoring:
                case AutoboiState.Timeout:
                case AutoboiState.AutoboiOn:
                    ChangeAutoboiState(AutoboiState.AutoboiOff);
                    AppVars.Profile.Pers.Ready = 0;
                    AppVars.Profile.LezDoAutoboi = false;
                    AppVars.Profile.Save();
                    if (AppVars.FastNeed && AppVars.FastId.Equals(AppConsts.Bait, StringComparison.CurrentCultureIgnoreCase))
                    {
                        FastCancelSafe();
                    }

                    break;

                case AutoboiState.AutoboiOff:
                    ChangeAutoboiState(AutoboiState.AutoboiOn);
                    AppVars.Profile.LezDoAutoboi = true;
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
               
                    break;
            }
        }
    }
}