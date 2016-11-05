using System.Collections.Generic;

namespace ABClient.ABForms
{
    using System;
    using System.Windows.Forms;

    internal delegate void UpdateServerTimeDelegate(DateTime serverDateTime);

    internal delegate void UpdateHttpLogDelegate(string message);

    internal delegate void UpdateTexLogDelegate(string message);

    internal delegate void UpdateXPIncDelegate(long exp);

    internal delegate void UpdateFishNVIncDelegate(int nvinc);

    internal delegate void UpdateThingIncDelegate(string timestr, List<string> thinglist);

    internal delegate void UpdateAccountErrorDelegate(string error);

    internal delegate void UpdateAutoboiOffDelegate();

    internal delegate void UpdateFuryOffDelegate();

    internal delegate void UpdateGameDelegate(string message);
   
    internal delegate void UpdateGuamodMessageDelegate(string message);

    internal delegate void UpdateAutoboiResetDelegate();

    internal delegate void UpdateTimersDelegate();

    internal delegate void UpdateTiedDelegate(int tied);

    internal delegate void UpdateCheckTiedDelegate();

    internal delegate void UpdateTrayBaloonDelegate(string message);

    internal delegate void UpdateTrayFlashDelegate(string message);

    internal delegate void UpdateFishOffDelegate();
    
    internal delegate void UpdateComplectsDelegate(string[] complects);

    internal delegate void UpdateRoomDelegate(ToolStripMenuItem[] tsmi, string trtext, ToolStripMenuItem[] tstr);

    internal delegate void UpdateChatDelegate(string message);

    internal delegate void UpdateContactDelegate(Contact ce);

    internal delegate void UpdateTraceDrinkPotionDelegate(string wnickname, string wnametxt);

    internal delegate void UpdateWriteChatMsgDelegate(string message);

    internal delegate void UpdateWriteRealChatMsgDelegate(string message);

    internal delegate void UpdateGuamodTurnOnDelegate();

    internal delegate void AddContactFromBulkDelegate(string nick);

    internal delegate void ReloadMainPhpInvokeDelegate();

    internal delegate void ReloadChPhpInvokeDelegate();

    internal delegate void NavigatorOffInvokeDelegate();

    internal delegate void SetMainTopInvokeDelegate(string address);

    internal delegate void FormMainCloseDelegate(string address);

    internal delegate void ShowActivityDelegate(int numberOfThreads);
}