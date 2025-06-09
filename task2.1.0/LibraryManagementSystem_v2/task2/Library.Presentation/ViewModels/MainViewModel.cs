using Library.Data.SqlServer.Context;
using Library.Data.SqlServer.Repositories;
using Library.Logic;
using Library.Presentation.ViewModels.Tabs;
using System.Configuration;
using System.Windows.Input;

namespace Library.Presentation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public MainViewModel()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["LibraryDatabase"].ConnectionString;
            var factory = new LibraryDataContextFactory(connectionString);
            var context = factory.CreateContext();

            var bookRepository = new BookRepository(context);
            var userRepository = new UserRepository(context);
            var bookCopyRepository = new BookCopyRepository(context);
            var eventRepository = new EventRepository(context);

            ILibraryService libraryService = new LibraryService(
                context,
                bookRepository,
                userRepository,
                bookCopyRepository,
                eventRepository);

            BooksViewModel = new BooksViewModel(libraryService);
            UsersViewModel = new UsersViewModel(libraryService);
            BookCopiesViewModel = new BookCopiesViewModel(libraryService);
            TransactionsViewModel = new TransactionsViewModel(libraryService);
            EventsViewModel = new EventsViewModel(libraryService);

            CurrentViewModel = BooksViewModel;
        }

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public BooksViewModel BooksViewModel { get; }
        public UsersViewModel UsersViewModel { get; }
        public BookCopiesViewModel BookCopiesViewModel { get; }
        public TransactionsViewModel TransactionsViewModel { get; }
        public EventsViewModel EventsViewModel { get; }

        public ICommand ShowBooksCommand => new Commands.RelayCommand(p => CurrentViewModel = BooksViewModel);
        public ICommand ShowUsersCommand => new Commands.RelayCommand(p => CurrentViewModel = UsersViewModel);
        public ICommand ShowBookCopiesCommand => new Commands.RelayCommand(p => CurrentViewModel = BookCopiesViewModel);
        public ICommand ShowTransactionsCommand => new Commands.RelayCommand(p => CurrentViewModel = TransactionsViewModel);
        public ICommand ShowEventsCommand => new Commands.RelayCommand(p => CurrentViewModel = EventsViewModel);
    }
}