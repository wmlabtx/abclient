namespace ABClient.MyForms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    internal partial class FormFishAdvisor : Form
    {
        private readonly ArrayList botlevels = new ArrayList();
        private readonly List<FishTip> listFishTip = new List<FishTip>();
        private int fishUm;

        internal FormFishAdvisor()
        {
            InitializeComponent();

            fishUm = AppVars.Profile.FishUm;
            textFishUm.Text = fishUm.ToString(CultureInfo.InvariantCulture);

            botlevels.Add(new ListItemBotLevelEx(8));
            botlevels.Add(new ListItemBotLevelEx(9));
            botlevels.Add(new ListItemBotLevelEx(12));
            botlevels.Add(new ListItemBotLevelEx(15));
            botlevels.Add(new ListItemBotLevelEx(19));

            comboHandleBots.DataSource = botlevels;
            comboHandleBots.DisplayMember = "BotLevel";
            comboHandleBots.ValueMember = "BotLevelValue";
            comboHandleBots.SelectedValue = AppVars.Profile.FishMaxLevelBots;
            if (comboHandleBots.SelectedIndex == -1)
            {
                comboHandleBots.SelectedIndex = 0;
            }

            listFishTip.Add(new FishTip(-42, 0, "8-224", 12, "Гобл 8-12", "Верхоплавка на хлеб"));
            listFishTip.Add(new FishTip(-42, 0, "8-384", 8, "Гобл 4-8", "Верхоплавка на хлеб"));
            listFishTip.Add(new FishTip(-42, 0, "12-208", 15, "Орки 15", "Верхоплавка на хлеб"));

            listFishTip.Add(new FishTip(60, 20, "8-224", 12, "Гобл 8-12", "Красноперка на мотыля"));
            listFishTip.Add(new FishTip(60, 20, "8-384", 8, "Гобл 4-8", "Красноперка на мотыля"));

            listFishTip.Add(new FishTip(177, 30, "8-224", 12, "Гобл 8-12", "Бычок на опарыш"));
            listFishTip.Add(new FishTip(177, 30, "8-384", 8, "Гобл 4-8", "Бычок на опарыш"));
            listFishTip.Add(new FishTip(177, 30, "7-552", 19, "Огры 19", "Бычок на опарыш"));

            listFishTip.Add(new FishTip(311, 40, "8-224", 12, "Гобл 8-12", "Ёрш на червяка"));
            listFishTip.Add(new FishTip(311, 40, "8-384", 8, "Гобл 4-8", "Ёрш на червяка"));
            listFishTip.Add(new FishTip(311, 40, "8-413", 8, "Гобл 4-8", "Ёрш на червяка"));
            listFishTip.Add(new FishTip(311, 40, "8-414", 8, "Гобл 4-8", "Ёрш на червяка"));
            listFishTip.Add(new FishTip(311, 40, "7-552", 19, "Огры 19", "Ёрш на червяка"));

            listFishTip.Add(new FishTip(466, 60, "8-326", 9, "Гобл 5-9", "Плотва на червяка"));
            listFishTip.Add(new FishTip(466, 60, "8-358", 9, "Гобл 5-9", "Плотва на червяка"));

            /*listFishTip.Add(new FishTip(644, 80, "8-224", 12, "Гобл 8-12", "Пескарь на хлеб"));*/
            listFishTip.Add(new FishTip(644, 80, "8-437", 9, "Гобл 7-9", "Пескарь на хлеб"));
            listFishTip.Add(new FishTip(644, 80, "8-467", 9, "Гобл 7-9", "Пескарь на хлеб"));

            listFishTip.Add(new FishTip(848, 95, "8-326", 9, "Гобл 5-9", "Карась на хлеб"));
            listFishTip.Add(new FishTip(848, 95, "8-358", 9, "Гобл 5-9", "Карась на хлеб"));
            listFishTip.Add(new FishTip(848, 95, "7-552", 19, "Огры 19", "Карась на хлеб"));

            listFishTip.Add(new FishTip(1084, 120, "8-326", 9, "Гобл 5-9", "Подлещик на крупного червяка"));
            listFishTip.Add(new FishTip(1084, 120, "8-358", 9, "Гобл 5-9", "Подлещик на крупного червяка"));

            /*listFishTip.Add(new FishTip(1354, 140, "8-326", 9, "Гобл 5-9", "Карп на крупного червяка"));*/
            listFishTip.Add(new FishTip(1354, 140, "8-437", 9, "Гобл 7-9", "Карп на крупного червяка"));
            listFishTip.Add(new FishTip(1354, 140, "8-467", 9, "Гобл 7-9", "Карп на крупного червяка"));
            listFishTip.Add(new FishTip(1354, 140, "12-208", 15, "Орки 15", "Карп на крупного червяка"));

            listFishTip.Add(new FishTip(1675, 180, "8-326", 9, "Гобл 5-9", "Окунь на мотыля с сачком"));
            listFishTip.Add(new FishTip(1675, 180, "8-358", 9, "Гобл 5-9", "Окунь на мотыля с сачком"));

            listFishTip.Add(new FishTip(2142, 210, "8-384", 8, "Гобл 4-8", "Лещ на донку с сачком"));
            listFishTip.Add(new FishTip(2142, 210, "8-413", 8, "Гобл 4-8", "Лещ на донку с сачком"));
            listFishTip.Add(new FishTip(2142, 210, "8-414", 8, "Гобл 4-8", "Лещ на донку с сачком"));
            listFishTip.Add(new FishTip(2142, 210, "8-437", 9, "Гобл 7-9", "Лещ на донку с сачком"));
            listFishTip.Add(new FishTip(2142, 210, "8-467", 9, "Гобл 7-9", "Лещ на донку с сачком"));

            /*listFishTip.Add(new FishTip(2679, 250, "8-326", 9, "Гобл 5-9", "Голавль на крупного червяка с сачком"));*/
            listFishTip.Add(new FishTip(2679, 250, "8-384", 8, "Гобл 4-8", "Голавль на крупного червяка с сачком"));
            listFishTip.Add(new FishTip(2679, 250, "8-413", 8, "Гобл 4-8", "Голавль на крупного червяка с сачком"));
            listFishTip.Add(new FishTip(2679, 250, "8-414", 8, "Гобл 4-8", "Голавль на крупного червяка с сачком"));
            listFishTip.Add(new FishTip(2679, 250, "7-552", 19, "Огры 19", "Голавль на крупного червяка с сачком"));

            listFishTip.Add(new FishTip(3297, 370, "8-224", 12, "Гобл 8-12", "Налим на донку с сачком"));

            listFishTip.Add(new FishTip(4008, 450, "8-326", 9, "Гобл 5-9", "Язь на мормышку с сачком"));
            listFishTip.Add(new FishTip(4008, 450, "7-552", 19, "Огры 19", "Язь на мормышку с сачком"));
            listFishTip.Add(new FishTip(4008, 450, "12-208", 15, "Орки 15", "Язь на мормышку с сачком"));

            listFishTip.Add(new FishTip(4825, 500, "8-326", 9, "Гобл 5-9", "Щука на блесну с сачком"));
            listFishTip.Add(new FishTip(4825, 500, "8-437", 9, "Гобл 7-9", "Щука на блесну с сачком"));
            listFishTip.Add(new FishTip(4825, 500, "8-467", 9, "Гобл 7-9", "Щука на блесну с сачком"));
            listFishTip.Add(new FishTip(4825, 500, "8-358", 9, "Гобл 5-9", "Щука на блесну с сачком"));
            listFishTip.Add(new FishTip(4825, 500, "7-552", 19, "Огры 19", "Щука на блесну с сачком"));

            listFishTip.Add(new FishTip(5765, 600, "8-437", 9, "Гобл 7-9", "Линь на мормышку с сачком"));
            listFishTip.Add(new FishTip(5765, 600, "8-467", 9, "Гобл 7-9", "Линь на мормышку с сачком"));
            listFishTip.Add(new FishTip(5765, 600, "8-358", 9, "Гобл 5-9", "Линь на мормышку с сачком"));
            listFishTip.Add(new FishTip(5765, 600, "12-208", 15, "Орки 15", "Линь на мормышку с сачком"));

            /*listFishTip.Add(new FishTip(6846, 700, "8-326", 9, "Гобл 5-9", "Судак на червяка с сачком"));*/
            listFishTip.Add(new FishTip(6846, 700, "8-437", 9, "Гобл 7-9", "Судак на червяка с сачком"));
            listFishTip.Add(new FishTip(6846, 700, "8-467", 9, "Гобл 7-9", "Судак на червяка с сачком"));

            listFishTip.Add(new FishTip(8089, 800, "8-358", 9, "Гобл 5-9", "Сом на заговоренную блесну с сачком"));

            listFishTip.Add(new FishTip(9518, 1000, "8-384", 8, "Гобл 4-8", "Форель на блесну с сачком"));
            listFishTip.Add(new FishTip(9518, 1000, "8-413", 8, "Гобл 4-8", "Форель на блесну с сачком"));
            listFishTip.Add(new FishTip(9518, 1000, "8-414", 8, "Гобл 4-8", "Форель на блесну с сачком"));
            listFishTip.Add(new FishTip(9518, 1000, "12-208", 15, "Орки 15", "Форель на блесну с сачком"));
        }

        private void FormFishAdvisor_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var isError = !int.TryParse(textFishUm.Text.Trim(), out fishUm);
            if (!isError && fishUm < 0)
            {
                isError = true;
            }

            if (isError)
            {
                e.Cancel = true;
                textFishUm.Select(0, textFishUm.Text.Length);
                errorProvider.SetError(textFishUm, "Умение рыбалки - это целое число больше нуля, например, 65");
            }
            else
            {
                errorProvider.SetError(textFishUm, string.Empty);
            }
        }

        private void ButtonCalc_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textFishUm.Text.Trim(), out fishUm))
            {
                return;
            }

            var maxBotLevel = (int) comboHandleBots.SelectedValue;
            AppVars.Profile.FishMaxLevelBots = maxBotLevel;
            var listFishTipFiltered = new List<FishTip>();
            foreach (var fishTip in listFishTip)
            {
                if (fishTip.FishUm <= fishUm && fishTip.MaxBotLevel <= maxBotLevel)
                {
                    listFishTipFiltered.Add(fishTip);
                }
            }
            
            listFishTipFiltered.Sort();

            var shaded = Color.FromArgb(240, 240, 240);
            var i = 0;
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (var fishTip in listFishTipFiltered)
            {
                var item = new ListViewItem(fishTip.Money.ToString(CultureInfo.InvariantCulture) + " NV");
                item.SubItems.Add(fishTip.Location);
                item.SubItems.Add(fishTip.BotDescription);
                item.SubItems.Add(fishTip.Tip + " (" + fishTip.FishUm + ")");
                if (i % 2 == 1)
                {
                    item.BackColor = shaded;
                    item.UseItemStyleForSubItems = true;
                }

                listView.Items.Add(item);
                i++;
            }

            listView.EndUpdate();
        }
    }
}
