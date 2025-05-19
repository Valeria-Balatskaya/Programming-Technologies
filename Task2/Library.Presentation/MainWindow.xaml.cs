using Library.Presentation.ViewModels;
using System.Windows;

namespace Library.Presentation
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}