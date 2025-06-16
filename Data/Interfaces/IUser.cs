using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Interfaces
{
    public interface IUser
    {
        public string Name { get;}
        public string Surname { get; }
        public string Email { get; }
        public Guid Guid { get; }
        public double FineAmount { get; }
        public void SetFineAmount(double amount);
    }
}
