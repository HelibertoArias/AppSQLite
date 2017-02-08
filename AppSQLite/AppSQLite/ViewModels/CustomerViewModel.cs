using AppSQLite.Entities;
using AppSQLite.Models;
using AppSQLite.Services.Dialog;
using AppSQLite.Services.Navigation;
using AppSQLite.Services.Storage;
using System;
using Xamarin.Forms;

namespace AppSQLite.ViewModels
{
    public class CustomerViewModel : BindableObject
    {
        #region Attributes
        private const string EditTitle = "Editar cliente";
        private const string AddTitle = "Registrar cliente";
        private const string AddSuccess = "Registro guardado correctamente";
        private const string DeleteSucess = "Eliminación realizada correctamente";
        private const string OperacionSuccesTitle = "Operacion";
        private const string OperacionErrorTitle = "Error";
        private const string ErrorMessage = "Error presentado";

        private Command _saveCommand;

        private Command _deleteCommand;
        #endregion

        #region Properties
       public bool IsEditing { get; set; }


        public string TitleView
        {
            get
            {
                // return IsEditing ? EditTitle : AddTitle;
                return Customer.Id != 0 ? EditTitle : AddTitle;
            } 
        }

        #endregion
        public CustomerModel Customer { get; set; } = new CustomerModel();

        public CustomerViewModel()
        {
            IsEditing =  Customer.Id != 0;
        }

        #region Command

        public Command SaveCommand { get { return _saveCommand = _saveCommand ?? new Command(SaveCommandExecute); } }

        public Command DeleteCommand { get { return _deleteCommand = _deleteCommand ?? new Command(DeleteCommandExecute); } }

        #endregion Command

        private async void SaveCommandExecute()
        {
            try
            {
                //-> Validate

                Customer entity = new Customer()
                {
                    DateBirth = Customer.DateBirth,
                    DocumentNumber = Customer.DocumentNumber,
                    FirstName = Customer.FirstName,
                    Id = Customer.Id,
                    LastName = Customer.LastName,
                    Thumbnail = Customer.Thumbnail
                };

                await DataBaseManager.Instance.SaveOrUpdate(entity);

                await DialogService.Instance.ShowMessage(OperacionSuccesTitle, AddSuccess);

                //->Redirect to MainView. requiere Message Center implements
                //NavigationService.Instance.NavigateBack();
                NavigationService.Instance.NavigateTo<MainViewModel>();
            }
            catch (Exception ex)
            {
                await DialogService.Instance.ShowMessage(OperacionErrorTitle, $"{ErrorMessage} {ex.Message}");
            }
        }

        private async void DeleteCommandExecute()
        {
            try
            {
                //-> Validate

                Customer entity = new Customer()
                {
                    Id = Customer.Id
                };

                await DataBaseManager.Instance.Delete(entity);

                await DialogService.Instance.ShowMessage(OperacionSuccesTitle, DeleteSucess);

                //->Redirect to MainView. requiere Message Center implements
                //NavigationService.Instance.NavigateBack();
                NavigationService.Instance.NavigateTo<MainViewModel>();
            }
            catch (Exception ex)
            {
                await DialogService.Instance.ShowMessage(OperacionErrorTitle, $"{ErrorMessage} {ex.Message}");
            }
        }
    }
}