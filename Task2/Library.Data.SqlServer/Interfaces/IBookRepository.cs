using System;
using System.Collections.Generic;

namespace Library.Data.SqlServer.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Books> GetAllBooks();
        Books GetBookByIsbn(string isbn);
        void AddBook(Books book);
        void UpdateBook(Books book);
        void DeleteBook(string isbn);
    }
}