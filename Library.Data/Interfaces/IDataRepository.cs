using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Entities;

namespace Library.Data.Interfaces
{
    public interface IDataRepository
    {
        void AddUser(User user);
        void AddCatalogItem(CatalogItem item);
        void BorrowBook(User user, CatalogItem book);
        void ReturnBook(User user, CatalogItem book);
    }
}
