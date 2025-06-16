using Library.Data;
using Library.Data.Interfaces;
using Library.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DataFactory
    {
        public static IRepository CreateRepository(string? connectionString = null)
        {
            if (connectionString != null)
            {
                return Repository.Create(connectionString);
            }
            return Repository.Create();
        }

        public static IBook CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages)
        {
            return new Book(title, author, genre, publishedDate, isbn, pages);
        }
        public static IBook CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid)
        {
            return new Book(title, author, genre, publishedDate, isbn, pages, guid);
        }
        public static IBook CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid, Guid ownerGuid)
        {
            return new Book(title, author, genre, publishedDate, isbn, pages, guid, ownerGuid);
        }

        public static IUser CreateUser(string name, string surname, string email)
        {
            return new User(name, surname, email);
        }

        public static IUser CreateUser(string name, string surname, string email, Guid guid, double fineAmount)
        {
            return new User(name, surname, email, guid, fineAmount);
        }

        public static ILibraryState CreateLibraryState(IEnumerable<IBook> books, IEnumerable<IUser> users)
        {
            return new LibraryState(books, users);
        }
    }
}
