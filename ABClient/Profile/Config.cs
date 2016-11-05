// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="wmlab.org">
//   Murad Ismayilov
// </copyright>
// <summary>
//   Defines the Config type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ABClient.Profile
{
    using System;
    using System.Collections.Generic;
    using MyProfile;

    internal class Config : IComparable
    {
        private readonly List<string> listtabs = new List<string>();
        private readonly List<TFoeGroup> listfoegroups = new List<TFoeGroup>();
        private readonly List<string> listfavlocations = new List<string>();
        private readonly List<MyTimer> listtimers = new List<MyTimer>();
        private readonly object lockObject = new object();
        private string configFileName;
        private string configFileNameTemp;

        internal Config(string fileName)
        {
            
        }

        #region ToString()

        public override string ToString()
        {
            return UserNick;
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is UserConfig))
            {
                return -1;
            }

            var other = (UserConfig)obj;
            if (ConfigLastSaved > other.ConfigLastSaved)
            {
                return -1;
            }

            return ConfigLastSaved < other.ConfigLastSaved ? 1 : 0;
        }

        #endregion
    }
}
