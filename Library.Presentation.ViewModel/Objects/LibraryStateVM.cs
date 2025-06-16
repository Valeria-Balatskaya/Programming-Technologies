using Library.Presentation.Model.Interfaces;
using Library.Presentation.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.ViewModel.Objects
{
    internal class LibraryStateVM: ILibraryStateVM
    {
        public IEnumerable<IBookVM> Books { get; private set; }
        public IEnumerable<IUserVM> Users { get; private set; }
        public LibraryStateVM(IEnumerable<IBookVM> books, IEnumerable<IUserVM> users)
        {
            Books = books;
            Users = users;
        }
    }
}
