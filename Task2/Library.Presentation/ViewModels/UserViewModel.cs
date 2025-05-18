using System.ComponentModel;
using Library.Data.Interfaces.Models;

namespace Library.Presentation.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly IUser _user;

        public int Id => _user.Id;
        public string Name => _user.Name;
        public string Email => _user.Email;

        public event PropertyChangedEventHandler? PropertyChanged;

        public UserViewModel(IUser user) => _user = user;
    }
}