using LibraryLogicTests.MockData;
using Logic.Logic;
using Logic.Logic.Interfaces;
using Logic.Logic.Object;

namespace LibraryLogicTests
{
    internal class PredefinedDataGenerator
    {
        public static UserLogic GenerateUser1()
        {
            UserLogic user1 = new UserLogic("John", "Pork", "jpork@email.com");
            return user1;
        }

        public static UserLogic GenerateUser2()
        {
            UserLogic user2 = new UserLogic("Tim", "Cheese", "tcheese@email.com");
            return user2;
        }

        public static UserLogic GenerateUser3()
        {
            UserLogic user3 = new UserLogic("Tralalero", "Tralala", "ttralala@email.com");
            return user3;
        }

        public static BookLogic GenerateBook1()
        {
            BookLogic book1 = new BookLogic("1984", "George Orwell", "Dystopian", new DateTime(1949, 1, 1), "978-0451524935", 328);
            return book1;
        }

        public static BookLogic GenerateBook2()
        {
            BookLogic book2 = new BookLogic("To Kill a Mockingbird", "Harper Lee", "Classic", new DateTime(1960, 1, 1), "978-0061120084", 336);
            return book2;
        }

        public static BookLogic GenerateBook3()
        {
            BookLogic book3 = new BookLogic("The Hobbit", "J.R.R. Tolkien", "Fantasy", new DateTime(1937, 1, 1), "978-0547928227", 310);
            return book3;
        }

        public static MockRepo GeneratePredefinedRepo()
        {
            MockRepo repo = new MockRepo();

            UserLogic user1 = new UserLogic("John", "Pork", "jpork@email.com");
            UserLogic user2 = new UserLogic("Tim", "Cheese", "tcheese@email.com");
            UserLogic user3 = new UserLogic("Tralalero", "Tralala", "ttralala@email.com");

            BookLogic book1 = new BookLogic("1984", "George Orwell", "Dystopian", new DateTime(1949, 1, 1), "978-0451524935", 328);
            BookLogic book2 = new BookLogic("To Kill a Mockingbird", "Harper Lee", "Classic", new DateTime(1960, 1, 1), "978-0061120084", 336);
            BookLogic book3 = new BookLogic("The Hobbit", "J.R.R. Tolkien", "Fantasy", new DateTime(1937, 1, 1), "978-0547928227", 310);

            repo.AddUser(user1);
            repo.AddUser(user2);
            repo.AddUser(user3);


            repo.AddBook(book1);
            repo.AddBook(book2);
            repo.AddBook(book3);
            return repo;
        }
    }

    internal class RandomDataGenerator
    {
        private static Random random = new Random();
        public static MockRepo GenerateRandomRepo(int userCount, int bookCount)
        {
            MockRepo repo = new MockRepo();
            for (int i = 0; i < userCount; i++)
            {
                string name = "User" + i;
                string surname = "Surname" + i;
                string email = "Email" + i + "@example.com";
                UserLogic UserLogic = new UserLogic(name, surname, email);
                repo.AddUser(UserLogic);
            }
            for (int i = 0; i < bookCount; i++)
            {
                string title = "Book" + i;
                string author = "Author" + i;
                string genre = "Genre" + random.Next(1, 5);
                int year = random.Next(1900, 2023);
                string isbn = random.Next(100, 999).ToString() + "-" + random.Next(1000000000, int.MaxValue);
                int pages = random.Next(100, 1000);
                BookLogic BookLogic = new BookLogic(title, author, genre, new DateTime(year, 1, 1), isbn, pages);
                repo.AddBook(BookLogic);
            }
            return repo;
        }
    }
}
