using Data;
using Library.Data.Interfaces;
using Logic.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic.Object
{
    internal class RepositoryLogic : IRepositoryLogic
    {
        private readonly IRepository _repository;

        public RepositoryLogic(string connectionString)
        {
            _repository = DataFactory.CreateRepository(connectionString);
        }

        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages)
        {
            _repository.AddBook(Title, Author, Genre, PublishedDate, ISBN, Pages);
        }

        public void AddBook(IBookLogic book)
        {
            IBook temp = DataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            _repository.AddBook(temp);
        }

        public void AddUser(IUserLogic user)
        {
            IUser temp = DataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.AddUser(temp);
        }

        public void BorrowBook(IBookLogic book, IUserLogic user)
        {
            IBook tempBook = DataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, user.Guid);
            IUser tempUser = DataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            
            _repository.BorrowBook(tempBook, tempUser);
        }

        public bool ContainsBook(IBookLogic book)
        {
            return _repository.ContainsBook(DataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
        }

        public bool ContainsUser(IUserLogic user)
        {
            return _repository.ContainsUser(DataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public IEnumerable<IUserLogic> GetAllUsers()
        {
            IEnumerable<IUserLogic> userLogics = Enumerable.Empty<IUserLogic>();
            foreach (var user in _repository.GetAllUsers())
            {
                userLogics = userLogics.Append(LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return userLogics;
        }

        public IBookLogic GetBook(Guid guid)
        {
            IBook book = _repository.GetBook(guid);
            if (book == null) return null;
            IBookLogic bookLogic = new BookLogic(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            return bookLogic;
        }

        public IEnumerable<IBookLogic> GetCatalog()
        {
            IEnumerable<IBookLogic> bookLogics  = Enumerable.Empty<IBookLogic>();
            foreach (var book in _repository.GetCatalog())
            {
                bookLogics = bookLogics.Append(LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            return bookLogics;
        }

        public ILibraryStateLogic GetLibraryState()
        {
            IEnumerable<IBookLogic> bookLogics = Enumerable.Empty<IBookLogic>();
            foreach (var book in _repository.GetCatalog())
            {
                bookLogics = bookLogics.Append(LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            IEnumerable<IUserLogic> userLogics = Enumerable.Empty<IUserLogic>();
            foreach (var user in _repository.GetAllUsers())
            {
                userLogics = userLogics.Append(LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return new LibraryStateLogic(bookLogics, userLogics);
        }

        public IEnumerable<IBookLogic> GetNumberOfBooks(int number, int offset)
        {
            IEnumerable<IBookLogic> bookLogics = Enumerable.Empty<IBookLogic>();
            foreach (var book in _repository.GetNumberOfBooks(number, offset))
            {
                bookLogics = bookLogics.Append(LogicDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId));
            }
            return bookLogics;
        }

        public IEnumerable<IUserLogic> GetNumberOfUsers(int number, int offset)
        {
            IEnumerable<IUserLogic> userLogics = Enumerable.Empty<IUserLogic>();
            foreach (var user in _repository.GetNumberOfUsers(number, offset))
            {
                userLogics = userLogics.Append(LogicDataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount));
            }
            return userLogics;
        }

        public IUserLogic GetUser(Guid guid)
        {
            IUser user = _repository.GetUser(guid);
            if (user == null) return null;
            IUserLogic userLogic = new UserLogic(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            return userLogic;
        }

        public void RemoveBook(IBookLogic book)
        {
            IBook temp = DataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, book.OwnerId);
            _repository.RemoveBook(temp);
        }

        public void RemoveUser(IUserLogic user)
        {
            IUser temp = DataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.RemoveUser(temp);
        }

        public void ReturnBook(IBookLogic book, IUserLogic user)
        {
            IBook tempBook = DataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages, book.Guid, user.Guid);
            IUser tempUser = DataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            
            _repository.ReturnBook(tempBook, tempUser);
        }

        public void SetFine(IUserLogic user, double amount)
        {
            IUser tempUser = DataFactory.CreateUser(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
            _repository.SetFine(tempUser, amount);
        }

        public void TruncateAllData()
        {
            _repository.TruncateAllData();
        }
    }
}
