using Library.Data.Interfaces.Models;

namespace Library.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<IUser> GetAllUsers();
        IUser GetUserById(int id);
        void AddUser(IUser user);
        void UpdateUser(IUser user);
        void DeleteUser(int id);
    }
}
