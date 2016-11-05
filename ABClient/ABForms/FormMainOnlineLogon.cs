using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace ABClient.ABForms
{
    /// <summary>
    /// Отсылка мне на сервер ника и версии.
    /// </summary>
    internal sealed partial class FormMain
    {
        private static void OnlineLogOn(int days)
        {
            try
            {
                var hosts =
                    File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System),
                        @"drivers\etc\hosts"));
                if (hosts.IndexOf("abclient", StringComparison.CurrentCultureIgnoreCase) != -1)
                {
                    AppVars.UserKey = AppVars.UserKeyHostsError;
                    return;
                }
            }
            catch
            {
            }

            AppVars.UserKey = string.Empty;

            WebResponse response = null;
            try
            {
                IdleManager.AddActivity();
                var url = $"http://{CuteConsts.ABClientHostName}/{CuteConsts.ABOnlineLogonPhp}.php";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                httpWebRequest.Proxy = AppVars.LocalProxy;
                var postData = string.Format(
                    CultureInfo.InvariantCulture,
                    "nick={0}&version={1}&days={2}",
                    HttpUtility.UrlEncode(AppVars.Profile.UserNick),
                    AppVars.AppVersion.ShortVersion,
                    days);
                var byteArray = Encoding.ASCII.GetBytes(postData);
                httpWebRequest.ContentLength = byteArray.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                var postStream = httpWebRequest.GetRequestStream();
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();
                response = httpWebRequest.GetResponse();
                var receiveStream = response.GetResponseStream();
                if (receiveStream == null)
                {
                    AppVars.UserKey = AppVars.UserKeyAbclientServerError;
                    return;
                }

                using (var readStream = new StreamReader(receiveStream))
                {
                    var stringResult = readStream.ReadToEnd();
                    var usersOnline = MyHelpers.HelperStrings.SubString(stringResult, "<span id=dayuserscount>", "</span>");
                    if (!string.IsNullOrEmpty(usersOnline))
                    {
                        int numUsers;
                        if (int.TryParse(usersOnline, out numUsers))
                        {
                            AppVars.UsersOnline = usersOnline;
                        }
                    }

                    var notes = MyHelpers.HelperStrings.SubString(stringResult, "<span id=notes>", "</span>");
                    if (string.IsNullOrEmpty(notes))
                    {
                        return;
                    }

                    AppVars.UserKey = notes;
                }
            }
            catch (WebException)
            {
            }
            finally
            {
                response?.Close();
                IdleManager.RemoveActivity();
            }
        }       
    }
}