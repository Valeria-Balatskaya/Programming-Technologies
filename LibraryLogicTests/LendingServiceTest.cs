using LibraryLogicTests.MockData;
using Logic.Logic;
using Logic.Logic.Interfaces;

namespace LibraryLogicTests
{
    [TestClass]
    public class LendingServiceTest
    {
        private MockRepo repo;
        [TestInitialize]
        public void Setup()
        {
            repo = new MockRepo();
        }

        [TestMethod]
        public void SetFineWorksCorrectly()
        {
            IUserLogic user1 = LogicDataFactory.CreateUser("John", "Pork", "jpork@email.com");
            repo.AddUser(user1);
            var users = repo.GetAllUsers();
            var usr = users.Last();
            repo.SetFine(usr, 100.0);
            Assert.AreEqual(100, usr.FineAmount);
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
