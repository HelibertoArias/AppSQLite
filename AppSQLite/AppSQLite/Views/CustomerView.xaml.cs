using AppSQLite.ViewModels;
using Xamarin.Forms;

namespace AppSQLite.Views
{
    public partial class CustomerView : ContentPage
    {
        public CustomerView(CustomerViewModel model)
        {
            InitializeComponent();
            
            BindingContext = model;
        }
    }
}