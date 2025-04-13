using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   Name == user.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    } 
}
