using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Objects.Events
{
    internal class EventRemoveBook: IEventRemoveBook
    {
        public Guid BookGuid { get; }

        public Guid Guid { get; }
        public string Action { get; }

        public DateTime Date { get; }

        public EventRemoveBook(Guid guid, DateTime date, Guid bookGuid)
        {
            BookGuid = bookGuid;
            Guid = guid;
            Date = date;
            Action = "RemoveBook";
        }
    }
}
