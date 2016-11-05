namespace ABClient.ABProxy
{
    using System;
    using System.Globalization;

    internal sealed class HttpHeaderItem : ICloneable
    {
        internal string Name { get; set; }

        internal string Value { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}: {1}", Name, Value);
        }
    }
}