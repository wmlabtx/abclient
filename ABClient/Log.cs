using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ABClient
{
    public static class Log
    {
        private static readonly string LogPath = Path.Combine(
            Application.StartupPath,
            AppVars.Profile.FileName + '\\' + "debug" + '\\');

        private static readonly ReaderWriterLock Lock = new ReaderWriterLock();

        private static string FileLogName
        {
            get
            {
                var fileName = $"{AppVars.Profile.FileName}{DateTime.Now.Year:D4}{DateTime.Now.Month:D2}{DateTime.Now.Day:D2}.txt";
                return Path.Combine(LogPath, fileName);
            }
        }

        public static void Write(string address, string html)
        {
            /*
            try
            {
                Lock.AcquireWriterLock(5000);
                try
                {
                    var serverTime = DateTime.Now.Subtract(AppVars.Profile.ServDiff);
                    var sb = new StringBuilder();
                    sb.AppendLine();
                    sb.Append($"{serverTime:F} {address}");
                    sb.AppendLine();
                    sb.AppendLine();
                    sb.Append(html);
                    sb.AppendLine();

                    if (!Directory.Exists(LogPath))
                        Directory.CreateDirectory(LogPath);

                    File.AppendAllText(FileLogName, sb.ToString());

                }
                finally
                {
                    Lock.ReleaseWriterLock();
                }
            }
            catch(ApplicationException)
            {
            }
            */
        }

        public static void Write(string message)
        {
            /*
            try
            {
                Lock.AcquireWriterLock(5000);
                try
                {
                    var serverTime = DateTime.Now.Subtract(AppVars.Profile.ServDiff);
                    var sb = new StringBuilder();
                    sb.Append($"{serverTime:F} {message}");
                    sb.AppendLine();

                    if (!Directory.Exists(LogPath))
                        Directory.CreateDirectory(LogPath);

                    File.AppendAllText(FileLogName, sb.ToString());
                }
                finally
                {
                    Lock.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
            */
        }
    }
}
