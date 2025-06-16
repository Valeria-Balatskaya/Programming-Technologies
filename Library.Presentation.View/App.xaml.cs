using Library.Presentation.ViewModel;
using Library.Presentation.ViewModel.Interfaces;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace Library.Presentation.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IRepositoryVM? repo = null;
            string _DBRelativePath = @"..\..\..\..\LibraryTest\Database\LibraryDatabase.mdf";
            string _BasePath = AppContext.BaseDirectory;
            string _DBPath = Path.GetFullPath(Path.Combine(_BasePath, _DBRelativePath));
            string connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True";
            if (File.Exists(_DBPath))
            {
                repo = VMDataFactory.CreateRepository(connectionString);
            }
            var mainWindowViewModel = new MainWindowViewModel(repo);
            var mainWindow = new MainWindow(mainWindowViewModel);
            mainWindow.Show();
        }
    }
}
