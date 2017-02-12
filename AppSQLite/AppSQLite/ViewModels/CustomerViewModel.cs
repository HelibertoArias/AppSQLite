using AppSQLite.Entities;
using AppSQLite.Models;
using AppSQLite.Services.Dialog;
using AppSQLite.Services.Navigation;
using AppSQLite.Services.Storage;
using AppSQLite.ViewModels.Base;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppSQLite.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        #region Attributes

        private const string EditTitle = "Editar cliente";
        private const string AddTitle = "Registrar cliente";
        private const string AddSuccess = "Registro guardado correctamente";
        private const string DeleteSucess = "Eliminación realizada correctamente";
        private const string OperacionSuccesTitle = "Operacion";
        private const string OperacionErrorTitle = "Error";
        private const string ErrorMessage = "Error presentado";
        private const string DeleteTitleConfirmation = "Confirmar eliminación";
        private const string DeleleteQuestring = "¿Esta seguro de eliminar este registro?";
        private const string ValidationTitle = "Validación";
        private const string acceptText = "Si";
        private const string cancelText = "No";
        private const string ErrorFistNameValidation = "El campo nombre esta vacío";
        private const string ErrorLastNameValidation = "El campo apellido esta vacío";
        private const string ErrorBirdthDateValidation = "La fecha de nacimiento debe se menor a la actual";

        private ICommand _saveCommand;

        private ICommand _deleteCommand;

        #endregion Attributes

        #region Properties

        private bool _enableDelete;

        public bool EnableDelete
        {
            get { return _enableDelete; }
            set
            {
                _enableDelete = value;
                OnPropertyChanged();
            }
        }

        public string TitleView
        {
            get
            {
                // return IsEditing ? EditTitle : AddTitle;
                return EnableDelete ? EditTitle : AddTitle;
            }
        }

        #endregion Properties

        public CustomerModel Customer { get; set; } = new CustomerModel();

        public CustomerViewModel()
        {
            
        }

        #region Command

        public ICommand SaveCommand { get { return _saveCommand = _saveCommand ?? new Command(SaveCommandExecute); } }

        public ICommand DeleteCommand { get { return _deleteCommand = _deleteCommand ?? new Command(DeleteCommandExecute); } }

        #endregion Command

        private async void SaveCommandExecute()
        {


            if (String.IsNullOrEmpty(Customer.FirstName) || string.IsNullOrWhiteSpace(Customer.FirstName))
            {
                await DialogService.Instance.ShowMessage(ValidationTitle, ErrorFistNameValidation);
                return;
            }

            if (String.IsNullOrEmpty(Customer.LastName) || string.IsNullOrWhiteSpace(Customer.LastName))
            {
                await DialogService.Instance.ShowMessage(ValidationTitle, ErrorLastNameValidation);
                return;
            }

            if (Customer.DateBirth >= DateTime.Now.Date)
            {
                await DialogService.Instance.ShowMessage(ValidationTitle, ErrorBirdthDateValidation);
                return;
            }

            try
            {
                //-> Validate

                string action = (Customer.Id == 0) ? "AddedRecord" : "UpdatedRecord";

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

                Customer.Id = entity.Id;/*Get new id if is new*/

                //--> Send notification    
                Xamarin.Forms.MessagingCenter.Send(new MainViewModel(), action, Customer );
                NavigationService.Instance.NavigateBack();
                //NavigationService.Instance.NavigateTo<MainViewModel>();
            }
            catch (Exception ex)
            {
                await DialogService.Instance.ShowMessage(OperacionErrorTitle, $"{ErrorMessage} {ex.Message}");
            }

           
        }

        private async void DeleteCommandExecute()
        {
            var result = await DialogService.Instance.ShowConfirm(DeleteTitleConfirmation, DeleleteQuestring, acceptText, cancelText);

            if (!result)
                return;

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
                
               // NavigationService.Instance.NavigateTo<MainViewModel>();

                Xamarin.Forms.MessagingCenter.Send(new MainViewModel(), "DeletedRecord", Customer);
                NavigationService.Instance.NavigateBack();
            }
            catch (Exception ex)
            {
                await DialogService.Instance.ShowMessage(OperacionErrorTitle, $"{ErrorMessage} {ex.Message}");
            }
        }
    }
}