using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Entities;

namespace Library.Data.Interfaces
{
    public interface IDataContext
    {
        List<User> Users { get; }
        Dictionary<int, CatalogItem> Catalog { get; }
        LibraryState State { get; }
        List<Event> Events { get; }
    }
}
