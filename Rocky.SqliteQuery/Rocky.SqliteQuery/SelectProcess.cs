using System.Data;
using System.Text;

namespace Rocky.SqliteQuery
{
    public class SelectProcess : ISqlProcess
    {
        public ExecuteResult Process(string connectionString, string sql)
        {
            StringBuilder rowinfo = new StringBuilder();//记录受影响的行数
            DataSet ds = SqliteHelper.ExecuteDataset(connectionString, sql);
            if (ds.Tables.Count > 0)
            {
                foreach (DataTable dt in ds.Tables)
                {
                    rowinfo.Append("(" + dt.Rows.Count.ToString() + "行受影响)\r\n");
                }
            }

            return new ExecuteResult()
            {
                CommandType = SqlCommandType.Select,
                Result = ds,
                Message = rowinfo.ToString()
            };
        }
    }
}
