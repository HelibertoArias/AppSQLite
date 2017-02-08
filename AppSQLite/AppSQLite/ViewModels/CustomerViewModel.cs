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
        public string TitleView { get; set; }

        public CustomerModel Customer { get; set; } = new CustomerModel();

        public CustomerViewModel()
        {
            
        }


        private Command _SaveCommand;

        public Command SaveCommand { get { return _SaveCommand = _SaveCommand ?? new Command(SaveCommandExecute); } }

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

                await DialogService.Instance.ShowMessage("Operacion", "Registro guardado correctamente");

                //->Redirect to MainView.
                NavigationService.Instance.NavigateBack();
            }
            catch (Exception ex)
            {
                await DialogService.Instance.ShowMessage("Error", $"Error presentado {ex.Message}");
            }
        }

        
    }
}