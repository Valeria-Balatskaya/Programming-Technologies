using Logic.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.Model.Interfaces
{
    public interface IRepositoryModel : IDisposable
    {
        public IEnumerable<IUserModel> GetAllUsers();
        public IEnumerable<IBookModel> GetCatalog();
        public ILibraryStateModel GetLibraryState();
        public void AddUser(IUserModel user);
        public void RemoveUser(IUserModel user);
        public IUserModel GetUser(Guid guid);
        public bool ContainsUser(IUserModel user);
        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages);
        public void AddBook(IBookModel book);
        public void RemoveBook(IBookModel book);
        public bool ContainsBook(IBookModel book);
        public IBookModel GetBook(Guid guid);
        public void BorrowBook(IBookModel book, IUserModel user);
        public void ReturnBook(IBookModel book, IUserModel user);
        public IEnumerable<IUserModel> GetNumberOfUsers(int number, int offset);
        public IEnumerable<IBookModel> GetNumberOfBooks(int number, int offset);
        public void SetFine(IUserModel user, double amount);
        public void TruncateAllData();
    }
}
