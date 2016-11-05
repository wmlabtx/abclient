using System.Globalization;
using ABClient.MyHelpers;

namespace ABClient.PostFilter
{
    internal static partial class Filter
    {
        private static string MainPhpDrinkHpMa(string address, string html)
        {
            // var inshp = [730,730,1787,1787,1500,3462];
            // ins_HP(730,730,1787,1787,1500,3462);

            if (!AppVars.Profile.LezDoDrinkHp && !AppVars.Profile.LezDoDrinkMa)
                return null;

            var sp = HelperStrings.SubString(html, "var inshp = [", "];");
            if (string.IsNullOrEmpty(sp))
                sp = HelperStrings.SubString(html, "ins_HP(", ");");

            if (string.IsNullOrEmpty(sp))
                return null;

            var pars = sp.Split(',');
            if (pars.Length != 6)
                return null;

            double d;
            if (!double.TryParse(pars[0], NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return null;

            var currentHp = (int)d;
            if (currentHp < 0)
                currentHp = 0;

            if (!double.TryParse(pars[1], NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return null;

            var maxHp = (int)d;
            if (maxHp <= 0)
                maxHp = 0;

            if (!double.TryParse(pars[2], NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return null;

            var currentMa = (int)d;
            if (currentMa < 0)
                currentMa = 0;

            if (!double.TryParse(pars[3], NumberStyles.Any, CultureInfo.InvariantCulture, out d))
                return null;

            var maxMa = (int)d;
            if (maxMa <= 0)
                maxMa = 0;

            var percentHp = maxHp > 0 ? (int)((currentHp * 100.0) / maxHp) : 0;
            var percentMa = maxMa > 0 ? (int)((currentMa * 100.0) / maxMa) : 0;

            if (
                (AppVars.Profile.LezDoDrinkHp && maxHp > 0 && percentHp < AppVars.Profile.LezDrinkHp) ||
                (AppVars.Profile.LezDoDrinkMa && maxMa > 0 && percentMa < AppVars.Profile.LezDrinkMa)
                )
            {
                AppVars.DrinkDrinkHpMaCount++;
                if (AppVars.DrinkDrinkHpMaCount > 5)
                {
                    AppVars.MainForm.WriteChatMsgSafe("Слишком много попыток выпить Эликсир Восстановления. Восстановление здоровья/маны вне боя отключено. Не забудьте включить их обратно.");
                    AppVars.Profile.LezDoDrinkHp = false;
                    AppVars.Profile.LezDoDrinkMa = false;
                    AppVars.DrinkDrinkHpMaCount = 0;
                    return null;
                }

                // if(confirm('Использовать Эликсир Восстановления сейчас?')) { location='main.php?get_id=43&act=101&uid=85177140&curs=20&subid=0&ft=0&vcode=b2c6136b715609ab0b8a4429c3dc46ff' }"

                if (!MainPhpIsInv(html))
                {
                    var invHtml = MainPhpFindInv(html, "&im=6");
                    if (!string.IsNullOrEmpty(invHtml))
                        return invHtml;
                }

                if (MainPhpIsInv(html))
                {
                    var link = HelperStrings.SubString(html, "if(confirm('Использовать Эликсир Восстановления сейчас?')) { location='", "' }");
                    if (!string.IsNullOrEmpty(link))
                    {
                        AppVars.MainForm.WriteChatMsgSafe("Используем Эликсир Восстановления...");
                        var htmlElixir = BuildRedirect("Используем Эликсир Восстановления...", link);
                        return htmlElixir;
                    }
                    
                    if (!address.EndsWith("im=6"))
                    {
                        var htmlElixir = BuildRedirect("Переключение на элексиры", "main.php?im=6");
                        return htmlElixir;
                    }

                    AppVars.MainForm.WriteChatMsgSafe("Эликсир Восстановления не найден. Восстановление здоровья/маны вне боя отключено. Не забудьте включить их обратно.");
                    AppVars.Profile.LezDoDrinkHp = false;
                    AppVars.Profile.LezDoDrinkMa = false;
                    AppVars.DrinkDrinkHpMaCount = 0;
                    return null;
                }
            }
            else
            {
                // Сбрасываем счетчик попыток
                AppVars.DrinkDrinkHpMaCount = 0;
            }

            return null;
        }
    }
}
