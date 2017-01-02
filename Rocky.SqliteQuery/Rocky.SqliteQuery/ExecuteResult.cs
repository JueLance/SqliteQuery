using System;

namespace Rocky.SqliteQuery
{
    public class ExecuteResult
    {
        public object Result { get; set; }

        public SqlCommandType CommandType { get; set; }

        public string Message { get; set; }
    }
}
