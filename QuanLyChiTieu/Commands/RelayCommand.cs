using System.Windows.Input;

namespace QuanLyChiTieu.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _thucThi;
        private readonly Func<object?, bool>? _dieuKienChay;

        public RelayCommand(Action<object?> thucThi, Func<object?, bool>? dieuKienChay = null)
        {
            _thucThi = thucThi;
            _dieuKienChay = dieuKienChay;
        }
     
        public bool CanExecute(object? tham_so) => _dieuKienChay?.Invoke(tham_so) ?? true; // WPF tự gọi lại CanExecute mỗi khi có tương tác UI (focus đổi, phím bấm...)

        public void Execute(object? tham_so) => _thucThi(tham_so);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    // bản async - dùng khi Command gọi DB (không block UI thread)
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object?, Task> _thucThi;
        private readonly Func<object?, bool>? _dieuKienChay;
        private bool _dangChay;

        public AsyncRelayCommand(Func<object?, Task> thucThi, Func<object?, bool>? dieuKienChay = null)
        {
            _thucThi = thucThi;
            _dieuKienChay = dieuKienChay;
        }

        public bool CanExecute(object? tham_so) => !_dangChay && (_dieuKienChay?.Invoke(tham_so) ?? true);

        public async void Execute(object? tham_so)
        {
            _dangChay = true;
            CommandManager.InvalidateRequerySuggested(); // disable nút trong lúc chạy, tránh double-click
            try
            {
                await _thucThi(tham_so);
            }
            finally
            {
                _dangChay = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
