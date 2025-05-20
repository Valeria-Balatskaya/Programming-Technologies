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
    public class BooksViewModel : ViewModelBase
    {
        private readonly ILibraryService _libraryService;
        private BookModel _selectedBook;
        private bool _isEditing;
        private bool _isNewBook;

        public BooksViewModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            Books = new ObservableCollection<BookModel>();

            var serviceBooks = _libraryService.GetAllBooks().ToList();
            Console.WriteLine($"BooksViewModel loaded {serviceBooks.Count} books from service");

            foreach (var book in serviceBooks)
            {
                Books.Add(new BookModel
                {
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Publisher = book.Publisher,
                    PublicationYear = book.PublicationYear ?? 0,
                    Genre = book.Genre,
                    Description = book.Description
                });
            }

            Console.WriteLine($"BooksViewModel has {Books.Count} books in its collection");

            LoadBooksCommand = new RelayCommand(p => LoadBooks());
            SaveBookCommand = new RelayCommand(p => SaveBook(), p => CanSaveBook());
            EditBookCommand = new RelayCommand(p => EditBook(), p => CanEditBook());
            NewBookCommand = new RelayCommand(p => NewBook());
            CancelEditCommand = new RelayCommand(p => CancelEdit(), p => IsEditing);
        }

        public ObservableCollection<BookModel> Books { get; }

        public BookModel SelectedBook
        {
            get => _selectedBook;
            set
            {
                if (SetProperty(ref _selectedBook, value) && !IsEditing)
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

        public bool IsNewBook
        {
            get => _isNewBook;
            set => SetProperty(ref _isNewBook, value);
        }

        public ICommand LoadBooksCommand { get; }
        public ICommand SaveBookCommand { get; }
        public ICommand EditBookCommand { get; }
        public ICommand NewBookCommand { get; }
        public ICommand CancelEditCommand { get; }

        private void LoadBooks()
        {
            Task.Run(() =>
            {
                var books = _libraryService.GetAllBooks();

                App.Current.Dispatcher.Invoke(() =>
                {
                    Books.Clear();
                    foreach (var book in books)
                    {
                        Books.Add(new BookModel
                        {
                            ISBN = book.ISBN,
                            Title = book.Title,
                            Author = book.Author,
                            Publisher = book.Publisher,
                            PublicationYear = book.PublicationYear ?? 0,
                            Genre = book.Genre,
                            Description = book.Description
                        });
                    }
                });
            });
        }

        private void SaveBook()
        {
            if (SelectedBook == null)
                return;

            Task.Run(() =>
            {
                try
                {
                    var book = new Books
                    {
                        ISBN = SelectedBook.ISBN,
                        Title = SelectedBook.Title,
                        Author = SelectedBook.Author,
                        Publisher = SelectedBook.Publisher,
                        PublicationYear = SelectedBook.PublicationYear,
                        Genre = SelectedBook.Genre,
                        Description = SelectedBook.Description
                    };

                    if (IsNewBook)
                    {
                        _libraryService.AddBook(book);
                    }
                    else
                    {
                        _libraryService.UpdateBook(book);
                    }

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        IsEditing = false;
                        IsNewBook = false;
                        LoadBooks();
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

        private bool CanSaveBook()
        {
            if (!IsEditing || SelectedBook == null)
                return false;

            return !string.IsNullOrWhiteSpace(SelectedBook.ISBN) &&
                   !string.IsNullOrWhiteSpace(SelectedBook.Title) &&
                   !string.IsNullOrWhiteSpace(SelectedBook.Author);
        }

        private void EditBook()
        {
            if (SelectedBook != null)
            {
                IsEditing = true;
                IsNewBook = false;
            }
        }

        private bool CanEditBook()
        {
            return SelectedBook != null && !IsEditing;
        }

        private void NewBook()
        {
            var newBook = new BookModel
            {
                ISBN = "",
                Title = "",
                Author = "",
                Publisher = "",
                PublicationYear = DateTime.Now.Year,
                Genre = "",
                Description = ""
            };

            SelectedBook = newBook;
            IsEditing = true;
            IsNewBook = true;
        }

        private void CancelEdit()
        {
            if (IsNewBook)
            {
                SelectedBook = Books.FirstOrDefault();
            }
            else if (SelectedBook != null)
            {
                var original = _libraryService.GetBookByIsbn(SelectedBook.ISBN);
                if (original != null)
                {
                    SelectedBook.Title = original.Title;
                    SelectedBook.Author = original.Author;
                    SelectedBook.Publisher = original.Publisher;
                    SelectedBook.PublicationYear = original.PublicationYear ?? 0;
                    SelectedBook.Genre = original.Genre;
                    SelectedBook.Description = original.Description;
                }
            }

            IsEditing = false;
            IsNewBook = false;
        }
    }
}