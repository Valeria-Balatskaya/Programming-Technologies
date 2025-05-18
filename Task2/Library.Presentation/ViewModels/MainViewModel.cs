using System.Collections.ObjectModel;
using System.ComponentModel;
using Library.Logic.Interfaces;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly ILibraryService _service;
    private ObservableCollection<BookViewModel> _books;
    
    public ObservableCollection<BookViewModel> Books
    {
        get => _books;
        set
        {
            _books = value;
            OnPropertyChanged(nameof(Books));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MainViewModel(ILibraryService service)
    {
        _service = service;
        LoadData();
    }

    private void LoadData()
    {
        Books = new ObservableCollection<BookViewModel>(
            _service.GetAllBooks().Select(b => new BookViewModel(b))
        );
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}