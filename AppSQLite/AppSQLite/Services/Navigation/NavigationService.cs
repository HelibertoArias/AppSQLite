using AppSQLite.ViewPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSQLite.Services.Navigation
{
    public class NavigationService
    {
        public enum Pages {
            MainPage,
            NewCustomerPage
        }


        #region Methods

        public async Task Navigate(Pages page)
        {
           // App.Master.IsPresented = false; /*Oculta el menú lateral al seleccionar*/

            switch (page)
            {
                case Pages.NewCustomerPage:
                    await App.Navigator.PushAsync(new NewCustomerPage()  );
                    break;

                case Pages.MainPage:
                    await App.Navigator.PushAsync(new MainPage());
                    break;

                default:
                    break;
            }
        }


        public async Task Back()
        {
            await App.Navigator.PopAsync();
        }
        #endregion
    }


}
