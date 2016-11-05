namespace ABClient.MyHelpers
{
    using System;

    internal static class HelperDice
    {
        private static Random rand;

        internal static int Make(int max)
        {
            if (rand == null)
            {
                rand = new Random();
            }

            return rand.Next(max);
        }

        internal static int Make(int min, int max)
        {
            if (rand == null)
            {
                rand = new Random();
            }

            return rand.Next(min, max);
        }
    }
}