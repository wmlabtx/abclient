using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace ABClient.MyForms
{
    public partial class FormTurotor : Form
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public FormTurotor()
        {
            this.InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            AppVars.MainForm.TurotorCancel();
        }

		private void InitializeTurotorTop()
		{
			bool flag = false;
			AppTimer[] timers = AppTimerManager.GetTimers();
			for (int i = 0; i < timers.Length; i++)
			{
				if (timers[i].IsIsland)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				string str = AppVars.DoParseRegionCells ? "Остров Туротор" : "Гиблая Топь";
				AppTimerManager.AddAppTimer(new AppTimer
				{
					Description = "Использован Телепорт (" + str + ")",
					TriggerTime = DateTime.Now.AddSeconds(1.0),
					Potion = "Телепорт (" + str + ")",
					DrinkCount = 1,
					IsRecur = true,
					IsIsland = true,
					EveryMinutes = AppVars.EndInterval - AppVars.BeginInterval//(AppVars.DoParseRegionCells ? 28800 : 14400)
				});
			}
		}

        private void buttonGo_Click_1(object sender, EventArgs e)
        {
			try
			{
				string text = this.comboBoxLocations.Text;
				string text2 = this.comboBoxLocationsSecondary.Text;
				int num = (int)this.IntervalStart.Value;
				int num2 = (int)this.IntervalEnd.Value;
				if (!string.IsNullOrEmpty(text))
				{
					text = text.Substring(0, 6);
					if (!string.IsNullOrEmpty(text2) && text[0] == text2[0] && num != num2 && num > -1 && num < 24 && num2 > -1 && num2 < 24)
					{
						AppVars.TurotorTopDestination2 = text2.Substring(0, 6);
						AppVars.BeginInterval = num;
						AppVars.EndInterval = num2;
					}
					AppVars.DoParseRegionCells = text.StartsWith("11");
					AppVars.DoSentToIsland = (!AppVars.LocationName.Contains("Туротор") && !AppVars.LocationName.Contains("Гиблая Топь"));
					AppVars.TurotorTopDestination1 = text;
					string text3 = AppVars.DoParseRegionCells ? "Остров Туротор" : "Гиблая Топь";
					string text4 = string.IsNullOrEmpty(AppVars.TurotorTopDestination2) ? string.Empty : string.Format("В интервале с <b>{0}:00</b> до <b>{1}:00</b> на <b>{2}</b>.", num, num2, this.comboBoxLocationsSecondary.Text);
					AppVars.MainForm.WriteChatMsgSafe(string.Concat(new string[]
					{
					"Включено авто использование свитка телепорта в локацию <b>",
					text3,
					"</b> на <b>",
					this.comboBoxLocations.Text,
					"</b>. ",
					text4
					}));
					this.InitializeTurotorTop();
					base.Close();
				}
			}
			catch (Exception ex)
			{
				logger.Error($"Some error " + ex.GetType() + " with message - " + ex.Message + " occured in " + ex.StackTrace);
			}
		}
    }
}
