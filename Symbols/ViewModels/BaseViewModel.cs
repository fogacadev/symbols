using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Symbols.ViewModels
{
    public  class BaseViewModel : INotifyPropertyChanged
    {

        public void SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            property = value;
            OnPropertyChanged(propertyName);
        }

        private bool loading;
        public bool Loading
        {
            get { return loading; }
            set { SetProperty(ref loading, value); }
        }

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
