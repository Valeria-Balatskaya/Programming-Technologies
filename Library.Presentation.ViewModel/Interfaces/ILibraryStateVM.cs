using Library.Presentation.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.ViewModel.Interfaces
{
    public interface ILibraryStateVM
    {
        public IEnumerable<IBookVM> Books { get; }


        public IEnumerable<IUserVM> Users { get; }
    }
}
