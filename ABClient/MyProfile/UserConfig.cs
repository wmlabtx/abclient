using System.Threading;

namespace ABClient.MyProfile
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using Profile;

    internal sealed partial class UserConfig : IComparable
    {
        private static readonly ReaderWriterLock UserConfigLock = new ReaderWriterLock();

        internal readonly SortedDictionary<string, HerbCell> HerbCells = new SortedDictionary<string, HerbCell>();
        internal readonly List<string> HerbsAutoCut = new List<string>();
        internal readonly List<AppTimer> AppConfigTimers = new List<AppTimer>();        

        private readonly List<string> _listtabs = new List<string>();
        private readonly List<string> _listfavlocations = new List<string>();

        private string _configFileName;
        private string _configFileNameTemp;

        internal UserConfig()
        {
            UserNick = string.Empty;
            UserPassword = string.Empty;
            UserKey = string.Empty;
            EncryptedUserPassword = string.Empty;
            UserPasswordFlash = string.Empty;
            EncryptedUserPasswordFlash = string.Empty;
            ConfigLastSaved = DateTime.Now.Ticks;
            UserAutoLogon = ConstUserAutoLogonDefault;
            DoPromptExit = ConstDoPromptExitDefault;
            DoGuamod = ConstDoGuamodDefault;
            ConfigPassword = string.Empty;
            ConfigHash = string.Empty;
            
            DoProxy = ConstDoProxyDefault;
            ProxyAddress = string.Empty;
            ProxyUserName = string.Empty;
            ProxyPassword = string.Empty;

            MapShowExtend = ConstMapShowExtendDefault;
            MapBigWidth = ConstMapBigWidthDedault;
            MapBigHeight = ConstMapBigHeightDedault;
            MapBigScale = ConstMapBigScaleDedault;
            MapBigTransparency = ConstMapBigTransparencyDedault;
            MapShowBackColorWhite = ConstMapShowBackColorWhiteDedault;
            MapShowMiniMap = ConstMapShowMiniMapDedault;
            MapMiniWidth = ConstMapMiniWidthDedault;
            MapMiniHeight = ConstMapMiniHeightDedault;
            MapMiniScale = ConstMapMiniScaleDedault;
            MapLocation = string.Empty;
            MapDrawRegion = ConstMapDrawRegionDefault;

            CureNV = new[] { ConstCureNVOneDefault, ConstCureNVTwoDefault, ConstCureNVThreeDefault, ConstCureNVFourDefault };
            CureAsk = new[] { ConstCureAskOneDefault, ConstCureAskTwoDefault, ConstCureAskThreeDefault, ConstCureAskFourDefault };
            CureAdv = ConstCureAdvDefault;
            CureAfter = ConstCureAfterDefault;
            CureBoi = ConstCureBoiDefault;
            CureEnabled = new[] { ConstCureEnabledOneDefault, ConstCureEnabledTwoDefault, ConstCureEnabledThreeDefault, ConstCureEnabledFourDefault };
            CureDisabledLowLevels = ConstCureDisabledLowLevelsDefault;

            DoAutoAnswer = ConstDoAutoAnswerDefault;
            AutoAnswer = ConstAutoAnswerDefault;

            FishTiedHigh = ConstFishTiedHighDefault;
            FishTiedZero = ConstFishTiedZeroDefault;
            FishStopOverWeight = ConstFishStopOverWeightDefault;
            FishAutoWear = ConstFishAutoWearDefault;
            FishHandOne = ConstFishHandOneDefault;
            FishHandTwo = string.Empty;
            FishEnabledPrims = ConstFishEnabledPrimsDefault;
            FishUm = ConstFishUmDefault;
            FishMaxLevelBots = ConstFishMaxLevelBotsDefault;
            FishChatReport = ConstFishChatReportDefault;
            FishChatReportColor = ConstFishChatReportColorDefault;
            FishAuto = ConstFishAutoDefault;

            RazdChatReport = ConstRazdChatReportDefault;

            ChatKeepGame = ConstChatKeepGameDefault;
            ChatKeepMoving = ConstChatKeepMovingDefault;
            ChatKeepLog = ConstChatKeepLogDefault;
            ChatSizeLog = ConstChatSizeLogDefault;
            ChatHeight = ConstChatHeightDefault;
            ChatDelay = ConstChatDelayDefault;
            ChatMode = ConstChatModeDefault;

            LightForum = ConstLightForumDefault;

            TorgActive = ConstTorgActiveDefault;
            TorgTabl = ConstTorgTablDefault;
            TorgMessageTooExp = ConstTorgMessageTooExpDefault;
            TorgMessageAdv = ConstTorgMessageMsgAdvDefault;
            TorgAdvTime = AppConsts.TorgAdvTimeDefault;
            TorgMessageThanks = ConstTorgMessageThanksDefault;
            TorgMessageNoMoney = ConstTorgMessageNoMoneyDefault;
            TorgMessageLess90 = ConstTorgMessageLess90Default;
            TorgSliv = ConstTorgSlivDefault;
            TorgMinLevel = ConstTorgMinLevelDefault;
            TorgEx = ConstTorgExDefault;
            TorgDeny = ConstTorgDenyDefault;

            Window = new TWindow
            {
                State = FormWindowState.Normal,
                Left = 0,
                Top = 0,
                Width = 0,
                Height = 0
            };

            Stat = new TypeStat
            {
                LastReset = DateTime.Now.Ticks,
                LastUpdateDay = DateTime.Now.DayOfYear,
                Drop = string.Empty,
                ItemDrop = new List<TypeItemDrop>(),
                SavedTraffic = 0,
                Show = 0,
                Traffic = 0,
                XP = 0,
                NV = 0,
                FishNV = 0
            };

            Splitter = new TSplitter
            {
                Collapsed = false,
                Width = 200
            };

            Pers = new TPers
            {
                Guamod = true,
                IntHP = 2000,
                IntMA = 9000,
                Ready = 0,
                LogReady = string.Empty
            };

            Navigator = new TNavigator
            {
                AllowTeleports = true
            };

            AutoAdv = new TAutoAdv
            {
                Sec = 600,
                Phraz =
                    "Ойож фивж трансой" + Environment.NewLine +
                    "Иоф унита крамберри тро фифс" + Environment.NewLine +
                    "Про гинт гита" + Environment.NewLine +
                    "Ю кисон ноер!" + Environment.NewLine +
                    "; Эта закомментированная строчка рекламы будет пропускаться"
            };

            DoPromptExit = true;
            DoTexLog = true;
            Notepad = string.Empty;
            Tabs = new string[0];
            DoTray = true;
            ShowTrayBaloons = true;
            FavLocations = new string[0];
            ServDiff = new TimeSpan(0);

            Sound = new TSound
            {
                Enabled = true,
                DoPlayAlarm = true,
                DoPlayAttack = true,
                DoPlayDigits = true,
                DoPlayRefresh = true,
                DoPlaySndMsg = true,
                DoPlayTimer = true
            };

            DoInvPack = true;
            DoInvPackDolg = true;
            DoInvSort = true;

            DoShowFastAttack = false;
            DoShowFastAttackBlood = true;
            DoShowFastAttackUltimate = true;
            DoShowFastAttackClosedUltimate = true;
            DoShowFastAttackClosed = true;
            DoShowFastAttackFist = false;
            DoShowFastAttackClosedFist = true;
            DoShowFastAttackOpenNevid = true;
            DoShowFastAttackPoison = true;
            DoShowFastAttackStrong = true;
            DoShowFastAttackNevid = true;
            DoShowFastAttackFog = true;
            DoShowFastAttackZas = true;
            DoShowFastAttackTotem = true;
            DoShowFastAttackPortal = true;            

            DoAutoCutWriteChat = true;
            DoStopOnDig = true;

            DoAutoDrinkBlaz = false;
            AutoDrinkBlazTied = 84;

            NextCheckVersion = DateTime.MinValue;
            ShowOverWarning = false;

            Complects = string.Empty;

            DoRob = true;

            DoAutoCure = true;
            AutoWearComplect = string.Empty;

            DoContactTrace = false;
            DoBossTrace = true;

            SkinAuto = false;
        }

        /// <summary>
        /// Имя файла без пути и расширения
        /// </summary>
        internal string FileName
        {
            get { return Path.GetFileNameWithoutExtension(_configFileName); }
        }

        #region ToString()

        public override string ToString()
        {
            return UserNick;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// Сравнивает два файла конфигурации в зависимости от поля ConfigLastSaved
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is UserConfig))
            {
                return -1;
            }

            var other = (UserConfig) obj;
            if (ConfigLastSaved > other.ConfigLastSaved)
            {
                return -1;
            }

            return ConfigLastSaved < other.ConfigLastSaved ? 1 : 0;
        }

        #endregion

        /// <summary>
        /// Расшифровывает зашифрованные пароли в конфигурации
        /// </summary>
        /// <param name="password">Пароль</param>
        internal void Decrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            UserPassword = Helpers.Crypts.DecryptString(EncryptedUserPassword, password);
            UserPasswordFlash = Helpers.Crypts.DecryptString(EncryptedUserPasswordFlash, password);
            ConfigPassword = password;
        }

        /// <summary>
        /// Зашифровывает некоторые пароли в конфигурации
        /// </summary>
        /// <param name="password">Пароль</param>
        internal void Encrypt(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            EncryptedUserPassword = Helpers.Crypts.EncryptString(UserPassword, password);
            EncryptedUserPasswordFlash = Helpers.Crypts.EncryptString(UserPasswordFlash, password);
            ConfigPassword = password;
            ConfigHash = Helpers.Crypts.Password2Hash(password);
        }

        /// <summary>
        /// Возвращает "человеческую" строчку когда был последний заход в игру
        /// </summary>
        /// <returns>строка типа "менее 5 минут назад"</returns>
        internal string HumanFormatConfigLastSaved()
        {
            var timeConfigLastSaved = new DateTime(ConfigLastSaved);
            var timeDiff = DateTime.Now.Subtract(timeConfigLastSaved);
            if (timeDiff.TotalMinutes < 5)
            {
                return ConstLessFiveMinsFormat + ConstAgoFormat;
            }

            if (timeDiff.TotalHours < 1)
            {
                if (timeDiff.Minutes < 21)
                {
                    return timeDiff.Minutes + ConstMinutesOneFormat + ConstAgoFormat;
                }

                var lastDigit = timeDiff.Minutes % 10;
                if (lastDigit == 1)
                {
                    return timeDiff.Minutes + ConstMinutesTwoFormat + ConstAgoFormat;
                }

                if (lastDigit >= 2 && lastDigit <= 4)
                {
                    return timeDiff.Minutes + ConstMinutesThreeFormat + ConstAgoFormat;
                }

                return timeDiff.Minutes + ConstMinutesOneFormat + ConstAgoFormat;
            }

            if (timeDiff.TotalHours < 2)
            {
                return ConstOneHourFormat + ConstAgoFormat;
            }

            if (timeDiff.TotalHours < 5)
            {
                return timeDiff.Hours + ConstHourOneFormat + ConstAgoFormat;
            }

            if (timeDiff.TotalDays < 1)
            {
                return timeDiff.Hours + ConstHourTwoFormat + ConstAgoFormat;
            }

            if (timeDiff.TotalDays < 5)
            {
                return timeDiff.Days + ConstDayOneFormat + ConstAgoFormat;
            }

            if (timeDiff.TotalDays < 21)
            {
                return timeDiff.Days + ConstDayTwoFormat + ConstAgoFormat;
            }

            var lastDayDigit = timeDiff.Days % 10;
            if (lastDayDigit == 1)
            {
                return timeDiff.Days + ConstDayThreeFormat + ConstAgoFormat;
            }

            if (lastDayDigit >= 2 && lastDayDigit <= 4)
            {
                return timeDiff.Days + ConstDayOneFormat + ConstAgoFormat;
            }

            return timeDiff.Days + ConstDayTwoFormat + ConstAgoFormat;
        }

        internal void AddFavLocation(string loc)
        {
            _listfavlocations.Remove(loc);
            _listfavlocations.Insert(0, loc);
            if (_listfavlocations.Count > 20)
                _listfavlocations.RemoveRange(20, _listfavlocations.Count - 20);

            FavLocations = _listfavlocations.ToArray();
        }

        internal void ClearFavLocations()
        {
            _listfavlocations.Clear();
            FavLocations = _listfavlocations.ToArray();
        }

        internal string CureConvert(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            var sb = new StringBuilder(str);
            sb.Replace("[1]", CureNV[0].ToString(CultureInfo.InvariantCulture));
            sb.Replace("[2]", CureNV[1].ToString(CultureInfo.InvariantCulture));
            sb.Replace("[3]", CureNV[2].ToString(CultureInfo.InvariantCulture));
            sb.Replace("[4]", CureNV[3].ToString(CultureInfo.InvariantCulture));
            return sb.ToString();
        }
    }
}