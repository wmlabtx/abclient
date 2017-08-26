using System.Collections.Generic;
using ABClient.Lez;

namespace ABClient.MyProfile
{
    using System;
    using Profile;

    internal sealed partial class UserConfig
    {
        internal TypeStat Stat;
        internal TWindow Window;
        internal TSplitter Splitter;
        internal string[] Tabs;
        internal TPers Pers;
        internal TNavigator Navigator;
        internal string[] FavLocations;
        internal TimeSpan ServDiff;
        internal TSound Sound;
        internal TAutoAdv AutoAdv;

        /// <summary>
        /// Ник пользователя в игре. Пустое значение говорит о том, что конфигурация еще не заполнена пользователем
        /// </summary>
        internal string UserNick { get; set; }

        /// <summary>
        /// Пароль пользователя для игры. Если пустой, это значит, что пароль зашифрован
        /// </summary>
        internal string UserPassword { get; set; }

        /// <summary>
        /// Ключ для работоспособности клиента
        /// </summary>
        internal string UserKey { get; set; }

        /// <summary>
        /// Шифрованный пароль пользователя
        /// </summary>
        private string EncryptedUserPassword { get; set; }

        /// <summary>
        /// Флеш-пароль пользователя. Может быть пустым.
        /// </summary>
        internal string UserPasswordFlash { get; set; }

        /// <summary>
        /// Надо ли входить в игру автоматически. Игнорируется, если UserNick и UserPassword пустые
        /// </summary>
        internal bool UserAutoLogon { get; set; }

        /// <summary>
        /// Спрашивать ли о выходе из игры
        /// </summary>
        internal bool DoPromptExit { get; set; }

        /// <summary>
        /// Работает ли гуамод
        /// </summary>
        internal bool DoGuamod { get; set; }

        /// <summary>
        /// Пароль к конфигурации. Может быть пустым
        /// </summary>
        internal string ConfigPassword { get; private set; }

        /// <summary>
        /// Хеш пароля к конфигурации.  Может быть пустым - это говорит от незашифрованности отдельных значений
        /// </summary>
        internal string ConfigHash { get; set; }

        /// <summary>
        /// Использовать ли прокси
        /// </summary>
        internal bool DoProxy { get; set; }

        /// <summary>
        /// Адрес прокси
        /// </summary>
        internal string ProxyAddress { get; set; }

        /// <summary>
        /// Имя пользователя прокси
        /// </summary>
        internal string ProxyUserName { get; set; }
            
        /// <summary>
        /// Пароль к прокси
        /// </summary>
        internal string ProxyPassword { get; set; }

        /// <summary>
        /// Показывать ли расширенную карту
        /// </summary>
        internal bool MapShowExtend { get; set; }

        /// <summary>
        /// Ширина большой карты в клетках
        /// </summary>
        internal int MapBigWidth { get; set; }

        /// <summary>
        /// Высота большой карты в клетках
        /// </summary>
        internal int MapBigHeight { get; set; }

        /// <summary>
        /// Масштаб большой карты в клетках
        /// </summary>
        internal int MapBigScale { get; set; }

        /// <summary>
        /// Прозрачность большой карты в клетках
        /// </summary>
        internal int MapBigTransparency { get; set; }

        /// <summary>
        /// true = белая подсветка карты
        /// </summary>
        internal bool MapShowBackColorWhite { get; set; }

        /// <summary>
        /// Показывать ли миникарту
        /// </summary>
        internal bool MapShowMiniMap { get; set; }

        /// <summary>
        /// Ширина мини-карты в клетках
        /// </summary>
        internal int MapMiniWidth { get; set; }

        /// <summary>
        /// Высота мини-карты в клетках
        /// </summary>
        internal int MapMiniHeight { get; set; }

        /// <summary>
        /// Масштаб мини-карты в клетках
        /// </summary>
        internal int MapMiniScale { get; set; }

        /// <summary>
        /// Положение на карте, например, 8-259
        /// </summary>
        internal string MapLocation { get; set; }

        /// <summary>
        /// Подсвечивать ли текущий регион
        /// </summary>
        internal bool MapDrawRegion { get; set; }

        /// <summary>
        /// Стоимость лечения травм
        /// </summary>
        internal int[] CureNV { get; private set; }

        /// <summary>
        /// Вопросы по поводу лечения каждой травмы
        /// </summary>
        internal string[] CureAsk { get; private set; }

        /// <summary>
        /// Реклама лечения
        /// </summary>
        internal string CureAdv { get; set; }

        /// <summary>
        /// Фраза после лечения
        /// </summary>
        internal string CureAfter { get; set; }

        /// <summary>
        /// Фраза "выйди из боя"
        /// </summary>
        internal string CureBoi { get; set; }

        /// <summary>
        /// Разрешение работать с травмами
        /// </summary>
        internal bool[] CureEnabled { get; private set; }

        /// <summary>
        /// Запрещение работать с уровнями 0-4
        /// </summary>
        internal bool CureDisabledLowLevels { get; set; }

        /// <summary>
        /// Работающий автоответ
        /// </summary>
        internal bool DoAutoAnswer { get; set; }

        /// <summary>
        /// Длинная строчка с ответами
        /// </summary>
        internal string AutoAnswer { get; set; }

        /// <summary>
        /// При какой усталке начать пить
        /// </summary>
        internal int FishTiedHigh { get; set; }

        /// <summary>
        /// Пить ли до нуля усталости
        /// </summary>
        internal bool FishTiedZero { get; set; }

        /// <summary>
        /// Останавливать рыбалку при перегрузе
        /// </summary>
        internal bool FishStopOverWeight { get; set; }

        /// <summary>
        /// Выполнять автонадевание на рыбалке
        /// </summary>
        internal bool FishAutoWear { get; set; }

        /// <summary>
        /// Что одевать в правую руку
        /// </summary>
        internal string FishHandOne { get; set; }

        /// <summary>
        /// Что одевать в левую руку
        /// </summary>
        internal string FishHandTwo { get; set; }

        /// <summary>
        /// Какую наживку можно использовать
        /// </summary>
        internal Prims FishEnabledPrims { get; set; }

        /// <summary>
        /// Умелка на рыбалке
        /// </summary>
        internal int FishUm { get; set; }

        /// <summary>
        /// Запомненное значение максимально убиваемых ботов
        /// </summary>
        internal int FishMaxLevelBots { get; set; }

        /// <summary>
        /// Выводить ли результаты клева в чат
        /// </summary>
        internal bool FishChatReport { get; set; }

        /// <summary>
        /// Выводить ли результаты клева в чат
        /// </summary>
        internal bool FishChatReportColor { get; set; }

        /// <summary>
        /// Выводить ли результаты разделки в чат
        /// </summary>
        internal bool RazdChatReport { get; set; }

        /// <summary>
        /// Режим авторыбалки
        /// </summary>
        internal bool FishAuto { get; set; }

        /// <summary>
        /// Режим авторазделки
        /// </summary>
        internal bool SkinAuto { get; set; }

        /// <summary>
        /// Сохранять ли чат при переходе
        /// </summary>
        internal bool ChatKeepMoving { get; set; }

        /// <summary>
        /// Сохранять ли чат при перезагрузке игры
        /// </summary>
        internal bool ChatKeepGame { get; set; }

        /// <summary>
        /// Сохранять ли чат в файл
        /// </summary>
        internal bool ChatKeepLog { get; set; }

        /// <summary>
        /// Размер чата в килобайтах
        /// </summary>
        internal int ChatSizeLog { get; set; }

        /// <summary>
        /// Высота чата в пикселях
        /// </summary>
        internal int ChatHeight { private get; set; }

        /// <summary>
        /// Задержка обновления чата в секундах
        /// </summary>
        internal int ChatDelay { private get; set; }

        /// <summary>
        /// Режим чата
        /// </summary>
        internal int ChatMode { private get; set; }

        /// <summary>
        /// Облегченный форум
        /// </summary>
        internal bool LightForum { get; set; }

        /// <summary>
        /// Активный торговец
        /// </summary>
        internal bool TorgActive { get; set; }

        /// <summary>
        /// Таблица торговца
        /// </summary>
        internal string TorgTabl { get; set; }

        /// <summary>
        /// Сообщение "Слишком дорого!"
        /// </summary>
        internal string TorgMessageTooExp { get; set; }

        /// <summary>
        /// Сообщение "Реклама"
        /// </summary>
        internal string TorgMessageAdv { get; set; }

        /// <summary>
        /// Периодичность выбрасывания в чат рекламы
        /// </summary>
        internal int TorgAdvTime { get; set; }

        /// <summary>
        /// Сообщение "Спасибо"
        /// </summary>
        internal string TorgMessageThanks { get; set; }

        /// <summary>
        /// Сообщение "Нет денег"
        /// </summary>
        internal string TorgMessageNoMoney { get; set; }

        /// <summary>
        /// Сообщение "Меньше 90% по госу!"
        /// </summary>
        internal string TorgMessageLess90 { get; set; }

        /// <summary>
        /// Автослив вешей в лавку
        /// </summary>
        internal bool TorgSliv { get; set; }

        /// <summary>
        /// Ниже какого левела можно сдавать вещи в лавку
        /// </summary>
        internal int TorgMinLevel { get; set; }

        /// <summary>
        /// Вещи с какими ключевыми словами нельзя сдавать в лавку
        /// </summary>
        internal string TorgEx { get; set; }

        /// <summary>
        /// Вещи с какими ключевыми словами нельзя покупать
        /// </summary>
        internal string TorgDeny { get; set; }
        
        /// <summary>
        /// Вывод в чат результата спила
        /// </summary>
        internal bool DoAutoCutWriteChat { get; set; }

        /// <summary>
        /// Рисовать ли уровни
        /// </summary>
        internal bool DoChatLevels { get; set; }

        internal bool DoHttpLog { get; set; }

        internal bool DoTexLog { get; set; }

        internal bool ShowPerformance { get; set; }

        internal int SelectedRightPanel { get; set; }

        internal string Notepad { get; set; }

        internal bool DoTray { get; set; }

        internal bool ShowTrayBaloons { get; set; }       

        private bool DoConvertRussian { get; set; }

        internal bool DoAutoDrinkBlaz { get; set; }

        internal int AutoDrinkBlazTied { get; set; }

        internal DateTime NextCheckVersion { get; set; }

        internal bool ShowOverWarning { get; set; }

        internal bool DoRob { get; set; }

        internal bool DoAutoCure { get; set; }

        internal string AutoWearComplect { get; set; }

        /// <summary>
        /// Контакты.
        /// </summary>
        internal SortedList<string, Contact> Contacts { get; private set; }
        internal bool DoContactTrace { get; set; }
        internal bool DoBossTrace { get; set; }

        /// <summary>
        /// Стекировать одинаковые предметы в инвентаре
        /// </summary>
        internal bool DoInvPack { get; set; }

        /// <summary>
        /// Сортировать предметы в инвентаре с разной долговечностью
        /// </summary>
        internal bool DoInvPackDolg { get; set; }

        /// <summary>
        /// Сортировать предметы в инвентаре
        /// </summary>
        internal bool DoInvSort { get; set; }

        /// <summary>
        /// Показывать простое нападение
        /// </summary>
        internal bool DoShowFastAttack { get; set; }

        /// <summary>
        /// Показывать кровавое нападение
        /// </summary>
        internal bool DoShowFastAttackBlood { get; set; }

        /// <summary>
        /// Показывать боевое нападение
        /// </summary>
        internal bool DoShowFastAttackUltimate { get; set; }

        /// <summary>
        /// Показывать закрытое боевое нападение
        /// </summary>
        internal bool DoShowFastAttackClosedUltimate { get; set; }

        /// <summary>
        /// Показывать закрытое нападение
        /// </summary>
        internal bool DoShowFastAttackClosed { get; set; }

        /// <summary>
        /// Показывать кулачку
        /// </summary>
        internal bool DoShowFastAttackFist { get; set; }

        /// <summary>
        /// Показывать закрытую кулачку
        /// </summary>
        internal bool DoShowFastAttackClosedFist { get; set; }

        /// <summary>
        /// Показывать вскрытие невида
        /// </summary>
        internal bool DoShowFastAttackOpenNevid { get; set; }

        /// <summary>
        /// Показывать яд
        /// </summary>
        internal bool DoShowFastAttackPoison { get; set; }

        /// <summary>
        /// Показывать спину
        /// </summary>
        internal bool DoShowFastAttackStrong { get; set; }

        /// <summary>
        /// Показывать невид
        /// </summary>
        internal bool DoShowFastAttackNevid { get; set; }

        /// <summary>
        /// Показывать свиток искажающего тумана
        /// </summary>
        internal bool DoShowFastAttackFog { get; set; }

        /// <summary>
        /// Показывать свиток защиты
        /// </summary>
        internal bool DoShowFastAttackZas { get; set; }

        /// <summary>
        /// Показывать тотемное нападение
        /// </summary>
        internal bool DoShowFastAttackTotem { get; set; }

        /// <summary>
        /// Показывать портал
        /// </summary>
        internal bool DoShowFastAttackPortal { get; set; }

        /// <summary>
        /// Останавливаться на кладе
        /// </summary>
        internal bool DoStopOnDig { get; set; }

        internal string Complects { get; set; }

        internal int AutoDrinkBlazOrder { get; set; }

        // Lez AutoBoi

        internal bool LezDoAutoboi = true;
        internal bool LezDoWaitHp = false;
        internal bool LezDoWaitMa = false;
        internal int LezWaitHp = 100;
        internal int LezWaitMa = 100;
        internal bool LezDoDrinkHp = false;
        internal bool LezDoDrinkMa = true;
        internal int LezDrinkHp = 50;
        internal int LezDrinkMa = 50;
        internal bool LezDoWinTimeout = true;
        internal LezSayType LezSay = LezSayType.No;
        internal List<LezBotsGroup> LezGroups = new List<LezBotsGroup> {new LezBotsGroup(001, 0)};

        internal LezSayType BossSay = LezSayType.No;

        /// <summary>
        /// Шифрованный флеш-пароль пользователя.
        /// </summary>
        private string EncryptedUserPasswordFlash { get; set; }

        /// <summary>
        /// Время последнего сохранения конфигурации. В тиках
        /// </summary>
        private long ConfigLastSaved { get; set; }
    }

    public enum LezSayType
    {
        No = 0, Chat = 1, Clan = 2, Pair = 3
    }
}