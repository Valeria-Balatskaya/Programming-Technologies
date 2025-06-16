using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data;
using Library.Data.Objects;

namespace LibraryDataTest
{
    [TestClass]
    [DoNotParallelize]
    public class DataLayerTest
    {
        private Repository repo;
        [TestInitialize]
        public void Setup()
        {
            string _DBRelativePath = @"..\..\..\Database\LibraryDatabase.mdf";
            string _BasePath = AppContext.BaseDirectory;
            string _DBPath = Path.GetFullPath(Path.Combine(_BasePath, _DBRelativePath));
            string connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True";
            repo = Repository.Create(connectionString);
            repo.TruncateAllData();
        }

        [TestMethod]
        public void RandomDataGeneratorIsValid()
        {
            Repository repo = RandomDataGenerator.GenerateRandomRepo(10, 10);
            var users = repo.GetAllUsers();
            var books = repo.GetCatalog();
            Assert.IsTrue(users.Count() == 10 && books.Count() == 10);
            repo.TruncateAllData();
        }

        [TestMethod]
        public void PredefinedDataGeneratorIsValid()
        {
            Repository repo = PredefinedDataGenerator.GeneratePredefinedRepo();
            var users = repo.GetAllUsers();
            var books = repo.GetCatalog();
            Assert.IsTrue(users.Count() == 3 && books.Count() == 3);
            repo.TruncateAllData();
        }


        [TestMethod]
        public void GetAllUsersReturnsCorrectValue()
        {
            User user1 = PredefinedDataGenerator.GenerateUser1();
            User user2 = PredefinedDataGenerator.GenerateUser2();
            repo.AddUser(user1);
            repo.AddUser(user2);
            Assert.AreEqual(true, repo.ContainsUser(user1));
            Assert.AreEqual(true, repo.ContainsUser(user2));
            Assert.AreEqual(2, repo.GetAllUsers().Count());
            repo.TruncateAllData();
        }

        [TestMethod]
        public void GetCatalogReturnsCorrectValue()
        {
            Book book1 = PredefinedDataGenerator.GenerateBook1();
            Book book2 = PredefinedDataGenerator.GenerateBook3();

            repo.AddBook(book1);
            repo.AddBook(book2);

            Assert.AreEqual(true, repo.ContainsBook(book1));
            Assert.AreEqual(true, repo.ContainsBook(book2));
            Assert.AreEqual(2, repo.GetCatalog().Count());
            repo.TruncateAllData();
        }

        [TestMethod]
        public void GetLibraryStateReturnsCorrectValue()
        {
            User user1 = PredefinedDataGenerator.GenerateUser1();
            Book book1 = PredefinedDataGenerator.GenerateBook1();
            repo.AddUser(user1);
            repo.AddBook(book1);
            var state = repo.GetLibraryState();
            bool flag = (state.Users.ElementAt(0).Name == user1.Name) &&
                (state.Books.ElementAt(0).Title == book1.Title);
            Assert.IsTrue(flag);
            repo.TruncateAllData();
        }

        [TestMethod]
        public void RemoveBookIsSuccessful()
        {
            Book book1 = PredefinedDataGenerator.GenerateBook1();
            Book book2 = PredefinedDataGenerator.GenerateBook2();
            repo.AddBook(book1);
            repo.AddBook(book2);
            var book = repo.GetBook(book1.Guid);
            repo.RemoveBook(book);
            Assert.IsFalse(repo.ContainsBook(book));
            repo.TruncateAllData();
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
