using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entities
{
    public class LibraryState
    {
        public List<CatalogItem> AvailableBooks { get; } = new();
        public List<CatalogItem> BorrowedBooks { get; } = new();
    }
}
