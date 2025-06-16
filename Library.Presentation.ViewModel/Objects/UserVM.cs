using Library.Presentation.ViewModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.ViewModel.Objects
{
    internal class UserVM: IUserVM
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public Guid Guid { get; private set; }
        public double FineAmount { get; private set; }
        public void SetFineAmount(double amount)
        {
            FineAmount = amount;
        }
        public UserVM(string name, string surname, string email, Guid guid, double fineAmount)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Guid = guid;
            FineAmount = fineAmount;
        }
        public UserVM(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Guid = Guid.NewGuid();
            FineAmount = 0.0;
        }
    }
}
