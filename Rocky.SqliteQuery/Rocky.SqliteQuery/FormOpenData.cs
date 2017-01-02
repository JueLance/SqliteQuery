using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Data.SQLite;
using Rocky.SqliteQuery;

namespace Rocky.SqliteQuery
{
    public partial class FormOpenData : Form
    {
        public FormOpenData()
        {
            InitializeComponent();
        }

        #region field

        OpenFileDialog openfile = new OpenFileDialog();

        string dbpath = "";//Access数据库文件的位置

        #endregion

        #region event

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            openfile.FileName = "";
            openfile.Filter = "Sqlite数据库文件(*.db3,*.db,*.sqlite)|*.db3;*.db;*.sqlite|所有文件(*.*)|*.*";
            openfile.CheckFileExists = true;
            openfile.CheckPathExists = true;
            openfile.Multiselect = false;
            openfile.Title = "选择数据库文件";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                tbdataurl.Text = openfile.FileName;
                dbpath = openfile.FileName;
            }
        }

        //打开数据库
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 f1 = (Form1)this.Owner;

                if (tbdataurl.Text != string.Empty || !string.IsNullOrEmpty(dbpath))
                {
                    f1.OpenDatabase(openfile.SafeFileName, dbpath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开数据库错误，详细信息：" + ex.Message, "Sqlite Query", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
