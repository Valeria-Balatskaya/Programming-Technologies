using Library.Data.Interfaces.Models;
using System.ComponentModel;

namespace Library.Presentation.Models
{
    public class ObservableUser : INotifyPropertyChanged
    {
        private readonly IUser _user;

        public ObservableUser(IUser user) => _user = user;
        public IUser GetUser() => _user;

        public string Name
        {
            get => _user.Name;
            set { _user.Name = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => _user.Email;
            set { _user.Email = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}