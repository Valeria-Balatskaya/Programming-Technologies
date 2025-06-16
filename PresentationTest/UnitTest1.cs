using Library.Data;
namespace PresentationTest
{
    [TestClass]
    public class PresentationTest
    {
        [TestMethod]
        public void IsMainWindowViewModelCreatedSuccessfully()
        {
            string _DBRelativePath = @"..\..\..\Database\LibraryDatabase.mdf";
            string _BasePath = AppContext.BaseDirectory;
            string _DBPath = Path.GetFullPath(Path.Combine(_BasePath, _DBRelativePath));
            string connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True";
            var repo = Repository.Create(connectionString);
            var mainWindowViewModel = new ViewModel.MainWindowViewModel(repo);
        }
    }
}