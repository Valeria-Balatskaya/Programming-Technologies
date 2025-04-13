using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Entities
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CatalogItem item &&
                   Id == item.Id &&
                   Title == item.Title &&
                   Author == item.Author;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Author);
        }
    } 
}