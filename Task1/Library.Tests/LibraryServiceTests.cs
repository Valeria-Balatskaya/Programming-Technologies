using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Library.Data.Entities;
using Library.Data.Implementations;
using Library.Logic;
using Library.Data.Interfaces;

namespace Library.Tests
{
    public class LibraryServiceTests
    {
        private readonly IDataContext _context = new DataContext();
        private readonly IDataRepository _repository;
        private readonly LibraryService _service;

        public LibraryServiceTests()
        {
            _repository = new DataRepository(_context);
            _service = new LibraryService(_repository, _context);
        }

        [Fact]
        public void BorrowBook_UpdatesStateAndLogsEvent()
        {
            var book = TestData.CSharpBook;

            // Arrange
            _context.Catalog.Add(101, book);
            _context.State.AvailableBooks.Add(book);

            // Act
            _service.BorrowBook(TestData.Alice, book);

            // Assert
            Assert.Contains(book, _context.State.BorrowedBooks);
            Assert.Single(_context.Events, e =>
                e.Type == EventType.Borrow &&
                e.User.Name == TestData.Alice.Name &&
                e.Item.Id == book.Id);
        }

        [Fact] 
        public void ReturnBook_UpdatesStateAndLogsEvent()
        {
            var book = TestData.CSharpBook;

            // Arrange
            _context.Catalog.Add(101, book);
            _context.State.BorrowedBooks.Add(book);

            // Act
            _service.ReturnBook(TestData.Alice, book);

            // Assert
            Assert.Contains(book, _context.State.AvailableBooks);
            Assert.Single(_context.Events, e =>
                e.Type == EventType.Return &&
                e.User.Name == TestData.Alice.Name &&
                e.Item.Id == book.Id);
        }
    }
}