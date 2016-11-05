namespace ABClient
{
    internal class TorgPair
    {
        internal TorgPair(int priceLow, int priceHi, int bonus)
        {
            PriceLow = priceLow;
            PriceHi = priceHi;
            Bonus = bonus;
        }

        internal int PriceLow { get; private set; }

        internal int PriceHi { get; private set; }

        internal int Bonus { get; private set; }
    }
}