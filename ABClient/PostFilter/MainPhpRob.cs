namespace ABClient.PostFilter
{
    using MyHelpers;

    internal static partial class Filter
    {
        private static string MainPhpRob(string html)
        {
            // var fight_ty = [1,300,10,0,2,"","","1","1948222238",[],[800817,"5b5ce67a064cd39446d488a22b366525","БИЗОНИУС",1387692418]];
            var strfightty = HelperStrings.SubString(html, "var fight_ty = [", "];");
            if (string.IsNullOrEmpty(strfightty))
                return null;

            var xfightty = HelperStrings.ParseJsString(strfightty);

            // Можно ли обокрасть?
            if ((xfightty.Count > 10) && (xfightty[10].Count > 1))
            {
                //if(fight_ty[10].length > 0)
                //{
                //    rbut = '<input type=button class=fbut value="Обокрасть игрока '+fight_ty[10][2]+'" onclick="location=
                // \'main.php?get_id=17&type=0&p='+fight_ty[10][3]+'&uid='+fight_ty[10][0]+'&s=0&m=0&vcode='+fight_ty[10][1]+'\'">';
                //} 

                var robLink =
                    "http://www.neverlands.ru/main.php?get_id=17&type=0&p=" +
                    xfightty[10][3] +
                    "&uid=" +
                    xfightty[10][0] +
                    "&s=0&m=0&vcode=" +
                    xfightty[10][1];

                return BuildRedirect("Кража", robLink); 
            }

            return null;
        }
    }
}
