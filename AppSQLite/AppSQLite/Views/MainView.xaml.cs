using AppSQLite.ViewModels;
using Xamarin.Forms;

namespace AppSQLite.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();

        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    Xamarin.Forms.MessagingCenter.Subscribe<MainViewModel>(this.BindingContext, "AddedRecord", async (sender) =>
        //    {
        //        await ((MainViewModel)this.BindingContext).FillList();
        //    });

        //    Xamarin.Forms.MessagingCenter.Subscribe<MainViewModel>(this.BindingContext, "UpdatedRecord", async (sender) =>
        //    {
        //        await ((MainViewModel)this.BindingContext).FillList();
        //    });

        //    Xamarin.Forms.MessagingCenter.Subscribe<MainViewModel>(this.BindingContext, "DeletedRecord", async (sender) =>
        //    {
        //        await ((MainViewModel)this.BindingContext).FillList();
        //    });

        //}


        //protected override void OnDisappearing()
        //{

        //    base.OnDisappearing();

        //    Xamarin.Forms.MessagingCenter.Unsubscribe<MainViewModel>(this.BindingContext, "AddedRecord");
        //    Xamarin.Forms.MessagingCenter.Unsubscribe<MainViewModel>(this.BindingContext, "UpdatedRecord");
        //    Xamarin.Forms.MessagingCenter.Unsubscribe<MainViewModel>(this.BindingContext, "DeletedRecord");
        //}

    }
}