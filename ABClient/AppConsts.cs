namespace ABClient
{
    /// <summary>
    /// Статичный класс, содержащий константы для всего приложения.
    /// </summary>
    internal static class AppConsts
    {
        internal static readonly string ApplicationName = "ABClient";

        #region Фатальные ошибки

        internal static readonly string ApplicationThreadException = "Application.ThreadException";
        internal static readonly string AppDomainCurrentDomainUnhandledException = "AppDomain.CurrentDomain.UnhandledException";

        #endregion

        #region Русская кодировка

        internal static readonly int RussianCodePage = 1251;
        internal static readonly string RussianCulrure = "ru-RU";
        internal static readonly string UsCulrure = "en-US";

        #endregion

        #region Шифрование

        internal static readonly byte[] SaltBinary = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
        internal static readonly string SaltText = "we1022@alA0";
        internal static string PasswordForTheKeyFile = "Enot2OOpoloskun#";

        #endregion

        #region Форматы

        internal static readonly string PairSemicolonFormat = "{0}:{1}";
        internal static readonly string PairSemicolonSpaceFormat = "{0}: {1}";
        internal static readonly string PairSpaceFormat = "{0} {1}";
        internal static readonly string PairSpaceDashFormat = "{0} - {1}";
        internal static readonly string AutoLogOnFormat = "Автовход через {0} сек";
        internal static readonly string ErrorReadProfileFormat = "При чтении профайла {0} найдена ошибка";
        internal static readonly string ErrorWriteProfileFormat = "При записи профайла {0} возникла ошибка";
        internal static readonly string UriHostFormat = "{0}://{1}";
        internal static readonly string DnsLookupFailedFormat = "DNS Lookup for {0} failed.";
        internal static readonly string ConnectionFailedFormat = "Connection to {0} failed.<br>Exception: {1}";
        internal static readonly string SessionNumberFormat = "Session #{0}";

        #endregion

        #region Расширения

        internal static readonly string ProfileExtension = ".profile";
        internal static readonly string ProfileTempExtension = ".profiletmp";
        internal static readonly string HtmlExtension = ".html";
        internal static readonly string HtmExtension = ".htm";
        internal static readonly string JsExtension = ".js";
        internal static readonly string GifExtension = ".gif";
        internal static readonly string JpgExtension = ".jpg";
        internal static readonly string JpegExtension = ".jpeg";
        internal static readonly string PngExtension = ".png";
        internal static readonly string SwfExtension = ".swf";
        internal static readonly string IcoExtension = ".ico";
        internal static readonly string CssExtension = ".css";

        #endregion

        #region Маски

        internal static readonly string AllProfilesMask = "*" + ProfileExtension;

        #endregion

        #region Файлы

        internal static readonly string KeyFile = "abclient2.key";
        internal static readonly string KeyFileTemp = "abclient2.key$";

        #endregion

        #region Папки

        internal static readonly string CacheDir = "abcache";
        internal static readonly string TestDir = "abtest";
        internal static readonly string SamplesDir = "absamples";

        #endregion

        #region Адреса и хосты

        internal static readonly string HttpPrefix = "http://";
        internal static readonly string HttpWww = "www";
        internal static readonly string NeverHostWithNoWwwAndDot = "neverlands.ru";
        internal static readonly string NeverHostWithNoWww = "." + NeverHostWithNoWwwAndDot;
        internal static readonly string GameHost = HttpWww + NeverHostWithNoWww;
        internal static readonly string GameReferalUrl = GameHost + "/cgi-bin/go.cgi?uid=";
        internal static readonly string GameCaptchaUrl = GameHost + "/modules/code/code.php?";
        internal static readonly string GameMainUrl = GameHost + "/main.php";
        internal static readonly string ForumHost = "forum" + NeverHostWithNoWww;
        internal static readonly string GameUrl = HttpPrefix + GameHost;
        internal static readonly string ExitUrl = HttpPrefix + GameHost + "/exit.php";

        #endregion

        #region Прокси

        internal static readonly string VisitedEntry = "Visited:";
        internal static readonly int DefaultPort = 8052;
        internal static readonly string Local = "local";
        internal static readonly string LocalHost = "localhost";
        internal static readonly string BasicSpace = "Basic ";
        internal static readonly string Http = "http";
        internal static readonly string HttpWithSlash = "HTTP/";
        internal static readonly string HttpVersionOnePointOne = HttpWithSlash + "1.1";
        internal static readonly string HttpOk = "OK";
        internal static readonly string Host = "Host";
        internal static readonly string ContentType = "Content-Type";
        internal static readonly string TransferEncoding = "Transfer-encoding";
        internal static readonly string ContentLength = "Content-Length";
        internal static readonly string MimeTextHtml = "text/html";
        internal static readonly string Connection = "Connection";
        internal static readonly string Close = "close";
        internal static readonly string Chunked = "chunked";
        internal static readonly string WebAuthenticate = "WWW-Authenticate";
        internal static readonly string ProxyAuthenticate = "Proxy-Authenticate";
        internal static readonly string ProxyAuthorization = "Proxy-Authorization";
        internal static readonly string ProxySupport = "Proxy-Support";
        internal static readonly string SessionBasedAuthentication = "Session-Based-Authentication";
        internal static readonly string Cookie = "Cookie";
        internal static readonly string SetCookie = "Set-Cookie";
        internal static readonly string KeepAlive = "Keep-Alive";
        internal static readonly string ContentEncoding = "Content-Encoding";
        internal static readonly string Authorization = "Authorization";
        internal static readonly string Date = "Date";

        internal static readonly string HttpBrowserError = "Browser error";
        internal static readonly string HttpBadRequest = "Bad Request";
        internal static readonly string HttpReadRequestFailed = "Read request was failed.\n";
        internal static readonly string HttpRequestTooLarge = "HTTP request too large.\n";
        internal static readonly string HttpRequestBadHeader = "Incorrectly formed request header\n";
        internal static readonly string HttpHostMissing = "Host is missing.";
        internal static readonly string HttpRequestParsingFailed = "Request header parsing failed.";
        internal static readonly string HttpRequestContentLengthFailed = "Request Content-Length header parsing failed.";
        internal static readonly string HttpSendFailure = "Send Failure";
        internal static readonly string HttpResendRequestFailed = "ResendRequest failed";
        internal static readonly string HttpOutOfMemory = "Out of memory.";
        internal static readonly string Http200Ok = "200 OK without Headers";
        internal static readonly string DnsFailed = "DNS Lookup Failed";
        internal static readonly string ConnectionFailed = "Connection Failed";
        internal static readonly string HttpBadResponse = "Bad Response";
        internal static readonly string HttpResponseParsingFailed = "Response Header parsing failed.";
        internal static readonly string HttpResponseFailed = "Return response failed.\n";
        internal static readonly string HttpNotModified = "HTTP/1.1 304 Not Modified\r\nServer: Internal\r\n\r\n";
        internal static readonly string HttpResultOk = "HTTP/1.1 200 OK\r\nServer: Internal\r\n\r\n";
        internal static readonly string IfModifiedSince = "If-Modified-Since";
        internal static readonly string IfNoneMatch = "If-None-Match";
        internal static readonly string HttpServerError = "Server error";
        internal static readonly string Gzip = "gzip";
        internal static readonly string Deflate = "deflate";

        internal static readonly string ConnectionGatewayFailedText =
            "Connection to Gateway failed.<br>Exception Text: ";

        internal static readonly string HttpErrorHead =
            @"<html><head>" +
            @"<META Http-Equiv=""Cache-Control"" Content=""No-Cache"">" +
            @"<META Http-Equiv=""Pragma"" Content=""No-Cache"">" +
            @"<META Http-Equiv=""Expires"" Content=""0"">" +
            @"<style type=""text/css"">" +
            "body {" +
            "font-family:Tahoma, Verdana, Arial, Verdana, Arial, Helvetica, Tahoma, Verdana, sans-serif;" +
            "font-size:11px;" +
            "text-decoration:none;" +
            "color:black;" +
            "background-color:white;" +
            "}" +
            ".massm { color:white; background-color:#003893; }" +
            ".gray { color:gray; }" +
            "</style>" +
            "</head><body>" +
            @"<SPAN class=massm>&nbsp;" +
            ApplicationName +
            "&nbsp;</SPAN> ";

        internal static readonly string HttpErrorHeadTail = @"<br><br><span class=""gray"">";
        internal static readonly string HttpErrorFinal = "</span></body></html>";

        #endregion

        #region Теги

        //internal static readonly string EnabledVersionTag = "ev";

        internal static readonly string SpanEnabledVersions = "<span id=ev>";
        internal static readonly string SpanLastVersion = "<span id=lv>";
        //internal static readonly string SpanBoFreq = "<span id=boreq>";
        internal static readonly string SpanClose = "</span>";

        public const string TagLezDoAutoboi = "LezDoAutoboi";
        public const string TagLezDoWaitHp = "LezDoWaitHp";
        public const string TagLezDoWaitMa = "LezDoWaitMa";
        public const string TagLezWaitHp = "LezWaitHp";
        public const string TagLezWaitMa = "LezWaitMa";

        public const string TagLezDoDrinkHp = "LezDoDrinkHp";
        public const string TagLezDoDrinkMa = "LezDoDrinkMa";
        public const string TagLezDrinkHp = "LezDrinkHp";
        public const string TagLezDrinkMa = "LezDrinkMa";

        public const string TagLezDoWinTimeout = "LezDoWinTimeout";
        public const string TagLezSay = "LezSay";
        public const string TagLezBotsGroups = "LezBotsGroups";
        public const string TagLezBotsGroup = "LezBotsGroup";
        public const string AttrLezBotsGroupId = "Id";
        public const string AttrLezBotsMinimalLevelId = "MinimalLevel";
        public const string AttrLezBotsDoRestoreHp = "DoRestoreHp";
        public const string AttrLezBotsDoRestoreMa = "DoRestoreMa";
        public const string AttrLezBotsRestoreHp = "RestoreHp";
        public const string AttrLezBotsRestoreMa = "RestoreMa";
        public const string AttrLezBotsDoAbilBlocks = "DoAbilBlocks";
        public const string AttrLezBotsDoAbilHits = "DoAbilHits";
        public const string AttrLezBotsDoMagHits = "DoMagHits";
        public const string AttrLezBotsMagHits = "MagHits";
        public const string AttrLezBotsDoMagBlocks = "DoMagBlocks";
        public const string AttrLezBotsDoHits = "DoHits";
        public const string AttrLezBotsDoBlocks = "DoBlocks";
        public const string AttrLezBotsDoMiscAbils = "DoMiscAbils";
        public const string AttrLezBotsDoStopNow = "DoStopNow";
        public const string AttrLezBotsDoStopLowHp = "DoStopLowHp";
        public const string AttrLezBotsDoStopLowMa = "DoStopLowMa";
        public const string AttrLezBotsStopLowHp = "StopLowHp";
        public const string AttrLezBotsStopLowMa = "StopLowMa";
        public const string AttrLezBotsDoExit = "DoExit";
        public const string AttrLezBotsDoExitRisky = "DoExitDoExitRisky";
        public const string AttrLezBotsSpellsHits = "SpellsHits";
        public const string AttrLezBotsSpellsBlocks = "SpellsBlocks";
        public const string AttrLezBotsSpellsRestoreHp = "SpellsRestoreHp";
        public const string AttrLezBotsSpellsRestoreMa = "SpellsRestoreMa";
        public const string AttrLezBotsSpellsMisc = "SpellsMisc";

        public const string TagMapCell = "cell";
        public const string AttrMapCellNumber = "cellNumber";
        public const string AttrMapCellCost = "cost";
        public const string AttrMapCellHasFish = "hasFish";
        public const string AttrMapCellHasWater = "hasWater";
        public const string AttrMapCellHerbGroup = "herbGroup";
        public const string AttrMapCellName = "name";
        public const string AttrMapCellTooltip = "tooltip";
        public const string AttrMapCellUpdated = "updated";
        public const string AttrMapCellNameUpdated = "nameUpdated";
        public const string AttrMapCellCostUpdated = "costUpdated";
        public const string TagMapBots = "bots";
        public const string AttrMapBotsName = "name";
        public const string AttrMapBotsMinLevel = "minLevel";
        public const string AttrMapBotsMaxLevel = "maxLevel";
        public const string AttrMapBotsC = "c";
        public const string AttrMapBotsD = "d";

        public const string TagAbcMapCell = "cell";
        public const string AttrAbcMapRegNum = "regnum";
        public const string AttrAbcMapCost = "cost";
        public const string AttrAbcMapLabel = "label";
        public const string AttrAbcMapVisited = "visited";
        public const string AttrAbcMapVerified = "verified";

        public const string TagDoContactTrace = "docontacttrace";
        public const string TagDoBossTrace = "dobosstrace";

        public const string TagBossSay = "BossSay";

        public const string TagSkinAuto = "SkinAuto";

        #endregion

        #region Сообщения в диалоговых окнах

        internal static readonly string MessageKeyUpdated = "Ключ обновлен";
        internal static readonly string MessageClientWillBeRestarted = "Сейчас клиент будет перезапущен";
        internal static readonly string MessageErrorKeyFileUpdate = "Ошибка обновления ключа";
        internal static readonly string MessageDownloadKeyFileCanceled = "Обновление ключа отменено";
        internal static readonly string MessageKeyFileError = "Ошибка открытия файла ключа";
        internal static readonly string MessageKeyDataError = "Ключ испорчен";
        internal static readonly string MessageKeyExpired = "Ключ устарел";
        internal static readonly string MessageKeyAskUpdate = "Обновить ключ с сайта клиента?";
        internal static readonly string MessageVerisonObsolete = "Ваша версия клиента устарела. Обновите клиент!";

        #endregion

        #region Автовход

        internal static readonly int AutoLogOnCountDown = 3;

        #endregion

        #region Теги конфига

        internal static readonly string ConfigProfile = "Profile";
        internal static readonly string ConfigTableMain = "Main";
        internal static readonly string ConfigTableFavLocations = "FavLocations";
        internal static readonly string ConfigTableContactGroups = "Contactgroups";
        internal static readonly string ConfigTableContacts = "Contacts";
        internal static readonly string ConfigTableTimers = "Timers";
        internal static readonly string ConfigTableTabs = "Tabs";
        internal static readonly string ConfigTableFoeGroups = "FoeGroups";

        #endregion

        #region Куски html-кода

        internal static readonly string HtmlValueRiba = @"value=""" + "Рыбалка" + @"""";
        internal static readonly string HtmlCodePhp = "/code/code.php?";
        internal static readonly string HtmlCodePhpFull = GameUrl + "/modules" + HtmlCodePhp;

        #endregion

        #region Всплывающие подсказки на карте природы

        internal static readonly string HtmlMapPopupCss =
            @"<style type=""text/css"">" +
            @".invborder { background-color: #303030; } " +
            @".invback { background-color: #EEEEEE; } " +
            @".invimg { width: 50px; height: 50px; border: 1px; } " +
            @".invsmall { font: 9px Tahoma; color: #222222; } " +
            @".invspancell { font: 10px Tahoma; color: #222222; } " +
            @".invtitle { font: 11px Tahoma; font-weight: bold; color: #222222; } " +
            @".invfish { color: #000080; } " +
            @".invht { color: #006600; } " +
            @".invha { color: #009900; } " +
            @".invhn { color: #003300; } " +
            ".slobar {" +
            "filter:alpha(opacity=90);" +
            "position: absolute;" +
            "left: 0px;" +
            "visibility: hidden; }" +
            "</style>" +
            @"<script type=""text/javascript"">" +
            "function showslo(ev, id) {" +
            "MouseX = ev.clientX + document.body.scrollLeft;" +
            "MouseY = ev.clientY + document.body.scrollTop;" +
            @"obj = document.getElementById(""sloi["" + id + ""]"");" +
            @"obj.style.top = MouseY + 20 + ""px"";" +
            @"obj.style.left = MouseX + 10 + ""px"";" +
            @"obj.style.visibility = ""visible"";" +
            "}" +
            "function hideslo(id) {" +
            @"document.getElementById(""sloi["" + id + ""]"").style.visibility=""hidden"";" +
            "}" +
            "</script>";

        #endregion

        /// <summary>
        /// Вывод перед ником.
        /// </summary>
        internal const string NickForm = "Ник: ";

        /// <summary>
        /// Разделительное тире.
        /// </summary>
        //internal const string DashFormat = " - ";

        //internal const string AbcObrazyUrl = "http://abclient.1gb.ru/abcobrazy/";
        //internal const string NeverObrazyUrl = "http://image.neverlands.ru/obrazy/";
        //internal const string FishPortUrl = "http://fishport/";

        internal const string LogsDir = "logs";

        internal const string Br = "[BR]";

        internal const string HtmlBr = "<br>";

        internal const int MapBigWidthMax = 19;
        internal const int MapBigWidthMin = 3;
        internal const int MapBigHeightMax = 19;
        internal const int MapBigHeightMin = 3;
        internal const int MapBigScaleMin = 50;
        internal const int MapBigScaleMax = 150;
        internal const int MapBigTransparencyMin = 0;
        internal const int MapBigTransparencyMax = 100;
        internal const int MapMiniWidthMax = 19;
        internal const int MapMiniWidthMin = 3;
        internal const int MapMiniHeightMax = 19;
        internal const int MapMiniHeightMin = 3;
        internal const int MapMiniScaleMin = 50;
        internal const int MapMiniScaleMax = 150;

        internal const string ConstTagObraz = "uo";
        internal const string ConstAttibuteObrazNick = "n";
        internal const string ConstAttibuteObrazImage = "o";
        internal const string ConstAttibuteObrazDescription = "d";
        internal const string ConstAttibuteObrazEnabled = "e";
        internal const string ConstAttibuteObrazIsEnabled = "1";

        internal const int TorgAdvTimeDefault = 10;
        internal const int TorgAdvTimeMin = 1;
        internal const int TorgAdvTimeMax = 59;

        internal const string HtmlCounters = @"document.write(view_top());";

        internal const string Bait = "Приманку Для Ботов";
        internal const int FastTotemMessageTimeBlockSeconds = 5;

        internal const string FileMap = "abcells.xml";


    }
}