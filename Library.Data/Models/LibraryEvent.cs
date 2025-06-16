using Library.Data.Interfaces.Models;

namespace Library.Data.Models
{
    internal class LibraryEvent : ILibraryEvent
    {
        public int Id { get; set; }
        public EventType Type { get; set; }
        public int? UserId { get; set; }
        public string ISBN { get; set; }
        public int? BookCopyId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
    }

    public enum EventType
    {
        BookAdded,
        BookRemoved,
        BookBorrowed,
        BookReturned,
        BookLost,
        UserRegistered,
        UserRemoved,
        FineAssessed,
        FineCollected
    }
}
