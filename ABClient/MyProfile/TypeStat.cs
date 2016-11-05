namespace ABClient.MyProfile
{
    using System.Collections.Generic;

    internal sealed class TypeStat
    {
        internal long LastReset;
        internal int LastUpdateDay;
        internal bool Reset;
        internal long Traffic;
        internal long SavedTraffic;
        internal string Drop;
        internal int Show;
        internal long XP;
        internal int NV;
        internal int FishNV;
        internal List<TypeItemDrop> ItemDrop;
    }
}
