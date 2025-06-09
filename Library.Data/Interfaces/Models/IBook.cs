namespace Library.Data.Interfaces.Models
{
    public interface IBook
    {
        string ISBN { get; }
        string Title { get; }
        string Author { get; }
        string Publisher { get; }
        int PublicationYear { get; }
        string Genre { get; }
        string Description { get; }
    }
}
