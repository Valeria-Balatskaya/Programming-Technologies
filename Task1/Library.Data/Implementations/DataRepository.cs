using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Entities;
using Library.Data.Interfaces;

namespace Library.Data.Implementations
{
    public class DataRepository : IDataRepository
    {
        private readonly IDataContext _context;

        public DataRepository(IDataContext context)
        {
            _context = context;
        }

        public void AddUser(User user) => _context.Users.Add(user);

        public void AddCatalogItem(CatalogItem item) => _context.Catalog[item.Id] = item;

        public void BorrowBook(User user, CatalogItem book)
        {
            _context.State.AvailableBooks.Remove(book);
            _context.State.BorrowedBooks.Add(book);
            _context.Events.Add(new Event
            {
                Type = EventType.Borrow,
                User = user,
                Item = book,
                Timestamp = DateTime.Now
            });
        }

        public void ReturnBook(User user, CatalogItem book)
        {
            _context.State.BorrowedBooks.Remove(book);
            _context.State.AvailableBooks.Add(book);
            _context.Events.Add(new Event
            {
                Type = EventType.Return,
                User = user,
                Item = book,
                Timestamp = DateTime.Now
            });
        }
    }
}
