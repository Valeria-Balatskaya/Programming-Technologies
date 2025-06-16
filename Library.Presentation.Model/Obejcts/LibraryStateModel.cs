using Library.Presentation.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.Model.Obejcts
{
    internal class LibraryStateModel : ILibraryStateModel
    {
        public IEnumerable<IBookModel> Books { get; private set; }
        public IEnumerable<IUserModel> Users { get; private set; }
        public LibraryStateModel(IEnumerable<IBookModel> books, IEnumerable<IUserModel> users)
        {
            Books = books;
            Users = users;
        }
    }
}
