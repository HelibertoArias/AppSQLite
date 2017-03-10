using AppSQLite.Entities;
using AppSQLite.Services.Navigation;
using AppSQLite.ViewModels;
using Xamarin.Forms;

namespace AppSQLite.Models
{
    public class CustomerModel : Customer
    {
        public string FullName { get { return $"{LastName} {FirstName} "; } }
    }
}