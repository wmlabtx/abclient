namespace ABClient.MyForms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Properties;

    internal partial class FormSmiles : Form
    {
        internal string Result
        {
            get { return textBox.Text; }
        }

        internal FormSmiles(int index)
        {
            InitializeComponent();

            FillSmiles(flowPanel1,
                new[] { "001","005","007","006","010","018","022","019","026","034","033","037","038","036","040","039","043","049","052","056","059","057","062","066","068","073","082","080","079","083","086","085","114","118","119","123","161","158","164","167","166","170","174","177","175","179","178","186","189","188","190","202","205","203","206","221","237","239","238","243","246","254","253","255","277","276","275","278","284","289","288","294","293","295","310","313","324","336","347","346","345","348","349","351","352","361","362","366","367","382","393","411","415","413","419","422","434","442","447","453","467","471","472","475","551","554","559","564","568","573","600","601","602","603","604","605","606","607","608","609","610","611","612","613","614","615","616","617","618","619","620","621","622" },
                new[] { Resources.smiles_001, Resources.smiles_005, Resources.smiles_007, Resources.smiles_006, Resources.smiles_010, Resources.smiles_018, Resources.smiles_022, Resources.smiles_019, Resources.smiles_026, Resources.smiles_034, Resources.smiles_033, Resources.smiles_037, Resources.smiles_038, Resources.smiles_036, Resources.smiles_040, Resources.smiles_039, Resources.smiles_043, Resources.smiles_049, Resources.smiles_052, Resources.smiles_056, Resources.smiles_059, Resources.smiles_057, Resources.smiles_062, Resources.smiles_066, Resources.smiles_068, Resources.smiles_073, Resources.smiles_082, Resources.smiles_080, Resources.smiles_079, Resources.smiles_083, Resources.smiles_086, Resources.smiles_085, Resources.smiles_114, Resources.smiles_118, Resources.smiles_119, Resources.smiles_123, Resources.smiles_161, Resources.smiles_158, Resources.smiles_164, Resources.smiles_167, Resources.smiles_166, Resources.smiles_170, Resources.smiles_174, Resources.smiles_177, Resources.smiles_175, Resources.smiles_179, Resources.smiles_178, Resources.smiles_186, Resources.smiles_189, Resources.smiles_188, Resources.smiles_190, Resources.smiles_202, Resources.smiles_205, Resources.smiles_203, Resources.smiles_206, Resources.smiles_221, Resources.smiles_237, Resources.smiles_239, Resources.smiles_238, Resources.smiles_243, Resources.smiles_246, Resources.smiles_254, Resources.smiles_253, Resources.smiles_255, Resources.smiles_277, Resources.smiles_276, Resources.smiles_275, Resources.smiles_278, Resources.smiles_284, Resources.smiles_289, Resources.smiles_288, Resources.smiles_294, Resources.smiles_293, Resources.smiles_295, Resources.smiles_310, Resources.smiles_313, Resources.smiles_324, Resources.smiles_336, Resources.smiles_347, Resources.smiles_346, Resources.smiles_345, Resources.smiles_348, Resources.smiles_349, Resources.smiles_351, Resources.smiles_352, Resources.smiles_361, Resources.smiles_362, Resources.smiles_366, Resources.smiles_367, Resources.smiles_382, Resources.smiles_393, Resources.smiles_411, Resources.smiles_415, Resources.smiles_413, Resources.smiles_419, Resources.smiles_422, Resources.smiles_434, Resources.smiles_442, Resources.smiles_447, Resources.smiles_453, Resources.smiles_467, Resources.smiles_471, Resources.smiles_472, Resources.smiles_475, Resources.smiles_551, Resources.smiles_554, Resources.smiles_559, Resources.smiles_564, Resources.smiles_568, Resources.smiles_573, Resources.smiles_600, Resources.smiles_601, Resources.smiles_602, Resources.smiles_603, Resources.smiles_604, Resources.smiles_605, Resources.smiles_606, Resources.smiles_607, Resources.smiles_608, Resources.smiles_609, Resources.smiles_610, Resources.smiles_611, Resources.smiles_612, Resources.smiles_613, Resources.smiles_614, Resources.smiles_615, Resources.smiles_616, Resources.smiles_617, Resources.smiles_618, Resources.smiles_619, Resources.smiles_620, Resources.smiles_621, Resources.smiles_622 });

            FillSmiles(flowPanel2,
                new[] { "000","029","030","077","126","127","131","155","156","267","297","319","350","353","354","357","358","368","376","385","386","414","417","457","459","469","473","474","477","552","558","560","570","574","575","576","579","950","951","952","953","954","955","956","957","958","959","960","002","003","004","008","009","011","012","013","014","015","016","021","023","024","025","027","028","031","032","623","624","625","626","627","628","629","630","631","632","633","634","635","636","637","638","639","640","641","642","643","644","645","646","647","648","650","651","652","653","654","655","656","657" },
                new[] { Resources.smiles_000, Resources.smiles_029, Resources.smiles_030, Resources.smiles_077, Resources.smiles_126, Resources.smiles_127, Resources.smiles_131, Resources.smiles_155, Resources.smiles_156, Resources.smiles_267, Resources.smiles_297, Resources.smiles_319, Resources.smiles_350, Resources.smiles_353, Resources.smiles_354, Resources.smiles_357, Resources.smiles_358, Resources.smiles_368, Resources.smiles_376, Resources.smiles_385, Resources.smiles_386, Resources.smiles_414, Resources.smiles_417, Resources.smiles_457, Resources.smiles_459, Resources.smiles_469, Resources.smiles_473, Resources.smiles_474, Resources.smiles_477, Resources.smiles_552, Resources.smiles_558, Resources.smiles_560, Resources.smiles_570, Resources.smiles_574, Resources.smiles_575, Resources.smiles_576, Resources.smiles_579, Resources.smiles_950, Resources.smiles_951, Resources.smiles_952, Resources.smiles_953, Resources.smiles_954, Resources.smiles_955, Resources.smiles_956, Resources.smiles_957, Resources.smiles_958, Resources.smiles_959, Resources.smiles_960, Resources.smiles_002, Resources.smiles_003, Resources.smiles_004, Resources.smiles_008, Resources.smiles_009, Resources.smiles_011, Resources.smiles_012, Resources.smiles_013, Resources.smiles_014, Resources.smiles_015, Resources.smiles_016, Resources.smiles_021, Resources.smiles_023, Resources.smiles_024, Resources.smiles_025, Resources.smiles_027, Resources.smiles_028, Resources.smiles_031, Resources.smiles_032, Resources.smiles_623, Resources.smiles_624, Resources.smiles_625, Resources.smiles_626, Resources.smiles_627, Resources.smiles_628, Resources.smiles_629, Resources.smiles_630, Resources.smiles_631, Resources.smiles_632, Resources.smiles_633, Resources.smiles_634, Resources.smiles_635, Resources.smiles_636, Resources.smiles_637, Resources.smiles_638, Resources.smiles_639, Resources.smiles_640, Resources.smiles_641, Resources.smiles_642, Resources.smiles_643, Resources.smiles_644, Resources.smiles_645, Resources.smiles_646, Resources.smiles_647, Resources.smiles_648, Resources.smiles_650, Resources.smiles_651, Resources.smiles_652, Resources.smiles_653, Resources.smiles_654, Resources.smiles_655, Resources.smiles_656, Resources.smiles_657 });

            if (index == 2)
            {
                tabControl.SelectedIndex = 1;
            }
        }

        private void FillSmiles(Control flowLayoutPanel, string[] num, Bitmap[] img)
        {
            flowLayoutPanel.SuspendLayout();
            for (var i = 0; i < num.Length; i++)
            {
                var button = new Button { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
                button.FlatAppearance.BorderColor = Color.White;
                button.FlatAppearance.BorderSize = 0;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(224, 224, 224);
                button.FlatStyle = FlatStyle.Flat;
                /* img[i].SelectActiveFrame(new FrameDimension(img[i].FrameDimensionsList[0]), 0); */
                /* button.Image = new Bitmap(img[i]); */
                button.Image = img[i];
                button.Name = Guid.NewGuid().ToString();
                button.Size = new Size(63, 41);
                button.UseVisualStyleBackColor = true;
                button.Cursor = Cursors.Hand;
                button.Tag = num[i];
                button.Click += button_Click;
                flowLayoutPanel.Controls.Add(button);
            }

            flowLayoutPanel.PerformLayout();
            flowLayoutPanel.ResumeLayout(false);
        }

        private void button_Click(object sender, EventArgs e)
        {
            var button = (Button) sender;
            var num = (string)button.Tag;
            textBox.Paste(":" + num + ": ");
        }
    }
}
