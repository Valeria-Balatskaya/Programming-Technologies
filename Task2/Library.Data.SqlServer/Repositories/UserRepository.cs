using Library.Data.SqlServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Data.SqlServer.Context
{
    public class UserRepository : IUserRepository
    {
        private readonly ILibraryDataContext _context;

        public UserRepository(ILibraryDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Users> GetAllUsers()
        {
            var users = from u in _context.Users
                        select u;

            return users.ToList();
        }

        public Users GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(Users user)
        {
            var newUser = _context.CreateUser();
            newUser.Name = user.Name;
            newUser.Email = user.Email;
            newUser.PhoneNumber = user.PhoneNumber;
            newUser.Type = user.Type;
            newUser.RegistrationDate = user.RegistrationDate;

            _context.SubmitChanges();
        }

        public void UpdateUser(Users user)
        {
            var existingUser = GetUserById(user.Id);
            if (existingUser == null)
                throw new ArgumentException($"User with ID {user.Id} not found.");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.Type = user.Type;
            existingUser.RegistrationDate = user.RegistrationDate;

            _context.SubmitChanges();
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);

            if (userToDelete != null)
            {
                _context.DeleteUser(userToDelete);
                _context.SubmitChanges();
            }
        }
    }
}