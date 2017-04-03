using System;
using System.IO;
using System.Windows.Forms;
using WinFormsMappingDecoder.Properties;

namespace WinFormsMappingDecoder
{
    public partial class Home : Form
    {
        private readonly HomeData _data = new HomeData();
        private string _text;

        public Home()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown ||
                MessageBox.Show(string.Format(Resources.Home_OnFormClosing_, Text), //Text - base.Text
                    @"Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes) return;

            e.Cancel = true;
        }

        private void MenuExit_Click(object sender, EventArgs e) => Close();

        private void MenuOpenFile_Click(object sender, EventArgs e)
        {
            _text = OpenFile();

            if (_text == null) return;

            textBox.Text = _text;
        }

        private string OpenFile()
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    return new StreamReader(openFileDialog.FileName).ReadToEnd();

            }
            catch (Exception)
            {
                // ignored
            }

            return null;
        }

        private void MenuSaveFile_Click(object sender, EventArgs e)
        {
            if (textBox.Text.Trim().Equals(string.Empty)) return;
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    File.WriteAllText(saveFileDialog.FileName, textBox.Text);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Home_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode & Keys.Escape) == Keys.Escape) Close();
        }
        
        private void MenuClear_Click(object sender, EventArgs e)
        {
            textBoxInfo.Text = string.Empty;
        }

        private void Analysis1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_text == null) return;

            _data.Analysis_1(_text);
            textBoxInfo.Text += _data.GetData();
        }

        private void Analysis2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_text == null) return;

            _data.Analysis_2(_text);
            textBoxInfo.Text += _data.GetData();
        }

        private void Analysis3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_text == null) return;

            _data.Analysis_3(_text);
            textBoxInfo.Text += _data.GetData();
        }

        private void Analysis4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_text == null) return;

            _data.Analysis_4(_text);
            textBoxInfo.Text += _data.GetData();
        }
    }
}