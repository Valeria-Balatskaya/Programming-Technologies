using System;
using System.Collections.Generic;

namespace Library.Data.SqlServer.Interfaces
{
    public interface IBookCopyRepository
    {
        IEnumerable<BookCopies> GetAllBookCopies();
        BookCopies GetBookCopyById(int id);
        IEnumerable<BookCopies> GetAvailableBooks();
        IEnumerable<BookCopies> GetCheckedOutBooks();
        void AddBookCopy(BookCopies bookCopy);
        void UpdateBookCopy(BookCopies bookCopy);
        void DeleteBookCopy(int id);
    }
}
