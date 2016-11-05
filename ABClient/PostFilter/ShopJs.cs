using ABClient.Helpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static byte[] ShopJs(byte[] array)
        {
            var html = Russian.Codepage.GetString(array);
            html = html.Replace(
                "AjaxPost('shop_ajax.php', data, function(xdata) {",
                $"AjaxPost('shop_ajax.php', data, function(xdata){{ var arg1 = window.external.BulkSellOldArg1(); var arg2 =  window.external.BulkSellOldArg2(); if (arg1 > 0) shop_item_sell(arg1, arg2);");

            return Russian.Codepage.GetBytes(html);
        }
    }
}
