using Library.Data.SqlServer;
using Library.Data.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Library.Data.SqlServer.Context;
using Library.Data.SqlServer.Interfaces;
using Library.Data.SqlServer.Repositories;

namespace Library.Logic.Tests
{
    [TestClass]
    public class LibraryServiceTests
    {
        private ILibraryDataContext _mockContext;
        private IBookRepository _mockBookRepository;
        private IUserRepository _mockUserRepository;
        private IBookCopyRepository _mockBookCopyRepository;
        private IEventRepository _mockEventRepository;
        private ILibraryService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockContext = DataTestHelpers.CreateTestDataContext();

            _mockBookRepository = new BookRepository(_mockContext);
            _mockUserRepository = new UserRepository(_mockContext);
            _mockBookCopyRepository = new BookCopyRepository(_mockContext);
            _mockEventRepository = new EventRepository(_mockContext);

            _service = new LibraryService(
                _mockContext,
                _mockBookRepository,
                _mockUserRepository,
                _mockBookCopyRepository,
                _mockEventRepository);
        }

        [TestMethod]
        public void GetAllBooks_ReturnsAllBooks()
        {
            var contextBooks = _mockContext.Books.ToList();
            Console.WriteLine($"Books in context: {contextBooks.Count}");
            foreach (var book in contextBooks)
            {
                Console.WriteLine($"Book: {book.ISBN} - {book.Title}");
            }

            var books = _service.GetAllBooks().ToList();

            Assert.AreEqual(2, books.Count);
            Assert.IsTrue(books.Any(b => b.ISBN == "978-0-061-12241-5"));
            Assert.IsTrue(books.Any(b => b.ISBN == "978-0-743-27325-1"));
        }

        [TestMethod]
        public void BorrowBook_AvailableBook_Success()
        {
            var context = new MockLibraryDataContext();

            var user = context.CreateUser();
            user.Id = 1;
            user.Name = "Test User";
            user.Email = "test@example.com";
            user.Type = (int)UserType.Patron;
            user.RegistrationDate = DateTime.Now.AddDays(-30);

            var book = context.CreateBook();
            book.ISBN = "TEST-ISBN";
            book.Title = "Test Book";
            book.Author = "Test Author";

            var bookCopy = context.CreateBookCopy();
            bookCopy.Id = 1;
            bookCopy.ISBN = "TEST-ISBN";
            bookCopy.Status = (int)BookStatus.Available;
            bookCopy.AcquisitionDate = DateTime.Now.AddDays(-60);
            bookCopy.Location = "Test Location";

            var bookRepo = new BookRepository(context);
            var userRepo = new UserRepository(context);
            var bookCopyRepo = new BookCopyRepository(context);
            var eventRepo = new EventRepository(context);

            var service = new LibraryService(context, bookRepo, userRepo, bookCopyRepo, eventRepo);
            var dueDate = DateTime.Now.AddDays(14);

            var result = service.BorrowBook(1, 1, dueDate);

            var directBookCopy = context.BookCopies.FirstOrDefault(bc => bc.Id == 1);
            Console.WriteLine($"Book copy status after borrow: {directBookCopy?.Status}");
            Console.WriteLine($"Book copy borrower after borrow: {directBookCopy?.CurrentBorrowerId}");

            Assert.IsTrue(result);
            Assert.AreEqual((int)BookStatus.CheckedOut, directBookCopy.Status);
            Assert.AreEqual(1, directBookCopy.CurrentBorrowerId);
            Assert.AreEqual(dueDate.Date, directBookCopy.DueDate.Value.Date);
        }

        [TestMethod]
        public void BorrowBook_CheckedOutBook_Failure()
        {
            var context = DataTestHelpers.CreateTestDataContext();
            var bookRepo = new BookRepository(context);
            var userRepo = new UserRepository(context);
            var bookCopyRepo = new BookCopyRepository(context);
            var eventRepo = new EventRepository(context);

            var service = new LibraryService(context, bookRepo, userRepo, bookCopyRepo, eventRepo);

            service.BorrowBook(1, 1, DateTime.Now.AddDays(14));

            var result = service.BorrowBook(2, 1, DateTime.Now.AddDays(7));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetBookByIsbn_ExistingIsbn_ReturnsBook()
        {
            var context = DataTestHelpers.CreateTestDataContext();

            var booksInContext = context.Books.ToList();
            Console.WriteLine($"Books in context: {booksInContext.Count}");
            foreach (var bookItem in booksInContext)
            {
                Console.WriteLine($"Book: {bookItem.ISBN} - {bookItem.Title}");
            }

            var bookRepo = new BookRepository(context);
            var userRepo = new UserRepository(context);
            var bookCopyRepo = new BookCopyRepository(context);
            var eventRepo = new EventRepository(context);

            var service = new LibraryService(context, bookRepo, userRepo, bookCopyRepo, eventRepo);

            var book = service.GetBookByIsbn("978-0-061-12241-5");

            Assert.IsNotNull(book);
            Assert.AreEqual("To Kill a Mockingbird", book.Title);
        }

        [TestMethod]
        public void GetUsersWithOverdueBooks_ReturnsCorrectUsers()
        {
            var context = DataTestHelpers.CreateTestDataContext();

            var users = context.Users.ToList();
            Console.WriteLine($"Users in context: {users.Count}");
            foreach (var user in users)
            {
                Console.WriteLine($"User: Id={user.Id}, Name={user.Name}");
            }

            var bookCopies = context.BookCopies.ToList();
            Console.WriteLine($"Book copies in context: {bookCopies.Count}");
            foreach (var bc in bookCopies)
            {
                Console.WriteLine($"BookCopy: Id={bc.Id}, Status={bc.Status}, BorrowerId={bc.CurrentBorrowerId}");
            }

            var bookRepo = new BookRepository(context);
            var userRepo = new UserRepository(context);
            var bookCopyRepo = new BookCopyRepository(context);
            var eventRepo = new EventRepository(context);

            var service = new LibraryService(context, bookRepo, userRepo, bookCopyRepo, eventRepo);

            try
            {
                var bookCopy = context.BookCopies.FirstOrDefault(bc => bc.Id == 1);
                if (bookCopy != null)
                {
                    Console.WriteLine($"Found book copy with Id=1: {bookCopy.ISBN}");
                    bookCopy.Status = (int)BookStatus.CheckedOut;
                    bookCopy.CurrentBorrowerId = 1;
                    bookCopy.DueDate = DateTime.Now.AddDays(-1);
                    Console.WriteLine("Updated book copy to be overdue");
                }
                else
                {
                    Console.WriteLine("Could not find book copy with Id=1");
                    bookCopy = context.CreateBookCopy();
                    bookCopy.Id = 1;
                    bookCopy.ISBN = "978-0-061-12241-5";
                    bookCopy.Status = (int)BookStatus.CheckedOut;
                    bookCopy.CurrentBorrowerId = 1;
                    bookCopy.DueDate = DateTime.Now.AddDays(-1);
                    Console.WriteLine("Created new overdue book copy");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during test setup: {ex.Message}");
            }

            try
            {
                var usersWithOverdueBooks = service.GetUsersWithOverdueBooks().ToList();

                Assert.AreEqual(1, usersWithOverdueBooks.Count);
                Assert.AreEqual(1, usersWithOverdueBooks[0].Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during test: {ex.Message}");
                throw;
            }
        }

        [TestMethod]
        public void ReturnBook_CheckedOutBook_Success()
        {
            var context = DataTestHelpers.CreateTestDataContext();
            var bookRepo = new BookRepository(context);
            var userRepo = new UserRepository(context);
            var bookCopyRepo = new BookCopyRepository(context);
            var eventRepo = new EventRepository(context);

            var service = new LibraryService(context, bookRepo, userRepo, bookCopyRepo, eventRepo);

            service.BorrowBook(1, 1, DateTime.Now.AddDays(14));

            var result = service.ReturnBook(1);

            Assert.IsTrue(result);
            var bookCopy = service.GetBookCopyById(1);
            Assert.AreEqual((int)BookStatus.Available, bookCopy.Status);
            Assert.IsNull(bookCopy.CurrentBorrowerId);
            Assert.IsNull(bookCopy.DueDate);
        }

        [TestMethod]
        public void ReturnBook_AvailableBook_Failure()
        {
            var context = new MockLibraryDataContext();

            var book = context.CreateBook();
            book.ISBN = "TEST-ISBN";
            book.Title = "Test Book";

            var bookCopy = context.CreateBookCopy();
            bookCopy.Id = 1;
            bookCopy.ISBN = "TEST-ISBN";
            bookCopy.Status = (int)BookStatus.Available;
            bookCopy.AcquisitionDate = DateTime.Now.AddDays(-30);

            var bookRepo = new BookRepository(context);
            var userRepo = new UserRepository(context);
            var bookCopyRepo = new BookCopyRepository(context);
            var eventRepo = new EventRepository(context);

            var service = new LibraryService(context, bookRepo, userRepo, bookCopyRepo, eventRepo);

            Console.WriteLine($"Book copy status before return: {bookCopy.Status}");

            var result = service.ReturnBook(1);

            Assert.IsFalse(result);
        }
    }
}