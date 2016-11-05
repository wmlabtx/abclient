namespace ABClient.PostFilter
{
    using MyHelpers;

    internal static partial class Filter
    {
        private static string MainPhpRaz(string html)
        {
            var strfightty = HelperStrings.SubString(html, "var fight_ty = [", "];");
            if (string.IsNullOrEmpty(strfightty))
                return null;

            var xfightty = HelperStrings.ParseJsString(strfightty);

            //if(fight_ty[9].length > 0)
            //{
            //    rbut = '<input type=button class=fbut value="Разделать" onclick="location=\'
            //    main.php?get_id=17&type='+fight_ty[9][0]+'&p='+fight_ty[9][1]+'&uid='+fight_ty[9][2]+'&s='+fight_ty[9][3]+'&m='+fight_ty[9][4]+'&vcode='+fight_ty[9][5]+'\'">';
            //}

            // Можно ли провести разделку?
            if ((xfightty.Count > 9) && (xfightty[9].Count > 1))
            {
                var razLink =
                    "http://www.neverlands.ru/main.php?get_id=17&type=" +
                    xfightty[9][0] +
                    "&p=" +
                    xfightty[9][1] +
                    "&uid=" +
                    xfightty[9][2] +
                    "&s=" +
                    xfightty[9][3] +
                    "&m=" +
                    xfightty[9][4] +
                    "&vcode=" +
                    xfightty[9][5];
                
                return BuildRedirect("Разделка", razLink); 
            }

            return null;
        }
    }
}
