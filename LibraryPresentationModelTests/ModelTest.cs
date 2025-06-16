using Library.Data.Interfaces;
using Library.Presentation.Model;
using Library.Presentation.Model.Interfaces;

namespace LibraryPresentationModelTests
{
    [TestClass]
    [DoNotParallelize]
    public class ModelTest
    {
        private IRepositoryModel repo;
        [TestInitialize]
        public void Setup()
        {
            string _DBRelativePath = @"..\..\..\..\LibraryTest\Database\LibraryDatabase.mdf";
            string _BasePath = AppContext.BaseDirectory;
            string _DBPath = Path.GetFullPath(Path.Combine(_BasePath, _DBRelativePath));
            string connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True";
            repo = ModelDataFactory.CreateRepository(connectionString);
            repo.TruncateAllData();
        }

        [TestMethod]
        public void ModelRepositoryIsNotNull()
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