using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface IEventBorrowBook: IEvent
    {
        Guid BookGuid { get; }
        Guid UserGuid { get; }
    }
}
