using AppSQLite.Services.Navigation;
using System.Collections.Generic;

using Xamarin.Forms;

namespace AppSQLite.Views
{
    public partial class MasterView : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public MasterView()
        {
            InitializeComponent();

            var masterPageItems = new List<MasterViewItem>();
            masterPageItems.Add(new MasterViewItem
            {
                Title = "Customers",
                IconSource = "customer.png",
                TargetType = typeof(CustomerListView)
            });

            listView.ItemsSource = masterPageItems;
        }

        //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MasterViewItem;
        //    if (item != null)
        //    {
        //        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
        //        masterPage.ListView.SelectedItem = null;
        //        IsPresented = false;
        //    }
        //}
    }
}