using Library.Data.Interfaces.Models;
using System.ComponentModel;

namespace Library.Presentation.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly IUser _user;

        public UserViewModel(IUser user)
        {
            _user = user;
        }

        public string Name
        {
            get => _user.Name;
            set
            {
                _user.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}