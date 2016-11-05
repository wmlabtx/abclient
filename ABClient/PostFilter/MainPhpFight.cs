using ABClient.Lez;
using System;
using System.IO;
using System.Text;
using ABClient.ABForms;

namespace ABClient.PostFilter
{
    /// <summary>
    /// Разбор состояния боя.
    /// </summary>
    internal static partial class Filter
    {
        private static string MainPhpFight(string htmlFight)
        {
            if (htmlFight == null)
                throw new ArgumentNullException(nameof(htmlFight));

            //htmlFight = File.ReadAllText("f4.txt", AppVars.Codepage);
            //AppVars.ContentMainPhp = htmlFight;

            //var fight = new Fight.Module();
            var fight = new LezFight(htmlFight);

            // Даже, если автобой отключен, надо делать разбор боя (проверка на испорченный фрейм)
            //if (!fight.Parse(htmlFight))
            if (!fight.IsValid)
                return AppVars.ContentMainPhp;

            // Ожидаем хода противника?
            if (fight.IsWaitingForNextTurn && AppVars.AutoRefresh)
                return fight.Frame;

            // Автобой включен?
            if (AppVars.Profile.LezDoAutoboi)
            {
                if (fight.IsBoi)
                {
                    // Мы находимся в активном бою
                    if (!fight.DoStop && !fight.IsLowHp && !fight.IsLowMa && !fight.DoExit)
                    {
                        // Никаких сдерживающих факторов нет, продолжаем быстрый бой
                        return fight.Frame;
                    }

                    if ((fight.DoStop && fight.FoeGroup.DoExit) || fight.DoExit)
                    {
                        // Закрыть клиент !

                        var message = string.Format($"Выход из боя ({fight.FoeName})");

                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateWriteChatMsgDelegate(AppVars.MainForm.FormMainClose),
                                    message);
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                    }

                    // Продолжать бой опасно
                    if (AppVars.Autoboi != AutoboiState.Timeout)
                    {
                        try
                        {
                            var sb = new StringBuilder();
                            if (fight.IsLowHp)
                                sb.AppendFormat($"Здоровье упало ниже {fight.FoeGroup.StopLowHp}%");
                            else
                            {
                                if (fight.IsLowMa)
                                    sb.AppendFormat($"Мана упала ниже {fight.FoeGroup.StopLowMa}%");
                                else
                                    sb.AppendFormat("Опасный противник.");
                            }

                            sb.AppendFormat($" Группа <b>\"{fight.FoeGroup}\"</b>. Бой остановлен.");

                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateWriteChatMsgDelegate(AppVars.MainForm.WriteChatMsg),
                                    sb.ToString());
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }

                        AppVars.Autoboi = AutoboiState.Timeout;
                        if (!fight.LogBoi.Equals(AppVars.Profile.Pers.LogReady, StringComparison.Ordinal) ||
                            (fight.LogBoi.Equals(AppVars.Profile.Pers.LogReady, StringComparison.Ordinal) &&
                             DateTime.Now.Ticks > AppVars.Profile.Pers.Ready))
                        {
                            AppVars.Profile.Pers.LogReady = fight.LogBoi;
                            AppVars.Profile.Pers.Ready = DateTime.Now.Add(new TimeSpan(0, 15, 0)).Ticks;
                        }
                    }
                }
                else
                {
                    // Бой завершился.
                    if (AppVars.Autoboi != AutoboiState.Restoring)
                    {
                        // Возможно, мы ожидали таймаута и вот бой закончился.
                        if (AppVars.Autoboi == AutoboiState.Timeout)
                        {
                            AppVars.Profile.Pers.Ready = 0;
                            AppVars.Profile.Pers.LogReady = string.Empty;
                            AppVars.Autoboi = AutoboiState.AutoboiOn;
                        }  

                        // Нужно ли лечиться после него?
                        var newReady = fight.CalcRestoreAfterBoi();
                        if (newReady >= 60)
                        {
                            AppVars.Autoboi = AutoboiState.Restoring;
                            if (!fight.LogBoi.Equals(AppVars.Profile.Pers.LogReady, StringComparison.Ordinal) ||
                                (fight.LogBoi.Equals(AppVars.Profile.Pers.LogReady, StringComparison.Ordinal) && DateTime.Now.Ticks > AppVars.Profile.Pers.Ready))
                            {
                                AppVars.Profile.Pers.LogReady = fight.LogBoi;
                                AppVars.Profile.Pers.Ready = newReady;
                            }
                        }
                        else
                        {
                            AppVars.Profile.Pers.Ready = 0;
                            AppVars.Profile.Pers.LogReady = string.Empty;
                            AppVars.Autoboi = AutoboiState.AutoboiOn;
                            
                            if (!string.IsNullOrEmpty(AppVars.FightLink) && (AppVars.FightLink.IndexOf("????", StringComparison.Ordinal) == -1))
                            {
                                var fightLink = AppVars.FightLink;
                                AppVars.FightLink = string.Empty;
                                return BuildRedirect("Завершение боя", fightLink);
                            }
                        }
                    }
                    else
                    {
                        // Лечение выполнено?
                        if (!fight.LogBoi.Equals(AppVars.Profile.Pers.LogReady, StringComparison.Ordinal) ||
                            (fight.LogBoi.Equals(AppVars.Profile.Pers.LogReady, StringComparison.Ordinal) && DateTime.Now.Ticks > AppVars.Profile.Pers.Ready))
                        {
                            AppVars.Profile.Pers.Ready = 0;
                            AppVars.Profile.Pers.LogReady = string.Empty;
                            AppVars.Autoboi = AutoboiState.AutoboiOn;

                            if (!string.IsNullOrEmpty(AppVars.FightLink))
                            {
                                var fightLink = AppVars.FightLink;
                                AppVars.FightLink = string.Empty;
                                return BuildRedirect("Завершение боя после лечения", fightLink);
                            }
                        }
                    }
                }
            }

            return AppVars.ContentMainPhp;
        }
    }
}