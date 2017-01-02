namespace Rocky.SqliteQuery
{
    public interface ISqlProcess
    {
        ExecuteResult Process(string connectionString, string sql);
    }
}
