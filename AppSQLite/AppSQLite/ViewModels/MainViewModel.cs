using AppSQLite.Entities;
using AppSQLite.Helpers;
using AppSQLite.Services.Navigation;
using AppSQLite.Services.Storage;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using static AppSQLite.Services.Navigation.NavigationService;

namespace AppSQLite.ViewModels
{
    public class MainViewModel : ObservableBaseObject
    {
        Func<Customer, string> orderby = (x => x.LastName);

        private static DataBaseManager _db;
        private bool _isRunning;
        private static NavigationService _navigationService;
        private string _filter;

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
                OnFilter();
            }
        }

        public Command CreataSampleDataCommand { get; set; }

        public Command NewCustomerNavigationCommand { get; set; }

        public Command SearchCustomerCommand { get; set; }

        private string labelText;

        public string LabelText
        {
            set
            {
                labelText = value;
                OnPropertyChanged();
            }
            get { return labelText; }
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

        public ObservableCollection<Customer> Customers { get; set; } = new ObservableCollection<Customer>();


        public MainViewModel()
        {
            _isRunning = false;

            LabelText = "Texto inicial";

            CreataSampleDataCommand = new Command((cmd) => CreataSampleData());
            NewCustomerNavigationCommand = new Command((cmd) => NewCustomerNavigation());
            SearchCustomerCommand = new Command((cmd) => OnFilter());
        }

        private async void CreataSampleData()
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

                var rowsQuery = await DB.GetAll<Customer>();
                var existe = rowsQuery.Any(x => x.FirstName.Equals(customer.FirstName)
                                             && x.LastName.Equals(customer.LastName));

                if (!existe)
                {
                    IsRunning = true;

                    await DB.SaveOrUpdate(customer);

                    rowsQuery = await DB.GetAll<Customer>();

                    

                    Customers.Sort(rowsQuery, orderby);

                    await Task.Delay(1500);
                    i++;
                    IsRunning = false;
                }
            }
        }

        private async void NewCustomerNavigation()
        {
            await NavigationService.Navigate(Pages.NewCustomerPage);
        }

        private async void OnFilter()
        {
            var records = await DB.GetAll<Customer>();
            if (!string.IsNullOrEmpty(Filter))
            {
                records = records.Where(x => x.FirstName.Trim().ToLower().Contains(Filter.ToLower())
                               || x.LastName.Trim().ToLower().Contains(Filter.ToLower()))
                               .ToList();
            }

            Customers.Sort(records, orderby);

        }
    }
}