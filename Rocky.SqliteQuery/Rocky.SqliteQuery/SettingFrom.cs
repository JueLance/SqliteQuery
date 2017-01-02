using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using Configure;

namespace Rocky.SqliteQuery
{
    public partial class SettingFrom : Form
    {

        Form1 f1 = new Form1();
        public SettingFrom()
        {
            InitializeComponent();
            InstalledFontCollection fontfalimy = new InstalledFontCollection();
            for (int i = 0; i < fontfalimy.Families.Length; i++)
            {
                cbFontFamily.Items.Add(fontfalimy.Families[i].Name);
            }

            for (int i = 0; i < cbFontFamily.Items.Count; i++)
            {
                if (string.Equals(cbFontFamily.Items[i], f1.richTextBox1.Font.Name))
                {
                    cbFontFamily.SelectedIndex = i;
                }
            }
        }


        private void SettingFrom_Load(object sender, EventArgs e)
        {

        }

        private void btnOKToSys_Click(object sender, EventArgs e)
        {
            SetConfig();
            Font font = new Font(new FontFamily(cbFontFamily.SelectedItem.ToString()), float.Parse(numFontSize.Value.ToString()));
            f1.richTextBox1.Font = font;
            f1.Validate();
            Close();
        }

        private void btnCancleToSys_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnApplyToSys_Click(object sender, EventArgs e)
        {
            SetConfig();
            Font font = new Font(new FontFamily(cbFontFamily.SelectedItem.ToString()), float.Parse(numFontSize.Value.ToString()));
            f1.richTextBox1.Font = font;
            f1.Validate();
        }

        private void SetConfig()
        {
            Configurer.WritePrivateProfileString("FontFamily", "Name", cbFontFamily.SelectedItem.ToString(), Configurer.configPath);
            Configurer.WritePrivateProfileString("FontFamily", "FontSize", numFontSize.Value.ToString(), Configurer.configPath);
        }
    }
}