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
        
        public ObservableCollection<ObservableBook> Books { get; } = new();
        public ICommand LoadBooksCommand { get; }
        public ICommand SaveCommand { get; }

        public ObservableBook? SelectedBook
        {
            get => _selectedBook;
            set => SetField(ref _selectedBook, value);
        }

        public MainViewModel(ILibraryService service)
        {
            _service = service;
            LoadBooksCommand = new RelayCommand(LoadBooks);
            SaveCommand = new RelayCommand(SaveChanges);
        }

        private void LoadBooks()
        {
            Books.Clear();
            foreach (var book in _service.GetAllBooks())
                Books.Add(new ObservableBook(book));
        }

        private void SaveChanges()
        {
            foreach (var book in Books)
                _service.UpdateBook(book.GetBook());
        }
    }
}