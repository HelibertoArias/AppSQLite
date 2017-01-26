using AppSQLite.Droid.Services.Storage;
using AppSQLite.Services.Storage;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLiteConnectionDroid))]
namespace AppSQLite.Droid.Services.Storage
{
    public class SQLiteConnectionDroid : ISQLiteConnection
    {
        public SQLiteConnectionDroid()
        {
        }

        public SQLiteAsyncConnection GetConnection()
        {
            SQLitePCL.Batteries.Init();
            var sqliteFileName = Constants.FileNameDB;
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);

            //Var create the connection
            var connection = new SQLiteAsyncConnection(path);

            return connection;
        }
    }
}