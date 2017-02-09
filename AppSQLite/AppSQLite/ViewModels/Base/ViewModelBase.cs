using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppSQLite.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isRunning;

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        public ViewModelBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate
        {
        };

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}