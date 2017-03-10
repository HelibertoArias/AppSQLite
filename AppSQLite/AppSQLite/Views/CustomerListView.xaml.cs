using AppSQLite.Models;
using AppSQLite.Services.Navigation;
using AppSQLite.ViewModels;

using Xamarin.Forms;

namespace AppSQLite.Views
{
    public partial class CustomerListView : ContentPage
    {
        public CustomerListView()
        {
            InitializeComponent();

            listView.ItemSelected += (sender, e) =>
            {
                var item = e.SelectedItem as CustomerModel;

                NavigationService.Instance.NavigateTo<CustomerViewModel>(
                    new CustomerViewModel()
                    {
                        Customer = item,
                        EnableDelete = true
                    });
            };
        }
    }
}