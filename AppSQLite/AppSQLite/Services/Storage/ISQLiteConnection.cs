namespace AppSQLite.Services.Storage
{
    internal interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}