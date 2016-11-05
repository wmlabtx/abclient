namespace ABClient.MyProfile
{
    internal struct TPers
    {
        internal bool Guamod;
        internal double IntHP;
        internal double IntMA;

        /// <summary>
        /// Когда истечет время перед продолжением боя.
        /// </summary>
        internal long Ready;

        /// <summary>
        /// ID лога, к которому относится Ready.
        /// </summary>
        internal string LogReady;
    }
}