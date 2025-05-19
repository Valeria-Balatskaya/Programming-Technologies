using Library.Data.SqlServer;
using System;
using System.Collections.Generic;

namespace Library.Logic
{
    public interface ILibraryService
    {
        IEnumerable<Users> GetAllUsers();
        Users GetUserById(int id);
        void RegisterUser(Users user);
        void UpdateUserInformation(Users user);
        void RemoveUser(int id);

        IEnumerable<Books> GetAllBooks();
        Books GetBookByIsbn(string isbn);
        void AddBook(Books book);
        void UpdateBook(Books book);
        void RemoveBook(string isbn);

        IEnumerable<BookCopies> GetAllBookCopies();
        BookCopies GetBookCopyById(int id);
        IEnumerable<BookCopies> GetAvailableBooks();
        IEnumerable<BookCopies> GetCheckedOutBooks();
        void AddBookCopy(BookCopies bookCopy);
        void UpdateBookCopy(BookCopies bookCopy);

        bool BorrowBook(int userId, int bookCopyId, DateTime dueDate);
        bool ReturnBook(int bookCopyId);
        IEnumerable<BookCopies> GetBorrowedBooksByUser(int userId);
        IEnumerable<Users> GetUsersWithOverdueBooks();

        IEnumerable<LibraryEvents> GetAllEvents();
        IEnumerable<LibraryEvents> GetEventsByUser(int userId);
        IEnumerable<LibraryEvents> GetEventsByBook(string isbn);
    }
}