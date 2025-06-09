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
    public class TransactionsViewModel : ViewModelBase
    {
        private readonly ILibraryService _libraryService;
        private BookCopyModel _selectedAvailableBook;
        private BookCopyModel _selectedCheckedOutBook;
        private Users _selectedUser;
        private DateTime _dueDate;

        public TransactionsViewModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            AvailableBookCopies = new ObservableCollection<BookCopyModel>();
            CheckedOutBookCopies = new ObservableCollection<BookCopyModel>();
            Users = new ObservableCollection<Users>();
            DueDate = DateTime.Now.AddDays(14);

            LoadAvailableBooksCommand = new RelayCommand(p => LoadAvailableBooks());
            LoadCheckedOutBooksCommand = new RelayCommand(p => LoadCheckedOutBooks());
            LoadUsersCommand = new RelayCommand(p => LoadUsers());
            BorrowBookCommand = new RelayCommand(p => BorrowBook(), p => CanBorrowBook());
            ReturnBookCommand = new RelayCommand(p => ReturnBook(), p => CanReturnBook());

            LoadAvailableBooks();
            LoadCheckedOutBooks();
            LoadUsers();
        }

        public ObservableCollection<BookCopyModel> AvailableBookCopies { get; }
        public ObservableCollection<BookCopyModel> CheckedOutBookCopies { get; }
        public ObservableCollection<Users> Users { get; }

        public BookCopyModel SelectedAvailableBook
        {
            get => _selectedAvailableBook;
            set => SetProperty(ref _selectedAvailableBook, value);
        }

        public BookCopyModel SelectedCheckedOutBook
        {
            get => _selectedCheckedOutBook;
            set => SetProperty(ref _selectedCheckedOutBook, value);
        }

        public Users SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        public ICommand LoadAvailableBooksCommand { get; }
        public ICommand LoadCheckedOutBooksCommand { get; }
        public ICommand LoadUsersCommand { get; }
        public ICommand BorrowBookCommand { get; }
        public ICommand ReturnBookCommand { get; }

        private void LoadAvailableBooks()
        {
            Task.Run(() =>
            {
                var availableBooks = _libraryService.GetAvailableBooks();

                App.Current.Dispatcher.Invoke(() =>
                {
                    AvailableBookCopies.Clear();
                    foreach (var bookCopy in availableBooks)
                    {
                        var book = _libraryService.GetBookByIsbn(bookCopy.ISBN);

                        AvailableBookCopies.Add(new BookCopyModel
                        {
                            Id = bookCopy.Id,
                            ISBN = bookCopy.ISBN,
                            Status = (BookStatus)bookCopy.Status,
                            AcquisitionDate = bookCopy.AcquisitionDate,
                            Location = bookCopy.Location,
                            BookTitle = book?.Title
                        });
                    }
                });
            });
        }

        private void LoadCheckedOutBooks()
        {
            Task.Run(() =>
            {
                var checkedOutBooks = _libraryService.GetCheckedOutBooks();

                App.Current.Dispatcher.Invoke(() =>
                {
                    CheckedOutBookCopies.Clear();
                    foreach (var bookCopy in checkedOutBooks)
                    {
                        var book = _libraryService.GetBookByIsbn(bookCopy.ISBN);
                        string borrowerName = null;
                        if (bookCopy.CurrentBorrowerId.HasValue)
                        {
                            var borrower = _libraryService.GetUserById(bookCopy.CurrentBorrowerId.Value);
                            borrowerName = borrower?.Name;
                        }

                        CheckedOutBookCopies.Add(new BookCopyModel
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
                        Users.Add(user);
                    }
                });
            });
        }

        private void BorrowBook()
        {
            if (SelectedAvailableBook == null || SelectedUser == null)
                return;

            Task.Run(() =>
            {
                try
                {
                    var success = _libraryService.BorrowBook(SelectedUser.Id, SelectedAvailableBook.Id, DueDate);

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (success)
                        {
                            LoadAvailableBooks();
                            LoadCheckedOutBooks();
                            System.Windows.MessageBox.Show($"Book '{SelectedAvailableBook.BookTitle}' has been successfully borrowed by {SelectedUser.Name}.", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Failed to borrow the book.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        }
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

        private bool CanBorrowBook()
        {
            return SelectedAvailableBook != null && SelectedUser != null;
        }

        private void ReturnBook()
        {
            if (SelectedCheckedOutBook == null)
                return;

            Task.Run(() =>
            {
                try
                {
                    var success = _libraryService.ReturnBook(SelectedCheckedOutBook.Id);

                    App.Current.Dispatcher.Invoke(() =>
                    {
                        if (success)
                        {
                            LoadAvailableBooks();
                            LoadCheckedOutBooks();
                            System.Windows.MessageBox.Show($"Book '{SelectedCheckedOutBook.BookTitle}' has been successfully returned.", "Success", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Failed to return the book.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                        }
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

        private bool CanReturnBook()
        {
            return SelectedCheckedOutBook != null;
        }
    }
}