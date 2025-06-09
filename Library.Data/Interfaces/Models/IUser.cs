namespace Library.Data.Interfaces.Models
{
    public interface IUser
    {
        int Id { get; }
        string Name { get; }
        string Email { get; }
        string PhoneNumber { get; }
        UserType Type { get; }
        DateTime RegistrationDate { get; }
    }
}
