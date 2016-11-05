namespace ABClient.MyChat
{
    using System;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    internal static class Chat
    {
        private const string ConstLogChatFormat = "chat{0:00}{1:00}{2:00}.html";
        private const string ConstErrorLogSaveTitle = "Ошибка сохранения чата";

        private static readonly StringCollection AnswersCollection = new StringCollection();
        private static readonly StringBuilder ChatBody = new StringBuilder();
        private static readonly string LogPath = Path.Combine(
            Application.StartupPath, 
            AppVars.Profile.FileName + '\\' + AppConsts.LogsDir + '\\');

        static Chat()
        {
            LastChanged = DateTime.Now;
        }

        internal static DateTime LastChanged { private get; set; }

        internal static bool Critical { private get; set; }

        /// <summary>
        /// Добавляет сообщение для автоответчика
        /// </summary>
        /// <param name="message"></param>
        internal static void AddAnswer(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (AnswersCollection.SyncRoot != null)
            {
                lock (AnswersCollection.SyncRoot)
                {
                    AnswersCollection.Add(message);
                }
            }
        }

        /// <summary>
        /// Забирает сообщение из очереди для автоответчика
        /// </summary>
        /// <returns></returns>
        internal static string GetAnswer()
        {
            if (Critical || (DateTime.Now.Subtract(LastChanged).TotalSeconds < 3))
            {
                return string.Empty;
            }

            if (AnswersCollection.SyncRoot != null)
            {
                lock (AnswersCollection.SyncRoot)
                {
                    if (AnswersCollection.Count == 0)
                    {
                        return string.Empty;
                    }

                    var message = AnswersCollection[0];
                    AnswersCollection.RemoveAt(0);
                    LastChanged = DateTime.Now;
                    return message;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Добавляет сообщение для чата (и возможно сохраняет в лог)
        /// </summary>
        /// <param name="message"></param>
        internal static void AddStringToChat(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (ChatBody.Length > 0)
            {
                ChatBody.Append(AppConsts.HtmlBr);
            }

            ChatBody.Append(message);
            if (AppVars.Profile.ChatKeepLog)
            {
                TextWriter textWriter = null;
                try
                {
                    if (!Directory.Exists(LogPath))
                    {
                        Directory.CreateDirectory(LogPath);
                    }

                    var logName = GetLogName();
                    if (!File.Exists(logName))
                    {
                        textWriter = new StreamWriter(logName, false, Helpers.Russian.Codepage, 1024);
                        textWriter.WriteLine(
                            @"<HTML>" +
                            @"<META Content=""text/html; Charset=windows-1251"" Http-Equiv=Content-type>" +
                            @"<HEAD>" +
                            @"<LINK href=""http://www.neverlands.ru/ch/chat.css"" rel=STYLESHEET type=text/css>" +
                            @"</HEAD>" +
                            @"<BODY LeftMargin=2 TopMargin=2 RightMargin=2 MarginHeight=2 MarginWidth=2 BgColor=#F5F5F5>");
                    }
                    else
                    {
                        textWriter = new StreamWriter(logName, true, Helpers.Russian.Codepage, 1024);
                    }

                    textWriter.WriteLine(AppConsts.HtmlBr);
                    textWriter.Write(message);
                }
                catch (IOException ex)
                {
                    LogSaveError(ex.Message);
                }
                catch (ArgumentNullException ex)
                {
                    LogSaveError(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    LogSaveError(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    LogSaveError(ex.Message);
                }
                catch (NotSupportedException ex)
                {
                    LogSaveError(ex.Message);
                }
                catch (UnauthorizedAccessException ex)
                {
                    LogSaveError(ex.Message);
                }
                finally
                {
                    if (textWriter != null)
                    {
                        textWriter.Close();
                    }
                }
            }
        }

        private static void LogSaveError(string message)
        {
            MessageBox.Show(
                message,
                ConstErrorLogSaveTitle,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        internal static string GetLogName()
        {
            var fileName = string.Format(
                CultureInfo.InvariantCulture,
                ConstLogChatFormat,
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day);
            return Path.Combine(LogPath, fileName);
        }
    }
}
