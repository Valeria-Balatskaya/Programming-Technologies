using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Entities;
using Library.Data.Interfaces;

namespace Library.Data.Implementations
{
    public class DataContext : IDataContext
    {
        public List<User> Users { get; } = new();
        public Dictionary<int, CatalogItem> Catalog { get; } = new();
        public LibraryState State { get; } = new();
        public List<Event> Events { get; } = new();
    }
}
