using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ABClient.ABForms
{
    using System;
    using System.Text;
    using MyChat;
    using MyHelpers;
    using MySounds;

    /// <summary>
    /// Работа с чатом.
    /// </summary>
    internal sealed partial class FormMain
    {
        internal static void ChangeChatSize(int size)
        {
            AppVars.Profile.ChatHeight = size;
        }

        internal static void ChangeChatSpeed(int delay)
        {
            AppVars.Profile.ChatDelay = delay;
        }

        internal static void ChangeChatMode(int mode)
        {
            AppVars.Profile.ChatMode = mode;
        }

        internal static void ChatUpdated()
        {
            Chat.LastChanged = DateTime.Now;
            Chat.Critical = false;
        }

        internal static string ChatFilter(string message)
        {
            var xpstr = HelperStrings.SubString(message, "Получено <font color=#CC0000>боевого</font> опыта: <b><font color=#CC0000>", "</font></b>.");
            if (!string.IsNullOrEmpty(xpstr))
            {
                long xp;
                if (long.TryParse(xpstr, out xp))
                {
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateXPIncDelegate(AppVars.MainForm.UpdateXPInc),
                                new object[] { xp });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }
            }

            // <font class=chattime>&nbsp;23:56:08&nbsp;</font>
            // Результат обыска бота: <B>Денежные средства «19 NV», Денежные средства «30 NV»</B>.
            // Результат обыска бота: <B>Вещь «Кинжал Разбойника»</B>.

            //message =
            //    "<font class=chattime>&nbsp;23:56:08&nbsp;</font> <font color=000000><B><font color=#CC0000>Внимание!</font> Системная информация.</B> Результат обыска бота: <B>Денежные средства «19 NV», Денежные средства «30 NV»</B>.</font><br>";

            var thingstr = HelperStrings.SubString(message, "Результат обыска бота: <B>", "</B>.");
            if (!string.IsNullOrEmpty(thingstr))
            {
                var timestr = HelperStrings.SubString(message, "<font class=chattime>&nbsp;", "&nbsp;</font> <font color=000000><B><font color=#CC0000>Внимание!</font> Системная информация.</B> Результат обыска бота: ");
                if (!string.IsNullOrEmpty(timestr))
                {
                    var thinglist = new List<string>();
                    const string pattern = @"«([^»]+)»";
                    foreach (Match match in Regex.Matches(thingstr, pattern))
                    {
                        thinglist.Add(match.ToString().Trim(new[] { '«', '»' }));
                    }

                    if (thinglist.Count > 0)
                    {
                        try
                        {
                            if (AppVars.MainForm != null)
                            {
                                AppVars.MainForm.BeginInvoke(
                                    new UpdateThingIncDelegate(AppVars.MainForm.UpdateThingInc),
                                    new object[] { timestr, thinglist });
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }                        
                    }
                }
            }

            // <font class=clchattime>&nbsp;16:45:54&nbsp;</font> >>> <SPAN alt="%Josaphina">Josaphina</SPAN>  > clan: > <SPAN alt="%Josaphina">Райская Птица</SPAN>: <font color=CC0099>+</font> 

            if (message.IndexOf("<font color=#000000><b>Системная информация.</b></font> Поединок завершён.", StringComparison.OrdinalIgnoreCase) != -1)
            {
                // Конец боя
                if (!string.IsNullOrEmpty(AppVars.LastBoiLog) &&
                    !string.IsNullOrEmpty(AppVars.LastBoiSostav) &&
                    !string.IsNullOrEmpty(AppVars.LastBoiTravm) &&
                    !string.IsNullOrEmpty(AppVars.LastBoiUron))
                {
                    var newLog =
                        "Бой" +
                        AppVars.LastBoiTravm +
                        " против " +
                        AppVars.LastBoiSostav +
                        @" завершен (<a href=http://www.neverlands.ru/logs.fcg?fid=" +
                        AppVars.LastBoiLog +
                        @" onclick=""window.open(this.href);"">лог</a> боя). Нанесено урона: <FONT color=#339900><b>" +
                        AppVars.LastBoiUron +
                        "</b></FONT>";

                    /*@" target=_blank>лог</a> боя). Нанесено урона: <FONT color=#339900><b>" +*/
                    /*@" onclick=""window.open(this.href);"">лог</a> боя). Нанесено урона: <FONT color=#339900><b>" +*/

                    message = message.Replace("Поединок завершён", newLog);
                    var pos = message.IndexOf("Получено <font color=#004BBB>магического", StringComparison.OrdinalIgnoreCase);
                    if (pos != -1)
                    {
                        const string se = "</font></b>.";
                        var spos = message.IndexOf(se, pos, StringComparison.OrdinalIgnoreCase);
                        if (spos != -1)
                        {
                            message = message.Remove(pos, spos + se.Length - pos);
                        }
                    }

                    var texlogMessage =
                        "Бой против " +
                        AppVars.LastBoiSostav +
                        @" завершен (" +
                        AppVars.LastBoiLog +
                        @")";
                    try
                    {
                        if (AppVars.MainForm != null)
                        {
                            AppVars.MainForm.BeginInvoke(
                                new UpdateTexLogDelegate(AppVars.MainForm.UpdateTexLog),
                                new object[] { texlogMessage });
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }

                    AppVars.LastBoiLog = string.Empty;
                    AppVars.LastBoiSostav = string.Empty;
                }
            }
            else
            {
                /*
                message = "<font class=prchattime>&nbsp;11:22:41&nbsp;</font> >>> <SPAN alt=\"%МасКит\">МасКит</SPAN>  > <SPAN alt=\"%Умник\">Умник</SPAN>: <font color=000000>хай</font> ";
                message = "<font class=yochattime>&nbsp;10:46:03&nbsp;</font> <SPAN>МасКит</SPAN>&nbsp;для <SPAN alt=\"Умник\">Умник</SPAN>: <font color=000000>я, а что?</font> ";
                message = "<font class=clchattime>&nbsp;16:45:54&nbsp;</font> >>> <SPAN alt=\"%Josaphina\">Josaphina</SPAN>  > clan: > <SPAN alt=\"%Умник\">Умник</SPAN>: <font color=CC0099>+</font> "; 
                 */ 

                var posSpanEnd = message.IndexOf(@""">" + AppVars.Profile.UserNick + "</SPAN>", StringComparison.OrdinalIgnoreCase);
                if (posSpanEnd != -1)
                {
                    const string strSpanStart = "<SPAN title=\"";
                    const string strSpanEnd = "\">";
                    var fromNick = HelperStrings.SubString(message, strSpanStart, strSpanEnd).TrimStart(new[] { '%' });
                    if (!fromNick.Equals(AppVars.Profile.UserNick, StringComparison.OrdinalIgnoreCase))
                    {
                        EventSounds.PlaySndMsg();
                        var istoclan = message.IndexOf(" > clan: ", StringComparison.OrdinalIgnoreCase) != -1;
                        var istopair = message.IndexOf(" > pair: ", StringComparison.OrdinalIgnoreCase) != -1;
                        if (AppVars.Profile.DoAutoAnswer)
                        {
                            var answer = "%<" + fromNick + "> " + AutoAnswerMachine.GetNextAnswer();
                            if (istoclan)
                                answer = answer.Insert(0, "%clan%");
                            else
                                if (istopair)
                                    answer = answer.Insert(0, "%pair%");

                            Chat.AddAnswer(answer);
                        }
                    }
                }

                if (AppVars.Profile.DoChatLevels)
                {
                    posSpanEnd = message.IndexOf("</SPAN>", StringComparison.OrdinalIgnoreCase);
                    if (posSpanEnd != -1)
                    {
                        var posSpanTagEnd = message.LastIndexOf('>', posSpanEnd);
                        if (posSpanTagEnd != -1)
                        {
                            var sayNick = message.Substring(posSpanTagEnd + 1, posSpanEnd - posSpanTagEnd - 1);
                            if (ChatUsersManager.Exists(sayNick))
                            {
                                var posSpanTagStart = message.LastIndexOf('<', posSpanTagEnd);
                                if (posSpanTagStart != -1)
                                {
                                    var chatUser = ChatUsersManager.GetUserData(sayNick);
                                    if (!string.IsNullOrEmpty(chatUser.Level))
                                    {
                                        message = message.Insert(posSpanEnd + "</SPAN>".Length,
                                                                 "&nbsp;[" + chatUser.Level +
                                                                 @"]<a href=""http://www.neverlands.ru/pinfo.cgi?" +
                                                                 chatUser.Nick +
                                                                 @""" onclick=""window.open(this.href);""><img src=http://image.neverlands.ru/chat/info.gif width=11 height=12 border=0 align=bottom></a>");
                                    }

                                    var sb = new StringBuilder();
                                    if (!string.IsNullOrEmpty(chatUser.Sign))
                                    {
                                        sb.Append("<img src=http://image.neverlands.ru/signs/");
                                        sb.Append(chatUser.Sign);
                                        sb.Append(@" width=15 height=12 align=bottom title=""");
                                        sb.Append(chatUser.Status);
                                        sb.Append(@""">&nbsp;");
                                    }

                                    message = message.Insert(posSpanTagStart, sb.ToString());
                                }
                            }
                        }
                    }
                }
            }

            // >>> <SPAN alt="%Not Alone">Not Alone</SPAN>  > clan: > <SPAN alt="%Not Alone">Райская Птица</SPAN>: 

            if (message.IndexOf("pair:", StringComparison.OrdinalIgnoreCase) != -1)
                message = message.Replace("<SPAN title=\"%", "<SPAN title=\"%%%");
            else
            {
                if (message.IndexOf("clan:", StringComparison.OrdinalIgnoreCase) != -1)
                    message = message.Replace("<SPAN title=\"%", "<SPAN title=\"%%");
            }

            do
            {
                var pos1 = message.IndexOf("[[[", StringComparison.Ordinal);
                if (pos1 == -1)
                    break;

                var pos2 = message.IndexOf("]]]", pos1, StringComparison.Ordinal);
                if (pos2 == -1)
                    break;

                var sorig = message.Substring(pos1 + 3, pos2 - pos1 - 3);
                string msg = string.Empty;
                if (!sorig.Contains(":"))
                {
                    msg =
                        $"<a href=http://www.neverlands.ru/logs.fcg?fid={sorig} onclick=\"window.open(this.href);\">лог</a> боя";
                }

                message = string.Concat(message.Substring(0, pos1), msg, message.Substring(pos2 + 3));
            } while (true);

            Chat.AddStringToChat(message);
            return message;
        }

        internal void WriteChatMsg(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            var msg =
                HelperErrors.Marker() +
                "<FONT color=#000000> " +
                message +
                "</FONT><BR>";
            if (AppVars.Profile.ServDiff != TimeSpan.MinValue)
            {
                var ts = DateTime.Now.Subtract(AppVars.Profile.ServDiff);
                var st = $"{ts.Hour:00}:{ts.Minute:00}:{ts.Second:00}";
                msg = "<font class=chattime>&nbsp;" + st + "&nbsp;</font>" + msg;
            }

            WriteColorMessageToChat(msg);
        }

        internal void WriteChatMsgSafe(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((MethodInvoker)(() => WriteChatMsgSafe(message)));
                return;
            }

            if (string.IsNullOrEmpty(message))
                return;

            var msg =
                HelperErrors.Marker() +
                "<FONT color=#000000> " +
                message +
                "</FONT><BR>";
            if (AppVars.Profile.ServDiff != TimeSpan.MinValue)
            {
                var ts = DateTime.Now.Subtract(AppVars.Profile.ServDiff);
                var st = $"{ts.Hour:00}:{ts.Minute:00}:{ts.Second:00}";
                msg = "<font class=chattime>&nbsp;" + st + "&nbsp;</font>" + msg;
            }

            WriteColorMessageToChat(msg);
        }

        private void WriteChatTip(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            var msg =
                @"<SPAN class=massm>&nbsp;" + "ABClient" + "&nbsp;</SPAN> " +
                "<FONT color=#000000> " +
                message +
                "</FONT><BR>";
            WriteColorMessageToChat(msg);
        }
    }
}