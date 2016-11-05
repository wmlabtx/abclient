namespace ABClient.Helpers
{
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Все, что относится к русской кодировке.
    /// </summary>
    internal static class Russian
    {
        internal static readonly Encoding Codepage = Encoding.GetEncoding(AppConsts.RussianCodePage);
        internal static readonly CultureInfo Culture = CultureInfo.GetCultureInfo(AppConsts.RussianCulrure);
    }
}