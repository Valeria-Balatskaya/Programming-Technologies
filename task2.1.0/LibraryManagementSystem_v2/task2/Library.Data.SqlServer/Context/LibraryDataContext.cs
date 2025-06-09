using System.Linq;

namespace Library.Data.SqlServer.Context
{
    public class LibraryDataContext : ILibraryDataContext
    {
        private readonly LibraryDataModelDataContext _context;

        public LibraryDataContext(string connectionString)
        {
            _context = new LibraryDataModelDataContext(connectionString);
        }

        public IQueryable<Books> Books => _context.Books;
        public IQueryable<Users> Users => _context.Users;
        public IQueryable<BookCopies> BookCopies => _context.BookCopies;
        public IQueryable<LibraryEvents> LibraryEvents => _context.LibraryEvents;

        public void SubmitChanges()
        {
            _context.SubmitChanges();
        }

        public Books CreateBook()
        {
            var book = new Books();
            _context.Books.InsertOnSubmit(book);
            return book;
        }

        public Users CreateUser()
        {
            var user = new Users();
            _context.Users.InsertOnSubmit(user);
            return user;
        }

        public BookCopies CreateBookCopy()
        {
            var bookCopy = new BookCopies();
            _context.BookCopies.InsertOnSubmit(bookCopy);
            return bookCopy;
        }

        public LibraryEvents CreateLibraryEvent()
        {
            var libraryEvent = new LibraryEvents();
            _context.LibraryEvents.InsertOnSubmit(libraryEvent);
            return libraryEvent;
        }

        public void DeleteBook(Books book)
        {
            _context.Books.DeleteOnSubmit(book);
        }

        public void DeleteUser(Users user)
        {
            _context.Users.DeleteOnSubmit(user);
        }

        public void DeleteBookCopy(BookCopies bookCopy)
        {
            _context.BookCopies.DeleteOnSubmit(bookCopy);
        }

        public void DeleteLibraryEvent(LibraryEvents libraryEvent)
        {
            _context.LibraryEvents.DeleteOnSubmit(libraryEvent);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}