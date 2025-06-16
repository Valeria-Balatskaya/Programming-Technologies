using Data;
using Library.Data.Interfaces;
using Library.Data.Objects;
using Library.Data.Objects.Events;
using System.Drawing;

namespace Library.Data
{
    internal class Repository : LibraryDataContext, IRepository
    {
        protected LibraryDataContext context;

        protected Repository(string connectionString) : base(connectionString)
        {
            context = new LibraryDataContext(connectionString);
        }

        public static Repository Create(string? connectionString = null)
        {
            if (connectionString != null)
            {
                return new Repository(connectionString);
            }
            throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null. Please provide a valid connection string.");
        }

        private IBook DbToObject(book book)
        {
            if (book == null) return null;
            return new Book(book.Title, book.Author, book.Genre, book.PublishedDate, book.ISBN, book.Pages, book.GUID, book.OwnerId);
        }

        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages)
        {
            book Book = new book
            {
                Title = Title,
                Author = Author,
                Genre = Genre,
                PublishedDate = PublishedDate,
                ISBN = ISBN,
                Pages = Pages
            };
            @event bookEvent = new @event
            {
                EventID = Guid.NewGuid(),
                EventName = "AddBook",
                EventDate = DateTime.Now,
                BookGuid = Book.GUID
            };
            context.books.InsertOnSubmit(Book);
            context.events.InsertOnSubmit(bookEvent);
            context.SubmitChanges();
        }

        public void AddBook(IBook book)
        {
            book Book = new book
            {
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                PublishedDate = book.Year,
                GUID = book.Guid,
                ISBN = book.Isbn,
                Pages = book.Pages,
                IsAvailable = true
            };
            @event bookEvent = new @event
            {
                EventID = Guid.NewGuid(),
                EventName = "AddBook",
                EventDate = DateTime.Now,
                BookGuid = Book.GUID
            };
            context.books.InsertOnSubmit(Book);
            context.events.InsertOnSubmit(bookEvent);
            context.SubmitChanges();
        }

        public void RemoveBook(IBook book)
        {
            var dbBook = context.books.FirstOrDefault(u => u.GUID == book.Guid);
            if (dbBook != null)
            {
                context.books.DeleteOnSubmit(dbBook);
                @event bookEvent = new @event
                {
                    EventID = Guid.NewGuid(),
                    EventName = "RemoveBook",
                    EventDate = DateTime.Now,
                    BookGuid = book.Guid
                };
                context.events.InsertOnSubmit(bookEvent);
                context.SubmitChanges();
            }
        }

        private IUser DbToObject(user user)
        {
            if (user == null) return null;
            return new User(user.Name, user.Surname, user.Email, user.Guid, user.FineAmount);
        }

        public void AddUser(IUser user)
        {
            user User = new user
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Guid = user.Guid,
                FineAmount = user.FineAmount
            };
            @event userEvent = new @event
            {
                EventID = Guid.NewGuid(),
                EventName = "AddUser",
                EventDate = DateTime.Now,
                UserGuid = User.Guid
            };
            context.users.InsertOnSubmit(User);
            context.events.InsertOnSubmit(userEvent);
            context.SubmitChanges();
        }

        public void RemoveUser(IUser user)
        {
            var dbUser = context.users.FirstOrDefault(u => u.Guid == user.Guid);
            if (dbUser != null)
            {
                context.users.DeleteOnSubmit(dbUser);
                @event userEvent = new @event
                {
                    EventID = Guid.NewGuid(),
                    EventName = "RemoveUser",
                    EventDate = DateTime.Now,
                    UserGuid = user.Guid
                };
                context.events.InsertOnSubmit(userEvent);
                context.SubmitChanges();
            }
        }

        public void SetFine(IUser user, double amount)
        {
            var dbUser = context.users.FirstOrDefault(u => u.Guid == user.Guid);
            if (dbUser != null)
            {
                dbUser.FineAmount = amount;
                @event userEvent = new @event
                {
                    EventID = Guid.NewGuid(),
                    EventName = "SetFine",
                    EventDate = DateTime.Now,
                    UserGuid = user.Guid,
                    FineAmount = amount
                };
                context.events.InsertOnSubmit(userEvent);
                context.SubmitChanges();
            }
            user.SetFineAmount(amount);
        }


        public void BorrowBook(IBook book, IUser user)
        {
            var tempBook = context.books.FirstOrDefault(b => b.GUID == book.Guid);
            var tempUser = context.users.FirstOrDefault(u => u.Guid == user.Guid);

            if (tempBook != null && tempBook.IsAvailable && tempUser != null)
            {
                tempBook.IsAvailable = false;
                tempBook.OwnerId = tempUser.Guid;

                @event borrowEvent = new @event
                {
                    EventID = Guid.NewGuid(),
                    EventName = "BorrowBook",
                    EventDate = DateTime.Now,
                    BookGuid = tempBook.GUID,
                    UserGuid = tempUser.Guid
                };

                context.events.InsertOnSubmit(borrowEvent);

                context.SubmitChanges(); 

                book.SetAvailability(false);
                book.SetOwner(user.Guid);
            }
        }

        public void ReturnBook(IBook book, IUser user)
        {
            var dbBook = context.books.FirstOrDefault(b => b.GUID == book.Guid);
            var dbUser = context.users.FirstOrDefault(u => u.Guid == user.Guid);
            if (dbBook != null && dbUser != null)
            {
                dbBook.IsAvailable = true;
                dbBook.OwnerId = Guid.Empty;
                @event returnEvent = new @event
                {
                    EventID = Guid.NewGuid(),
                    EventName = "ReturnBook",
                    EventDate = DateTime.Now,
                    BookGuid = book.Guid,
                    UserGuid = user.Guid
                };
                context.events.InsertOnSubmit(returnEvent);
                context.SubmitChanges();
                book.SetAvailability(true);
                book.SetOwner(Guid.Empty);
            }
        }
        
        public IEnumerable<IUser> GetNumberOfUsers(int number, int offset)
        {
            return context.users
                .Skip(offset)
                .Take(number)
                .Select(u => DbToObject(u))
                .Where(u => u != null)
                .ToList();
        }

        public IEnumerable<IBook> GetNumberOfBooks(int number, int offset)
        {
            var query = from b in context.books
                        select DbToObject(b);

            return query.Skip(offset).Take(number).ToList();
        }

        public IEnumerable<IUser> GetAllUsers()
        {

            IEnumerable<user> temp = context.users.ToList();
            return temp.Select(u => DbToObject(u)).ToList();
            
        }
        public IEnumerable<IBook> GetCatalog()
        {

            IEnumerable<book> books = context.books.ToList();
            return books.Select(b => DbToObject(b)).ToList();

        }
        public ILibraryState GetLibraryState()
        {
            return new LibraryState(GetCatalog(), GetAllUsers());
        }

        public void TruncateAllData()
        {
            context.events.DeleteAllOnSubmit(context.events);
            context.users.DeleteAllOnSubmit(context.users);
            context.books.DeleteAllOnSubmit(context.books);
            context.SubmitChanges();
           
        }

        public IBook GetBook(Guid guid)
        {
            var book = context.books.FirstOrDefault(b => b.GUID == guid);
            return DbToObject(book);
        }

        public IUser GetUser(Guid guid)
        {
            var user = context.users.FirstOrDefault(u => u.Guid == guid);
            return DbToObject(user);
        }

        public bool ContainsUser(IUser user)
        {
            return context.users.Any(u => u.Guid == user.Guid);
        }

        public bool ContainsBook(IBook book)
        {
            return context.books.Any(u => u.GUID == book.Guid);
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}

