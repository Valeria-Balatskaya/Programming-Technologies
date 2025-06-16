using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Interfaces;

namespace Library.Data.Objects.Events
{
    internal class EventAddBook: IEventAddBook
    {
        public Guid Guid { get;}
        public Guid BookGuid { get;}
        public string Action { get;}
        public DateTime Date { get;}
        public EventAddBook(Guid guid, DateTime date, Guid bookGuid)
        {
            Guid = guid;
            BookGuid = bookGuid;
            Date = date;
            Action = "AddBook";
        }
    }
}
