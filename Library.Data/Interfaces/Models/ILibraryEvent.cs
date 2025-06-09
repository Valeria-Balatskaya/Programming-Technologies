namespace Library.Data.Interfaces.Models
{
    public interface ILibraryEvent
    {
        int Id { get; }
        EventType Type { get; }
        int? UserId { get; }
        string ISBN { get; }
        int? BookCopyId { get; }
        DateTime Timestamp { get; }
        string Description { get; }
    }
}
