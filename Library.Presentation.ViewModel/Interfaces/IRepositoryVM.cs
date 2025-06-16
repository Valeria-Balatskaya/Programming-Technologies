using Library.Presentation.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.ViewModel.Interfaces
{
    public interface IRepositoryVM : IDisposable
    {
        public IEnumerable<IUserVM> GetAllUsers();
        public IEnumerable<IBookVM> GetCatalog();
        public ILibraryStateVM GetLibraryState();
        public void AddUser(IUserVM user);
        public void RemoveUser(IUserVM user);
        public IUserVM GetUser(Guid guid);
        public bool ContainsUser(IUserVM user);
        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages);
        public void AddBook(IBookVM book);
        public void RemoveBook(IBookVM book);
        public bool ContainsBook(IBookVM book);
        public IBookVM GetBook(Guid guid);
        public void BorrowBook(IBookVM book, IUserVM user);
        public void ReturnBook(IBookVM book, IUserVM user);
        public IEnumerable<IUserVM> GetNumberOfUsers(int number, int offset);
        public IEnumerable<IBookVM> GetNumberOfBooks(int number, int offset);
        public void SetFine(IUserVM user, double amount);
        public void TruncateAllData();
    }
}
