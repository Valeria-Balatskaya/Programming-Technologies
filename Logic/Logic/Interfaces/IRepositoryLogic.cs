using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic.Interfaces
{
    public interface IRepositoryLogic : IDisposable
    {
        public IEnumerable<IUserLogic> GetAllUsers();
        public IEnumerable<IBookLogic> GetCatalog();
        public ILibraryStateLogic GetLibraryState();
        public void AddUser(IUserLogic user);
        public void RemoveUser(IUserLogic user);
        public IUserLogic GetUser(Guid guid);
        public bool ContainsUser(IUserLogic user);
        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages);
        public void AddBook(IBookLogic book);
        public void RemoveBook(IBookLogic book);
        public bool ContainsBook(IBookLogic book);
        public IBookLogic GetBook(Guid guid);
        public void BorrowBook(IBookLogic book, IUserLogic user);
        public void ReturnBook(IBookLogic book, IUserLogic user);
        public IEnumerable<IUserLogic> GetNumberOfUsers(int number, int offset);
        public IEnumerable<IBookLogic> GetNumberOfBooks(int number, int offset);
        public void SetFine(IUserLogic user, double amount);
        public void TruncateAllData();
    }
}
