using System.ComponentModel;
using Library.Data.Interfaces.Models;

public class BookViewModel : INotifyPropertyChanged
{
    private readonly IBook _book;
    
    public string Title
    {
        get => _book.Title;
        set
        {
            _book.Title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public BookViewModel(IBook book) => _book = book;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}