using Library.Data.SqlServer;
using Library.Data.SqlServer.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Library.Data.Tests
{
    [TestClass]
    public class BookRepositoryTests
    {
        [TestMethod]
        public void GetAllBooks_ReturnsAllBooks()
        {
            var context = new MockLibraryDataContext();
            var book1 = context.CreateBook();
            book1.ISBN = "ISBN-1";
            book1.Title = "Test Book 1";

            var book2 = context.CreateBook();
            book2.ISBN = "ISBN-2";
            book2.Title = "Test Book 2";

            var repository = new BookRepository(context);

            var result = repository.GetAllBooks();

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Any(b => b.ISBN == "ISBN-1"));
            Assert.IsTrue(result.Any(b => b.ISBN == "ISBN-2"));
        }

        [TestMethod]
        public void GetBookByIsbn_ExistingIsbn_ReturnsBook()
        {
            var context = new MockLibraryDataContext();
            var book = context.CreateBook();
            book.ISBN = "ISBN-1";
            book.Title = "Test Book";

            var repository = new BookRepository(context);

            var result = repository.GetBookByIsbn("ISBN-1");

            Assert.IsNotNull(result);
            Assert.AreEqual("Test Book", result.Title);
        }

        [TestMethod]
        public void GetBookByIsbn_NonExistingIsbn_ReturnsNull()
        {
            var context = new MockLibraryDataContext();
            var repository = new BookRepository(context);

            var result = repository.GetBookByIsbn("NonExistingISBN");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddBook_AddsBookToContext()
        {
            var context = new MockLibraryDataContext();
            var repository = new BookRepository(context);
            var book = new Books
            {
                ISBN = "ISBN-1",
                Title = "Test Book",
                Author = "Test Author"
            };

            repository.AddBook(book);

            Assert.AreEqual(1, context.Books.Count());
            var addedBook = context.Books.First();
            Assert.AreEqual("ISBN-1", addedBook.ISBN);
            Assert.AreEqual("Test Book", addedBook.Title);
            Assert.AreEqual("Test Author", addedBook.Author);
        }

        [TestMethod]
        public void UpdateBook_ExistingBook_UpdatesInContext()
        {
            var context = new MockLibraryDataContext();
            var existingBook = context.CreateBook();
            existingBook.ISBN = "ISBN-1";
            existingBook.Title = "Original Title";

            var repository = new BookRepository(context);
            var bookToUpdate = new Books
            {
                ISBN = "ISBN-1",
                Title = "Updated Title",
                Author = "Updated Author"
            };

            repository.UpdateBook(bookToUpdate);

            var updatedBook = context.Books.First();
            Assert.AreEqual("Updated Title", updatedBook.Title);
            Assert.AreEqual("Updated Author", updatedBook.Author);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateBook_NonExistingBook_ThrowsException()
        {
            var context = new MockLibraryDataContext();
            var repository = new BookRepository(context);
            var bookToUpdate = new Books
            {
                ISBN = "NonExistingISBN",
                Title = "Some Title"
            };

            repository.UpdateBook(bookToUpdate);
        }

        [TestMethod]
        public void DeleteBook_ExistingBook_RemovesFromContext()
        {
            var context = new MockLibraryDataContext();
            var book = context.CreateBook();
            book.ISBN = "ISBN-1";

            var repository = new BookRepository(context);

            repository.DeleteBook("ISBN-1");

            Assert.AreEqual(0, context.Books.Count());
        }

        [TestMethod]
        public void DeleteBook_NonExistingBook_DoesNothing()
        {
            var context = new MockLibraryDataContext();
            var book = context.CreateBook();
            book.ISBN = "ISBN-1";

            var repository = new BookRepository(context);

            repository.DeleteBook("NonExistingISBN");

            Assert.AreEqual(1, context.Books.Count());
        }
    }
}