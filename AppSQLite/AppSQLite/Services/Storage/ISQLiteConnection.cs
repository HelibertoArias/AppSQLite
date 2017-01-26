using SQLite;

namespace AppSQLite.Services.Storage
{
    public  interface ISQLiteConnection
    {
        SQLiteAsyncConnection GetConnection();
    }
}