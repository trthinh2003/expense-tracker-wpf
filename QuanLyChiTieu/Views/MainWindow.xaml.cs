using System.Windows;
using QuanLyChiTieu.ViewModels;

namespace QuanLyChiTieu.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}