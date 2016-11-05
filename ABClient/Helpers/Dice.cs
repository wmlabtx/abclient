namespace ABClient.Helpers
{
    using System;

    internal static class Dice
    {
        private static readonly Random Rand = new Random();

        internal static int Make(int max)
        {
            return Rand.Next(max);
        }

        internal static int Make(int min, int max)
        {
            return Rand.Next(min, max);
        }
    }
}