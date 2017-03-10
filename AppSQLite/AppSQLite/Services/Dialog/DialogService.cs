using System.Threading.Tasks;

namespace AppSQLite.Services.Dialog
{
    public class DialogService
    {
        private static DialogService _instance;

        public static DialogService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DialogService();
                return _instance;
            }
        }

        public async Task ShowMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Acceptar");
        }

        public async Task<bool> ShowConfirm(string title, string message, string acceptText, string cancelText)
        {
            var answer = await App.Current.MainPage.DisplayAlert(title, message, acceptText, cancelText);
            return answer;
        }
    }
}