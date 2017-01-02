using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace SyntaxHighlighter
{
    /// <summary>
    /// 
    /// </summary>
    public class SyntaxRichTextBox : RichTextBox
    {
        private SyntaxSettings settings = new SyntaxSettings();

        public SyntaxSettings Settings
        {
            get { return settings; }
        }
        private static bool paint = true;
        private string strLine = "";
        private int contentLength = 0;
        private int lineLength = 0;
        private int lineStart = 0;
        private int lineEnd = 0;
        private string strKeywords = "";
        private int curSelection = 0;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x00f)
            {
                if (paint)
                {
                    base.WndProc(ref m);
                }
                else
                {
                    m.Result = IntPtr.Zero;
                }
            }
            else
            {
                base.WndProc(ref m);
            }

        }
        /// <summary>
        /// �����ָı�ʱ��ƥ��������ɫ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            contentLength = this.TextLength;
            int curSelectionStart = SelectionStart;
            int curSelectionLength = SelectionLength;

            paint = false;
            lineStart = curSelectionStart;
            while ((lineStart > 0) && (Text[lineStart - 1] != '\n'))
            {
                lineStart--;
            }
            lineEnd = curSelectionStart;

            while ((lineEnd < Text.Length) && (Text[lineEnd] != '\n'))
            {
                lineEnd++;
            }

            lineLength = lineEnd - lineStart;

             strLine = Text.Substring(lineStart, lineLength);

            ProcessLine();
            //base.OnTextChanged(e);

            paint = true;
        }
        /// <summary>
        /// ������
        /// </summary>
        private void ProcessLine()
        {
            int position = SelectionStart;
            SelectionStart = lineStart;
            SelectionLength = lineLength;
            SelectionColor = Color.Black;

            ProcessRegex(strKeywords, settings.KeyWordColor);

            if (settings.EnableInterger)
            {
                ProcessRegex("\\b(?:[0-9]*\\.)?[0-9]+\\b", settings.ColorInterger);

            }

            if (settings.EnableComment && !string.IsNullOrEmpty(settings.Comment))
            {
                ProcessRegex(settings.Comment + ".*$", settings.CommentColor);
            }

            SelectionStart = position;
            SelectionLength = 0;
            SelectionColor = Color.Black;
            curSelection = position;
        }
        /// <summary>
        /// ����������ɫ
        /// </summary>
        /// <param name="strRegex">������ʽ</param>
        /// <param name="color">��ɫ</param>
        private void ProcessRegex(string strRegex, Color color)
        {
            Regex regkeywords = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match regMatch;

            for (regMatch = regkeywords.Match(strLine); regMatch.Success; regMatch = regMatch.NextMatch())
            {
                int start = lineStart + regMatch.Index;
                int length = regMatch.Length;
                SelectionStart = start;
                SelectionLength = length;
                SelectionColor = color;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void CopmileKeywords()
        {
            for (int i = 0; i < settings.KeyWords.Count; i++)
            {
                string strKeyword = settings.KeyWords[i];
                if (i == settings.KeyWords.Count - 1)
                {
                    strKeywords += "\\b" + strKeyword + "\\b";
                }
                else
                {
                    strKeywords += "\\b" + strKeyword + "\\b|";
                }
            }
        }

        /// <summary>
        /// ���������е�����
        /// </summary>
        public void ProcessAllLines()
        {
            paint = false;
            int startPos = 0;
            int i = 0;
            int originalPos = SelectionStart;
            while (i < Lines.Length)
            {
                strLine = Lines[i];
                lineStart = startPos;
                lineEnd = lineStart + strLine.Length;

                ProcessLine();
                i++;
                startPos += strLine.Length + 1;

            }
            paint = true;
        }
    }

    /// <summary>
    /// �洢Syntax����
    /// </summary>
    public class SyntaxList
    {
        public List<string> list = new List<string>();
        public Color color = new Color();
    }
    /// <summary>
    /// ���ùؼ��ֺ���ɫ
    /// </summary>
    public class SyntaxSettings
    {

        string strComment = "";//ע��
        Color colorComment = Color.Green;

        public Color ColorComment
        {
            get { return colorComment; }
            set { colorComment = value; }
        }
        Color colorString = Color.Gray;

        public Color ColorString
        {
            get { return colorString; }
            set { colorString = value; }
        }
        Color colorInterger = Color.Red;

        public Color ColorInterger
        {
            get { return colorInterger; }
            set { colorInterger = value; }
        }
        bool enableComment = true;
        bool enableString = true;

        public bool EnableString
        {
            get { return enableString; }
            set { enableString = value; }
        }
        bool enableInterger = true;

        public bool EnableInterger
        {
            get { return enableInterger; }
            set { enableInterger = value; }
        }
        SyntaxList keyword = new SyntaxList();
        /// <summary>
        /// ��ȡ�ؼ����б�
        /// </summary>
        public List<string> KeyWords
        {
            get
            {
                return keyword.list;
            }
        }

        /// <summary>
        /// ��ȡ�������ùؼ��ֵ���ɫ
        /// </summary>
        public Color KeyWordColor
        {
            get
            {
                return keyword.color;
            }
            set
            {
                keyword.color = value;
            }
        }
        /// <summary>
        /// ��ȡ��������ע�ӵ�����
        /// </summary>
        public string Comment
        {
            get
            {
                return strComment;
            }
            set
            {
                strComment = value;
            }
        }

        /// <summary>
        /// ��ȡ��������ע�����ֵ���ɫ
        /// </summary>
        public Color CommentColor
        {
            get
            {
                return colorComment;
            }
            set
            {
                colorComment = value;
            }
        }

        /// <summary>
        /// ��ȡ���������Ƿ�����ע��
        /// </summary>
        public bool EnableComment
        {
            get
            {
                return enableComment;
            }
            set
            {
                enableComment = value;
            }
        }



    }
}
