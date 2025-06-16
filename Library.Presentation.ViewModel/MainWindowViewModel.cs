using Library.Presentation.ViewModel.Interfaces;
using System.Collections.ObjectModel;

namespace Library.Presentation.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private IRepositoryVM? _repo;
        public ObservableCollection<IUserVM> Users { get; set; } = new();
        public ObservableCollection<IBookVM> Books { get; set; } = new();
        public RelayCommand ShowAllBooksCommand => new RelayCommand(execute => LoadAllBooks());
        public MainWindowViewModel(IRepositoryVM? repo)
        {
            _repo = repo;
            if (repo == null)
            {
                return;
            }

            foreach (var user in repo.GetAllUsers())
            {
                Users.Add(VMDataFactory.CreateUser(user.Name, user.Surname, user.Email));
            }

            foreach (var book in repo.GetCatalog())
            {
                var temp = VMDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages);
                temp.SetOwner(book.OwnerId);
                temp.SetAvailability(book.IsAvailable);
                Books.Add(temp);
            }
        }

        private bool isUserSelected;

        public bool IsUserSelected
        {
            get { return isUserSelected; }
            set
            {
                isUserSelected = value;
                OnPropertyChanged();
            }
        }

        private void LoadAllBooks()
        {
            if (_repo == null)
            {
                return;
            }
            Books.Clear();
            foreach (var book in _repo.GetCatalog())
            {
                var temp = VMDataFactory.CreateBook(book.Title, book.Author, book.Genre, book.Year, book.Isbn, book.Pages);
                temp.SetOwner(book.OwnerId);
                temp.SetAvailability(book.IsAvailable);
                Books.Add(temp);
            }
        }

        public void Dispose()
        {
            if (_repo != null)
            {
                _repo.Dispose();
            }
        }

        private IBookVM selectedBook;

        public IBookVM SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged();
            }
        }

        private IUserVM selectedUser;

        public IUserVM SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                IsUserSelected = selectedUser != null;
                OnPropertyChanged();
            }
        }

    }
}
