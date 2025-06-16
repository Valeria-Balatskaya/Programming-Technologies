using Library.Data.Interfaces.Models;

namespace Library.Data.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<ILibraryEvent> GetAllEvents();
        ILibraryEvent GetEventById(int id);
        IEnumerable<ILibraryEvent> GetEventsByUser(int userId);
        IEnumerable<ILibraryEvent> GetEventsByBook(string isbn);
        void AddEvent(ILibraryEvent libraryEvent);
    }
}
