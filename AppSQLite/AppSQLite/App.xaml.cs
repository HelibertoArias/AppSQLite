using AppSQLite.Services.Storage;
using Xamarin.Forms;

namespace AppSQLite
{
    public partial class App : Application
    {
        #region Attributes
        static DataBaseManager db;
        #endregion


        #region Properties

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

        #endregion


        #region Constructor
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ViewPages.MainPage());
        }
        #endregion


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
        #endregion
    }
}