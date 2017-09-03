namespace ABClient.MyHelpers
{
    using System.Text;

    internal static class HelperErrors
    {
        internal static string Marker()
        {
            return @"<SPAN class=massm>&nbsp;" + AppConsts.ApplicationName + "&nbsp;</SPAN> ";
            //return "&nbsp;";
        }

        internal static string Head()
        {
            var sb = new StringBuilder();
            sb.Append(@"<html><head>");
            /*
            if (trans)
            {
                sb.Append(@"<meta http-equiv=""Page-Enter"" content=""blendTrans(Duration=0.3)"">");
            }
             */ 

            sb.Append(
                @"<META Http-Equiv=""Cache-Control"" Content=""No-Cache"">" +
                @"<META Http-Equiv=""Pragma"" Content=""No-Cache"">" +
                @"<META Http-Equiv=""Expires"" Content=""0"">" +
                @"<style type=""text/css"">" +
                "body {" +
                "  font-family:Tahoma, Verdana, Arial, Verdana, Arial, Helvetica, Tahoma, Verdana, sans-serif;" +
                "	  font-size:11px;" +
                "	  text-decoration:none;" +
                "	  color:black;" +
                "	  background-color:white;" +
                "}" +
                ".massm { color:white; background-color:#003893; }" +
                ".gray { color:gray; }" +
                "</style>" +
                "</head><body>");
            sb.Append(Marker());
            return sb.ToString();
        }
    }
}