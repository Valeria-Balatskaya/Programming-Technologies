using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Entities;
using Library.Data.Interfaces;

namespace Library.Logic
{
    public class LibraryService
    {
        private readonly IDataRepository _repository;
        private readonly IDataContext _context; 

       
        public LibraryService(IDataRepository repository, IDataContext context)
        {
            _repository = repository;
            _context = context;
        }

        public void AddUser(User user) => _repository.AddUser(user);
        public void AddBook(CatalogItem book) => _repository.AddCatalogItem(book);
        public void BorrowBook(User user, CatalogItem book) => _repository.BorrowBook(user, book);
        public void ReturnBook(User user, CatalogItem book) => _repository.ReturnBook(user, book);

        
        public LibraryState GetLibraryState() => _context.State;
        public List<Event> GetEventLog() => _context.Events;
    }
}