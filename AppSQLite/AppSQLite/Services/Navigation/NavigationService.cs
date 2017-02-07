using AppSQLite.ViewModels;
using AppSQLite.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppSQLite.Services.Navigation
{
    public class NavigationService
    {
        private static NavigationService _instance;

        private NavigationService() { }

        private IDictionary<Type, Type> viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(MainViewModel),  typeof(MainView) },
            { typeof(CustomerViewModel), typeof(NewCustomerView)}
            /*
             * Add here your viewmodel-view...to call them you use
             * NavigationService.Instance.NavigateTo<NewCustomerViewModel>();
             */
        };

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService();
                }

                return _instance;
            }
        }

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = viewModelRouting[typeof(TDestinationViewModel)];
            /*Add navigationcontext as parameter*/
            Page page = null;
            if (navigationContext == null)
                page = Activator.CreateInstance(pageType) as Page;
            else
                page = Activator.CreateInstance(pageType, navigationContext) as Page;
            

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateTo(Type destinationType, object navigationContext = null)
        {
            Type pageType = viewModelRouting[destinationType];
            var page = Activator.CreateInstance(pageType, navigationContext) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}