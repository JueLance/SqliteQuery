using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocky.SqliteQuery
{
    public class LogHelper
    {
        public static void WriteLog(string ErrorMsg)
        {
            FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\error.log", FileMode.Append, FileAccess.Write);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString() + "\r\n===============================================================================================\r\n" + ErrorMsg + "\r\n\r\n\r\n\r\n");
            fs.Write(buffer, 0, buffer.Length);
            fs.Flush();
            fs.Close();
        }

        public static void WriteFile(string filename, string content)
        {
            //用这种方式保存文本 当文字过长时  后面的文字不会被保存
            //FileStream fs = new FileStream(savefiledig.FileName, FileMode.OpenOrCreate);
            //byte[] buffer = Encoding.Default.GetBytes(richTextBox1.Text);
            //fs.Write(buffer, 0, richTextBox1.Text.Length);
            //fs.Flush();
            //fs.Close();

            StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);
            sw.Write(content);
            sw.Flush();
            sw.Close();
        }

    }
}
