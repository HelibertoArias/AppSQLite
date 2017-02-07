using AppSQLite.ViewModels;
using Xamarin.Forms;

namespace AppSQLite.Views
{
    public partial class NewCustomerView : ContentPage
    {
        public NewCustomerView(CustomerViewModel model)
        {
            InitializeComponent();
            
            BindingContext = model;
        }
    }
}