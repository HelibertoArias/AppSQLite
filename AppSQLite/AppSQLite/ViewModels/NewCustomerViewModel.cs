using System;
using AppSQLite.Models;
using Xamarin.Forms;
using AppSQLite.Services.Storage;
using AppSQLite.Services.Dialog;
using AppSQLite.Entities;
using AppSQLite.Services.Navigation;

namespace AppSQLite.ViewModels
{
    public class NewCustomerViewModel : BindableObject
    {
        public CustomerModel NewCustomer { get; set; }

        private Command _newCustomerSaveCommand;

        public Command NewCustomerSaveCommand { get { return _newCustomerSaveCommand = _newCustomerSaveCommand ?? new Command(NewCustomerSaveCommandExecute); } }

        private async void NewCustomerSaveCommandExecute()
        {
            try
            {
                Customer entity = new Customer() {
                       DateBirth = NewCustomer.DateBirth,
                       DocumentNumber= NewCustomer.DocumentNumber,
                       FirstName=NewCustomer.FirstName,
                       Id=NewCustomer.Id,
                       LastName=NewCustomer.LastName,
                       Thumbnail=NewCustomer.Thumbnail
                };

                await DataBaseManager.Instance.SaveOrUpdate(entity);

                await DialogService.Instance.ShowMessage("Operacion", "Registro guardado correctamente");

                //->Redirect to MainView.

                NavigationService.Instance.NavigateTo<MainViewModel>();

            }
            catch (Exception ex)
            {
                await DialogService.Instance.ShowMessage("Error", $"Error presentado {ex.Message}");
            }

        }



        public NewCustomerViewModel()
        {
            this.NewCustomer = new CustomerModel
            {
                FirstName = "Heliberto",
                LastName = "Arias",
                DateBirth = DateTime.Now.Date,
                DocumentNumber = 85151351,
                Thumbnail = null
            };

        }
    }
}