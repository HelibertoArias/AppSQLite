using SQLite;

namespace AppSQLite.Entities.Base
{
    public class EntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}