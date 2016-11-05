
namespace ABClient
{
    using System;
    using System.Text;

    internal sealed class AppTimer
    {
        internal DateTime TriggerTime = DateTime.MinValue;
        internal string Description = string.Empty;
        internal string Potion = string.Empty;
        internal int DrinkCount;
        internal bool IsRecur;
        internal int EveryMinutes;
        internal string Destination = string.Empty;
        internal string Complect = string.Empty;
        internal int Id;
        internal bool IsHerb;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Id);
            if (IsRecur)
            {
                sb.Append('*');
            }

            sb.Append(") Еще ");

            var triggerTime = TriggerTime;
            if (IsHerb)
            {
                triggerTime = triggerTime.Subtract(new TimeSpan(0, 30, 0));
            }

            if (triggerTime < DateTime.Now)
            {
                if (IsHerb)
                {
                    var ts = TriggerTime.Subtract(DateTime.Now);
                    if (ts.Hours > 0)
                    {
                        sb.AppendFormat("{0}:{1:00}:{2:00} (?)", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
                    }
                    else
                    {
                        if (ts.Minutes > 0)
                        {
                            sb.AppendFormat("{0}:{1:00} (?)", ts.Minutes, ts.Seconds);
                        }
                        else
                        {
                            sb.AppendFormat("0:{0:00} (?)", ts.Seconds);
                        }
                    }
                }
                else
                {
                    sb.Append("0:00");
                }
            }
            else
            {
                var ts = triggerTime.Subtract(DateTime.Now);
                if (ts.Hours > 0)
                {
                    sb.AppendFormat("{0}:{1:00}:{2:00}", (int)ts.TotalHours, ts.Minutes, ts.Seconds);
                }
                else
                {
                    if (ts.Minutes > 0)
                    {
                        sb.AppendFormat("{0}:{1:00}", ts.Minutes, ts.Seconds);
                    }
                    else
                    {
                        sb.AppendFormat("0:{0:00}", ts.Seconds);
                    }
                }
            }

            sb.Append(" - ");
            sb.Append(Description);
            if (DrinkCount > 1)
            {
                sb.AppendFormat(" [{0}]", DrinkCount);
            }

            return sb.ToString();
        }
    }
}
