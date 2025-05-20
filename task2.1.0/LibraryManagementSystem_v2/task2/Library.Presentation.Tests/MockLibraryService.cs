using Library.Data.SqlServer;
using Library.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Presentation.Tests
{
    public class MockLibraryService : ILibraryService
    {
        private List<Books> _books;
        private List<Users> _users;
        private List<BookCopies> _bookCopies;
        private List<LibraryEvents> _events;

        public MockLibraryService()
        {
            InitializeTestData();
        }

        private void InitializeTestData()
        {
            _books = new List<Books>
            {
                new Books
                {
                    ISBN = "978-0-061-12241-5",
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Publisher = "HarperCollins",
                    PublicationYear = 1960,
                    Genre = "Fiction",
                    Description = "Classic novel about racial injustice"
                },
                new Books
                {
                    ISBN = "978-0-743-27325-1",
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Publisher = "Scribner",
                    PublicationYear = 1925,
                    Genre = "Fiction",
                    Description = "Classic novel about the American Dream"
                }
            };

            _users = new List<Users>
            {
                new Users
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "john@example.com",
                    PhoneNumber = "555-1234",
                    Type = (int)UserType.Patron,
                    RegistrationDate = DateTime.Now.AddDays(-30)
                },
                new Users
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "jane@example.com",
                    PhoneNumber = "555-5678",
                    Type = (int)UserType.Librarian,
                    RegistrationDate = DateTime.Now.AddDays(-60)
                }
            };

            _bookCopies = new List<BookCopies>
            {
                new BookCopies
                {
                    Id = 1,
                    ISBN = "978-0-061-12241-5",
                    Status = (int)BookStatus.Available,
                    AcquisitionDate = DateTime.Now.AddDays(-90),
                    Location = "Shelf A1"
                },
                new BookCopies
                {
                    Id = 2,
                    ISBN = "978-0-743-27325-1",
                    Status = (int)BookStatus.CheckedOut,
                    AcquisitionDate = DateTime.Now.AddDays(-60),
                    Location = "Shelf B2",
                    CurrentBorrowerId = 1,
                    DueDate = DateTime.Now.AddDays(7)
                }
            };

            _events = new List<LibraryEvents>
            {
                new LibraryEvents
                {
                    Id = 1,
                    Type = (int)EventType.BookBorrowed,
                    UserId = 1,
                    ISBN = "978-0-743-27325-1",
                    BookCopyId = 2,
                    Timestamp = DateTime.Now.AddDays(-3),
                    Description = "John Doe borrowed The Great Gatsby"
                }
            };
        }
        public IEnumerable<Books> GetAllBooks()
        {
            Console.WriteLine($"GetAllBooks called, returning {_books.Count} books");
            return _books;
        }
        public Books GetBookByIsbn(string isbn) => _books.FirstOrDefault(b => b.ISBN == isbn);
        public void AddBook(Books book) => _books.Add(book);
        public void UpdateBook(Books book)
        {
            var existing = _books.FirstOrDefault(b => b.ISBN == book.ISBN);
            if (existing != null)
            {
                _books.Remove(existing);
                _books.Add(book);
            }
        }
        public void RemoveBook(string isbn) => _books.RemoveAll(b => b.ISBN == isbn);

        public IEnumerable<Users> GetAllUsers() => _users;
        public Users GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);
        public void RegisterUser(Users user) => _users.Add(user);
        public void UpdateUserInformation(Users user)
        {
            var existing = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existing != null)
            {
                _users.Remove(existing);
                _users.Add(user);
            }
        }
        public void RemoveUser(int id) => _users.RemoveAll(u => u.Id == id);

        public IEnumerable<BookCopies> GetAllBookCopies() => _bookCopies;
        public BookCopies GetBookCopyById(int id) => _bookCopies.FirstOrDefault(bc => bc.Id == id);
        public IEnumerable<BookCopies> GetAvailableBooks() => _bookCopies.Where(bc => bc.Status == (int)BookStatus.Available);
        public IEnumerable<BookCopies> GetCheckedOutBooks() => _bookCopies.Where(bc => bc.Status == (int)BookStatus.CheckedOut);
        public void AddBookCopy(BookCopies bookCopy) => _bookCopies.Add(bookCopy);
        public void UpdateBookCopy(BookCopies bookCopy)
        {
            var existing = _bookCopies.FirstOrDefault(bc => bc.Id == bookCopy.Id);
            if (existing != null)
            {
                _bookCopies.Remove(existing);
                _bookCopies.Add(bookCopy);
            }
        }

        public bool BorrowBook(int userId, int bookCopyId, DateTime dueDate)
        {
            var bookCopy = _bookCopies.FirstOrDefault(bc => bc.Id == bookCopyId);
            if (bookCopy == null || bookCopy.Status != (int)BookStatus.Available) return false;

            bookCopy.Status = (int)BookStatus.CheckedOut;
            bookCopy.CurrentBorrowerId = userId;
            bookCopy.DueDate = dueDate;

            _events.Add(new LibraryEvents
            {
                Id = _events.Count + 1,
                Type = (int)EventType.BookBorrowed,
                UserId = userId,
                ISBN = bookCopy.ISBN,
                BookCopyId = bookCopyId,
                Timestamp = DateTime.Now,
                Description = $"User borrowed book"
            });

            return true;
        }

        public bool ReturnBook(int bookCopyId)
        {
            var bookCopy = _bookCopies.FirstOrDefault(bc => bc.Id == bookCopyId);
            if (bookCopy == null || bookCopy.Status != (int)BookStatus.CheckedOut) return false;

            var userId = bookCopy.CurrentBorrowerId;
            bookCopy.Status = (int)BookStatus.Available;
            bookCopy.CurrentBorrowerId = null;
            bookCopy.DueDate = null;

            _events.Add(new LibraryEvents
            {
                Id = _events.Count + 1,
                Type = (int)EventType.BookReturned,
                UserId = userId,
                ISBN = bookCopy.ISBN,
                BookCopyId = bookCopyId,
                Timestamp = DateTime.Now,
                Description = $"User returned book"
            });

            return true;
        }

        public IEnumerable<BookCopies> GetBorrowedBooksByUser(int userId) =>
            _bookCopies.Where(bc => bc.CurrentBorrowerId == userId);

        public IEnumerable<Users> GetUsersWithOverdueBooks()
        {
            var today = DateTime.Now;
            var overdueBookCopies = _bookCopies
                .Where(bc => bc.Status == (int)BookStatus.CheckedOut &&
                            bc.DueDate.HasValue &&
                            bc.DueDate.Value < today);

            var userIds = overdueBookCopies.Select(bc => bc.CurrentBorrowerId.Value).Distinct();
            return _users.Where(u => userIds.Contains(u.Id));
        }

        public IEnumerable<LibraryEvents> GetAllEvents() => _events;
        public IEnumerable<LibraryEvents> GetEventsByUser(int userId) => _events.Where(e => e.UserId == userId);
        public IEnumerable<LibraryEvents> GetEventsByBook(string isbn) => _events.Where(e => e.ISBN == isbn);
    }
}