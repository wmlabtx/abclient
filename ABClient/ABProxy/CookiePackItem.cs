namespace ABClient.ABProxy
{
    internal sealed class CookiePackItem
    {
        internal string Name { get; set; }

        internal string Value { get; set; }

        public override string ToString()
        {
            return Name + ": " + Value;
        }
    }
}