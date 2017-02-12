using AppSQLite.Entities;
using AppSQLite.Entities.Base;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

/// <summary>
/// More information in https://developer.xamarin.com/guides/xamarin-forms/working-with/databases/
/// </summary>
namespace AppSQLite.Services.Storage
{
    public class DataBaseManager : IDataService
    {
        private SQLiteAsyncConnection database;

        private object thisLock = new Object();

        private DataBaseManager()
        {
            database = DependencyService.Get<ISQLiteConnection>().GetConnection();
            //-->Add your entities here
            database.CreateTableAsync<Customer>().Wait();
        }

        private static DataBaseManager _db;

        public static DataBaseManager Instance
        {
            get
            {
                if (_db == null)
                    _db = new DataBaseManager();
                return _db;
            }
        }

        public Task SaveOrUpdate<T>(T value) where T : EntityBase, new()
        {
            lock (thisLock)
            {
                if (value.Id != 0)
                {
                    return database.UpdateAsync(value);
                }
                else
                {
                    return database.InsertAsync(value);
                }
            }
        }

        public Task Delete<T>(T value) where T : EntityBase, new()
        {
            lock (thisLock)
            {
                return database.DeleteAsync(value);
            }
        }

        public Task<List<T>> GetAll<T>() where T : EntityBase, new()
        {
            lock (thisLock)
            {
                return database.Table<T>().ToListAsync();
            }
        }

        public Task<T> GetItem<T>(int id) where T : EntityBase, new()
        {
            lock (thisLock)
            {
                return database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }
    }
}