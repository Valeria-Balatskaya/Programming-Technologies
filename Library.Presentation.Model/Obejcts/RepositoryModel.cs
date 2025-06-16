using Data;
using Library.Data.Interfaces;
using Library.Presentation.Model.Interfaces;
using Logic.Logic;
using Logic.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.Model.Obejcts
{
    internal class RepositoryModel: IRepositoryModel
    {
        private readonly IRepositoryLogic _repository;

        public RepositoryModel(string connectionString)
        {
            _repository = LogicDataFactory.CreateRepository(connectionString);
        }

        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages)
        {
            _repository.AddBook(Title, Author, Genre, PublishedDate, ISBN, Pages);
        }

        public void AddBook(IBookModel book)
        {
            IBookLogic temp = LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            _repository.AddBook(temp);
        }

        public void AddUser(IUserModel user)
        {
            IUserLogic temp = LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.AddUser(temp);
        }

        public void BorrowBook(IBookModel book, IUserModel user)
        {
            IBookLogic tempBook = LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, user.Guid);
            IUserLogic tempUser = LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);

            _repository.BorrowBook(tempBook, tempUser);
        }

        public bool ContainsBook(IBookModel book)
        {
            return _repository.ContainsBook(LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
        }

        public bool ContainsUser(IUserModel user)
        {
            return _repository.ContainsUser(LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public IEnumerable<IUserModel> GetAllUsers()
        {
            IEnumerable<IUserModel> userModels = Enumerable.Empty<IUserModel>();
            foreach (var user in _repository.GetAllUsers())
            {
                userModels = userModels.Append(ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return userModels;
        }

        public IBookModel GetBook(Guid guid)
        {
            IBookLogic book = _repository.GetBook(guid);
            if (book == null) return null;
            IBookModel bookModel = new BookModel(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            return bookModel;
        }

        public IEnumerable<IBookModel> GetCatalog()
        {
            IEnumerable<IBookModel> bookModels = Enumerable.Empty<IBookModel>();
            foreach (var book in _repository.GetCatalog())
            {
                bookModels = bookModels.Append(ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            return bookModels;
        }

        public ILibraryStateModel GetLibraryState()
        {
            IEnumerable<IBookModel> bookModels = Enumerable.Empty<IBookModel>();
            foreach (var book in _repository.GetCatalog())
            {
                bookModels = bookModels.Append(ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            IEnumerable<IUserModel> userModels = Enumerable.Empty<IUserModel>();
            foreach (var user in _repository.GetAllUsers())
            {
                userModels = userModels.Append(ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return new LibraryStateModel(bookModels, userModels);
        }

        public IEnumerable<IBookModel> GetNumberOfBooks(int number, int offset)
        {
            IEnumerable<IBookModel> bookModels = Enumerable.Empty<IBookModel>();
            foreach (var book in _repository.GetNumberOfBooks(number, offset))
            {
                bookModels = bookModels.Append(ModelDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            return bookModels;
        }

        public IEnumerable<IUserModel> GetNumberOfUsers(int number, int offset)
        {
            IEnumerable<IUserModel> userModels = Enumerable.Empty<IUserModel>();
            foreach (var user in _repository.GetNumberOfUsers(number, offset))
            {
                userModels = userModels.Append(ModelDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return userModels;
        }

        public IUserModel GetUser(Guid guid)
        {
            IUserLogic user = _repository.GetUser(guid);
            if (user == null) return null;
            IUserModel userModel = new UserModel(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            return userModel;
        }

        public void RemoveBook(IBookModel book)
        {
            IBookLogic temp = LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            _repository.RemoveBook(temp);
        }

        public void RemoveUser(IUserModel user)
        {
            IUserLogic temp = LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.RemoveUser(temp);
        }

        public void ReturnBook(IBookModel book, IUserModel user)
        {
            IBookLogic tempBook = LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, user.Guid);
            IUserLogic tempUser = LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);

            _repository.ReturnBook(tempBook, tempUser);
        }

        public void SetFine(IUserModel user, double amount)
        {
            IUserLogic tempUser = LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.SetFine(tempUser, amount);
        }

        public void TruncateAllData()
        {
            _repository.TruncateAllData();
        }
    }
}
