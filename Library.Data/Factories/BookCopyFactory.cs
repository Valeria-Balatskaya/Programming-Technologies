using Library.Data.Interfaces.Models;

namespace Library.Data.Factories
{
    public static class BookCopyFactory
    {
        public static IBookCopy CreateBookCopy(int id, string isbn, BookStatus status, DateTime acquisitionDate, string location, int? currentBorrowerId = null, DateTime? dueDate = null)
        {
            return new BookCopy
            {
                Id = id,
                ISBN = isbn,
                Status = status,
                AcquisitionDate = acquisitionDate,
                Location = location,
                CurrentBorrowerId = currentBorrowerId,
                DueDate = dueDate
            };
        }
    }
}