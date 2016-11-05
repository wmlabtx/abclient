namespace ABClient.PostFilter
{
    using System;
    using System.Globalization;
    using Helpers;
    using MyHelpers;

    internal static partial class Filter
    {
        private static byte[] GameJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);

            /*
            var c = @"+fr_size+"",1,30,0"";";
            var p = html.IndexOf(c, StringComparison.OrdinalIgnoreCase);
            if (p != -1)
            {
                p += c.Length;
                html = html.Insert(p, " window.external.ChangeChatSize(fr_size);");
            }

            c = "+ChatDelay+' секунд)';";
            p = html.IndexOf(c, StringComparison.OrdinalIgnoreCase);
            if (p != -1)
            {
                p += c.Length;
                html = html.Insert(p, " window.external.ChangeChatSpeed(ChatDelay);");
            }

            html = HelperStrings.Replace(
                html,
                "var fr_size = ",
                ";",
                AppVars.Profile.ChatHeight.ToString(CultureInfo.InvariantCulture));

            html = HelperStrings.Replace(
                html,
                @"rows=""*,8,1,",
                ",1,30,0",
                AppVars.Profile.ChatHeight.ToString(CultureInfo.InvariantCulture));

             */
            html = html.Replace("*,300", "*,400");

            /*
                        html = HelperStrings.Replace(
                            html,
                            "var ChatClearSize = ",
                            ";",
                            (AppVars.Profile.ChatSizeLog * 1024).ToString(CultureInfo.InvariantCulture));

                        var pone = html.IndexOf("change_chatsetup()", StringComparison.OrdinalIgnoreCase);
                        if (pone != -1)
                        {
                            c = "ChatFyo = 0;";
                            p = html.IndexOf(c, pone, StringComparison.OrdinalIgnoreCase);
                            if (p != -1)
                            {
                                p += c.Length;
                                html = html.Insert(p, " window.external.ChangeChatMode(0);");
                            }

                            c = "ChatFyo = 1;";
                            p = html.IndexOf(c, pone, StringComparison.OrdinalIgnoreCase);
                            if (p != -1)
                            {
                                p += c.Length;
                                html = html.Insert(p, " window.external.ChangeChatMode(1);");
                            }

                            c = "ChatFyo = 2;";
                            p = html.IndexOf(c, pone, StringComparison.OrdinalIgnoreCase);
                            if (p != -1)
                            {
                                p += c.Length;
                                html = html.Insert(p, " window.external.ChangeChatMode(2);");
                            }
                        }

                        var chatfyo = string.Empty;
                        switch (AppVars.Profile.ChatMode)
                        {
                            case 1:
                                chatfyo =
                                    "ChatFyo = 1; " +
                                    "top.frames['ch_buttons'].document.FBT.fyo.value = 1; " +
                                    "top.frames['ch_buttons'].document.FBT.schat.src = 'http://image.neverlands.ru/chat/bb3_me.gif'; " +
                                    "top.frames['ch_buttons'].document.FBT.schat.alt = 'Режим чата (Показывать только личные сообщения)';";
                                break;

                            case 2:
                                chatfyo =
                                    "ChatFyo = 2; " +
                                    "top.frames['ch_buttons'].document.FBT.fyo.value = 2; " +
                                    "ch_stop_refresh(); " +
                                    "top.frames['ch_buttons'].document.FBT.schat.src = 'http://image.neverlands.ru/chat/bb3_none.gif'; " +
                                    "top.frames['ch_buttons'].document.FBT.schat.alt = 'Режим чата (Не показывать сообщения)';";
                                break;
                        }

                        c = "ChatTimerID = setTimeout('ch_refresh()', 1000);";
                        p = html.IndexOf(c, StringComparison.OrdinalIgnoreCase);
                        if (p != -1)
                        {
                            html = html.Insert(p, "ChatDelay = " + AppVars.Profile.ChatDelay + "; ");
                        }

                        p = html.IndexOf(c, StringComparison.OrdinalIgnoreCase);
                        if (p != -1)
                        {
                            p += c.Length;
                            html = html.Insert(
                                p,
                                " top.frames['ch_buttons'].document.FBT.spchat.src = 'http://image.neverlands.ru/chat/bb_'+ChatDelay+'.gif';" +
                                " top.frames['ch_buttons'].document.FBT.spchat.alt = 'Скорость обновления (раз в '+ChatDelay+' секунд)'; " +
                                chatfyo);
                        }

                        html = html.Replace("'%clan% '", "'%clan%'");
             * 
             */

            html = html.Replace("*,300", "*,400");

            html = html.Replace(
                "var ChatClearSize = 12228;",

                "var ChatClearSize=12228;" + Environment.NewLine +
                "var AutoArena = 1;" + Environment.NewLine +
                "var AutoArenaTimer = -1;" + Environment.NewLine +
                "function arenareload(now) {" + Environment.NewLine +
                "  if(!AutoArena && (AutoArenaTimer < 0 || now)) {" + Environment.NewLine +
                "    var tm = now ? 1000 : 500;" + Environment.NewLine +
                "    AutoArenaTimer = setTimeout('toprefresh('+now+')', tm);" + Environment.NewLine +
                "  }" + Environment.NewLine +
                "}" + Environment.NewLine +
                "function toprefresh(now){" + Environment.NewLine +
                "  if(AutoArenaTimer >= 0) {" + Environment.NewLine +
                "    clearTimeout(AutoArenaTimer);" + Environment.NewLine +
                "    if(!AutoArena) AutoArenaTimer = setTimeout ('toprefresh(0)', 500);" + Environment.NewLine +
                "    else AutoArenaTimer = -1;" + Environment.NewLine +
                "  }" + Environment.NewLine +
                "  if(!AutoArena || now) top.frames['main_top'].location = './main.php';" + Environment.NewLine +
                "}" + Environment.NewLine);

            return Russian.Codepage.GetBytes(html);
        }
    }
}