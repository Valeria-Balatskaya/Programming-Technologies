using Data;
using Library.Data.Interfaces;
using Logic.Logic.Interfaces;
using Logic.Logic.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic
{
    public static class LogicDataFactory
    {
        public static IRepositoryLogic CreateRepository(string? connectionString = null)
        {
            return new RepositoryLogic(connectionString);
        }

        public static IBookLogic CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages)
        {
            return new BookLogic(title, author, genre, publishedDate, isbn, pages);
        }
        public static IBookLogic CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid)
        {
            return new BookLogic(title, author, genre, publishedDate, isbn, pages, guid);
        }
        public static IBookLogic CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid, Guid ownerGuid)
        {
            return new BookLogic(title, author, genre, publishedDate, isbn, pages, guid, ownerGuid);
        }

        public static IUserLogic CreateUser(string name, string surname, string email)
        {
            return new UserLogic(name, surname, email);
        }

        public static IUserLogic CreateUser(string name, string surname, string email, Guid guid, double fineAmount)
        {
            return new UserLogic(name, surname, email, guid, fineAmount);
        }

        public static ILibraryStateLogic CreateLibraryState(IEnumerable<IBookLogic> books, IEnumerable<IUserLogic> users)
        {
            return new LibraryStateLogic(books, users);
        }
    }
}
