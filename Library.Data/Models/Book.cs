using Library.Data.Interfaces.Models;

namespace Library.Data.Models
{
    internal class Book : IBook
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int PublicationYear { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
    }
}
