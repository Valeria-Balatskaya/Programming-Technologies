using Library.Data.Interfaces.Models;

namespace Library.Data.Interfaces
{
    public interface ICatalogRepository
    {
        IEnumerable<IBook> GetAllBooks();
        IBook GetBookById(string isbn);
        void AddBook(IBook book);
        void UpdateBook(IBook book);
        void DeleteBook(string isbn);
    }
}
