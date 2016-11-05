namespace ABClient
{
    using System;

    [Flags]
    internal enum Prims
    {
        Bread = 0x01,
        Worm = 0x02,
        BigWorm = 0x04,
        Stink = 0x08,
        Fly = 0x10,
        Light = 0x20,
        Donka = 0x40,
        Morm = 0x80,
        HiFlight = 0x100
    }
}
