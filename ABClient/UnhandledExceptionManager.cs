namespace ABClient
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using ABForms;

    internal static class UnhandledExceptionManager
    {
        internal static void AddHandler()
        {
            if (Debugger.IsAttached)
            {
                return;
            }

            Application.ThreadException -= ThreadExceptionHandler;
            Application.ThreadException += ThreadExceptionHandler;
            
            AppDomain.CurrentDomain.UnhandledException -= UnhandledExceptionHandler;
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
        }

        internal static void RemoveHandler()
        {
            if (Debugger.IsAttached)
            {
                return;
            }

            Application.ThreadException -= ThreadExceptionHandler;
            AppDomain.CurrentDomain.UnhandledException -= UnhandledExceptionHandler;
        }

        private static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            var exception = e.Exception;
            GenericExceptionHandler(exception);
        }

        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            var exception = (Exception)args.ExceptionObject;
            GenericExceptionHandler(exception);
        }

        private static void GenericExceptionHandler(Exception exception)
        {
            string strException;
            try
            {
                strException = ExceptionToString(exception);
            }
            catch (Exception ex)
            {
                strException = string.Format(CultureInfo.InvariantCulture, "Error '{0}' while generating exception string", ex.Message);
            }

            using (var formError = new FormAutoTrap(strException))
            {
                formError.ShowDialog();
            }

            KillApp();
            Application.Exit();
        }

        private static void KillApp()
        {
            Process.GetCurrentProcess().Kill();
        }

        private static string ExceptionToString(Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine(AppVars.AppVersion.ProductShortVersion);
            sb.AppendLine(Environment.OSVersion.VersionString);
            sb.AppendLine(Application.StartupPath);
            sb.AppendLine();
            sb.AppendLine(exception.Message);
            sb.AppendLine(exception.Source);
            sb.AppendLine(exception.StackTrace);
            return sb.ToString();
        }
    }
}