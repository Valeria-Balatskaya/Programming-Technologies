using Library.Data.SqlServer;
using Library.Data.SqlServer.Context;
using System.Collections.Generic;
using System.Linq;

public class MockLibraryDataContext : ILibraryDataContext
{
    private List<Books> _books = new List<Books>();
    private List<Users> _users = new List<Users>();
    private List<BookCopies> _bookCopies = new List<BookCopies>();
    private List<LibraryEvents> _libraryEvents = new List<LibraryEvents>();
    private int _nextEventId = 1;

    public IQueryable<Books> Books => _books.AsQueryable();
    public IQueryable<Users> Users => _users.AsQueryable();
    public IQueryable<BookCopies> BookCopies => _bookCopies.AsQueryable();
    public IQueryable<LibraryEvents> LibraryEvents => _libraryEvents.AsQueryable();

    public void SubmitChanges()
    {
       
    }

    public Books CreateBook()
    {
        var book = new Books();
        _books.Add(book);
        return book;
    }

    public Users CreateUser()
    {
        var user = new Users();
        _users.Add(user);
        return user;
    }

    public BookCopies CreateBookCopy()
    {
        var bookCopy = new BookCopies();
        _bookCopies.Add(bookCopy);
        return bookCopy;
    }

    public LibraryEvents CreateLibraryEvent()
    {
        var libraryEvent = new LibraryEvents { Id = _nextEventId++ };
        _libraryEvents.Add(libraryEvent);
        return libraryEvent;
    }

    public void DeleteBook(Books book)
    {
        _books.Remove(book);
    }

    public void DeleteUser(Users user)
    {
        _users.Remove(user);
    }

    public void DeleteBookCopy(BookCopies bookCopy)
    {
        _bookCopies.Remove(bookCopy);
    }

    public void DeleteLibraryEvent(LibraryEvents libraryEvent)
    {
        _libraryEvents.Remove(libraryEvent);
    }

    public void Dispose()
    {
        
    }
}