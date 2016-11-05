namespace ABClient.PostFilter
{
    using System.Text;
    using Helpers;

    internal static partial class Filter
    {
        private static byte[] LogsJs(byte[] array)
        {
            var sb = new StringBuilder(Russian.Codepage.GetString(array));
            /*
            sb.Replace(AppConstants.HtmlCounters, string.Empty);
            sb.Replace(
                @"case 8: d.write('Результат разделки: <font color=#E34242><b>«'+logs[i][j][2]+'»</b></font>.'+(!logs[i][j][3] ? '' : ' Умение <b>«Охота»</b> повысилось на 1!')); break;",
                @"case 8: var razd = 'Результат разделки: <font color=#E34242><b>«'+logs[i][j][2]+'»</b></font>.'+(!logs[i][j][3] ? '' : ' Умение <b>«Охота»</b> повысилось на 1!'); d.write(razd); window.external.ShowRazd(razd); break;");
            */
            return Russian.Codepage.GetBytes(sb.ToString());
        }
    }
}