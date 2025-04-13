using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public EventType Type { get; set; }
        public DateTime Timestamp { get; set; }
        public required User User { get; set; }
        public required CatalogItem Item { get; set; } 
    }

    public enum EventType { Borrow, Return }
}
