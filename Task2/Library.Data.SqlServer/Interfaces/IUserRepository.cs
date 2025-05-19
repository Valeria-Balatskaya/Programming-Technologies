using System;
using System.Collections.Generic;

namespace Library.Data.SqlServer.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAllUsers();
        Users GetUserById(int id);
        void AddUser(Users user);
        void UpdateUser(Users user);
        void DeleteUser(int id);
    }
}
