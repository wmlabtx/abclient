namespace ABClient.ABForms
{
    using System;
    using System.Globalization;
    using System.Text;
    using MyChat;
    using MyGuamod;
    using MyHelpers;

    internal sealed partial class FormMain
    {
        private void TimerCrap()
        {
            if (AppVars.MustReload || (DateTime.Now > AppVars.NextCheckNoConnection))
            {                
                AppVars.MustReload = false;
                LogOn();
                return;
            }

            if (AppVars.Profile.SkinAuto && DateTime.Now.Subtract(AppVars.AutoSkinLastChecked).TotalMinutes > 1.0)
            {
                AppVars.AutoSkinLastChecked = DateTime.Now;
                AppVars.AutoSkinCheckKnife = true;
            }

            // Таймеры
            UpdateTimers();

            // После лечения
            if (!string.IsNullOrEmpty(AppVars.CureNickDone))
            {
                if (browserGame.Document != null)
                {
                    if (!string.IsNullOrEmpty(AppVars.Profile.CureAfter))
                    {
                        Chat.AddAnswer("%<" + AppVars.CureNickDone + "> " + AppVars.Profile.CureAfter);
                    }
                }

                AppVars.CureNickDone = string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(AppVars.CureNickBoi))
                {
                    if (browserGame.Document != null)
                    {
                        if (!string.IsNullOrEmpty(AppVars.CureNickBoi))
                        {
                            Chat.AddAnswer("%<" + AppVars.CureNickBoi + "> " + AppVars.Profile.CureBoi);
                        }
                    }

                    AppVars.CureNickBoi = string.Empty;
                }
            }

            // Реклама
            
            if (AppVars.AdvActive)
            {
                if (DateTime.Now > AppVars.LastAdv)
                {
                    statuslabelAutoAdv.Text = @"0:00";
                    AppVars.LastAdv = DateTime.Now.AddSeconds(AppVars.Profile.AutoAdv.Sec);
                    if (AppVars.AdvArray == null)
                    {
                        AppVars.AdvArray = HelperStrings.RandomArray(AppVars.Profile.AutoAdv.Phraz);
                        AppVars.AdvIndex = 0;
                    }

                    if (AppVars.AdvArray != null)
                    {
                        if (AppVars.AdvIndex < AppVars.AdvArray.Length)
                        {
                            Chat.AddAnswer(AppVars.AdvArray[AppVars.AdvIndex]);
                        }

                        AppVars.AdvIndex++;
                        if (AppVars.AdvIndex >= AppVars.AdvArray.Length)
                        {
                            AppVars.AdvArray = null;
                        }
                    }
                }
                else
                {
                    var advdiff = AppVars.LastAdv.Subtract(DateTime.Now);
                    statuslabelAutoAdv.Text = advdiff.Minutes + @":" + advdiff.Seconds.ToString("00");
                }
            }

            // Реклама автоторга

            if (AppVars.Profile.TorgActive)
            {
                if (DateTime.Now > AppVars.LastTorgAdv)
                {
                    statuslabelTorgAdv.Text = "0:00";
                    AppVars.LastTorgAdv = DateTime.Now.AddMinutes(AppVars.Profile.TorgAdvTime);
                    Chat.AddAnswer(AppVars.Profile.TorgMessageAdv.Replace("{таблица}", AppVars.Profile.TorgTabl));
                }
                else
                {
                    var advdiff = AppVars.LastTorgAdv.Subtract(DateTime.Now);
                    statuslabelTorgAdv.Text = advdiff.Minutes + ":" + advdiff.Seconds.ToString("00");
                }
            }

            if (DateTime.Now.Subtract(AppVars.LastTied).TotalSeconds > 200)
            {
                UpdateCheckTied();
            }

            // ChangeAutoboiState(AppVars.Profile.Autoboi.Active ? AutoboiState.AutoboiOn : AutoboiState.AutoboiOff);
            ChangeAutoboiState(AppVars.Autoboi);

            // Можно вывести в чат результат разделки?
            if (DateTime.Now > AppVars.RazdelkaTime)
            {
                if (AppVars.Profile.RazdChatReport)
                {
                    var sb = new StringBuilder();
                    //sb.Append(AppVars.AppVersion.ProductShortVersion);
                    //sb.Append(": ");
                    sb.Append("Результат разделки: ");
                    for (var i = 0; i < AppVars.RazdelkaResultList.Count; i++)
                    {
                        sb.Append('«');
                        sb.Append(AppVars.RazdelkaResultList[i]);
                        sb.Append('»');
                        if (AppVars.RazdelkaResultList.Count > 1 && i < (AppVars.RazdelkaResultList.Count - 1))
                        {
                            sb.Append(", ");
                        }
                    }

                    sb.Append('.');
                    if (AppVars.RazdelkaLevelUp > 0)
                    {
                        sb.Append(" Умение «Охота» повысилось на ");
                        sb.Append(AppVars.RazdelkaLevelUp);
                        sb.Append('!');
                    }

                    Chat.AddAnswer(sb.ToString());
                }

                AppVars.RazdelkaResultList.Clear();
                AppVars.RazdelkaLevelUp = 0;
                AppVars.RazdelkaTime = DateTime.MaxValue;
            }

            // Работает ли пауза?
            if (DateTime.Now.Ticks < AppVars.Profile.Pers.Ready)
            {
                if (AppVars.Autoboi == AutoboiState.Timeout)
                {
                    if (!string.IsNullOrEmpty(AppVars.AccountError))
                    {
                        AppVars.DoPromptExit = false;
                        Close();
                    }
                    else
                    {
                        if (DateTime.Now.Subtract(AppVars.LastMainPhp).TotalSeconds > 28)
                        {
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

                            var expected =
                                HelperConverters.TimeSpanToString(
                                    new TimeSpan(AppVars.Profile.Pers.Ready - DateTime.Now.Ticks));
                            WriteChatMsg(string.Format(CultureInfo.InvariantCulture, "До таймаута боя: <b>{0}</b>", expected));
                        }
                    }
                }

                return;
            }

            // Оглядываемся...
            /*
            if (AppVars.DoHerbAutoCut)
            {
                // ["ogl","Оглядеться","e108c4be017325666d1744fd977189f4",
                if (PressOgl())
                {
                }
            }
             */ 

            // Обновление списка людей на клетке
            /*
            if (AppVars.DoShowWalkers)
            {
                var diffc = DateTime.Now.Subtract(AppVars.LastChList);
                if (diffc.TotalSeconds > 1)
                {
                    AppVars.LastChList = DateTime.Now;
                    ReloadChlistFrame();
                }
            }
             */ 

            if (AppVars.FishNoCaptchaReady)
            {
                AppVars.FishNoCaptchaReady = false;
                UpdateTexLog("Завершение рыбалки без капчи");
                EnterFishCode(string.Empty);
                return;
            }

            if (string.IsNullOrEmpty(AppVars.FightLink))
            {
                if (DateTime.Now.Subtract(AppVars.IdleTimer).TotalMinutes > 4)
                {
                    AppVars.IdleTimer = DateTime.Now;
                    ReloadMainPhpInvoke();                    
                }

                return;
            }

            // Если нераспознанной капчи нет
            if (AppVars.FightLink.IndexOf("????", StringComparison.Ordinal) == -1)
            {
                if (AppVars.Profile.LezDoAutoboi)
                {
                    AppVars.Profile.Pers.Ready = 0;
                    if (AppVars.FightLink.Length > 5)
                    {
                        UpdateTexLog("Завершение боя");
                        if (AppVars.FightLink != null)
                        {
                            if (AppVars.FightLink.IndexOf("=00000&", StringComparison.Ordinal) == -1)
                            {
                                SetMainTopInvoke(AppVars.FightLink);
                            }
                        }                        
                    }
                    else
                    {
                        UpdateTexLog("Завершение рыбалки");
                        EnterFishCode(AppVars.FightLink);
                    }

                    if (AppVars.Profile.ShowTrayBaloons && trayIcon.Visible)
                    {
                        try
                        {
                            LockBaloon.AcquireWriterLock(5000);
                            try
                            {
                                trayIcon.Visible = false;
                                trayIcon.Visible = true;
                            }
                            finally
                            {
                                LockBaloon.ReleaseWriterLock();
                            }
                        }
                        catch (ApplicationException)
                        {
                        }
                    }

                    AppVars.FightLink = string.Empty;
                    ChangeAutoboiState(AppVars.Profile.LezDoAutoboi
                                           ? AutoboiState.AutoboiOn
                                           : AutoboiState.AutoboiOff);
                }

                return;
            }

            // Все дальнейшие операции - только если включен гуамод
            if (!AppVars.Profile.DoGuamod)
            {
                if (DateTime.Now.Subtract(AppVars.IdleTimer).TotalMinutes > 4)
                {
                    AppVars.IdleTimer = DateTime.Now;
                    ReloadMainPhpInvoke();
                }

                return;
            }

            if (AppVars.Autoboi == AutoboiState.Guamod)
            {
                // Уже идет распознавание
                ChangeAutoboiState(AutoboiState.Guamod);
            }
            else
            {
                // Запуск гуамода
                if (string.IsNullOrEmpty(AppVars.CodeAddress))
                {
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
                    
                    return;
                }

                AppVars.GuamodCode = "?????";
                ChangeAutoboiState(AutoboiState.Guamod);
                Recognizer.Perform();
            }
        }

        private void TimerClock()
        {
            var clock = (AppVars.Profile.ServDiff == TimeSpan.MinValue) ? DateTime.Now : DateTime.Now.Subtract(AppVars.Profile.ServDiff);
            statuslabelClock.Text =
                clock.Hour.ToString("00") +
                ":" +
                clock.Minute.ToString("00") +
                ":" +
                clock.Second.ToString("00");
            
            var message = Chat.GetAnswer();
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            WriteMessageToChat(message);
        }

        private bool CheckCrap(TimeSpan diff)
        {
            if (AppVars.ContentMainPhp != null && 
                (AppVars.ContentMainPhp.IndexOf(HelperErrors.Marker(), StringComparison.OrdinalIgnoreCase) != -1 ||
                 AppVars.ContentMainPhp.IndexOf(HelperErrors.Head(), StringComparison.OrdinalIgnoreCase) != -1))
            {
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
                
                return true;
            }

            if (diff.TotalSeconds > 360)
            {
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

                return true;
            }

            if (diff.TotalSeconds > 420)
            {
                UpdateGame("Перезаход из-за подвисания или обрыва");
                return true;
            }

            var last = DateTime.Now.Subtract(AppVars.LastInitForm).TotalMinutes;
            try
            {
                if (browserGame.Document == null)
                {
                    if (last > 2)
                    {
                        UpdateGame("Сбой главной страницы (Document == null)");
                        return true;
                    }

                    return false;
                }

                if (browserGame.Document.All.Count == 0)
                {
                    if (last > 2)
                    {
                        UpdateGame("Сбой главной страницы (Document.All.Count == 0)");
                        return true;
                    }

                    return false;
                }

                if (browserGame.Document.Body == null)
                {
                    if (last > 2)
                    {
                        UpdateGame("Сбой главной страницы (Document.Body == null)");
                        return true;
                    }

                    return false;
                }

                if (browserGame.Document.Body.InnerHtml == null)
                {
                    if (last > 2)
                    {
                        UpdateGame("Сбой главной страницы (Document.Body.InnerHtml == null)");
                        return true;
                    }

                    return false;
                }

                var currentWindow = browserGame.Document.Window;
                if (currentWindow == null)
                {
                    if (last > 2)
                    {
                        UpdateGame("Сбой главной страницы (Document.Window == null)");
                        return true;
                    }

                    return false;
                }

                if (currentWindow.Frames == null || currentWindow.Frames.Count == 0)
                {
                    if (last > 2)
                    {
                        UpdateGame("Сбой главной страницы (Document.Window.Frames.Count == 0)");
                        return true;
                    }

                    return false;
                }

                var frame = currentWindow.Frames["main_top"];
                if (frame == null)
                {
                    if (last > 2)
                    {
                        UpdateGame("Сбой главной страницы (frame == null)");
                        return true;
                    }

                    return false;
                }

                if (frame.Document == null)
                {
                    if (last > 2)
                    {
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
                        
                        return true;
                    }

                    return false;
                }

                if (frame.Document.Body == null)
                {
                    if (last > 2)
                    {
                        AppVars.DocumentBodyNullCount++;
                        if (AppVars.DocumentBodyNullCount > 5)
                        {
                            UpdateGame("Сбой верхнего фрейма (Document.Body == null)");
                            return true;
                        }

                        UpdateTexLog("Сбой верхнего фрейма (Document.Body == null). Счетчик: " + AppVars.DocumentBodyNullCount);
                    }

                    return false;
                }

                AppVars.DocumentBodyNullCount = 0;

                if (frame.Document.All.Count == 0)
                {
                    if (last > 2)
                    {
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

                        return true;
                    }

                    return false;
                }

                if (!string.IsNullOrEmpty(AppVars.ContentMainPhp) && DateTime.Now.Subtract(AppVars.LastMainPhp).TotalSeconds > 30)
                {
                    if (AppVars.ContentMainPhp.IndexOf(HelperErrors.Marker(), StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        if (AppVars.ContentMainPhp.IndexOf(AppConsts.HttpServerError, StringComparison.Ordinal) != -1 ||
                            AppVars.ContentMainPhp.IndexOf("Connection error", StringComparison.Ordinal) != -1 ||
                            AppVars.ContentMainPhp.IndexOf("Server failure", StringComparison.Ordinal) != -1)
                        {
                            UpdateGame("Ошибка связи с сервером игры");
                        }
                        else
                        {
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

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (AppVars.MainForm != null)
                    {
                        AppVars.MainForm.BeginInvoke(
                            new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                            new object[] { "Ошибка с IE: " + ex.Message });
                    }
                }
                catch (InvalidOperationException)
                {
                }
            }

            return false;
        }
    }
}