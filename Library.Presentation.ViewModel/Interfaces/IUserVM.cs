using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.ViewModel.Interfaces
{
    public interface IUserVM
    {
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public Guid Guid { get; }
        public double FineAmount { get; }
        public void SetFineAmount(double amount);
    }
}
