using Xamarin.Forms;

namespace AppSQLite
{
    public partial class App : Application
    {
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