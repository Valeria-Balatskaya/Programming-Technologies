using System;
using System.Linq;

namespace Library.Data.SqlServer.Context
{
    public interface ILibraryDataContext : IDisposable
    {
        IQueryable<Books> Books { get; }
        IQueryable<Users> Users { get; }
        IQueryable<BookCopies> BookCopies { get; }
        IQueryable<LibraryEvents> LibraryEvents { get; }

        void SubmitChanges();
        Books CreateBook();
        Users CreateUser();
        BookCopies CreateBookCopy();
        LibraryEvents CreateLibraryEvent();

        void DeleteBook(Books book);
        void DeleteUser(Users user);
        void DeleteBookCopy(BookCopies bookCopy);
        void DeleteLibraryEvent(LibraryEvents libraryEvent);
    }
}