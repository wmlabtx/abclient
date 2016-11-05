namespace ABClient
{
    using System;

    internal class FishTip : IComparable
    {
        internal FishTip(int money, int fishUm, string location,  int maxBotLevel, string botDescription, string tip)
        {
            Money = money;
            FishUm = fishUm;
            Location = location;
            MaxBotLevel = maxBotLevel;
            BotDescription = botDescription;
            Tip = tip;
        }

        internal int Money { get; private set; }

        internal int FishUm { get; private set; }
        
        internal string Location { get; private set; }
        
        internal int MaxBotLevel { get; private set; }
        
        internal string BotDescription { get; private set; }
        
        internal string Tip { get; private set; }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is FishTip))
            {
                return -1;
            }

            var other = (FishTip)obj;
            if (Money > other.Money)
            {
                return -1;
            }

            return Money < other.Money ? 1 : 0;
        }

        #endregion
    }
}