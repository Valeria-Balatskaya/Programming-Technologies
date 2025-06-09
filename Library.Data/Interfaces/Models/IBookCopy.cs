namespace Library.Data.Interfaces.Models
{
    public interface IBookCopy
    {
        int Id { get; }
        string ISBN { get; }
        BookStatus Status { get; }
        DateTime AcquisitionDate { get; }
        string Location { get; }
        int? CurrentBorrowerId { get; }
        DateTime? DueDate { get; }
    }
}
