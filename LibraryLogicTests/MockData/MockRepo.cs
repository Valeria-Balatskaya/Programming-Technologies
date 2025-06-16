using Logic.Logic;
using Logic.Logic.Interfaces;


namespace LibraryLogicTests.MockData
{
    internal class MockRepo:IRepositoryLogic
    {
        private Dictionary<Guid, IBookLogic> Books = new Dictionary<Guid, IBookLogic>();
        private Dictionary<Guid, IUserLogic> Users = new Dictionary<Guid, IUserLogic>();
        public MockRepo()
        {

        }

        public void AddBook(string Title, string Author, string Genre, DateTime PublishedDate, string ISBN, int Pages)
        {
            IBookLogic book = LogicDataFactory.CreateBook(Title, Author, Genre, PublishedDate, ISBN, Pages);
        }

        public void AddBook(IBookLogic book)
        {
            Books[book.Guid] = book;
        }

        public void AddUser(IUserLogic user)
        {
            Users[user.Guid] = user;
        }

        public void BorrowBook(IBookLogic book, IUserLogic user)
        {
            if (!Books.ContainsKey(book.Guid)) throw new KeyNotFoundException("Book not found in repository.");
            if (!Users.ContainsKey(user.Guid)) throw new KeyNotFoundException("User not found in repository.");
            book.SetOwner(user.Guid);
        }

        public bool ContainsBook(IBookLogic book)
        {
            return Books.ContainsKey(book.Guid);
        }

        public bool ContainsUser(IUserLogic user)
        {
            return Users.ContainsKey(user.Guid);
        }

        public void Dispose()
        {
            Books.Clear();
            Users.Clear();
        }


        public void RemoveBook(IBookLogic book)
        {
            if (Books.ContainsKey(book.Guid))
            {
                Books.Remove(book.Guid);
            }
            else
            {
                throw new KeyNotFoundException("Book not found in repository.");
            }
        }

        public void RemoveUser(IUserLogic user)
        {
            if (Users.ContainsKey(user.Guid))
            {
                Users.Remove(user.Guid);
            }
            else
            {
                throw new KeyNotFoundException("User not found in repository.");
            }
        }

        public void ReturnBook(IBookLogic book, IUserLogic user)
        {
            if (!Books.ContainsKey(book.Guid)) throw new KeyNotFoundException("Book not found in repository.");
            if (!Users.ContainsKey(user.Guid)) throw new KeyNotFoundException("User not found in repository.");
            book.SetOwner(Guid.Empty);
        }

        public void SetFine(IUserLogic user, double amount)
        {
            if (!Users.ContainsKey(user.Guid)) throw new KeyNotFoundException("User not found in repository.");
            user.SetFineAmount(amount);
        }

        public IEnumerable<IUserLogic> GetAllUsers()
        {
            return Users.Values;
        }

        public IBookLogic GetBook(Guid guid)
        {
            if (Books.TryGetValue(guid, out IBookLogic book))
            {
                return book;
            }
            throw new KeyNotFoundException("Book not found in repository.");
        }

        public IEnumerable<IBookLogic> GetCatalog()
        {
            return Books.Values;
        }

        public ILibraryStateLogic GetLibraryState()
        {
            return LogicDataFactory.CreateLibraryState(Books.Values, Users.Values);
        }

        public IEnumerable<IBookLogic> GetNumberOfBooks(int number, int offset)
        {
            return Books.Values.Skip(offset).Take(number).ToList();
        }

        public IEnumerable<IUserLogic> GetNumberOfUsers(int number, int offset)
        {
            return Users.Values.Skip(offset).Take(number).ToList();
        }

        public IUserLogic GetUser(Guid guid)
        {
            if (Users.TryGetValue(guid, out IUserLogic user))
            {
                return user;
            }
            throw new KeyNotFoundException("User not found in repository.");
        }

        public void TruncateAllData()
        {
            Books.Clear();
            Users.Clear();
        }
    }
}
