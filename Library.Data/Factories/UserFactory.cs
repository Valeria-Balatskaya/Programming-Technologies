using Library.Data.Interfaces.Models;

namespace Library.Data.Factories
{
    public static class UserFactory
    {
        public static IUser CreateUser(int id, string name, string email, string phoneNumber, UserType type, System.DateTime registrationDate)
        {
            return new User
            {
                Id = id,
                Name = name,
                Email = email,
                PhoneNumber = phoneNumber,
                Type = type,
                RegistrationDate = registrationDate
            };
        }
    }
}
