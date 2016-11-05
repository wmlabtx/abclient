namespace ABClient.MyProfile
{
    using System.Xml;
    using System;
    using MyHelpers;

    internal class MyAutoAnswer
    {
        private int m_LastAutoAnswer;
        private int[] m_PrepAutoAnswers;
        private string[] m_Answers;

        internal string StrAnswers { get; private set; }

        internal bool Active { get; set; }

        internal MyAutoAnswer()
        {
            SetAnswers(
                "Это автоответ ABClient. Не ждали?" + Environment.NewLine +
                "Я все понимаю, но это автоответ ABClient" + Environment.NewLine +
                "Хватит! Автоответу ABClient это не нравится" + Environment.NewLine +
                "Это автоответ ABClient, не старайся мне что-то объяснить" + Environment.NewLine +
                "Хозяин отошел, но я ему это передам. Это автоответ ABClient" + Environment.NewLine +
                "Автоответ ABClient. Я ничего не читаю, что ты мне пишешь" + Environment.NewLine +
                "Серьезно? Это автоответ ABClient" + Environment.NewLine +
                "Что-то? Повтори. Автоответ ABClient плохо тебя понимает" + Environment.NewLine +
                "Перезагрузись, от тебя закорючки идут. Автоответ ABClient" + Environment.NewLine +
                "Автоответ ABClient советует тебе помолчать" + Environment.NewLine +
                "Это автоответ ABClient. Оставьте сообщение после длинного гудка" + Environment.NewLine +
                "Ты расстраиваешь автоответ ABClient своими глупостями" + Environment.NewLine +
                "Автоответ ABClient ненавидит спаммеров... Где моя нападалка?..." + Environment.NewLine +
                "А в рыло? Автоответ ABClient не понимает шуток" + Environment.NewLine +
                "Ну даешь! Нравится говорить с автоответом ABClient?" + Environment.NewLine +
                "Давай, давай. Ты только заводишь автоответ ABClient" + Environment.NewLine +
                "Автоответ ABClient думает, что это бред" + Environment.NewLine +
                "Как вы все меня утомили! Это автоответ ABClient" + Environment.NewLine +
                "Пиши еще. Автоответ ABClient питается твоими словами" + Environment.NewLine +
                "Я так и передам МСу. Это автоответ ABClient" + Environment.NewLine +
                "Не мешай автоответу ABClient медитировать" + Environment.NewLine +
                "Автоответ ABClient думает, просит не мешать" + Environment.NewLine +
                "Что-что? Пиши медленней, автоответ ABClient не успевает за тобой" + Environment.NewLine +
                "Я хоть и бот, но обидчивый. Автоответ ABClient может и боевую влепить" + Environment.NewLine +
                "Ты говоришь с автоответом ABClient, но не расстраивайся. Хозяин не намного умнее меня" +
                Environment.NewLine +
                "Ты даже автоответ ABClient сумел разозлить!" + Environment.NewLine +
                "Что за ерунду ты мне пишешь? Автоответ ABClient ничего не понимает!" + Environment.NewLine +
                "Я запишу и запомню. Автоответ ABClient ничего не забывает!");
        }

        internal void SetAnswers(string answers)
        {
            if (answers == null) throw new ArgumentNullException("answers");
            StrAnswers = answers;
            m_Answers = answers.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal string GetNextAnswer()
        {
            if ((m_Answers == null) || (m_Answers.Length == 0))
            {
                return string.Empty;
            }

            if (m_PrepAutoAnswers == null || (m_PrepAutoAnswers.Length != m_Answers.Length))
            {
                m_PrepAutoAnswers = new int[m_Answers.Length];
                for (var i = 0; i < m_PrepAutoAnswers.Length; i++)
                {
                    m_PrepAutoAnswers[i] = i;
                }

                for (var i = 0; i < m_PrepAutoAnswers.Length; i++)
                {
                    var j = Helpers.Dice.Make(m_PrepAutoAnswers.Length);
                    var t = m_PrepAutoAnswers[i];
                    m_PrepAutoAnswers[i] = m_PrepAutoAnswers[j];
                    m_PrepAutoAnswers[j] = t;
                }

                m_LastAutoAnswer = -1;
            }

            m_LastAutoAnswer++;
            if (m_LastAutoAnswer == m_PrepAutoAnswers.Length)
            {
                m_LastAutoAnswer = 0;
            }

            return m_Answers[m_PrepAutoAnswers[m_LastAutoAnswer]];
        }

        internal void Write(XmlWriter writer)
        {
            writer.WriteStartElement("autoanswer");

            if (Active)
            {
                writer.WriteStartAttribute("active");
                writer.WriteValue(Active);
                writer.WriteEndAttribute();
            }

            writer.WriteStartAttribute("answers");
            writer.WriteString(StrAnswers.Replace(Environment.NewLine, "[BR]"));
            writer.WriteEndAttribute();

            writer.WriteEndElement();
        }

        internal void Read(XmlReader reader)
        {
            if (reader["active"] != null)
            {
                bool active;
                if (!bool.TryParse(reader["active"], out active))
                {
                    active = false;
                }

                Active = active;
            }

            if (reader["answers"] != null)
            {
                SetAnswers(reader["answers"].Replace("[BR]", Environment.NewLine));
            }
        }
    }
}