using AppSQLite.Entities;
using AppSQLite.Services.Storage;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace AppSQLite.ViewModels
{
    public class MainViewModel : ObservableBaseObject
    {


        private static DataBaseManager db;
        private bool isRunning;



        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
        }

        public Command CreataSampleDataCommand { get; set; }
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
                if (db == null)
                {
                    db = new DataBaseManager();
                }
                return db;
            }
        }

        private ObservableCollection<Customer> customers;

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            isRunning = false;

            LabelText = "Texto inicial";

            CreataSampleDataCommand = new Command((obj) => CreataSampleData());
            Customers = new ObservableCollection<Customer>();
        }

        private async void CreataSampleData()
        {

            var rows = await DB.GetAll<Customer>();
            if (rows.Count > 10)
            {
                Customers = new ObservableCollection<Customer>(rows.OrderBy(x => x.FirstName).ThenBy(x => x.LastName));
                return;
            }


            string[] firstNames = { "Jóse", "María", "Luís", "Lucas", "Matías", "Martín", "Lucho", "Josefa", "Karen", "Kate", "Pedro", "Marcho", "Yose", "Carlos", "Jaime", "Francisco", "Alfonso", "Ricardo", "Yuri", "Estafanni" };

            string[] lastNames = { "Cucunubá", "Coronado", "Arias", "Balaguera", "Grísales", "Fuentes", "Lopéz", "Galán", "Baños", "Piedrahita", "Granados" };

            int firstNamesLength = firstNames.Length - 1;
            int lastNameLength = lastNames.Length - 1;


            Random rdn = new Random(DateTime.Now.Millisecond);

            int i = 0;

            while (i < 10)
            {
                var customer = new Customer
                {
                    LastName = firstNames[rdn.Next(0, firstNamesLength)],
                    FirstName = $"{lastNames[rdn.Next(0, lastNameLength)]} {lastNames[rdn.Next(0, lastNameLength)]}"
                };

                var rowsQuery = await DB.GetAll<Customer>();
                var existe = rowsQuery.Any(x => x.FirstName.Equals(customer.FirstName) && x.LastName.Equals(customer.LastName));

                if (!existe)
                {
                    IsRunning = true;
                    await DB.SaveOrUpdate<Customer>(customer);

                    Customers.Clear();

                    Customers = new ObservableCollection<Customer>( rowsQuery.OrderBy(x => x.FirstName).ThenBy(x => x.LastName));

                    await Task.Delay(1500);
                    i++;
                    IsRunning = false;
                }
            }
        }
    }
}