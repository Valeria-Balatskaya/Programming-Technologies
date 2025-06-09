using Library.Data.SqlServer;
using Library.Data.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Logic.Tests
{
    public class MockBookRepository : IBookRepository
    {
        private List<Books> _books = new List<Books>();

        public MockBookRepository()
        {
            _books.Add(new Books
            {
                ISBN = "978-0-061-12241-5",
                Title = "To Kill a Mockingbird",
                Author = "Harper Lee",
                Publisher = "HarperCollins",
                PublicationYear = 1960,
                Genre = "Fiction",
                Description = "Classic novel about racial injustice"
            });

            _books.Add(new Books
            {
                ISBN = "978-0-743-27325-1",
                Title = "The Great Gatsby",
                Author = "F. Scott Fitzgerald",
                Publisher = "Scribner",
                PublicationYear = 1925,
                Genre = "Fiction",
                Description = "Classic novel about the American Dream"
            });
        }

        public void AddBook(Books book)
        {
            _books.Add(book);
        }

        public void DeleteBook(string isbn)
        {
            _books.RemoveAll(b => b.ISBN == isbn);
        }

        public IEnumerable<Books> GetAllBooks()
        {
            return _books;
        }

        public Books GetBookByIsbn(string isbn)
        {
            return _books.FirstOrDefault(b => b.ISBN == isbn);
        }

        public void UpdateBook(Books book)
        {
            var existing = _books.FirstOrDefault(b => b.ISBN == book.ISBN);
            if (existing != null)
            {
                _books.Remove(existing);
                _books.Add(book);
            }
        }
    }
}