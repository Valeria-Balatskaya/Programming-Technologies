using Library.Presentation.ViewModel.Interfaces;
using Library.Presentation.ViewModel.Objects;

namespace Library.Presentation.ViewModel
{
    public static class VMDataFactory
    {
        public static IRepositoryVM CreateRepository(string? connectionString = null)
        {
            return new RepositoryVM(connectionString);
        }

        public static IBookVM CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages)
        {
            return new BookVM(title, author, genre, publishedDate, isbn, pages);
        }
        public static IBookVM CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid)
        {
            return new BookVM(title, author, genre, publishedDate, isbn, pages, guid);
        }
        public static IBookVM CreateBook(string title, string author, string genre, DateTime publishedDate, string isbn, int pages, Guid guid, Guid ownerGuid)
        {
            return new BookVM(title, author, genre, publishedDate, isbn, pages, guid, ownerGuid);
        }

        public static IUserVM CreateUser(string name, string surname, string email)
        {
            return new UserVM(name, surname, email);
        }

        public static IUserVM CreateUser(string name, string surname, string email, Guid guid, double fineAmount)
        {
            return new UserVM(name, surname, email, guid, fineAmount);
        }

        public static ILibraryStateVM CreateLibraryState(IEnumerable<IBookVM> books, IEnumerable<IUserVM> users)
        {
            return new LibraryStateVM(books, users);
        }
    }
}
