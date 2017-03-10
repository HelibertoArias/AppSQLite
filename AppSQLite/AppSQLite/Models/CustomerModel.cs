using AppSQLite.Entities;

namespace AppSQLite.Models
{
    public class CustomerModel : Customer
    {
        public string FullName { get { return $"{LastName} {FirstName} "; } }
    }
}