using AppSQLite.Entities;
using AppSQLite.Helpers;
using AppSQLite.Models;
using AppSQLite.Services.Dialog;
using AppSQLite.Services.Navigation;
using AppSQLite.Services.Storage;
using AppSQLite.ViewModels.Base;
using AppSQLite.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppSQLite.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Attributes

        private Func<Customer, string> orderby = (x => x.LastName);

        private string _filter;

        private ICommand _creataSampleDataCommand { get; set; }

        public ICommand _newCustomerNavigationCommand { get; set; }

        public ICommand _searchCustomerCommand { get; set; }
        #endregion Attributes

        #region Properties

       

        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged();
                OnFilterExecute();
            }
        }



        public ObservableCollection<CustomerModel> Customers { get; set; } = new ObservableCollection<CustomerModel>();

        #endregion Properties

        #region Commands

        // public Command CreataSampleDataCommand { get { return _creataSampleDataCommand = _creataSampleDataCommand ?? new Command(CreataSampleDataExecute); } }

        public ICommand NewCustomerNavigationCommand { get { return _newCustomerNavigationCommand = _newCustomerNavigationCommand ?? new Command(NewCustomerNavigationExecute); } }

        public ICommand SearchCustomerCommand { get { return _searchCustomerCommand = _searchCustomerCommand ?? new Command(OnFilterExecute); } }



        #endregion Commands

        public MainViewModel()
        {
            IsRunning= false;
            var dummy = FillList();
        }

        //private async void CreataSampleDataExecute()
        //{
        //    string[] firstNames = { "Jóse", "María", "Luís", "Lucas", "Matías", "Martín", "Lucho", "Josefa", "Karen", "Kate", "Pedro", "Marcho", "Yose", "Carlos", "Jaime", "Francisco", "Alfonso", "Ricardo", "Yuri", "Estafanni" };

        //    string[] lastNames = { "Cucunubá", "Coronado", "Arias", "Balaguera", "Grísales", "Fuentes", "Lopéz", "Galán", "Baños", "Piedrahita", "Granados" };

        //    int firstNamesLength = firstNames.Length - 1;
        //    int lastNameLength = lastNames.Length - 1;

        //    Random rdn = new Random(DateTime.Now.Millisecond);

        //    int i = 0;

        //    while (i < 3)
        //    {
        //        var customer = new Customer
        //        {
        //            FirstName = firstNames[rdn.Next(0, firstNamesLength)],
        //            LastName = $"{lastNames[rdn.Next(0, lastNameLength)]} {lastNames[rdn.Next(0, lastNameLength)]}"
        //        };

        //        var rowsQuery = await GetCustomers();

        //        var existe = rowsQuery.Any(x => x.FirstName.Equals(customer.FirstName)
        //                                     && x.LastName.Equals(customer.LastName));

        //        if (!existe)
        //        {
        //            IsRunning = true;

        //            await DataBaseManager.Instance.SaveOrUpdate(customer);

        //            rowsQuery = await GetCustomers();

        //            Customers.Sort(rowsQuery, orderby);

        //            await Task.Delay(1500);
        //            i++;
        //            IsRunning = false;
        //        }
        //    }
        //}

        private void NewCustomerNavigationExecute()
        {
            //NavigationService.Instance.NavigateTo<NewCustomerViewModel>();
            //NavigationService.Instance.NavigateTo<CustomerViewModel>(new CustomerViewModel(){ IsEditing=false});
            NavigationService.Instance.NavigateTo<CustomerViewModel>(new CustomerViewModel() { EnableDelete = false } );
        }

        private async void OnFilterExecute()
        {
            await FillList();
        }

        private async Task FillList()
        {
            var records = await GetCustomers();
            if (!string.IsNullOrEmpty(Filter))
            {
                records = records.Where(x => x.FirstName.Trim().ToLower().Contains(Filter.ToLower())
                               || x.LastName.Trim().ToLower().Contains(Filter.ToLower()))
                               .ToList();
            }

            Customers.Sort(records, orderby);
        }

        private async Task<List<CustomerModel>> GetCustomers()
        {
            IsRunning = true;

            var collection = await DataBaseManager.Instance.GetAll<Customer>();
            var collectionModel = new List<CustomerModel>();

            foreach (var entity in collection)
            {
                collectionModel.Add(
                                        new CustomerModel
                                        {
                                            FirstName = entity.FirstName,
                                            LastName = entity.LastName,
                                            Id = entity.Id,
                                            DateBirth = entity.DateBirth,
                                            DocumentNumber = entity.DocumentNumber
                                        });
            }

            IsRunning = false;

            return collectionModel;
        }
 
    }
}