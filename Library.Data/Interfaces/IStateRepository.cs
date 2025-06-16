using Library.Data.Interfaces.Models;

namespace Library.Data.Interfaces
{
    public interface IStateRepository
    {
        IEnumerable<IBookCopy> GetAllBookCopies();
        IBookCopy GetBookCopyById(int id);
        IEnumerable<IBookCopy> GetAvailableBooks();
        IEnumerable<IBookCopy> GetCheckedOutBooks();
        void AddBookCopy(IBookCopy bookCopy);
        void UpdateBookCopy(IBookCopy bookCopy);
        void DeleteBookCopy(int id);
    }
}
