using AppSQLite.Entities;
using AppSQLite.Services.Navigation;
using AppSQLite.ViewModels;
using Xamarin.Forms;

namespace AppSQLite.Models
{
    public class CustomerModel : Customer
    {
        public Command _selectCommand { get; set; }

        public Command SelectCommand { get { return _selectCommand = _selectCommand ?? new Command(SelectCommandExecute); } }

        private void SelectCommandExecute()
        {
            NavigationService.Instance.NavigateTo<CustomerViewModel>(
                    new CustomerViewModel()
                    {
                        Customer = this,
                        EnableDelete = true
                    }
            );
        }

        public string FullName { get { return $"{LastName} {FirstName} "; } }
    }
}