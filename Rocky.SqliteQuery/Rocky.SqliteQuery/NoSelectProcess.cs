using System;
using System.Text;

namespace Rocky.SqliteQuery
{
    public class NoSelectProcess : ISqlProcess
    {
        public ExecuteResult Process(string connectionString, string sql)
        {
            StringBuilder rowinfo = new StringBuilder();//记录受影响的行数
            int rows = 0;
            //处理go
            if (sql.IndexOf("GO\r\n", StringComparison.OrdinalIgnoreCase) > 0)
            {
                string[] split = sql.Split(new string[] { "GO\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string sqlstr in split)
                {
                    if (sqlstr.Trim() != "")
                    {
                        rows = SqliteHelper.ExecuteNonQuery(connectionString, sqlstr);
                        rowinfo.Append(sqlstr.Trim() + "\r\n" + "命令成功完成！" + "\r\n\r\n");
                    }
                }
            }
            else
            {
                rows = SqliteHelper.ExecuteNonQuery(connectionString, sql);
                rowinfo.Append("（" + rows.ToString() + "行受影响）\r\n");
            }

            return new ExecuteResult()
            {
                CommandType = SqlCommandType.NoSelect,
                Message = rowinfo.ToString(),
                Result = rows
            };
        }
    }
}
