using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Objects
{
    internal class LibraryState: ILibraryState
    {
        public IEnumerable<IBook> Books { get; set; }
        public IEnumerable<IUser> Users { get; set; }

        public LibraryState(IEnumerable<IBook> books, IEnumerable<IUser> users)
        {
            Books = books;
            Users = users;
        }
    }
}
