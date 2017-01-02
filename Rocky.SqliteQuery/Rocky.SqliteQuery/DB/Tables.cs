using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocky.SqliteQuery
{
    public class Tables
    {
        public static List<TableModel> GetTables(string connectionString)
        {
            var list = new List<TableModel>();
            var ds = SqliteHelper.ExecuteDataset(connectionString, "select * from sqlite_master where type='table' order by name;");
            var dt = ds.Tables[0];

            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    TableModel model = new TableModel();
                    model.tabletype = row["type"].ToString();
                    model.name = row["name"].ToString();
                    model.tbl_name = row["tbl_name"].ToString();
                    model.rootpage = row["rootpage"].ToString();
                    model.sql = row["sql"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }

        public static List<ColumnModel> GetColumns(string connectionString, string tableName)
        {
            var list = new List<ColumnModel>();
            var ds = SqliteHelper.ExecuteDataset(connectionString, "PRAGMA table_info([" + tableName + "])");

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];

                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ColumnModel model = new ColumnModel();
                        model.cid = row["cid"].ToString();
                        model.name = row["name"].ToString();
                        model.type = row["type"].ToString();
                        model.notnull = row["notnull"].ToString();
                        model.dflt_value = row["dflt_value"].ToString();
                        model.pk = row["pk"].ToString();

                        list.Add(model);
                    }
                }
            }

            return list;
        }
    }

    public class TableModel
    {
        public string tabletype { get; set; }
        public string name { get; set; }
        public string tbl_name { get; set; }
        public string rootpage { get; set; }
        public string sql { get; set; }
    }

    public class ColumnModel
    {
        public string cid { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string notnull { get; set; }
        public string dflt_value { get; set; }
        public string pk { get; set; }
    }
}
