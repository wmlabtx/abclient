using System;
using System.Windows.Forms;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        internal void StartBulkSell(string thing, string price, string link)
        {
            int p;
            if (!int.TryParse(price, out p)) return;
            AppVars.BulkSellThing = thing;
            AppVars.BulkSellPrice = p;
            AppVars.BulkSellSum = 0;

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        internal void StartBulkDrop(string thing, string price)
        {
            AppVars.BulkDropThing = thing;
            AppVars.BulkDropPrice = price;

            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(
                        new ReloadMainPhpInvokeDelegate(AppVars.MainForm.ReloadMainPhpInvoke),
                        new object[] { });
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        internal void StartBulkOldSell(string name, string price)
        {
            AppVars.BulkSellOldName = name;
            AppVars.BulkSellOldPrice = price;

            foreach (var shopEntry in AppVars.ShopList)
            {
                if (string.IsNullOrEmpty(shopEntry.Name) ||
                    !shopEntry.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) ||
                    string.IsNullOrEmpty(shopEntry.Price) ||
                    !shopEntry.Price.Equals(price, StringComparison.CurrentCultureIgnoreCase))
                    continue;

                var pars = shopEntry.SellCall.Split(',');
                var a1 = pars[0].Trim(' ');
                WriteChatMsgSafe($"Сдача {shopEntry.Name} (ID:{a1}) за {shopEntry.Price}NV...");
                return;
            }
        }
    }
}