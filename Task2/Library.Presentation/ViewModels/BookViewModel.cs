using Library.Data.Interfaces.Models;
using System.ComponentModel;

namespace Library.Presentation.ViewModels
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private readonly IBook _book;

        public BookViewModel(IBook book)
        {
            _book = book;
        }

        public string ISBN => _book.ISBN;
        
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
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}