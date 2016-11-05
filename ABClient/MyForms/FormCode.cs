using System.Drawing;

namespace ABClient.Forms
{
    using System;
    using System.Windows.Forms;
    using System.IO;
    //using Controls;

    internal partial class FormCode : Form
    {
        //private readonly string m_doc;

        public FormCode()
        {
            InitializeComponent();

            //m_doc = string.Empty;

            ShowPic();
        }

        /*
    internal FormCode()
    {
        InitializeComponent();

        m_doc =                 
            "<HTML><HEAD></HEAD><BODY topmargin=0 bottommargin=0 leftmargin=0 rightmargin=0>" +
            @"<img src=""" + urlcode + @""" border=0 width=134 height=60>" +
            "</BODY></HTML>";
    }
         */

        internal string Code
        {
            get { return textCode.Text; }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ShowPic();
            /*
            Controls.Remove(wbCode);
            wbCode.Dispose();
            wbCode = new ExtendedWebBrowser
                         {
                             Location = new System.Drawing.Point(7, 7),
                             Margin = new Padding(0),
                             MinimumSize = new System.Drawing.Size(20, 20),
                             Name = "wbCode",
                             ScrollBarsEnabled = false,
                             Size = new System.Drawing.Size(134, 60),
                             TabIndex = 0,
                             TabStop = false
                         };
            Controls.Add(wbCode);
            wbCode.DocumentText = m_doc;
             */ 
        }

        private void ShowPic()
        {
            if (AppVars.CodePng == null) return;
            using (var ms = new MemoryStream(AppVars.CodePng))
            {
                pictureBox.Image = Image.FromStream(ms);
            }
        }


        private void btnMaximize_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormCode_Load(object sender, EventArgs e)
        {
            //wbCode.DocumentText = m_doc;
            Activate();
        }

        private void textCode_TextChanged(object sender, EventArgs e)
        {
            int code;

            if (int.TryParse(textCode.Text, out code))
            {
                if ((code < 0) || (code > 99999))
                {
                    btnEnter.Enabled = false;
                }
                else
                {
                    btnEnter.Enabled = true;
                }
            }
            else
            {
                btnEnter.Enabled = false;
            }
        }
    }
}