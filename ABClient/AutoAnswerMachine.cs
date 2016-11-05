namespace ABClient
{
    using System;

    internal static class AutoAnswerMachine
    {
        private static int lastAutoAnswer;
        private static int[] prepAutoAnswers;
        private static string[] arrayAnswers;

        internal static void SetAnswers(string answers)
        {
            if (answers == null) throw new ArgumentNullException("answers");
            arrayAnswers = answers.Split(new[] { AppConsts.Br }, StringSplitOptions.RemoveEmptyEntries);
        }

        internal static string GetNextAnswer()
        {
            if ((arrayAnswers == null) || (arrayAnswers.Length == 0))
            {
                return string.Empty;
            }

            if (prepAutoAnswers == null || (prepAutoAnswers.Length != arrayAnswers.Length))
            {
                prepAutoAnswers = new int[arrayAnswers.Length];
                for (var i = 0; i < prepAutoAnswers.Length; i++)
                {
                    prepAutoAnswers[i] = i;
                }

                for (var i = 0; i < prepAutoAnswers.Length; i++)
                {
                    var j = Helpers.Dice.Make(prepAutoAnswers.Length);
                    var t = prepAutoAnswers[i];
                    prepAutoAnswers[i] = prepAutoAnswers[j];
                    prepAutoAnswers[j] = t;
                }

                lastAutoAnswer = -1;
            }

            lastAutoAnswer++;
            if (lastAutoAnswer == prepAutoAnswers.Length)
            {
                lastAutoAnswer = 0;
            }

            return arrayAnswers[prepAutoAnswers[lastAutoAnswer]];
        }
    }
}
