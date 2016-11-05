using System;
using System.Threading;
using ABClient.ABForms;

namespace ABClient
{
    public static class IdleManager
    {
        private static int _numberOfActiveThreads;
        private static readonly ReaderWriterLock LockNumberOfActiveThreads = new ReaderWriterLock();

        public static void AddActivity()
        {
            try
            {
                LockNumberOfActiveThreads.AcquireWriterLock(5000);
                try
                {
                    _numberOfActiveThreads++;
                    ShowActivity();
                }
                finally
                {
                    LockNumberOfActiveThreads.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        public static void RemoveActivity()
        {
            try
            {
                LockNumberOfActiveThreads.AcquireWriterLock(5000);
                try
                {
                    _numberOfActiveThreads--;                    
                    ShowActivity();
                }
                finally
                {
                    LockNumberOfActiveThreads.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }

            if (_numberOfActiveThreads == 0)
                ContactsManager.Pulse();
        }

        private static void ShowActivity()
        {
            try
            {
                if (AppVars.MainForm != null)
                {
                    AppVars.MainForm.BeginInvoke(new ShowActivityDelegate(AppVars.MainForm.ShowActivity), _numberOfActiveThreads);
                }
            }
            catch (InvalidOperationException)
            {
            }
        }
    }
}
