using System;
using System.Collections.Generic;

namespace Library.Data.SqlServer.Interfaces
{
    public interface IEventRepository
    {
        IEnumerable<LibraryEvents> GetAllEvents();
        LibraryEvents GetEventById(int id);
        IEnumerable<LibraryEvents> GetEventsByUser(int userId);
        IEnumerable<LibraryEvents> GetEventsByBook(string isbn);
        void AddEvent(LibraryEvents libraryEvent);
    }
}
