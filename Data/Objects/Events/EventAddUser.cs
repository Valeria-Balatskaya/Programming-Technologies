using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Objects.Events
{
    internal class EventAddUser : IEventAddUser
    {

        public Guid UserGuid { get; }
        public DateTime Date { get; }

        public Guid Guid { get; }

        public string Action { get; }

        public EventAddUser(Guid guid, DateTime eventDate, Guid userGuid)
        {
            UserGuid = userGuid;
            Date = eventDate;
            Guid = guid;
            Action = "AddUser";
        }
    }
}
