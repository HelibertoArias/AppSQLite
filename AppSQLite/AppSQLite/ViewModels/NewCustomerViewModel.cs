using AppSQLite.Models;

namespace AppSQLite.ViewModels
{
    public class NewCustomerViewModel : CustomerModel
    {
        public NewCustomerViewModel()
        {
            this.FirstName = "default firstname";
            this.LastName = "default lastname";
        }
    }
}