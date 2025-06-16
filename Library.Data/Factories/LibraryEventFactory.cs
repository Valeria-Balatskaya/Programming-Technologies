using Library.Data.Interfaces.Models;

namespace Library.Data.Factories
{
    public static class LibraryEventFactory
    {
        public static ILibraryEvent CreateEvent(int id, EventType type, DateTime timestamp, string description, int? userId = null, string isbn = null, int? bookCopyId = null)
        {
            return new LibraryEvent
            {
                Id = id,
                Type = type,
                UserId = userId,
                ISBN = isbn,
                BookCopyId = bookCopyId,
                Timestamp = timestamp,
                Description = description
            };
        }
    }
}
