namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] MapActAjaxPhp(byte[] array)
        {
            var html = AppVars.Codepage.GetString(array);
            return array;
        }
    }
}