using ABClient.ExtMap;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ABClient.ABForms;
using ABClient.MyProfile;
using System.Threading;
using ABClient.PostFilter;

namespace ABClient
{
    internal static class AppVars
    {
        /// <summary>
        /// Версия приложения и все, что с ней связано.
        /// </summary>
        internal static readonly VersionClass AppVersion = new VersionClass("ABClient", Application.ProductVersion);

        /// <summary>
        /// Русская кодовая страница.
        /// </summary>
        internal static readonly Encoding Codepage = Encoding.GetEncoding(1251);

        /// <summary>
        /// Русская культура.
        /// </summary>
        internal static readonly CultureInfo Culture = CultureInfo.GetCultureInfo("ru-RU");

        /// <summary>
        /// Английская культура.
        /// </summary>
        internal static readonly CultureInfo EnUsCulture = CultureInfo.GetCultureInfo("en-US");

        /// <summary>
        /// Рабочий профайл пользователя.
        /// </summary>
        internal static UserConfig Profile;

        /// <summary>
        /// Локальный прокси, к которому надо обращаться.
        /// </summary>
        internal static WebProxy LocalProxy;

        /// <summary>
        /// Главная форма приложения.
        /// </summary>
        internal static FormMain MainForm;

        /// <summary>
        /// Ссылка, которую можно нажать для окончания боя.
        /// </summary>
        internal static string FightLink;

        /// <summary>
        /// Код последнего обработанного боя.
        /// </summary>
        internal static string LastBoiLog;

        /// <summary>
        /// Состав последнего боя.
        /// </summary>
        internal static string LastBoiSostav;

        /// <summary>
        /// Травматичность последнего боя.
        /// </summary>
        internal static string LastBoiTravm;

        /// <summary>
        /// Время начала последнего боя.
        /// </summary>
        internal static DateTime LastBoiTimer;

        /// <summary>
        /// Список добытых ресурсов.
        /// </summary>
        internal static readonly StringCollection RazdelkaResultList = new StringCollection();

        /// <summary>
        /// На сколько поднялось умение разделки.
        /// </summary>
        internal static int RazdelkaLevelUp;

        /// <summary>
        /// В какой момент уже можно вывести в чат результаты разделки.
        /// </summary>
        internal static DateTime RazdelkaTime = DateTime.MaxValue;

        /// <summary>
        /// Число юзеров ABC, зашедших за последние сутки.
        /// </summary>
        internal static string UsersOnline = string.Empty;

        /// <summary>
        /// Заметки о юзере с сервера
        /// </summary>
        internal static string UserKey = string.Empty;
        internal const string UserKeyHostsError = "!HOSTS";
        internal const string UserKeyAbclientServerError = "!ABCLIENT";
        internal const string UserKeyNeverlandsServerError = "!NEVERLANDS";

        /// <summary>
        /// Когда в последний раз получалась карта трав.
        /// </summary>

        /*
        internal static DateTime LastExAlchemy = DateTime.MinValue;
        */

        /// <summary>
        /// Блокировка обращения к кодированной картой трав.
        /// </summary>

        /*
        internal static object LockListMapHerbs = new object();
         */

        /// <summary>
        /// Кодированная карта трав.
        /// </summary>
        /*
        internal static SortedList<string, string> ListMapHerbs = new SortedList<string, string>();
         */

        /// <summary>
        /// Наше местоположение. Может отличаться от того, что в AppVars.Profile.MapLocation (в момент перехода).
        /// Используется для Багрового Глаза.
        /// </summary>
        internal static string LocationReal;

        /// <summary>
        /// Путь к рисункам карты.
        /// </summary>
        internal const string PathToMap = "map/world";

        /// <summary>
        /// Работа автоспила
        /// </summary>
        // internal static bool DoHerbAutoCut;

        /// <summary>
        /// Форма очистки кеша.
        /// </summary>
        internal static ClearExplorerCacheForm ClearExplorerCacheFormMain { get; set; }

        internal static bool MustReload { get; set; }

        internal static DateTime LastInitForm { get; set; }

        internal static string AccountError { get; set; }

        internal static bool WaitFlash { get; set; }

        internal static bool DoPromptExit { get; set; }

        internal static bool CacheRefresh { get; set; }

        /*
        internal static string DirChatLog { get; set; }

        internal static string FileChatLog { get; set; }

         */
 
        internal static DateTime LastMainPhp { get; set; }

        internal static string ContentMainPhp { get; set; }

        internal static AutoboiState Autoboi { get; set; }

        internal static string GuamodCode { get; set; }

        internal static string LastBoiEndLog { get; set; }

        internal static string LastBoiUron { get; set; }

        internal static string CodeAddress { get; set; }

        internal static byte[] CodePng { get; set; }

        internal static int Tied { get; set; }

        internal static DateTime LastTied { get; set; }

        internal static bool AutoDrink { get; set; }

        internal static bool SwitchToPerc { get; set; }

        internal static bool SwitchToFlora { get; set; }

        internal static bool AutoMoving { get; set; }
        internal static string AutoMovingNextJump { get; set; }
        internal static string AutoMovingDestinaton { get; set; }
        internal static int AutoMovingJumps { get; set; }
        internal static CityGateType AutoMovingCityGate { get; set; }
        internal static MapPath AutoMovingMapPath { get; set; }

        internal static string Chat { get; set; }

        internal static bool AutoSkinCheckUm { get; set; }
        internal static int SkinUm { get; set; }
        internal static bool AutoSkinCheckKnife { get; set; }
        internal static bool AutoSkinArmedKnife { get; set; }
        internal static string AutoSkinHand { get; set; }
        internal static string AutoSkinHandD { get; set; }
        internal static DateTime AutoSkinLastChecked { get; set; }
        internal static bool AutoSkinCheckRes { get; set; }
        internal static readonly Dictionary<string, double> SkinRes = new Dictionary<string, double>();

        internal static bool AutoFishCheckUd { get; set; }

        internal static bool AutoFishWearUd { get; set; }

        internal static bool AutoFishCheckUm { get; set; }

        internal static string AutoFishHand1 { get; set; }

        internal static string AutoFishHand2 { get; set; }

        internal static string AutoFishHand1D { get; set; }

        internal static string AutoFishHand2D { get; set; }

        internal static string AutoFishLikeId { get; set; }

        internal static string AutoFishLikeVal { get; set; }

        internal static string AutoFishMassa { get; set; }

        internal static double AutoFishNV { get; set; }

        internal static bool AutoFishDrink { get; set; }

        internal static bool AutoFishDrinkOnce { get; set; }

        internal static bool DoShowWalkers { get; set; }

        internal static string MyCoordOld { get; set; }

        internal static string MyLocOld { get; set; }

        internal static Dictionary<string, string> MyCharsOld { get; set; }

        internal static int MyNevids { get; set; }

        internal static int MyNevidsOld { get; set; }

        internal static string MyWalkers1 { get; set; }

        internal static string MyWalkers2 { get; set; }

        internal static DateTime LastChList { get; set; }

        internal static string[] AdvArray { get; set; }

        internal static int AdvIndex { get; set; }

        internal static bool AdvActive { get; set; }

        internal static DateTime LastAdv { get; set; }

        internal static int DocumentBodyNullCount { get; set; }

        internal static bool CureNeed { get; set; }

        internal static string CureNick { get; set; }

        internal static string CureTravm { get; set; } // "1", "2", "3", "4"

        internal static string CureNickDone { get; set; }

        internal static string CureNickBoi { get; set; }

        /// <summary>
        /// Списки подстановленных образов. Ключом является ник (приведенный в UpperCase)
        /// </summary>
        internal static SortedDictionary<string, string> UserObrazes { get; set; }

        /// <summary>
        /// Последняя посылка рекламы торга
        /// </summary>
        internal static DateTime LastTorgAdv { get; set; }

        /*
        internal static string LastRazd { get; set; }

        internal static DateTime LastRazdTime { get; set; }
         */

        internal static string MovingTime { private get; set; }

        internal static bool PriSelected { get; set; }

        internal static string NamePri { get; set; }

        internal static int ValPri { get; set; }

        internal static DateTime LastSwitch { get; set; }

        internal static bool FishNoCaptchaReady { get; set; }

        internal static DateTime LicenceExpired { get; set; }

        internal static DateTime ServerDateTime { get; set; }

        // internal const int BoReq = 50;

        internal static FormCompas VipFormCompas;

        internal static FormAddClan VipFormAddClan;

        internal static bool DoChatTip = true;

        /// <summary>
        /// Быстрые нападения
        /// </summary>

        internal static bool FastNeed; // true - нужно быстрое действие
        internal static string FastId; // Что использовать
        internal static string FastNick; // На кого применяется
        internal static int FastCount; // Сколько раз

        internal static bool FastNeedAbilDarkTeleport;
        internal static bool FastNeedAbilDarkFog;

        internal static bool FastWaitEndOfBoiActive;
        internal static bool FastWaitEndOfBoiCancel;

        internal static string BulkDropThing;
        internal static string BulkDropPrice;

        internal static string BulkSellThing { get; set; }

        internal static int BulkSellPrice { get; set; }

        internal static int BulkSellSum { get; set; }

        internal static string WearComplect;

        internal static string LocationName { get; set; }

        internal static bool AutoRefresh { get; set; }

        internal static bool WaitOpen { get; set; }

        internal static bool AutoOpenNevid { get; set; }

        internal static bool DoSelfNevid;
        internal static bool SelfNevidNeed;
        internal static string SelfNevidSkl;
        internal static int SelfNevidStage;

        internal static int AutoAttackToolId { get; set; }

        internal static Thread ThreadWaitForTurn { get; set; }

        internal static string CurrentDayNight { get; private set; }

        internal static int[] PoisonAndWounds { get; set; }

        internal static DateTime LastMessageAboutTraumaOrPoison { get; set; }

        internal static DateTime IdleTimer;

        internal static bool DoPerenap;

        internal static bool DoFury;

        internal static DateTime FastTotemMessageTime;

        internal static DateTime NextCheckNoConnection;

        internal static bool DoSearchBox;

        //internal static bool BossSearching;

        internal static bool DrinkBlazPotOrElixirFirst = false;

        internal static int DrinkDrinkHpMaCount;

        internal static SortedList<string, BossContact> BossContacts { get; set; }

        internal static string BossSayLastLog { get; set; }

        internal static readonly List<ShopEntry> ShopList = new List<ShopEntry>();
        internal static string BulkSellOldName;
        internal static string BulkSellOldPrice;
        internal static string BulkSellOldScript;

        // Блокировочный таймер

        internal static DateTime NeverTimer = DateTime.MinValue;

        static AppVars()
        {
            MyCharsOld = new Dictionary<string, string>();
            MyWalkers1 = string.Empty;
            MyWalkers2 = string.Empty;
            PoisonAndWounds = new int[4];
            FastTotemMessageTime = DateTime.MinValue;
            NextCheckNoConnection = DateTime.Now.AddMinutes(5);
        }
    }
}