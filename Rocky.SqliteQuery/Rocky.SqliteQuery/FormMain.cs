using Configure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Rocky.SqliteQuery
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
#if DEBUG
            this.menuStrip1.BackColor = Color.Red;
#endif

        }

        #region Field

        OpenFileDialog openfile = new OpenFileDialog();

        string sqltext = string.Empty;//记录sql语句是否发生改变

        private Dictionary<string, string> dbFiles = new Dictionary<string, string>();

        string connectionStr = string.Empty;

        #endregion

        #region Method

        #region 清空结果
        /// <summary>
        /// 清空结果
        /// </summary>
        private void ClearResult()
        {
            this.dataGridView1.DataSource = null;
            this.lbResult.Text = string.Empty;
        }
        #endregion

        #region 清空SQL脚本
        /// <summary>
        /// 清空SQL脚本
        /// </summary>
        private void ClearSQLSentence()
        {
            richTextBox1.Text = string.Empty;
        }
        #endregion

        #region 打开窗体2

        //private void ShowForm2()
        //{
        //    Form2 f2 = new Form2();
        //    f2.Owner = this;
        //    f2.ShowDialog();
        //}

        #endregion

        #region 运行SQL语句
        /**
       * 
       * 运行sql语句的原理
       * 1.首先的判断数据库是否连接
       * 
       * 2.连接后  判断sql语句的类型
       * 
       * select update  insert delete create 
       * 
       * 对于查询select返回数据
       * 
       * 对于update   和  insert  delete 返回受影响的行数
       * 
       * 
       * 3.适当给出提示
       * 
       * **/
        private void RunSQL()
        {
            #region 执行SQL语句
            try
            {
                if (treeView1.GetNodeCount(false) < 1)
                {
                    lbResult.Text = "数据库未连接！";
                    tabControl1.SelectedIndex = 1;//显示消息
                    return;
                }
                tslbtips.Text = "正在进行查询……";

                #region 构造sql语句
                string sql = richTextBox1.Text;


                if (richTextBox1.SelectedText.Length > 1)
                {
                    sql = richTextBox1.SelectedText;
                }
                else
                {
                    sql = richTextBox1.Text;
                }


                if (sql.Trim() == "")
                {
                    this.lbResult.Text = "查询语句为空";
                    tslbtips.Text = string.Empty;
                    tabControl1.SelectedIndex = 1;//显示消息
                    return;
                }
                #endregion

                ExecuteResult result = SqlProcessFactory.Process(connectionStr, sql);

                switch (result.CommandType)
                {
                    case SqlCommandType.Select:
                        this.tabControl1.SelectedIndex = 0;//显示结果
                        break;
                    case SqlCommandType.NoSelect:
                        this.tabControl1.SelectedIndex = 1;//显示消息
                        break;
                    default:
                        break;
                }

                #region 绑定数据
                try
                {
                    if (result.CommandType == SqlCommandType.Select && result.Result != null)
                    {
                        DataSet ds = result.Result as DataSet;
                        if (ds.Tables.Count > 1)
                        {
                            this.dataGridView1.DataSource = ds;
                        }
                        else if (ds.Tables.Count == 1)
                        {
                            this.dataGridView1.DataSource = ds.Tables[0];
                        }

                    }
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(richTextBox1.Text + "\r\n\r\n" + ex.ToString());
                }
                #endregion

                this.lbResult.Text = result.Message;
                this.tslbtips.Text = "命令成功完成。";
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(richTextBox1.Text + "\r\n\r\n" + ex.Message);
                this.tslbtips.Text = "命令执行失败！";
                this.lbResult.Text = ex.Message;
                this.tabControl1.SelectedIndex = 1;
            }
            #endregion
        }


        #endregion

        #region 保存SQL脚本文件
        /// <summary>
        /// 保存SQL脚本文件
        /// </summary>
        private void SaveSQLSentence()
        {
            SaveFileDialog savefiledig = new SaveFileDialog();
            savefiledig.Filter = "sql文件|.sql|文本文件|*.txt|所有文件|*.*";
            if (savefiledig.ShowDialog() == DialogResult.OK)
            {
                LogHelper.WriteFile(savefiledig.FileName, richTextBox1.Text);
            }
        }
        #endregion

        #region 打开数据库窗体

        private void frmOpenDataShow()
        {
            FormOpenData opendata = new FormOpenData();
            opendata.Owner = this;
            opendata.ShowDialog();
        }

        #endregion

        public void OpenDatabase(string dbName, string dbfilePath)
        {
            if (!dbFiles.ContainsKey(dbfilePath))
            {
                dbFiles.Add(dbfilePath, dbName);

                string connectionString = string.Format("Data Source={0};Version=3;", dbfilePath);

                treeView1.BeginUpdate();

                TreeNode tr = CreateDbNode(dbName, connectionString);

                treeView1.Nodes.Add(tr);
                treeView1.Nodes[0].Expand();//展开根节点下的子节点

                treeView1.EndUpdate();
            }
        }

        private TreeNode CreateDbNode(string dbname, string connectionString)
        {
            var tables = Tables.GetTables(connectionString);

            //处理数据库的名字
            //string dbname=openfile.SafeFileName;
            //TreeNode tr = new TreeNode(dbname.Replace(".mdb", ""));
            TreeNode tr = new TreeNode(dbname);
            tr.ImageIndex = 8;//根目录的图标为数据库图标
            tr.SelectedImageIndex = 8;
            tr.Tag = connectionString;

            //遍历表名
            foreach (TableModel table in tables)
            {
                TreeNode tn = new TreeNode(table.name);//为每个表名创建一个节点
                treeView1.SelectedImageIndex = 4;//字段的图标
                treeView1.ImageIndex = 4;
                tn.ContextMenuStrip = contextMenuStrip1;

                //获得列名
                var columns = Tables.GetColumns(connectionString, table.name);

                if (columns != null && columns.Count > 0)
                {
                    for (int i = 0; i < columns.Count; i++)
                    {
                        var column = columns[i];
                        tn.Nodes.Add(string.Format("{0}({1})", column.name, column.type));
                        tn.SelectedImageIndex = 0;
                        tn.ImageIndex = 0;
                    }
                }

                tr.Nodes.Add(tn);
            }

            return tr;
        }

        #endregion

        #region Event

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Focus();

            #region 读取配置文件
            StringBuilder fontName = new StringBuilder();
            StringBuilder fontSize = new StringBuilder();
            Configurer.GetPrivateProfileString("FontFamily", "Name", "宋体", fontName, 255, Configurer.configPath);
            Configurer.GetPrivateProfileString("FontFamily", "FontSize", "12", fontSize, 255, Configurer.configPath);

            Font font = new Font(new FontFamily(fontName.ToString()), float.Parse(fontSize.ToString()));
            //syntaxRichTextBox2.Font = font;
            richTextBox1.Font = font;
            #endregion 读取配置文件
        }

        #region  工具栏
        // 打开数据库
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOpenDataShow();
        }

        //private void 显示登陆页ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ShowForm2();
        //}

        private void 打开SQL文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openfile.Filter = "SQL脚本文件|*.sql;*.txt|所有文件|*.*";
            openfile.FileName = "";
            openfile.CheckFileExists = true;
            openfile.CheckPathExists = true;
            openfile.Multiselect = false;
            openfile.Title = "选择数据库文件";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openfile.FileName, Encoding.Default);
                richTextBox1.Text = sr.ReadToEnd();
                sqltext = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void 保存SQL语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSQLSentence();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // 显示登录页
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmOpenDataShow();
        }

        //显示隐藏
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            this.toolStripButton2.Image = splitContainer1.Panel1Collapsed ? Rocky.SqliteQuery.Properties.Resources.FillLeftHS : Rocky.SqliteQuery.Properties.Resources.FillRightHS;
        }

        // 清空SQL脚本
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ClearSQLSentence();
        }

        // 清空结果
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            ClearResult();
        }

        // 运行
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            RunSQL();
        }

        private void 计算器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProcess("calc.exe");
        }

        private void 记事本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProcess("notepad.exe");
        }

        private void StartProcess(string ProcessName)
        {
            Process pro = new Process();
            pro.StartInfo.FileName = ProcessName;
            pro.Start();
        }

        private void 清空SQL脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSQLSentence();
        }

        private void 清空结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearResult();
        }

        #endregion

        #region 创建表
        private void 创建表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "\r\n\r\nCREATE [TEMPORARY] TABLE表 (字段1类型 [(字长)] [NOT NULL] ";
            sql += " [WITH COMPRESSION | WITH COMP] [索引1] [, 字段2类型 [(字长)] [NOT NULL] [索引2] [, ...]][, CONSTRAINT multifieldindex [, ...]])";

            richTextBox1.AppendText(sql);
        }
        #endregion

        #region 修改表

        private void 修改表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "\r\n\r\nALTER TABLE 表 {ADD ADD{COLUMN 字段类型 [ (字长)] [NOT NULL]    [CONSTRAINT 索引 ] |";
            sql += "\r\nALTER COLUMN 字段类型 [(字长)] |";
            sql += "\r\nCONSTRAINT 多重字段索引 } |";
            sql += "\r\nDROP DROP{COLUMN 字段 I CONSTRAINT 索引名 } }";

            richTextBox1.AppendText(sql);
        }
        #endregion

        #region 删除表
        private void 删除表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sql = "\r\n\r\nDROP {TABLE表 | INDEX索引 ON表 | PROCEDURE procedure | VIEW view}";
            richTextBox1.AppendText(sql);
        }
        #endregion

        #region 插入数据
        private void 插入数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (treeView1.Nodes.Count <= 0)
            //{
            //    return;
            //}
            //StringBuilder sb = new StringBuilder();
            //StringBuilder sbSapera = new StringBuilder();
            //for (int j = 0; j < treeView1.Nodes[0].Nodes.Count; j++)
            //{
            //    switch (treeView1.Nodes[0].Nodes[j].IsSelected)
            //    {
            //        case true:
            //            sb.Append(treeView1.Nodes[0].Nodes[j].Text + "(");
            //            string[] arr = new string[] { null, null, treeView1.Nodes[0].Nodes[j].Text };
            //            dt = Form1.con.GetSchema("columns", arr);//得到所有列的信息

            //            string[] field = new string[dt.Rows.Count + 2];
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                field[Convert.ToInt32(dt.Rows[i]["ORDINAL_POSITION"])] = dt.Rows[i]["COLUMN_NAME"].ToString();
            //            }
            //            //遍历列名
            //            foreach (string sitem in field)
            //            {
            //                if (!string.IsNullOrEmpty(sitem))
            //                {
            //                    sb.Append(string.Format("{0},", sitem));
            //                    sbSapera.Append("\"  \",");
            //                }
            //            }
            //            InertData(sb.ToString().Substring(0, sb.Length - 1), sbSapera.ToString().Substring(0, sbSapera.Length - 1));
            //            break;
            //        case false:
            InertData();
            //break;

            //}
            //}
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        private void InsertData(string strTableName, string sapera)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("\r\ninsert into {0}) values ( {1} )", strTableName, sapera));
            richTextBox1.AppendText(sb.ToString());
        }

        private void InertData()
        {
            string sql = "\r\n\r\n多重记录追加查询：";
            sql += "\r\nINSERT INTO target [(field1[, field2[, ...]])][IN外部数据库]";
            sql += "\r\nSELECT field1[, field2[, ...]]";
            sql += "\r\nFrom tableexpression";
            sql += "\r\n单一记录追加查询:";
            sql += "\r\nINSERT INTO target [(field1[, field2[, ...]])]";
            sql += "\r\nVALUES (value1[, value2[, ...])";
            richTextBox1.AppendText(sql);
        }
        #endregion

        #region 查询所有字段的信息
        private void selectFromTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;

            if (node == null)
            {
                return;
            }

            richTextBox1.AppendText("\r\n\r\nselect * from " + node.Text);
        }

        #endregion

        #region 查询部分字段的信息
        private void selectFieldFromTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;

            if (node == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n\r\nselect ");

            var columns = Tables.GetColumns(connectionStr, node.Text);

            for (int i = 0; i < columns.Count; i++)
            {
                if (i == columns.Count - 1)
                {
                    sb.Append(columns[i].name);
                }
                else
                {
                    sb.Append(columns[i].name + ",");
                }
            }
            sb.Append(" from " + node.Text);


            richTextBox1.AppendText(sb.ToString());

        }
        #endregion

        #region 纵向列出字段
        private void 纵向列出字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;

            if (node == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n\r\n");


            var conlumns = Tables.GetColumns(connectionStr, node.Text);

            //遍历列名
            foreach (ColumnModel sitem in conlumns)
            {
                if (!string.IsNullOrEmpty(sitem.name))
                {
                    sb.Append(string.Format("{0}\r\n", sitem.name));
                }
            }

            richTextBox1.AppendText(sb.ToString());

        }
        #endregion

        #region  横向列出字段
        private void 横向列出字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;

            if (node == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n\r\n");

            var conlumns = Tables.GetColumns(connectionStr, node.Text);

            //遍历列名
            foreach (ColumnModel sitem in conlumns)
            {
                if (!string.IsNullOrEmpty(sitem.name))
                {
                    sb.Append(string.Format("{0},", sitem.name));
                }
            }

            richTextBox1.AppendText(sb.ToString().Substring(0, sb.Length - 1));

        }

        #endregion

        private void tabControl1_Resize(object sender, EventArgs e)
        {
            ResizeRedraw = true;
        }

        private void tabControl1_SizeChanged(object sender, EventArgs e)
        {
            ResizeRedraw = true;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutMe am = new AboutMe();
            am.ShowDialog();
        }

        #region 窗体关闭
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /**
             * 判断文本是否改变的原理：
             * 
             * 第一种方式：
             * 用户是直接书写sql语句 没有打开sql文件
             * 
             * 第二种  是打开了sql文件再对其进行编辑
             * 
             * 由于sqltext赋给初值是empty  所以 如果用户直接书写sql语句的话  sqltext的length属性为0,  如果用户是以打开的方式编辑的话 那么sqltext的length就会大于0(如果打开的sql脚本文件
             * 本来就没有文字的话 就不会大于0  这种情况不讨论),所以，只需要判断sqltext的长度和是否等于sql编辑框中的text即可
             * 
             * 
             * (没有编辑的情况不讨论)
             * 
             * **/

            //打开了sql文件再对其进行编辑
            if ((sqltext.Length > 0 && !string.Equals(sqltext, richTextBox1.Text)) || (sqltext == string.Empty && richTextBox1.Text.Length > 0))
            {
                DialogResult dr = MessageBox.Show("是否保存SQL脚本文件？", "AccessQuery", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (dr)
                {
                    case DialogResult.Yes:
                        SaveSQLSentence();
                        break;
                    case DialogResult.No:
                        //  Application.Exit();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        #endregion

        private void 显示登陆页ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOpenDataShow();
        }

        #endregion

        private void 运行SQL语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunSQL();
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingFrom setfrom = new SettingFrom();
            setfrom.Owner = this;
            setfrom.tabControl1.SelectedIndex = 0;
            setfrom.Show();
        }

        private void tsInsertData_Click(object sender, EventArgs e)
        {
            var node = treeView1.SelectedNode;

            if (node == null)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            StringBuilder sbSapera = new StringBuilder();

            sb.Append(node.Text + "(");

            var conlumns = Tables.GetColumns(connectionStr, node.Text);

            //遍历列名
            foreach (ColumnModel sitem in conlumns)
            {
                if (!string.IsNullOrEmpty(sitem.name))
                {
                    sb.Append(string.Format("{0},", sitem.name));
                    sbSapera.Append("\"  \",");
                }
            }

            InsertData(sb.ToString().Substring(0, sb.Length - 1), sbSapera.ToString().Substring(0, sbSapera.Length - 1));
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R)
            //{
            //    e.SuppressKeyPress = true;
            //    e.Handled = true;
            //    splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
            //}
        }

        private void 全部转换为小写ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Parent == null)
            {
                connectionStr = treeView1.SelectedNode.Tag.ToString();
            }
        }
    }
}
