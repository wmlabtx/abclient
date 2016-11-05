using System;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace ABClient.MySounds
{

    internal static class EventSounds
    {
        private static readonly SoundPlayer player = new SoundPlayer();
        private static readonly string m_pathdigits = Path.Combine(Application.StartupPath, "digits.wav");
        private static DateTime m_lastdigits = DateTime.MinValue;
        private static readonly string m_pathattack = Path.Combine(Application.StartupPath, "attack.wav");
        private static DateTime m_lastattack = DateTime.MinValue;
        private static readonly string m_pathsndmsg = Path.Combine(Application.StartupPath, "sndmsg.wav");
        private static DateTime m_lastsndmsg = DateTime.MinValue;
        private static readonly string m_pathrefresh = Path.Combine(Application.StartupPath, "refresh.wav");
        private static DateTime m_lastrefresh = DateTime.MinValue;
        private static readonly string m_pathalarm = Path.Combine(Application.StartupPath, "alarm.wav");
        private static DateTime m_lastalarm = DateTime.MinValue;
        private static readonly string m_pathtimer = Path.Combine(Application.StartupPath, "timer.wav");
        private static DateTime m_lasttimer = DateTime.MinValue;
        private static readonly string m_pathbear = Path.Combine(Application.StartupPath, "bear.wav");
        private static DateTime m_lastbear = DateTime.MinValue;


        /*
        static EventSounds()
        {
            CheckWav(m_pathdigits, Resources.digits);
            CheckWav(m_pathattack, Resources.attack);
            CheckWav(m_pathsndmsg, Resources.sndmsg);
            CheckWav(m_pathrefresh, Resources.refresh);
            CheckWav(m_pathalarm, Resources.alarm);
            CheckWav(m_pathtimer, Resources.timer);
        }
         */ 

        internal static void PlayDigits()
        {
            if (!File.Exists(m_pathdigits) || !AppVars.Profile.Sound.DoPlayDigits) return;
            var ts = DateTime.Now.Subtract(m_lastdigits);
            if (ts.TotalSeconds <= 5) return;
            m_lastdigits = DateTime.Now;
            PlaySound(m_pathdigits);
        }

        internal static void PlayAttack()
        {
            if (!File.Exists(m_pathattack) || !AppVars.Profile.Sound.DoPlayAttack) return;
            var ts = DateTime.Now.Subtract(m_lastattack);
            if (ts.TotalSeconds <= 5) return;
            m_lastattack = DateTime.Now;
            PlaySound(m_pathattack);
        }

        internal static void PlaySndMsg()
        {
            if (!File.Exists(m_pathsndmsg) || !AppVars.Profile.Sound.DoPlaySndMsg) return;
            var ts = DateTime.Now.Subtract(m_lastsndmsg);
            if (ts.TotalSeconds <= 5) return;
            m_lastsndmsg = DateTime.Now;
            PlaySound(m_pathsndmsg);
        }

        internal static void PlayRefresh()
        {
            if (!File.Exists(m_pathrefresh) || !AppVars.Profile.Sound.DoPlayRefresh) return;
            var ts = DateTime.Now.Subtract(m_lastrefresh);
            if (ts.TotalSeconds <= 5) return;
            m_lastrefresh = DateTime.Now;
            PlaySound(m_pathrefresh);
        }

        internal static void PlayAlarm()
        {
            if (!File.Exists(m_pathalarm) || !AppVars.Profile.Sound.DoPlayAlarm) return;
            var ts = DateTime.Now.Subtract(m_lastalarm);
            if (ts.TotalSeconds <= 5) return;
            m_lastalarm = DateTime.Now;
            PlaySound(m_pathalarm);
        }

        internal static void PlayTimer()
        {
            if (!File.Exists(m_pathtimer) || !AppVars.Profile.Sound.DoPlayTimer) return;
            var ts = DateTime.Now.Subtract(m_lasttimer);
            if (ts.TotalSeconds <= 5) return;
            m_lasttimer = DateTime.Now;
            PlaySound(m_pathtimer);
        }

        internal static void PlayBear()
        {
            if (!File.Exists(m_pathbear)) return;
            var ts = DateTime.Now.Subtract(m_lastbear);
            if (ts.TotalSeconds <= 5) return;
            m_lastbear = DateTime.Now;
            PlaySound(m_pathbear);
        }

        private static void PlaySound(string wav)
        {
            if (!AppVars.Profile.Sound.Enabled)
            {
                return;
            }

            try
            {
                player.SoundLocation = wav;
                player.Play();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Ошибка проигрывания " + wav,
                    AppVars.AppVersion.NickProductShortVersion,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /*
        private static void CheckWav(string path, Stream res)
        {
            try
            {
                var buf = new byte[res.Length];
                res.Position = 0;
                res.Read(buf, 0, (int)res.Length);
                File.WriteAllBytes(path, buf);
            }
            catch(Exception ex)
            {
                HelperDialogs.ShowFatalError("Ошибка в работе с файлами звуков", ex);
            }
        }
         */ 
    }
}