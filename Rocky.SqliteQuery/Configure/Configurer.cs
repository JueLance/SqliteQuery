using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace Configure
{


    public static class Configurer
    {

        //[DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        //public static extern int WritePrivateProfileString(
        //    string lpApplicationName,
        //    string lpKeyName,
        //    string lpString,
        //    string lpFileName
        //);
        ////lpApplicationName -  String，要在其中写入新字串的小节名称。这个字串不区分大小写
        ////lpKeyName ------  Any，要设置的项名或条目名。这个字串不区分大小写。用vbNullString可删除这个小节的所有设置项
        ////lpString -------  String，指定为这个项写入的字串值。用vbNullString表示删除这个项现有的字串
        ////lpFileName -----  String，初始化文件的名字。如果没有指定完整路径名，则windows会在windows目录查找文件。如果文件没有找到，则函数会创建它
        //[DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        //public static extern int GetPrivateProfileString(
        //    string lpApplicationName,
        //    int lpKeyName,
        //    string lpDefault,
        //    string lpReturnedString,
        //    int nSize,
        //    string lpFileName
        //);
        ////lpReturnedString缓冲区内装载指定小节所有项的列表
        ////lpDefault ------  String，指定的条目没有找到时返回的默认值。可设为空（""）
        ////lpReturnedString -  String，指定一个字串缓冲区，长度至少为nSize
        ////nSize ----------  Long，指定装载到lpReturnedString缓冲区的最大字符数量
        ////lpFileName -----  String，初始化文件的名字。如没有指定一个完整路径名，windows就在Windows目录中查找文件


        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //参数说明：section：INI文件中的段落；key：INI文件中的关键字；val：INI文件中关键字的数值；filePath：INI文件的完整的路径和名称。 
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
            int size, string filePath);
        //参数说明：section：INI文件中的段落名称；key：INI文件中的关键字；def：无法读取时候时候的缺省数值；retVal：读取数值；
        //size：数值的大小；filePath：INI文件的完整路径和名称。 


        public static readonly string configPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Config.ini";
    }
}
