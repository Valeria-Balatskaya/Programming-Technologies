using Library.Data.Interfaces.Models;

namespace Library.Data.Models
{
    internal class User : IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserType Type { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public enum UserType
    {
        Patron,
        Librarian,
        Administrator
    }
}
