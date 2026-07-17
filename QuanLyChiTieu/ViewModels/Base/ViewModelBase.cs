using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QuanLyChiTieu.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? tenProperty = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tenProperty));
        }
        protected bool SetProperty<T>(ref T truong, T giaTri, [CallerMemberName] string? tenProperty = null)
        {
            if (EqualityComparer<T>.Default.Equals(truong, giaTri)) return false;
            truong = giaTri;
            OnPropertyChanged(tenProperty);
            return true;
        }
    }
}