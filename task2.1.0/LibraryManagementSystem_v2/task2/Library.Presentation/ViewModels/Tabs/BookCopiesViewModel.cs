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
    public class BookCopiesViewModel : ViewModelBase
    {
        private readonly ILibraryService _libraryService;
        private BookCopyModel _selectedBookCopy;
        private bool _isEditing;
        private bool _isNewBookCopy;

        public BookCopiesViewModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            BookCopies = new ObservableCollection<BookCopyModel>();
            Books = new ObservableCollection<Books>();
            BookStatuses = Enum.GetValues(typeof(BookStatus)).Cast<BookStatus>().ToArray();

            LoadBookCopiesCommand = new RelayCommand(p => LoadBookCopies());
            SaveBookCopyCommand = new RelayCommand(p => SaveBookCopy(), p => CanSaveBookCopy());
            EditBookCopyCommand = new RelayCommand(p => EditBookCopy(), p => CanEditBookCopy());
            NewBookCopyCommand = new RelayCommand(p => NewBookCopy());
            CancelEditCommand = new RelayCommand(p => CancelEdit(), p => IsEditing);

            LoadBookCopies();
            LoadBooks();
        }

        public ObservableCollection<BookCopyModel> BookCopies { get; }
        public ObservableCollection<Books> Books { get; }
        public BookStatus[] BookStatuses { get; }

        public BookCopyModel SelectedBookCopy
        {
            get => _selectedBookCopy;
            set
            {
                if (SetProperty(ref _selectedBookCopy, value) && !IsEditing)
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

        public bool IsNewBookCopy
        {
            get => _isNewBookCopy;
            set => SetProperty(ref _isNewBookCopy, value);
        }

        public ICommand LoadBookCopiesCommand { get; }
        public ICommand SaveBookCopyCommand { get; }
        public ICommand EditBookCopyCommand { get; }
        public ICommand NewBookCopyCommand { get; }
        public ICommand CancelEditCommand { get; }

        private void LoadBookCopies()
        {
            Task.Run(() =>
            {
                var bookCopies = _libraryService.GetAllBookCopies();

                App.Current.Dispatcher.Invoke(() =>
                {
                    BookCopies.Clear();
                    foreach (var bookCopy in bookCopies)
                    {
                        var book = _libraryService.GetBookByIsbn(bookCopy.ISBN);
                        string borrowerName = null;
                        if (bookCopy.CurrentBorrowerId.HasValue)
                        {
                            var borrower = _libraryService.GetUserById(bookCopy.CurrentBorrowerId.Value);
                            borrowerName = borrower?.Name;
                        }

                        BookCopies.Add(new BookCopyModel
                        {
                            Id = bookCopy.Id,
                            ISBN = bookCopy.ISBN,
                            Status = (BookStatus)bookCopy.Status,
                            AcquisitionDate = bookCopy.AcquisitionDate,
                            Location = bookCopy.Location,
                            CurrentBorrowerId = bookCopy.CurrentBorrowerId,
                            DueDate = bookCopy.DueDate,
                            BookTitle = book?.Title,
                            BorrowerName = borrowerName
                        });
                    }
                });
            });
        }

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
                        Books.Add(book);
                    }
                });
            });
        }

        private void SaveBookCopy()
        {
            if (SelectedBookCopy == null)
                return;

            Task.Run(() =>
            {
                try
                {
                    var bookCopy = new BookCopies
                    {
                        Id = SelectedBookCopy.Id,
                        ISBN = SelectedBookCopy.ISBN,
                        Status = (int)SelectedBookCopy.Status,
                        AcquisitionDate = SelectedBookCopy.AcquisitionDate,
                        Location = SelectedBookCopy.Location,
                        CurrentBorrowerId = SelectedBookCopy.CurrentBorrowerId,
                        DueDate = SelectedBookCopy.DueDate
                    };

                    if (IsNewBookCopy)
                    {
                        _libraryService.AddBookCopy(bookCopy);
                    }
                    else
                    {
                        _libraryService.UpdateBookCopy(bookCopy);
                    }

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        IsEditing = false;
                        IsNewBookCopy = false;
                        LoadBookCopies();
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

        private bool CanSaveBookCopy()
        {
            if (!IsEditing || SelectedBookCopy == null)
                return false;

            return !string.IsNullOrWhiteSpace(SelectedBookCopy.ISBN) &&
                   !string.IsNullOrWhiteSpace(SelectedBookCopy.Location);
        }

        private void EditBookCopy()
        {
            if (SelectedBookCopy != null)
            {
                IsEditing = true;
                IsNewBookCopy = false;
            }
        }

        private bool CanEditBookCopy()
        {
            return SelectedBookCopy != null && !IsEditing;
        }

        private void NewBookCopy()
        {
            var newBookCopy = new BookCopyModel
            {
                ISBN = Books.FirstOrDefault()?.ISBN ?? "",
                Status = BookStatus.Available,
                AcquisitionDate = DateTime.Now,
                Location = "",
                CurrentBorrowerId = null,
                DueDate = null
            };

            SelectedBookCopy = newBookCopy;
            IsEditing = true;
            IsNewBookCopy = true;
        }

        private void CancelEdit()
        {
            if (IsNewBookCopy)
            {
                SelectedBookCopy = BookCopies.FirstOrDefault();
            }
            else if (SelectedBookCopy != null)
            {
                var original = _libraryService.GetBookCopyById(SelectedBookCopy.Id);
                if (original != null)
                {
                    SelectedBookCopy.ISBN = original.ISBN;
                    SelectedBookCopy.Status = (BookStatus)original.Status;
                    SelectedBookCopy.AcquisitionDate = original.AcquisitionDate;
                    SelectedBookCopy.Location = original.Location;
                    SelectedBookCopy.CurrentBorrowerId = original.CurrentBorrowerId;
                    SelectedBookCopy.DueDate = original.DueDate;
                }
            }

            IsEditing = false;
            IsNewBookCopy = false;
        }
    }
}