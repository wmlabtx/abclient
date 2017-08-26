namespace ABClient.MyProfile
{
    internal sealed partial class UserConfig
    {
        private const string ConstCommentOne = " Profile for ABClient, the fast client for Neverlands (https://github.com/wmlabtx/abclient/wiki/) ";
        private const string ConstCommentTwo = " Written by Murad Ismayilov, 2007-2009; email: wmlab@hotmail.com; skype: wmlab.home ";
        private const string ConstErrorConfigSaveTitle = "Ошибка сохранения профайла";
        private const string ConstErrorConfigLoadTitle = "Ошибка загрузки профайла";
        private const string ConstLessFiveMinsFormat = "менее 5 минут";
        private const string ConstAgoFormat = " назад";
        private const string ConstMinutesOneFormat = " минут";
        private const string ConstMinutesTwoFormat = " минута";
        private const string ConstMinutesThreeFormat = " минуты";
        private const string ConstOneHourFormat = "час";
        private const string ConstHourOneFormat = " часа";
        private const string ConstHourTwoFormat = " часов";
        private const string ConstDayOneFormat = " дня";
        private const string ConstDayTwoFormat = " дней";
        private const string ConstDayThreeFormat = " день";
       
        private const string ConstProfileExtensionTemp = ".temprofile";
        private const string ConstProfile = "profile";

        private const string ConstTagUser = "user";
        private const string ConstAttibuteUserNick = "name";
        private const string ConstAttibuteConfigHash = "hash";
        private const string ConstAttibuteEncryptedUserPassword = "encryptedpassword";
        private const string ConstEncryptedUserPasswordFlash = "encryptedflash";
        private const string ConstAttibuteUserPassword = "password";
        private const string ConstAttibuteUserKey = "key";
        private const string ConstAttibuteUserPasswordFlash = "flash";
        private const bool ConstUserAutoLogonDefault = false;
        private const string ConstAttibuteUserAutoLogon = "autologon";
        private const bool ConstDoPromptExitDefault = true;
        private const string ConstAttibuteDoPromptExit = "dopromptexit";
        private const bool ConstDoGuamodDefault = true;
        private const string ConstAttibuteDoGuamod = "guamod";
        private const string ConstAttibuteConfigLastSaved = "lastlogon";
        
        private const string ConstTagProxy = "proxy";
        private const bool ConstDoProxyDefault = false;
        private const string ConstAttibuteDoProxy = "active";
        private const string ConstAttibuteProxyAddress = "address";
        private const string ConstAttibuteProxyUserName = "username";
        private const string ConstAttibuteProxyPassword = "password";

        private const string ConstTagMap = "mapset";
        private const bool ConstMapShowExtendDefault = true;
        private const string ConstAttibuteMapShowExtend = "showextend";
        private const int ConstMapBigWidthDedault = 9;
        private const string ConstAttibuteMapBigWidth = "bigwidth";
        private const int ConstMapBigHeightDedault = 5;
        private const string ConstAttibuteMapBigHeight = "bigheight";
        private const int ConstMapBigScaleDedault = 100;
        private const string ConstAttibuteMapBigScale = "bigscale";
        private const int ConstMapBigTransparencyDedault = 70;
        private const string ConstAttibuteMapBigTransparency = "bigtransparency";
        private const bool ConstMapShowBackColorWhiteDedault = false;
        private const string ConstAttibuteMapShowBackColorWhite = "showbackcolorwhite";
        private const bool ConstMapShowMiniMapDedault = true;
        private const string ConstAttibuteMapShowMiniMap = "showminimap";
        private const int ConstMapMiniWidthDedault = 5;
        private const string ConstAttibuteMapMiniWidth = "miniwidth";
        private const int ConstMapMiniHeightDedault = 3;
        private const string ConstAttibuteMapMiniHeight = "miniheight";
        private const int ConstMapMiniScaleDedault = 100;
        private const string ConstAttibuteMapMiniScale = "miniscale";
        private const string ConstAttibuteMapLocation = "location";
        private const bool ConstMapDrawRegionDefault = true;
        private const string ConstAttibuteMapDrawRegion = "drawregion";

        private const string ConstTagCure = "cure";
        private const string ConstAttibuteCureNVOne = "nv1";
        private const int ConstCureNVOneDefault = 10;
        private const int ConstCureNVOneMin = 5;
        private const int ConstCureNVOneMax = 50;
        private const string ConstAttibuteCureNVTwo = "nv2";
        private const int ConstCureNVTwoDefault = 15;
        private const int ConstCureNVTwoMin = 8;
        private const int ConstCureNVTwoMax = 100;
        private const string ConstAttibuteCureNVThree = "nv3";
        private const int ConstCureNVThreeDefault = 25;
        private const int ConstCureNVThreeMin = 11;
        private const int ConstCureNVThreeMax = 150;
        private const string ConstAttibuteCureNVFour = "nv4";
        private const int ConstCureNVFourDefault = 600;
        private const int ConstCureNVFourMin = 296;
        private const int ConstCureNVFourMax = 2000;
        private const string ConstAttibuteCureAskOne = "cask1";
        private const string ConstCureAskOneDefault = "Лечить легкую за [1]?";
        private const string ConstAttibuteCureAskTwo = "cask2";
        private const string ConstCureAskTwoDefault = "Лечить среднюю за [2]?";
        private const string ConstAttibuteCureAskThree = "cask3";
        private const string ConstCureAskThreeDefault = "Лечить тяж за [3]?";
        private const string ConstAttibuteCureAskFour = "cask4";
        private const string ConstCureAskFourDefault = "Лечить боевую за [4]?";
        private const string ConstAttibuteCureAdv = "cadv";
        private const string ConstCureAdvDefault = "Лечу [1]/[2]/[3], боевая - [4]";
        private const string ConstAttibuteCureAfter = "cafter";
        private const string ConstCureAfterDefault = "Поздравляю, не болей";
        private const string ConstAttibuteCureBoi = "cboi";
        private const string ConstCureBoiDefault = "Выйди из боя!";
        private const string ConstAttibuteCureEnabledOne = "e1";
        private const bool ConstCureEnabledOneDefault = true;
        private const string ConstAttibuteCureEnabledTwo = "e2";
        private const bool ConstCureEnabledTwoDefault = true;
        private const string ConstAttibuteCureEnabledThree = "e3";
        private const bool ConstCureEnabledThreeDefault = true;
        private const string ConstAttibuteCureEnabledFour = "e4";
        private const bool ConstCureEnabledFourDefault = true;
        private const string ConstAttibuteCureDisabledLowLevels = "d04";
        private const bool ConstCureDisabledLowLevelsDefault = true;

        private const string ConstTagAutoAnswer = "autoanswer";
        private const bool ConstDoAutoAnswerDefault = false;
        private const string ConstAttibuteDoAutoAnswer = "active";
        private const string ConstAttibuteAutoAnswer = "answers";
        private const string ConstAutoAnswerDefault =
            /*
            "Это автоответ ABClient. Не ждали?" + AppConsts.Br +
            "Я все понимаю, но это автоответ ABClient" + AppConsts.Br +
            "Хватит! Автоответу ABClient это не нравится" + AppConsts.Br +
            "Это автоответ ABClient, не старайся мне что-то объяснить" + AppConsts.Br +
            "Хозяин отошел, но я ему это передам. Это автоответ ABClient" + AppConsts.Br +
            "Автоответ ABClient. Я ничего не читаю, что ты мне пишешь" + AppConsts.Br +
            "Серьезно? Это автоответ ABClient" + AppConsts.Br +
            "Что-то? Повтори. Автоответ ABClient плохо тебя понимает" + AppConsts.Br +
            "Перезагрузись, от тебя закорючки идут. Автоответ ABClient" + AppConsts.Br +
            "Автоответ ABClient советует тебе помолчать" + AppConsts.Br +
            "Это автоответ ABClient. Оставьте сообщение после длинного гудка" + AppConsts.Br +
            "Ты расстраиваешь автоответ ABClient своими глупостями" + AppConsts.Br +
            "Автоответ ABClient ненавидит спаммеров... Где моя нападалка?..." + AppConsts.Br +
            "А в рыло? Автоответ ABClient не понимает шуток" + AppConsts.Br +
            "Ну даешь! Нравится говорить с автоответом ABClient?" + AppConsts.Br +
            "Давай, давай. Ты только заводишь автоответ ABClient" + AppConsts.Br +
            "Автоответ ABClient думает, что это бред" + AppConsts.Br +
            "Как вы все меня утомили! Это автоответ ABClient" + AppConsts.Br +
            "Пиши еще. Автоответ ABClient питается твоими словами" + AppConsts.Br +
            "Я так и передам МСу. Это автоответ ABClient" + AppConsts.Br +
            "Не мешай автоответу ABClient медитировать" + AppConsts.Br +
            "Автоответ ABClient думает, просит не мешать" + AppConsts.Br +
            "Что-что? Пиши медленней, автоответ ABClient не успевает за тобой" + AppConsts.Br +
            "Я хоть и бот, но обидчивый. Автоответ ABClient может и боевую влепить" + AppConsts.Br +
            "Ты говоришь с автоответом ABClient, но не расстраивайся. Хозяин ненамного умнее меня" + AppConsts.Br +
            "Ты даже автоответ ABClient сумел разозлить!" + AppConsts.Br +
            "Что за ерунду ты мне пишешь? Автоответ ABClient ничего не понимает!" + AppConsts.Br +
            "Я запишу и запомню. Автоответ ABClient ничего не забывает!";
             */

            "я на автоответе" + AppConsts.Br +
            "я все понимаю" + AppConsts.Br +
            "хватит" + AppConsts.Br +
            "не старайся" + AppConsts.Br +
            "я отошел" + AppConsts.Br +
            "что за бред ты мне пишешь" + AppConsts.Br +
            "серьезно?" + AppConsts.Br +
            "шта?" + AppConsts.Br +
            "от тебя закорючки идут" + AppConsts.Br +
            "лучше помолчи" + AppConsts.Br +
            "лучше скайпом" + AppConsts.Br +
            "это бред" + AppConsts.Br +
            "кончай спамить" + AppConsts.Br +
            "а в рыло?" + AppConsts.Br +
            "прикалываешься?" + AppConsts.Br +
            "жги исчо" + AppConsts.Br +
            "чушь!" + AppConsts.Br +
            "пиши исчо" + AppConsts.Br +
            "ок, передам хозяину" + AppConsts.Br +
            "я медитирую" + AppConsts.Br +
            "надо подумать" + AppConsts.Br +
            "пиши медленней" + AppConsts.Br +
            "забоевить?" + AppConsts.Br +
            "это автоответ" + AppConsts.Br +
            "не зли меня" + AppConsts.Br +
            "ерунда" + AppConsts.Br +
            "ладно";

        private const string ConstTagFish = "autofish";
        private const int ConstFishTiedHighDefault = 20;
        private const string ConstAttibuteFishTiedHigh = "tiedhigh";
        private const bool ConstFishTiedZeroDefault = false;
        private const string ConstAttibuteFishTiedZero = "tiedzero";
        private const bool ConstFishStopOverWeightDefault = true;
        private const string ConstAttibuteFishStopOverWeight = "stopoverw";
        private const bool ConstFishAutoWearDefault = true;
        private const string ConstAttibuteFishAutoWear = "autowear";
        private const string ConstFishHandOneDefault = "Любая удочка";
        private const string ConstAttibuteFishHandOne = "hand1";
        private const string ConstAttibuteFishHandTwo = "hand2";
        private const string ConstAttibuteFishEnabledPrims = "enabledprims";
        private const int ConstFishUmDefault = 0;
        private const string ConstAttibuteFishMaxLevelBots = "maxlevelbots";
        private const int ConstFishMaxLevelBotsDefault = 8; 
        private const string ConstAttibuteFishUm = "um";
        private const bool ConstFishChatReportDefault = false;
        private const string ConstAttibuteFishChatReport = "chatreport";
        private const bool ConstFishChatReportColorDefault = true;
        private const string ConstAttibuteFishChatReportColor = "chatreportcolor";
        private const string ConstAttibuteFishAuto = "auto";
        private const bool ConstFishAutoDefault = false;
        private const Prims ConstFishEnabledPrimsDefault =
            Prims.Bread | Prims.Worm | Prims.BigWorm | Prims.Stink | Prims.Fly | Prims.Light | Prims.Donka |
            Prims.Morm | Prims.HiFlight;

        private const string ConstAttibuteRazdChatReport = "razdchatreport";
        private const bool ConstRazdChatReportDefault = false;

        private const string ConstTagChat = "chat";
        private const bool ConstChatKeepGameDefault = true;
        private const string ConstAttibuteChatKeepGame = "keepgame";
        private const bool ConstChatKeepMovingDefault = true;
        private const string ConstAttibuteChatKeepMoving = "keepmoving";
        private const bool ConstChatKeepLogDefault = true;
        private const string ConstAttibuteChatKeepLog = "keeplog";
        private const int ConstChatSizeLogDefault = 16;
        private const string ConstAttibuteChatSizeLog = "sizelog";
        private const int ConstChatHeightDefault = 240;
        private const string ConstAttibuteChatHeight = "height";
        private const int ConstChatDelayDefault = 10;
        private const string ConstAttibuteChatDelay = "delay";
        private const int ConstChatModeDefault = 0;
        private const string ConstAttibuteChatMode = "mode";
        private const bool ConstDoChatLevelsDefault = true;
        private const string ConstAttibuteDoChatLevels = "dolevels";

        private const bool ConstLightForumDefault = true;
        private const string ConstTagLightForum = "lightforum";

        private const string ConstTagTorg = "torg";
        private const string ConstAttibuteTorgActive = "active";
        private const bool ConstTorgActiveDefault = false;
        private const string ConstAttibuteTorgTabl = "table";

        private const string ConstTorgTablDefault =
            "1-5(-0), 6-14(-1), 15-25(-2), 26-40(-3), 41-70(-4), 71-100(-5), 101-150(-6), 151-250(-7), 251-350(-8), 351-450(-10), 451-550(-12), 551-650(-14), 651-700(-16), 701-750(-18), 751-800(-20), 801-850(-22), 851-900(-24), 901-950(-26), 951-1000(-30), 1001-1550(-40), 1551-3000(-60)";

        private const string ConstAttibuteTorgMsgAdv = "msgadv";
        private const string ConstTorgMessageMsgAdvDefault = "Автоторг работает по таблице в инфе. Скупаю все!";
        private const string ConstAttibuteTorgMsgTooExp = "msgtooexp";
        private const string ConstTorgMessageTooExpDefault = "Слишком дорого! Автоторг купит вещь {вещь}{вещьур} {вещьдолг} за {минцена}NV!";
        private const string ConstAttibuteTorgMsgThanks = "msgthanks";
        private const string ConstTorgMessageThanksDefault = "Спасибо, вещь {вещь}{вещьур} {вещьдолг} куплена автоторгом за {цена}NV. Приходите еще!";
        private const string ConstAttibuteTorgMsgNoMoney = "msgnomoney";
        private const string ConstTorgMessageNoMoneyDefault = "Извините, для покупки вещи {вещь}{вещьур} {вещьдолг} за {цена}NV не хватает денег.";
        private const string ConstAttibuteTorgMsgLess90 = "msgless90";
        private const string ConstTorgMessageLess90Default = "Покупка ниже 90% от госцены - нарушение правил. 90% от госцены составляет {цена90}NV.";
        private const string ConstAttibuteTorgAdvTime = "advtime";
        private const string ConstAttibuteTorgSliv = "sliv";
        private const bool ConstTorgSlivDefault = false;
        private const string ConstAttibuteTorgMinLevel = "minlevel";
        private const int ConstTorgMinLevelDefault = 16;
        private const string ConstAttributeTorgEx = "ex";
        private const string ConstTorgExDefault = "силы;ловкости;удачи;зелье";
        private const string ConstAttributeTorgDeny = "deny";
        private const string ConstTorgDenyDefault = "зелье";
    }
}
