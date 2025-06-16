using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Objects.Events
{
    internal class EventBorrowBook : IEventBorrowBook
    {
        public Guid BookGuid { get; }

        public Guid UserGuid { get; }
        public Guid Guid { get; }

        public string Action { get; }

        public DateTime Date { get; }

        public EventBorrowBook(Guid guid, DateTime date, Guid bookGuid, Guid userGuid)
        {
            BookGuid = bookGuid;
            UserGuid = userGuid;
            Guid = guid;
            Date = date;
            Action = "BorrowBook";
        }
    }
}
