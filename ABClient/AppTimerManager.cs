using System.Threading;

namespace ABClient
{
    using System.Collections.Generic;
    using System;

    internal static class AppTimerManager
    {
        private static readonly List<AppTimer> ListAppTimers = new List<AppTimer>();
        private static readonly ReaderWriterLock LockTimers = new ReaderWriterLock();

        internal static void SetAppTimers(IEnumerable<AppTimer> appTimers)
        {
            ListAppTimers.Clear();
            foreach (var appTimer in appTimers)
            {
                AddAppTimer(appTimer);
            }
        }

        internal static void AddAppTimer(AppTimer appTimer)
        {
            try
            {
                LockTimers.AcquireWriterLock(5000);
                try
                {
                    if (appTimer.TriggerTime < DateTime.Now)
                    {
                        return;
                    }

                    if (ListAppTimers.Count == 0)
                    {
                        appTimer.Id = 1;
                        ListAppTimers.Add(appTimer);
                        return;
                    }

                    var maxId = 0;
                    foreach (var activeAppTimer in ListAppTimers)
                    {
                        if (activeAppTimer.Id > maxId)
                        {
                            maxId = activeAppTimer.Id;
                        }
                    }

                    appTimer.Id = maxId + 1;
                    var lastIndex = ListAppTimers.Count - 1;
                    if (appTimer.TriggerTime > ListAppTimers[lastIndex].TriggerTime)
                    {
                        ListAppTimers.Add(appTimer);
                    }
                    else
                    {
                        var isAdded = false;
                        var index = lastIndex - 1;
                        while (index >= 0)
                        {
                            if (appTimer.TriggerTime > ListAppTimers[index].TriggerTime)
                            {
                                ListAppTimers.Insert(index + 1, appTimer);
                                isAdded = true;
                                break;
                            }

                            index--;
                        }

                        if (!isAdded)
                        {
                            ListAppTimers.Insert(0, appTimer);
                        }
                    }
                }
                finally
                {
                    LockTimers.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static AppTimer[] GetTimers()
        {
            var arrayAppTimers = ListAppTimers.ToArray();
            return arrayAppTimers;
        }

        internal static void RemoveTimerAt(int index)
        {
            try
            {
                LockTimers.AcquireWriterLock(5000);
                try
                {
                    if ((index >= 0) && (index < ListAppTimers.Count))
                    {
                        ListAppTimers.RemoveAt(index);
                    }
                }
                finally
                {
                    LockTimers.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }

        internal static void RemoveTimerLastAdded()
        {
            try
            {
                LockTimers.AcquireWriterLock(5000);
                try
                {
                    var maxId = 0;
                    var maxIndex = -1;

                    for (var i = 0; i < ListAppTimers.Count; i++)
                    {
                        var activeAppTimer = ListAppTimers[i];
                        if (activeAppTimer.Id <= maxId)
                        {
                            continue;
                        }

                        maxId = activeAppTimer.Id;
                        maxIndex = i;
                    }

                    if (maxIndex >= 0)
                    {
                        RemoveTimerAt(maxIndex);
                    }
                }
                finally
                {
                    LockTimers.ReleaseWriterLock();
                }
            }
            catch (ApplicationException)
            {
            }
        }
    }
}
