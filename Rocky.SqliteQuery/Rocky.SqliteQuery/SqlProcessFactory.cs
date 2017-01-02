using System;

namespace Rocky.SqliteQuery
{
    public abstract class SqlProcessFactory
    {
        public static ExecuteResult Process(string connectionString, string sql)
        {
            SqlCommandType type = SqlCommandType.Select;
            if (sql.Trim().ToLower().StartsWith("select"))
            {
                type = SqlCommandType.Select;
            }
            else
            {
                type = SqlCommandType.NoSelect;
            }

            switch (type)
            {
                case SqlCommandType.Select:
                    return new SelectProcess().Process(connectionString, sql);

                case SqlCommandType.NoSelect:
                    return new NoSelectProcess().Process(connectionString, sql);
                default:
                    throw new Exception("Can't handle this sql sentense.");
            }
        }
    }
}
