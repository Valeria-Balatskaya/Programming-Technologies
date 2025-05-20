using Library.Data.SqlServer;
using Library.Logic;
using Library.Presentation.Commands;
using Library.Presentation.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Library.Presentation.ViewModels.Tabs
{
    public class EventsViewModel : ViewModelBase
    {
        private readonly ILibraryService _libraryService;
        private EventModel _selectedEvent;

        public EventsViewModel(ILibraryService libraryService)
        {
            _libraryService = libraryService;
            Events = new ObservableCollection<EventModel>();
            LoadEventsCommand = new RelayCommand(p => LoadEvents());

            LoadEvents();
        }

        public ObservableCollection<EventModel> Events { get; }

        public EventModel SelectedEvent
        {
            get => _selectedEvent;
            set => SetProperty(ref _selectedEvent, value);
        }

        public ICommand LoadEventsCommand { get; }

        private void LoadEvents()
        {
            Task.Run(() =>
            {
                var events = _libraryService.GetAllEvents();

                App.Current.Dispatcher.Invoke(() =>
                {
                    Events.Clear();
                    foreach (var evt in events)
                    {
                        string userName = null;
                        string bookTitle = null;

                        if (evt.UserId.HasValue)
                        {
                            var user = _libraryService.GetUserById(evt.UserId.Value);
                            userName = user?.Name;
                        }

                        if (!string.IsNullOrEmpty(evt.ISBN))
                        {
                            var book = _libraryService.GetBookByIsbn(evt.ISBN);
                            bookTitle = book?.Title;
                        }

                        Events.Add(new EventModel
                        {
                            Id = evt.Id,
                            Type = (EventType)evt.Type,
                            UserId = evt.UserId,
                            ISBN = evt.ISBN,
                            BookCopyId = evt.BookCopyId,
                            Timestamp = evt.Timestamp,
                            Description = evt.Description,
                            UserName = userName,
                            BookTitle = bookTitle
                        });
                    }
                });
            });
        }
    }
}