using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Objects;
using Library.Data.Objects.Events;

namespace Library.Data.Interfaces
{
    public interface IRepository : IDisposable
    {
        public IEnumerable<IUser> GetAllUsers();
        public IEnumerable<IBook> GetCatalog();
        public ILibraryState GetLibraryState();
        public void AddUser(IUser user);
        public void RemoveUser(IUser user);
        public IUser GetUser(Guid guid);
        public bool ContainsUser(IUser user);
        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages);
        public void AddBook(IBook book);
        public void RemoveBook(IBook book);
        public bool ContainsBook(IBook book);
        public IBook GetBook(Guid guid);
        public void BorrowBook(IBook book, IUser user);
        public void ReturnBook(IBook book, IUser user);
        public IEnumerable<IUser> GetNumberOfUsers(int number, int offset);
        public IEnumerable<IBook> GetNumberOfBooks(int number, int offset);
        public void SetFine(IUser user, double amount);
        public void TruncateAllData();
    }
}
