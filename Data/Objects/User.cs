using Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Objects
{
    internal class User: IUser
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public Guid Guid { get; private set; }
        public double FineAmount { get; private set; }
        public User(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Guid = Guid.NewGuid();
            Email = email;
            FineAmount = 0.0;
        }

        public User(string name, string surname, string email, Guid guid, double fine)
        {
            Name = name;
            Surname = surname;
            Guid = guid;
            Email = email;
            FineAmount = fine;
        }

        public void SetFineAmount(double amount)
        {
            FineAmount = amount;
        }
    }
}
