using Library.Presentation.ViewModel;
using Library.Presentation.ViewModel.Interfaces;

namespace LibraryPresentationVMTests
{
    [TestClass]
    [DoNotParallelize]
    public class VMTest
    {
        private IRepositoryVM repo;
        [TestInitialize]
        public void Setup()
        {
            string _DBRelativePath = @"..\..\..\..\LibraryTest\Database\LibraryDatabase.mdf";
            string _BasePath = AppContext.BaseDirectory;
            string _DBPath = Path.GetFullPath(Path.Combine(_BasePath, _DBRelativePath));
            string connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True";
            repo = VMDataFactory.CreateRepository(connectionString);
            repo.TruncateAllData();
        }

        [TestMethod]
        public void VMRepositoryIsNotNull()
        {
            Assert.IsNotNull(repo);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (repo != null)
            {
                repo.TruncateAllData();
                repo.Dispose();
            }
        }
    }
}