using Library.Data.Interfaces;
using Logic.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic.Object
{
    internal class LibraryStateLogic : ILibraryStateLogic
    {
        public IEnumerable<IBookLogic> Books { get; set; }

        public IEnumerable<IUserLogic> Users { get; set; }

        public LibraryStateLogic(IEnumerable<IBookLogic> books, IEnumerable<IUserLogic> users)
        {
            Books = books;
            Users = users;
        }
    }
}
