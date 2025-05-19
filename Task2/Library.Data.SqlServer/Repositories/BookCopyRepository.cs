using Library.Data.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Data.SqlServer.Context
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly ILibraryDataContext _context;

        public BookCopyRepository(ILibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<BookCopies> GetAllBookCopies()
        {
            try
            {
                return _context.BookCopies.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllBookCopies: {ex.Message}");
                return new List<BookCopies>();
            }
        }

        public BookCopies GetBookCopyById(int id)
        {
            var bookCopy = from bc in _context.BookCopies
                           where bc.Id == id
                           select bc;

            return bookCopy.FirstOrDefault();
        }

        public IEnumerable<BookCopies> GetAvailableBooks()
        {
            return _context.BookCopies.Where(bc => bc.Status == 0).ToList();
        }

        public IEnumerable<BookCopies> GetCheckedOutBooks()
        {
            var checkedOutBooks = from bc in _context.BookCopies
                                  where bc.Status == 1
                                  select bc;

            return checkedOutBooks.ToList();
        }

        public void AddBookCopy(BookCopies bookCopy)
        {
            var newBookCopy = _context.CreateBookCopy();
            newBookCopy.ISBN = bookCopy.ISBN;
            newBookCopy.Status = bookCopy.Status;
            newBookCopy.AcquisitionDate = bookCopy.AcquisitionDate;
            newBookCopy.Location = bookCopy.Location;
            newBookCopy.CurrentBorrowerId = bookCopy.CurrentBorrowerId;
            newBookCopy.DueDate = bookCopy.DueDate;

            _context.SubmitChanges();
        }

        public void UpdateBookCopy(BookCopies bookCopy)
        {
            var existingBookCopy = GetBookCopyById(bookCopy.Id);
            if (existingBookCopy == null)
                throw new ArgumentException($"Book copy with ID {bookCopy.Id} not found.");

            existingBookCopy.ISBN = bookCopy.ISBN;
            existingBookCopy.Status = bookCopy.Status;
            existingBookCopy.AcquisitionDate = bookCopy.AcquisitionDate;
            existingBookCopy.Location = bookCopy.Location;
            existingBookCopy.CurrentBorrowerId = bookCopy.CurrentBorrowerId;
            existingBookCopy.DueDate = bookCopy.DueDate;

            _context.SubmitChanges();
        }

        public void DeleteBookCopy(int id)
        {
            var bookCopyToDelete = _context.BookCopies.FirstOrDefault(bc => bc.Id == id);

            if (bookCopyToDelete != null)
            {
                _context.DeleteBookCopy(bookCopyToDelete);
                _context.SubmitChanges();
            }
        }
    }
}