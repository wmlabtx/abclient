namespace ABClient
{
    using System;
    using System.Globalization;

    internal sealed class VersionClass
    {
        internal VersionClass(string productName, string productVersion)
        {
            Product = productName;
            int pos = productVersion.LastIndexOf('.');
            ShortVersion = pos != -1 ? productVersion.Substring(0, pos) : productVersion;
            ProductShortVersion = string.Format(CultureInfo.InvariantCulture, "{0} {1}", productName, ShortVersion);
        }

        internal string Product { get; private set; }

        /// <summary>
        /// Например, "1.99.0".
        /// </summary>
        internal string ShortVersion { get; private set; }

        /// <summary>
        /// Например, "ABClient 1.99.0".
        /// </summary>
        internal string ProductShortVersion { get; private set; }

        /// <summary>
        /// Например, "Черный - ABClient 1.99.0".
        /// </summary>
        internal string NickProductShortVersion { get; private set; }

        internal void AddNick(string nick)
        {
            NickProductShortVersion = string.Format(CultureInfo.InvariantCulture, "{0} - {1}", nick, ProductShortVersion);
        }

        internal bool IsCurrentVersionAllowed(string[] arrayVersions)
        {
            if (ShortVersion != null)
            {
                return Array.FindIndex(arrayVersions, EqialWithStringVersion) != -1;
            }

            return false;
        }

        internal bool IsNewerVersionOnSite(string siteVersion)
        {
            var splitMyVersion = ShortVersion.Split('.');
            if (splitMyVersion.Length != 3)
            {
                return false;
            }

            int myVersionOne;
            if (!int.TryParse(splitMyVersion[0], out myVersionOne))
            {
                return false;
            }

            int myVersionTwo;
            if (!int.TryParse(splitMyVersion[1], out myVersionTwo))
            {
                return false;
            }

            int myVersionThree;
            if (!int.TryParse(splitMyVersion[2], out myVersionThree))
            {
                return false;
            }

            var splitSiteVersion = siteVersion.Split('.');
            if (splitSiteVersion.Length != 3)
            {
                return false;
            }

            int siteVersionOne;
            if (!int.TryParse(splitSiteVersion[0], out siteVersionOne))
            {
                return false;
            }

            int siteVersionTwo;
            if (!int.TryParse(splitSiteVersion[1], out siteVersionTwo))
            {
                return false;
            }

            int siteVersionThree;
            if (!int.TryParse(splitSiteVersion[2], out siteVersionThree))
            {
                return false;
            }

            if (siteVersionOne > myVersionOne)
            {
                return true;
            }

            if (siteVersionOne == myVersionOne)
            {
                if (siteVersionTwo > myVersionTwo)
                {
                    return true;
                }

                if (siteVersionTwo == myVersionTwo)
                {
                    if (siteVersionThree > myVersionThree)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool EqialWithStringVersion(string str)
        {
            var result = ShortVersion.Equals(str, StringComparison.Ordinal);
            return result;
        }
    }
}