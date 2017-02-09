using System;

namespace AppSQLite.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TDestinationViewModel>(object navigationContext = null);

        void NavigateTo(Type destinationType, object navigationContext = null);

        void NavigateBackToFirst();

        void NavigateBack();
    }
}