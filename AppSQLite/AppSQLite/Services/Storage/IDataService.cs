using AppSQLite.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppSQLite.Services.Storage
{
    public interface IDataService
    {
        Task SaveOrUpdate<T>(T value) where T : IEntityBase, new();

        Task Delete<T>(T value) where T : IEntityBase, new();

        Task<List<T>> GetAll<T>() where T : IEntityBase, new();

        Task<T> GetItem<T>(int id) where T : IEntityBase, new();
    }
}