using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace ABClient.MyForms
{
    internal partial class FormEnterInt : Form
    {
        private readonly string _title;
        private int _val;
        private readonly int _min;
        private readonly int _max;

        public int Val => _val;

        public FormEnterInt(string titleArg, int valArg, int minArg, int maxArg)
        {
            InitializeComponent();

            Left = MousePosition.X;
            Top = MousePosition.Y;

            _title = titleArg;
            _val = valArg;
            _min = minArg;
            _max = maxArg;

            labelDescription.Text = @"Введите значение от " + _min + @" до " + _max;
            textBox.Text = _val.ToString(CultureInfo.InvariantCulture);
        }

        private void FormEnterInt_Load(object sender, System.EventArgs e)
        {
            Text = _title;
        }

        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (ValidInt(textBox.Text, out errorMsg))
            {
                return;
            }

            e.Cancel = true;
            textBox.Select(0, textBox.Text.Length);
            errorProvider.SetError(textBox, errorMsg);
        }

        private void textBox_Validated(object sender, System.EventArgs e)
        {
            errorProvider.SetError(textBox, string.Empty);
        }

        private bool ValidInt(string text, out string errorMessage)
        {
            if (text.Length == 0)
            {
                errorMessage = "Значение не должно быть пустым";
                return false;
            }

            if (!int.TryParse(text.Trim(), out _val))
            {
                errorMessage = "Значение должно быть целым числом";
                return false;
            }

            if (_val < _min)
            {
                errorMessage = "Значение должно быть больше " + _min;
                return false;
            }

            if (_val > _max)
            {
                errorMessage = "Значение должно быть меньше " + _max;
                return false;
            }
                    
            errorMessage = "";
            return true;
        }
    }
}