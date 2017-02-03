using AppSQLite.Entities;
using AppSQLite.Helpers;
using AppSQLite.Models;
using AppSQLite.Services.Navigation;
using AppSQLite.Services.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace AppSQLite.ViewModels
{
    public class MainViewModel : ObservableBaseObject
    {
        #region Attributes

        private Func<Customer, string> orderby = (x => x.LastName);

        private static DataBaseManager _db;

        private bool _isRunning;

        private static NavigationService _navigationService;

        private string _filter;

        private Command _creataSampleDataCommand { get; set; }

        public Command _newCustomerNavigationCommand { get; set; }

        public Command _searchCustomerCommand { get; set; }

        #endregion Attributes

        #region Properties

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

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

        public static DataBaseManager DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new DataBaseManager();
                }
                return _db;
            }
        }

        public static NavigationService NavigationService
        {
            get
            {
                if (_navigationService == null)
                    _navigationService = new NavigationService();
                return _navigationService;
            }
        }

        public ObservableCollection<CustomerModel> Customers { get; set; } = new ObservableCollection<CustomerModel>();

        #endregion Properties

        #region Commands

        public Command CreataSampleDataCommand { get { return _creataSampleDataCommand = _creataSampleDataCommand ?? new Command(CreataSampleDataExecute); } }

        public Command NewCustomerNavigationCommand { get { return _newCustomerNavigationCommand = _newCustomerNavigationCommand ?? new Command(NewCustomerNavigationExecute); } }

        public Command SearchCustomerCommand { get { return _searchCustomerCommand = _searchCustomerCommand ?? new Command(OnFilterExecute); } }

        #endregion Commands

        public MainViewModel()
        {
            _isRunning = false;
        }

        private async void CreataSampleDataExecute()
        {
            string[] firstNames = { "Jóse", "María", "Luís", "Lucas", "Matías", "Martín", "Lucho", "Josefa", "Karen", "Kate", "Pedro", "Marcho", "Yose", "Carlos", "Jaime", "Francisco", "Alfonso", "Ricardo", "Yuri", "Estafanni" };

            string[] lastNames = { "Cucunubá", "Coronado", "Arias", "Balaguera", "Grísales", "Fuentes", "Lopéz", "Galán", "Baños", "Piedrahita", "Granados" };

            int firstNamesLength = firstNames.Length - 1;
            int lastNameLength = lastNames.Length - 1;

            Random rdn = new Random(DateTime.Now.Millisecond);

            int i = 0;

            while (i < 3)
            {
                var customer = new Customer
                {
                    FirstName = firstNames[rdn.Next(0, firstNamesLength)],
                    LastName = $"{lastNames[rdn.Next(0, lastNameLength)]} {lastNames[rdn.Next(0, lastNameLength)]}"
                };

                var rowsQuery = await GetCustomers();

                var existe = rowsQuery.Any(x => x.FirstName.Equals(customer.FirstName)
                                             && x.LastName.Equals(customer.LastName));

                if (!existe)
                {
                    IsRunning = true;

                    await DB.SaveOrUpdate(customer);

                    rowsQuery = await GetCustomers();

                    Customers.Sort(rowsQuery, orderby);

                    await Task.Delay(1500);
                    i++;
                    IsRunning = false;
                }
            }
        }

        private void NewCustomerNavigationExecute()
        {
            NavigationService.Instance.NavigateTo<NewCustomerViewModel>();
        }

        private async void OnFilterExecute()
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

        private static async Task<List<CustomerModel>> GetCustomers()
        {
            var collection = await DB.GetAll<Customer>();
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

            return collectionModel;
        }
    }
}