using Library.Data.SqlServer.Context;
using Library.Data.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Data.SqlServer.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ILibraryDataContext _context;

        public BookRepository(ILibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Books> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Books GetBookByIsbn(string isbn)
        {
            var book = from b in _context.Books
                       where b.ISBN == isbn
                       select b;

            return book.FirstOrDefault();
        }

        public void AddBook(Books book)
        {
            var newBook = _context.CreateBook();
            newBook.ISBN = book.ISBN;
            newBook.Title = book.Title;
            newBook.Author = book.Author;
            newBook.Publisher = book.Publisher;
            newBook.PublicationYear = book.PublicationYear;
            newBook.Genre = book.Genre;
            newBook.Description = book.Description;

            _context.SubmitChanges();
        }

        public void UpdateBook(Books book)
        {
            var existingBook = GetBookByIsbn(book.ISBN);
            if (existingBook == null)
                throw new ArgumentException($"Book with ISBN {book.ISBN} not found.");

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publisher = book.Publisher;
            existingBook.PublicationYear = book.PublicationYear;
            existingBook.Genre = book.Genre;
            existingBook.Description = book.Description;

            _context.SubmitChanges();
        }

        public void DeleteBook(string isbn)
        {
            var bookToDelete = from b in _context.Books
                               where b.ISBN == isbn
                               select b;

            if (bookToDelete.Any())
            {
                _context.DeleteBook(bookToDelete.First());
                _context.SubmitChanges();
            }
        }
    }
}