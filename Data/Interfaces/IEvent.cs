using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface IEvent
    {
        public Guid Guid { get;}
        public string Action { get; }
        public DateTime Date { get; }
    }
}
