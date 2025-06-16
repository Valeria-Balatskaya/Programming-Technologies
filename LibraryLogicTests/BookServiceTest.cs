using LibraryLogicTests.MockData;
using Logic.Logic;
using Logic.Logic.Interfaces;

namespace LibraryLogicTests
{
    [TestClass]
    [DoNotParallelize]
    public class BookServiceTest
    {
        private MockRepo repo;
        [TestInitialize]
        public void Setup()
        {
            repo = new MockRepo();
        }

        [TestMethod]
        public void BorrowBookWorksCorrectly()
        {
            IBookLogic book1 = LogicDataFactory.CreateBook("1984", "George Orwell", "Dystopian", new DateTime(1949, 1, 1), "978-0451524935", 328);
            IUserLogic user1 = LogicDataFactory.CreateUser("John", "Pork", "jpork@email.com");
            repo.AddBook(book1);
            repo.AddUser(user1);
            var users = repo.GetAllUsers();
            var cat = repo.GetCatalog();
            var user = users.ElementAt(0);
            var book = cat.ElementAt(0);
            repo.BorrowBook(book, user);
            Assert.AreEqual(book.OwnerId, user.Guid);
        }

        [TestMethod]
        public void ReturnBookWorksCorrectly()
        {
            IBookLogic book1 = LogicDataFactory.CreateBook("1984", "George Orwell", "Dystopian", new DateTime(1949, 1, 1), "978-0451524935", 328);
            IUserLogic user1 = LogicDataFactory.CreateUser("John", "Pork", "jpork@email.com");
            repo.AddBook(book1);
            repo.AddUser(user1);
            var users = repo.GetAllUsers();
            var cat = repo.GetCatalog();
            var user = users.ElementAt(0);
            var book = cat.ElementAt(0);
            repo.BorrowBook(book, user);
            repo.ReturnBook(book, user);
            Assert.AreEqual(book.OwnerId, Guid.Empty);
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
