namespace ABClient.MyForms
{
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ExtMap;

    internal partial class FormNewTimer : Form
    {
        private readonly AutoCompleteStringCollection _scAutoComplete = new AutoCompleteStringCollection();

        internal FormNewTimer()
        {
            InitializeComponent();

            comboPotion.SelectedIndex = 0;
            foreach (var index in Map.Cells.Keys)
            {
                _scAutoComplete.Add(index);
            }

            textCell.AutoCompleteCustomSource = _scAutoComplete;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var appTimer = new AppTimer();
            var text = textName.Text.Trim();
            appTimer.Description = text;
            var textTriggerHour = numTriggerHour.Text.Trim();
            int triggerHour;
            if (!int.TryParse(textTriggerHour, out triggerHour))
            {
                triggerHour = 0;
            }

            if (triggerHour < 0)
            {
                triggerHour = 0;
            }

            var textTriggerMin = numTriggerMin.Text.Trim();
            int triggerMin;
            if (!int.TryParse(textTriggerMin, out triggerMin))
            {
                triggerMin = 0;
            }

            if ((triggerMin < 0) || (triggerMin > 59))
            {
                triggerMin = 0;
            }

            triggerMin = (triggerHour * 60) + triggerMin;
            appTimer.TriggerTime = DateTime.Now.AddMinutes(triggerMin);
            if (radioPotion.Checked)
            {
                var selectedIndex = comboPotion.SelectedIndex;
                if (selectedIndex < 1)
                {
                    return;
                }

                var potion = comboPotion.Text.Trim();
                appTimer.Potion = potion;
                if (string.IsNullOrEmpty(appTimer.Description))
                {
                    appTimer.Description = string.Format("Выпьем {0}", appTimer.Potion);
                }

                var textDrinks = textDrinkCount.Text.Trim();
                int drinks;
                if (!int.TryParse(textDrinks, out drinks))
                {
                    drinks = 1;
                }

                if (drinks < 1)
                {
                    drinks = 1;
                }

                appTimer.DrinkCount = drinks;
                appTimer.IsRecur = checkRecur.Checked;
                if (appTimer.IsRecur)
                {
                    appTimer.EveryMinutes = triggerMin;
                }
            }

            if (radioDestination.Checked)
            {
                var destination = textCell.Text.Trim();
                if (string.IsNullOrEmpty(destination))
                {
                    return;
                }

                appTimer.Destination = destination;
                if (string.IsNullOrEmpty(appTimer.Description))
                {
                    appTimer.Description = string.Format("Идем на {0}", destination);
                }
            }

            if (radioComplect.Checked)
            {
                var complect = textComplect.Text.Trim();
                if (string.IsNullOrEmpty(complect))
                {
                    return;
                }

                appTimer.Complect = complect;
                if (string.IsNullOrEmpty(appTimer.Description))
                {
                    appTimer.Description = string.Format("Одеваем комплект {0}", complect);
                }
            }

            AppTimerManager.AddAppTimer(appTimer);
        }

        private void OnTextCellTextChanged(object sender, EventArgs e)
        {
            bool isValid;
            try
            {
                var cellRegEx = new Regex(@"\d{1,2}-\d{3}");
                var cellMatch = cellRegEx.Match(textCell.Text);
                isValid = cellMatch.Success && Map.Cells.ContainsKey(textCell.Text);
            }
            catch
            {
                isValid = false;
            }

            textCell.BackColor = isValid ? SystemColors.Window : Color.Pink;
        }

       private void radioNone_CheckedChanged(object sender, EventArgs e)
       {
            if (radioNone.Checked || radioDestination.Checked || radioComplect.Checked)
            {
                comboPotion.Enabled = false;
                textDrinkCount.Enabled = false;
                checkRecur.Enabled = false;
            }

            if (radioNone.Checked || radioPotion.Checked || radioComplect.Checked)
            {
                textCell.Enabled = false;
            }

            if (radioNone.Checked || radioPotion.Checked || radioDestination.Checked)
            {
                textComplect.Enabled = false;
            }

            if (radioPotion.Checked)
            {
                comboPotion.Enabled = true;
                textDrinkCount.Enabled = true;
                checkRecur.Enabled = true;
            }

            if (radioDestination.Checked)
            {
                textCell.Enabled = true;
            }

            if (radioComplect.Checked)
            {
                textComplect.Enabled = true;
            }
        }
    }
}