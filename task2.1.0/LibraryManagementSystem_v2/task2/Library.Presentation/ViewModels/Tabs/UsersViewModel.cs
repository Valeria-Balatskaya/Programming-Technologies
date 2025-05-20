using Library.Data.SqlServer;
using Library.Logic;
using Library.Presentation.Commands;
using Library.Presentation.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Presentation.ViewModels.Tabs
{
    public class UsersViewModel : ViewModelBase
    {
        private readonly ILibraryService _libraryService;
        private UserModel _selectedUser;
        private bool _isEditing;
        private bool _isNewUser;

        public UsersViewModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            Users = new ObservableCollection<UserModel>();
            UserTypes = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToArray();

            LoadUsersCommand = new RelayCommand(p => LoadUsers());
            SaveUserCommand = new RelayCommand(p => SaveUser(), p => CanSaveUser());
            EditUserCommand = new RelayCommand(p => EditUser(), p => CanEditUser());
            NewUserCommand = new RelayCommand(p => NewUser());
            CancelEditCommand = new RelayCommand(p => CancelEdit(), p => IsEditing);

            LoadUsers();
        }

        public ObservableCollection<UserModel> Users { get; }

        public UserType[] UserTypes { get; }

        public UserModel SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (SetProperty(ref _selectedUser, value) && !IsEditing)
                {
                    CancelEdit();
                }
            }
        }

        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        public bool IsNewUser
        {
            get => _isNewUser;
            set => SetProperty(ref _isNewUser, value);
        }

        public ICommand LoadUsersCommand { get; }
        public ICommand SaveUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand NewUserCommand { get; }
        public ICommand CancelEditCommand { get; }

        private void LoadUsers()
        {
            Task.Run(() =>
            {
                var users = _libraryService.GetAllUsers();

                App.Current.Dispatcher.Invoke(() =>
                {
                    Users.Clear();
                    foreach (var user in users)
                    {
                        Users.Add(new UserModel
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            Type = (UserType)user.Type,
                            RegistrationDate = user.RegistrationDate
                        });
                    }
                });
            });
        }

        private void SaveUser()
        {
            if (SelectedUser == null)
                return;

            Task.Run(() =>
            {
                try
                {
                    var user = new Users
                    {
                        Id = SelectedUser.Id,
                        Name = SelectedUser.Name,
                        Email = SelectedUser.Email,
                        PhoneNumber = SelectedUser.PhoneNumber,
                        Type = (int)SelectedUser.Type,
                        RegistrationDate = SelectedUser.RegistrationDate
                    };

                    if (IsNewUser)
                    {
                        _libraryService.RegisterUser(user);
                    }
                    else
                    {
                        _libraryService.UpdateUserInformation(user);
                    }

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        IsEditing = false;
                        IsNewUser = false;
                        LoadUsers();
                    });
                }
                catch (Exception ex)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    });
                }
            });
        }

        private bool CanSaveUser()
        {
            if (!IsEditing || SelectedUser == null)
                return false;

            return !string.IsNullOrWhiteSpace(SelectedUser.Name) &&
                   !string.IsNullOrWhiteSpace(SelectedUser.Email);
        }

        private void EditUser()
        {
            if (SelectedUser != null)
            {
                IsEditing = true;
                IsNewUser = false;
            }
        }

        private bool CanEditUser()
        {
            return SelectedUser != null && !IsEditing;
        }

        private void NewUser()
        {
            var newUser = new UserModel
            {
                Name = "",
                Email = "",
                PhoneNumber = "",
                Type = UserType.Patron,
                RegistrationDate = DateTime.Now
            };

            SelectedUser = newUser;
            IsEditing = true;
            IsNewUser = true;
        }

        private void CancelEdit()
        {
            if (IsNewUser)
            {
                SelectedUser = Users.FirstOrDefault();
            }
            else if (SelectedUser != null)
            {
                var original = _libraryService.GetUserById(SelectedUser.Id);
                if (original != null)
                {
                    SelectedUser.Name = original.Name;
                    SelectedUser.Email = original.Email;
                    SelectedUser.PhoneNumber = original.PhoneNumber;
                    SelectedUser.Type = (UserType)original.Type;
                    SelectedUser.RegistrationDate = original.RegistrationDate;
                }
            }

            IsEditing = false;
            IsNewUser = false;
        }
    }
}