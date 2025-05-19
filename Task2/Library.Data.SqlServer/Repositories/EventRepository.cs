using Library.Data.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Data.SqlServer.Context
{
    public class EventRepository : IEventRepository
    {
        private readonly ILibraryDataContext _context;

        public EventRepository(ILibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<LibraryEvents> GetAllEvents()
        {
            return _context.LibraryEvents.ToList();
        }

        public LibraryEvents GetEventById(int id)
        {
            var libraryEvent = from e in _context.LibraryEvents
                               where e.Id == id
                               select e;

            return libraryEvent.FirstOrDefault();
        }

        public IEnumerable<LibraryEvents> GetEventsByUser(int userId)
        {
            return _context.LibraryEvents.Where(e => e.UserId == userId).ToList();
        }

        public IEnumerable<LibraryEvents> GetEventsByBook(string isbn)
        {
            var events = from e in _context.LibraryEvents
                         where e.ISBN == isbn
                         select e;

            return events.ToList();
        }

        public void AddEvent(LibraryEvents libraryEvent)
        {
            var newEvent = _context.CreateLibraryEvent();
            newEvent.Type = libraryEvent.Type;
            newEvent.UserId = libraryEvent.UserId;
            newEvent.ISBN = libraryEvent.ISBN;
            newEvent.BookCopyId = libraryEvent.BookCopyId;
            newEvent.Timestamp = libraryEvent.Timestamp;
            newEvent.Description = libraryEvent.Description;

            _context.SubmitChanges();
        }
    }
}