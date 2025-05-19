using Library.Data.SqlServer;
using Library.Logic;
using System;

namespace Library.Data.Tests
{
    public static class DataTestHelpers
    {
        public static MockLibraryDataContext CreateTestDataContext()
        {
            var context = new MockLibraryDataContext();

            var book1 = context.CreateBook();
            book1.ISBN = "978-0-061-12241-5";
            book1.Title = "To Kill a Mockingbird";
            book1.Author = "Harper Lee";
            book1.Publisher = "HarperCollins";
            book1.PublicationYear = 1960;
            book1.Genre = "Fiction";
            book1.Description = "Classic novel about racial injustice";

            var book2 = context.CreateBook();
            book2.ISBN = "978-0-743-27325-1";
            book2.Title = "The Great Gatsby";
            book2.Author = "F. Scott Fitzgerald";
            book2.Publisher = "Scribner";
            book2.PublicationYear = 1925;
            book2.Genre = "Fiction";
            book2.Description = "Classic novel about the American Dream";

            var user1 = context.CreateUser();
            user1.Id = 1;
            user1.Name = "John Doe";
            user1.Email = "john@example.com";
            user1.Type = (int)UserType.Patron;
            user1.RegistrationDate = DateTime.Now.AddDays(-30);

            var user2 = context.CreateUser();
            user2.Id = 2;
            user2.Name = "Jane Smith";
            user2.Email = "jane@example.com";
            user2.Type = (int)UserType.Librarian;
            user2.RegistrationDate = DateTime.Now.AddDays(-60);

            var bookCopy1 = context.CreateBookCopy();
            bookCopy1.Id = 1;
            bookCopy1.ISBN = "978-0-061-12241-5";
            bookCopy1.Status = (int)BookStatus.Available;
            bookCopy1.AcquisitionDate = DateTime.Now.AddDays(-90);
            bookCopy1.Location = "Shelf A1";

            var bookCopy2 = context.CreateBookCopy();
            bookCopy2.Id = 2;
            bookCopy2.ISBN = "978-0-743-27325-1";
            bookCopy2.Status = (int)BookStatus.Available;
            bookCopy2.AcquisitionDate = DateTime.Now.AddDays(-60);
            bookCopy2.Location = "Shelf B2";

            var event1 = context.CreateLibraryEvent();
            event1.Id = 1;
            event1.Type = (int)EventType.BookAdded;
            event1.ISBN = "978-0-061-12241-5";
            event1.BookCopyId = 1;
            event1.Timestamp = DateTime.Now.AddDays(-90);
            event1.Description = "Added new copy of To Kill a Mockingbird";

            return context;
        }
    }
}