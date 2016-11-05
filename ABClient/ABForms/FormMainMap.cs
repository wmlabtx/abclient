using System.Text;

namespace ABClient.ABForms
{
    internal sealed partial class FormMain
    {
        internal static string MapText()
        {
            CheckTied();

            if (AppVars.AutoMoving && AppVars.AutoMovingJumps > 0)
            {
                var sb = new StringBuilder();
                sb.AppendFormat("Пункт назначения: <font color=#FFFF00>{0}</font>", AppVars.AutoMovingDestinaton);
                sb.AppendFormat("<br>Еще переходов: <font color=#FFFF00>{0}</font>", AppVars.AutoMovingJumps);
                if (AppVars.DoSearchBox)
                    sb.AppendFormat("<br>Ищем клад...");

                return sb.ToString();
            }

            return "Перемещаемся на соседнюю клетку...";
        }
    }
}