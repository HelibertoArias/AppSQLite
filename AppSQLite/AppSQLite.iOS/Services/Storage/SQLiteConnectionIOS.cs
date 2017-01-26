using AppSQLite.iOS.Services.Storage;
using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLiteConnectionIOS))]
namespace AppSQLite.iOS.Services.Storage
{
    public class SQLiteConnectionIOS
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = Constants.FileNameDB;
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, sqliteFilename);

            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}