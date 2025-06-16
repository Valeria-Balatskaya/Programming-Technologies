using Library.Data.Interfaces;
using Library.Presentation.Model.Interfaces;
using Library.Presentation.Model.Obejcts;
using Logic.Logic;
using Logic.Logic.Interfaces;

namespace Library.Presentation.Model
{
    public static class ModelDataFactory
    {
        public static IRepositoryModel CreateRepository(string? connectionString = null)
        {
            return new RepositoryModel(connectionString);
        }

        public static IBookModel CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages)
        {
            return new BookModel(title, author, genre, publishedDate, isbn, pages);
        }
        public static IBookModel CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid)
        {
            return new BookModel(title, author, genre, publishedDate, isbn, pages, guid);
        }
        public static IBookModel CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid, Guid ownerGuid)
        {
            return new BookModel(title, author, genre, publishedDate, isbn, pages, guid, ownerGuid);
        }

        public static IUserModel CreateUser(string name, string surname, string email)
        {
            return new UserModel(name, surname, email);
        }

        public static IUserModel CreateUser(string name, string surname, string email, Guid guid, double fineAmount)
        {
            return new UserModel(name, surname, email, guid, fineAmount);
        }

        public static ILibraryStateModel CreateLibraryState(IEnumerable<IBookModel> books, IEnumerable<IUserModel> users)
        {
            return new LibraryStateModel(books, users);
        }
    }
}
