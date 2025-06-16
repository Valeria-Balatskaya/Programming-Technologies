using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Objects.Events
{
    internal class EventRemoveUser : IEventRemoveUser
    {
        public Guid UserGuid { get; }

        public Guid Guid { get; }

        public string Action { get; }

        public DateTime Date { get; }
        public EventRemoveUser(Guid guid, DateTime date, Guid userGuid)
        {
            UserGuid = userGuid;
            Guid = guid;
            Date = date;
            Action = "RemoveUser";
        }
    }
}
