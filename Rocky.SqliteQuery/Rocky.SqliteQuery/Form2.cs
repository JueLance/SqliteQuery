using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Rocky.SqliteQuery
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            FormOpenData opendata = new FormOpenData();
            opendata.Owner = (Form1)this.Owner;
            opendata.ShowDialog();
        }

        //恢复上次的操作
        private void button2_Click(object sender, EventArgs e)
        {

        }
        //在历史记录中查找
        private void button3_Click(object sender, EventArgs e)
        {
            //if (File.Exists("keyword.txt"))
            //{
            //    StreamReader sr = new StreamReader("keyword.txt");

            //    StringBuilder sb = new StringBuilder();

            //    while (sr.Peek() >= 0)
            //    {
            //        sb.Append("-->"+sr.ReadLine()+"<--");
            //    }

            //    //MessageBox.Show(AppDomain.CurrentDomain.ToString());
            //    MessageBox.Show(sb.ToString());
            //}
            //else
            //{
            //    MessageBox.Show("文件不存在");

            //}
        }
    }
}
