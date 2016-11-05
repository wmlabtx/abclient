using ABClient.ABForms;

namespace ABClient.ExtMap
{
    using System.Runtime.InteropServices;
    using MyForms;

    [ComVisible(true)]
    public class NavScriptManager
    {
        private readonly FormNavigator _formnavigator;

        public NavScriptManager(FormNavigator form)
        {
            _formnavigator = form;
        }

        public void MoveTo(string dest)
        {
            _formnavigator.PointToDest(new [] { dest });
        }

        public string GetCellLabel(int x, int y)
        {
            var result = FormMain.GetCellLabel(x, y);
            return result;
        }

        public bool IsCellExists(int x, int y)
        {
            var result = FormMain.IsCellExists(x, y);
            return result;
        }

        public bool IsCellInPath(int x, int y)
        {
            var result = Map.IsCellInPath(x, y);
            return result;
        }

        public string GenMoveLink(int x, int y)
        {
            return FormMain.GenMoveLink(x, y);
        }

        public string CellDivText(int x, int y, int scale, string link, bool showmove, bool isframe)
        {
            var html = Map.CellDivText(x, y, scale, link, showmove, isframe);
            return html;
        }

        public string CellAltText(int x, int y, int scale)
        {
            var text = Map.CellAltText(x, y, scale);
            return text;
        }
    }
}
