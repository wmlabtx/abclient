namespace ABClient.PostFilter
{
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] CounterJs()
        {
            return Russian.Codepage.GetBytes(
                "function counterview(referr){}"
                );
        }
    }
}