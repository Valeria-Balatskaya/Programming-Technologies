using Library.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface ILibraryState
    {
        public IEnumerable<IBook> Books { get; }
        public IEnumerable<IUser> Users { get; }
    }
}
