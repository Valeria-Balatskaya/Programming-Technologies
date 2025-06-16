using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic.Interfaces
{
    public interface IBookLogic
    {
        public string Isbn { get; }
        public string Title { get; }
        public string Author { get; }
        public string Genre { get; }
        public DateTime Year { get; }
        public Guid Guid { get; }
        public int Pages { get; }
        public Guid OwnerId { get; }
        public bool IsAvailable { get; }
        public void SetAvailability(bool isAvailable);
        public void SetOwner(Guid ownerId);
    }
}
