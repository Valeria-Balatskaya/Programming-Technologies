using Library.Data;
using Library.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDataTest
{
    internal class PredefinedDataGenerator
    {
        private static string _DBRelativePath = @"..\..\..\Database\LibraryDatabase.mdf";
        private static string _BasePath = AppContext.BaseDirectory;
        private static string _DBPath = Path.GetFullPath(Path.Combine(_BasePath, _DBRelativePath));
        private static string connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True";

        public static User GenerateUser1()
        {
            User user1 = new User("John", "Pork", "jpork@email.com");
            return user1;
        }

        public static User GenerateUser2()
        {
            User user2 = new User("Tim", "Cheese", "tcheese@email.com");
            return user2;
        }

        public static User GenerateUser3()
        {
            User user3 = new User("Tralalero", "Tralala", "ttralala@email.com");
            return user3;
        }

        public static Book GenerateBook1()
        {
            Book book1 = new Book("1984", "George Orwell", "Dystopian", new DateTime(1949, 1, 1), "978-0451524935", 328);
            return book1;
        }

        public static Book GenerateBook2()
        {
            Book book2 = new Book("To Kill a Mockingbird", "Harper Lee", "Classic", new DateTime(1960, 1, 1), "978-0061120084", 336);
            return book2;
        }

        public static Book GenerateBook3()
        {
            Book book3 = new Book("The Hobbit", "J.R.R. Tolkien", "Fantasy", new DateTime(1937, 1, 1), "978-0547928227", 310);
            return book3;
        }

        public static Repository GeneratePredefinedRepo()
        {
            Repository repo = Repository.Create(connectionString);

            User user1 = new User("John", "Pork", "jpork@email.com");
            User user2 = new User("Tim", "Cheese", "tcheese@email.com");
            User user3 = new User("Tralalero", "Tralala", "ttralala@email.com");

            Book book1 = new Book("1984", "George Orwell", "Dystopian", new DateTime(1949, 1, 1), "978-0451524935", 328);
            Book book2 = new Book("To Kill a Mockingbird", "Harper Lee", "Classic", new DateTime(1960, 1, 1), "978-0061120084", 336);
            Book book3 = new Book("The Hobbit", "J.R.R. Tolkien", "Fantasy", new DateTime(1937, 1, 1), "978-0547928227", 310);

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
        private static string _DBRelativePath = @"..\..\..\Database\LibraryDatabase.mdf";
        private static string _BasePath = AppContext.BaseDirectory;
        private static string _DBPath = Path.GetFullPath(Path.Combine(_BasePath, _DBRelativePath));
        private static string connectionString = @$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={_DBPath};Integrated Security=True";


        private static Random random = new Random();
        public static Repository GenerateRandomRepo(int userCount, int bookCount)
        {
            Repository repo = Repository.Create(connectionString);
            for (int i = 0; i < userCount; i++)
            {
                string name = "User" + i;
                string surname = "Surname" + i;
                string email = "user" + i + "@example.com";
                User user = new User(name, surname, email);
                repo.AddUser(user);
            }
            for (int i = 0; i < bookCount; i++)
            {
                string title = "Book" + i;
                string author = "Author" + i;
                string genre = "Genre" + random.Next(1, 5);
                int year = random.Next(1900, 2023);
                string isbn = random.Next(100, 999).ToString() + "-" + random.Next(1000000000, int.MaxValue);
                int pages = random.Next(100, 1000);
                Book book = new Book(title, author, genre, new DateTime(year, 1, 1), isbn, pages);
                repo.AddBook(book);
            }
            return repo;
        }
    }
}
