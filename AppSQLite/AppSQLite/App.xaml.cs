using AppSQLite.Services.Storage;
using Xamarin.Forms;

namespace AppSQLite
{
    public partial class App : Application
    {
        #region Attributes

        private static DataBaseManager db;

        #endregion Attributes

        #region Properties

        // public static MasterPage Master { get; internal set; }

        public static NavigationPage Navigator { get; internal set; }

        public static DataBaseManager DB
        {
            get
            {
                if (db != null)
                {
                    db = new DataBaseManager();
                }
                return db;
            }
        }

        #endregion Properties

        #region Constructor

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainView());
        }

        #endregion Constructor

        #region Methods

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        #endregion Methods
    }
}