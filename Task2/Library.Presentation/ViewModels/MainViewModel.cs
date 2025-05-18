using Library.Logic.Interfaces;
using Library.Presentation.Commands;
using Library.Presentation.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Library.Presentation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILibraryService _service;
        private ObservableBook? _selectedBook;
        private ObservableUser? _selectedUser;

        public ObservableCollection<ObservableBook> Books { get; } = new();
        public ObservableCollection<ObservableUser> Users { get; } = new();
        
        public ICommand LoadDataCommand { get; }
        public ICommand SaveCommand { get; }

        public ObservableBook? SelectedBook
        {
            get => _selectedBook;
            set => SetField(ref _selectedBook, value);
        }

        public ObservableUser? SelectedUser
        {
            get => _selectedUser;
            set => SetField(ref _selectedUser, value);
        }

        public MainViewModel(ILibraryService service)
        {
            _service = service;
            LoadDataCommand = new RelayCommand(LoadData);
            SaveCommand = new RelayCommand(Save);
        }

        private void LoadData()
        {
            Books.Clear();
            foreach (var book in _service.GetAllBooks())
                Books.Add(new ObservableBook(book));

            Users.Clear();
            foreach (var user in _service.GetAllUsers())
                Users.Add(new ObservableUser(user));
        }

        private void Save()
        {
            foreach (var book in Books)
                _service.UpdateBook(book.GetBook());
            
            foreach (var user in Users)
                _service.UpdateUser(user.GetUser());
        }
    }
}