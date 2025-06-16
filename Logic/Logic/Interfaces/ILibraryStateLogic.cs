using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic.Interfaces
{
    public interface ILibraryStateLogic
    {
        public IEnumerable<IBookLogic> Books { get; }


        public IEnumerable<IUserLogic> Users { get; }
    }
}
