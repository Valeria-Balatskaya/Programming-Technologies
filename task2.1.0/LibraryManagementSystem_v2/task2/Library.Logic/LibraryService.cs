using Library.Data.SqlServer;
using Library.Data.SqlServer.Context;
using Library.Data.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Logic
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryDataContext _dataRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookCopyRepository _bookCopyRepository;
        private readonly IEventRepository _eventRepository;

        public LibraryService(
            ILibraryDataContext dataRepository,
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IBookCopyRepository bookCopyRepository,
            IEventRepository eventRepository)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _bookCopyRepository = bookCopyRepository ?? throw new ArgumentNullException(nameof(bookCopyRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        public IEnumerable<Books> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public Books GetBookByIsbn(string isbn)
        {
            return _bookRepository.GetBookByIsbn(isbn);
        }

        public void AddBook(Books book)
        {
            _bookRepository.AddBook(book);
        }

        public void UpdateBook(Books book)
        {
            _bookRepository.UpdateBook(book);
        }

        public void RemoveBook(string isbn)
        {
            var book = _bookRepository.GetBookByIsbn(isbn);
            if (book == null)
                throw new ArgumentException($"Book with ISBN {isbn} not found");

            var bookCopies = _bookCopyRepository.GetAllBookCopies()
                .Where(bc => bc.ISBN == isbn)
                .ToList();

            if (bookCopies.Any())
                throw new InvalidOperationException($"Cannot remove book with ISBN {isbn} because there are copies in the library");

            _bookRepository.DeleteBook(isbn);
        }

        public void UpdateBookCopy(BookCopies bookCopy)
        {
            _bookCopyRepository.UpdateBookCopy(bookCopy);
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public Users GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public void RegisterUser(Users user)
        {
            _userRepository.AddUser(user);

            var newEvent = _dataRepository.CreateLibraryEvent();
            newEvent.Type = (int)EventType.UserRegistered;
            newEvent.UserId = user.Id;
            newEvent.Timestamp = DateTime.Now;
            newEvent.Description = $"User {user.Name} registered";

            _eventRepository.AddEvent(newEvent);
        }

        public void UpdateUserInformation(Users user)
        {
            _userRepository.UpdateUser(user);
        }

        public void RemoveUser(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                throw new ArgumentException($"User with ID {id} not found");

            var borrowedBooks = GetBorrowedBooksByUser(id);
            if (borrowedBooks.Any())
                throw new InvalidOperationException($"Cannot remove user with ID {id} because they have borrowed books");

            _userRepository.DeleteUser(id);

            var newEvent = _dataRepository.CreateLibraryEvent();
            newEvent.Type = (int)EventType.UserRemoved;
            newEvent.UserId = id;
            newEvent.Timestamp = DateTime.Now;
            newEvent.Description = $"User {user.Name} removed";

            _eventRepository.AddEvent(newEvent);
        }

        public IEnumerable<BookCopies> GetAllBookCopies()
        {
            return _bookCopyRepository.GetAllBookCopies();
        }

        public BookCopies GetBookCopyById(int id)
        {
            return _bookCopyRepository.GetBookCopyById(id);
        }

        public IEnumerable<BookCopies> GetAvailableBooks()
        {
            return _bookCopyRepository.GetAvailableBooks();
        }

        public IEnumerable<BookCopies> GetCheckedOutBooks()
        {
            return _bookCopyRepository.GetCheckedOutBooks();
        }

        public void AddBookCopy(BookCopies bookCopy)
        {
            var book = _bookRepository.GetBookByIsbn(bookCopy.ISBN);
            if (book == null)
                throw new ArgumentException($"Book with ISBN {bookCopy.ISBN} not found in catalog");

            bookCopy.Status = (int)BookStatus.Available;
            bookCopy.AcquisitionDate = DateTime.Now;
            _bookCopyRepository.AddBookCopy(bookCopy);

            var newEvent = _dataRepository.CreateLibraryEvent();
            newEvent.Type = (int)EventType.BookAdded;
            newEvent.ISBN = bookCopy.ISBN;
            newEvent.BookCopyId = bookCopy.Id;
            newEvent.Timestamp = DateTime.Now;
            newEvent.Description = $"Added new copy of {book.Title}";

            _eventRepository.AddEvent(newEvent);
        }

        public bool BorrowBook(int userId, int bookCopyId, DateTime dueDate)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new ArgumentException($"User with ID {userId} not found");

            var bookCopy = _bookCopyRepository.GetBookCopyById(bookCopyId);
            if (bookCopy == null)
                throw new ArgumentException($"Book copy with ID {bookCopyId} not found");

            if (bookCopy.Status != (int)BookStatus.Available)
                return false;

            var book = _bookRepository.GetBookByIsbn(bookCopy.ISBN);
            if (book == null)
                throw new InvalidOperationException($"Book with ISBN {bookCopy.ISBN} not found in catalog");

            bookCopy.Status = (int)BookStatus.CheckedOut;
            bookCopy.CurrentBorrowerId = userId;
            bookCopy.DueDate = dueDate;

            _bookCopyRepository.UpdateBookCopy(bookCopy);

            var newEvent = _dataRepository.CreateLibraryEvent();
            newEvent.Type = (int)EventType.BookBorrowed;
            newEvent.UserId = userId;
            newEvent.ISBN = bookCopy.ISBN;
            newEvent.BookCopyId = bookCopyId;
            newEvent.Timestamp = DateTime.Now;
            newEvent.Description = $"{user.Name} borrowed {book.Title}";

            _eventRepository.AddEvent(newEvent);

            return true;
        }

        public bool ReturnBook(int bookCopyId)
        {
            var bookCopy = _bookCopyRepository.GetBookCopyById(bookCopyId);
            if (bookCopy == null)
                throw new ArgumentException($"Book copy with ID {bookCopyId} not found");

            if (bookCopy.Status != (int)BookStatus.CheckedOut)
                return false;

            var userId = bookCopy.CurrentBorrowerId;
            var user = userId.HasValue ? _userRepository.GetUserById(userId.Value) : null;
            var book = !string.IsNullOrEmpty(bookCopy.ISBN) ? _bookRepository.GetBookByIsbn(bookCopy.ISBN) : null;

            bookCopy.Status = (int)BookStatus.Available;
            bookCopy.CurrentBorrowerId = null;
            bookCopy.DueDate = null;

            _bookCopyRepository.UpdateBookCopy(bookCopy);

            var newEvent = _dataRepository.CreateLibraryEvent();
            newEvent.Type = (int)EventType.BookReturned;
            newEvent.BookCopyId = bookCopyId;
            newEvent.Timestamp = DateTime.Now;

            if (bookCopy.ISBN != null)
            {
                newEvent.ISBN = bookCopy.ISBN;
                var bookTitle = book?.Title ?? "Unknown book";

                if (userId.HasValue)
                {
                    newEvent.UserId = userId.Value;
                    var userName = user?.Name ?? "Unknown user";
                    newEvent.Description = $"{userName} returned {bookTitle}";
                }
                else
                {
                    newEvent.Description = $"Book {bookTitle} returned";
                }
            }
            else
            {
                newEvent.Description = $"Book copy {bookCopyId} returned";
            }

            _eventRepository.AddEvent(newEvent);
            return true;
        }

        public IEnumerable<BookCopies> GetBorrowedBooksByUser(int userId)
        {
            return _bookCopyRepository.GetCheckedOutBooks()
                .Where(bc => bc.CurrentBorrowerId == userId)
                .ToList();
        }

        public IEnumerable<Users> GetUsersWithOverdueBooks()
        {
            var today = DateTime.Now;
            var overdueBookCopies = _bookCopyRepository.GetCheckedOutBooks()
                .Where(bc => bc.DueDate.HasValue && bc.DueDate.Value < today)
                .ToList();

            if (!overdueBookCopies.Any())
                return new List<Users>();

            var userIds = overdueBookCopies
                .Where(bc => bc.CurrentBorrowerId.HasValue)
                .Select(bc => bc.CurrentBorrowerId.Value)
                .Distinct()
                .ToList();

            return userIds.Select(id => _userRepository.GetUserById(id))
                         .Where(user => user != null)
                         .ToList();
        }

        public IEnumerable<LibraryEvents> GetAllEvents()
        {
            return _eventRepository.GetAllEvents();
        }

        public IEnumerable<LibraryEvents> GetEventsByUser(int userId)
        {
            return _eventRepository.GetEventsByUser(userId);
        }

        public IEnumerable<LibraryEvents> GetEventsByBook(string isbn)
        {
            return _eventRepository.GetEventsByBook(isbn);
        }
    }

    public enum BookStatus
    {
        Available,
        CheckedOut,
        UnderMaintenance,
        Lost
    }

    public enum UserType
    {
        Patron,
        Librarian,
        Administrator
    }

    public enum EventType
    {
        BookAdded,
        BookRemoved,
        BookBorrowed,
        BookReturned,
        BookLost,
        UserRegistered,
        UserRemoved,
        FineAssessed,
        FineCollected
    }
}