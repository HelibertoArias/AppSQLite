using AppSQLite.ViewModels;
using Xamarin.Forms;

namespace AppSQLite.Views
{
    public partial class NewCustomerView : ContentPage
    {
        public NewCustomerView(NewCustomerViewModel model)
        {
            InitializeComponent();
            
            BindingContext = model;
        }
    }
}