using Library.Data.Interfaces.Models;
using System.ComponentModel;

namespace Library.Presentation.Models
{
    public class ObservableBook : INotifyPropertyChanged
    {
        private readonly IBook _book;

        public ObservableBook(IBook book) => _book = book;
        public IBook GetBook() => _book;

        public string ISBN => _book.ISBN;
        
        public string Title
        {
            get => _book.Title;
            set { _book.Title = value; OnPropertyChanged(); }
        }

        public string Author
        {
            get => _book.Author;
            set { _book.Author = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}