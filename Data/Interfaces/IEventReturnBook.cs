using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface IEventReturnBook: IEvent
    {
        Guid BookGuid { get; }
        Guid UserGuid { get; }
    }
}
