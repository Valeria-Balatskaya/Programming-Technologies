using Library.Presentation.Model;
using Library.Presentation.Model.Interfaces;
using Library.Presentation.ViewModel.Interfaces;
using Logic.Logic;
using Logic.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.ViewModel.Objects
{
    internal class RepositoryVM: IRepositoryVM
    {
        private readonly IRepositoryModel _repository;

        public RepositoryVM(string connectionString)
        {
            _repository = ModelDataFactory.CreateRepository(connectionString);
        }

        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages)
        {
            _repository.AddBook(Title, Author, Genre, PublishedDate, ISBN, Pages);
        }

        public void AddBook(IBookVM book)
        {
            IBookModel temp = ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            _repository.AddBook(temp);
        }

        public void AddUser(IUserVM user)
        {
            IUserModel temp = ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.AddUser(temp);
        }

        public void BorrowBook(IBookVM book, IUserVM user)
        {
            IBookModel tempBook = ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, user.Guid);
            IUserModel tempUser = ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);

            _repository.BorrowBook(tempBook, tempUser);
        }

        public bool ContainsBook(IBookVM book)
        {
            return _repository.ContainsBook(ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
        }

        public bool ContainsUser(IUserVM user)
        {
            return _repository.ContainsUser(ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public IEnumerable<IUserVM> GetAllUsers()
        {
            IEnumerable<IUserVM> userVMs = Enumerable.Empty<IUserVM>();
            foreach (var user in _repository.GetAllUsers())
            {
                userVMs = userVMs.Append(VMDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return userVMs;
        }

        public IBookVM GetBook(Guid guid)
        {
            IBookModel book = _repository.GetBook(guid);
            if (book == null) return null;
            IBookVM bookModel = new BookVM(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            return bookModel;
        }

        public IEnumerable<IBookVM> GetCatalog()
        {
            IEnumerable<IBookVM> bookVMs = Enumerable.Empty<IBookVM>();
            foreach (var book in _repository.GetCatalog())
            {
                bookVMs = bookVMs.Append(VMDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            return bookVMs;
        }

        public ILibraryStateVM GetLibraryState()
        {
            IEnumerable<IBookVM> bookVMs = Enumerable.Empty<IBookVM>();
            foreach (var book in _repository.GetCatalog())
            {
                bookVMs = bookVMs.Append(VMDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            IEnumerable<IUserVM> userVMs = Enumerable.Empty<IUserVM>();
            foreach (var user in _repository.GetAllUsers())
            {
                userVMs = userVMs.Append(VMDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return new LibraryStateVM(bookVMs, userVMs);
        }

        public IEnumerable<IBookVM> GetNumberOfBooks(int number, int offset)
        {
            IEnumerable<IBookVM> bookVMs = Enumerable.Empty<IBookVM>();
            foreach (var book in _repository.GetNumberOfBooks(number, offset))
            {
                bookVMs = bookVMs.Append(VMDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            return bookVMs;
        }

        public IEnumerable<IUserVM> GetNumberOfUsers(int number, int offset)
        {
            IEnumerable<IUserVM> userVMs = Enumerable.Empty<IUserVM>();
            foreach (var user in _repository.GetNumberOfUsers(number, offset))
            {
                userVMs = userVMs.Append(VMDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return userVMs;
        }

        public IUserVM GetUser(Guid guid)
        {
            IUserModel user = _repository.GetUser(guid);
            if (user == null) return null;
            IUserVM userModel = new UserVM(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            return userModel;
        }

        public void RemoveBook(IBookVM book)
        {
            IBookModel temp = ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            _repository.RemoveBook(temp);
        }

        public void RemoveUser(IUserVM user)
        {
            IUserModel temp = ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.RemoveUser(temp);
        }

        public void ReturnBook(IBookVM book, IUserVM user)
        {
            IBookModel tempBook = ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, user.Guid);
            IUserModel tempUser = ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);

            _repository.ReturnBook(tempBook, tempUser);
        }

        public void SetFine(IUserVM user, double amount)
        {
            IUserModel tempUser = ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.SetFine(tempUser, amount);
        }

        public void TruncateAllData()
        {
            _repository.TruncateAllData();
        }
    }
}
