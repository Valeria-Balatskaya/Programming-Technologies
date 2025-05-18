using System.ComponentModel;
using Library.Data.Interfaces.Models;

namespace Library.Presentation.ViewModels
{
    public class BookViewModel : INotifyPropertyChanged
    {
        private readonly IBook _book;

        public string ISBN => _book.ISBN;
        public string Title => _book.Title;
        public string Author => _book.Author;

        public event PropertyChangedEventHandler? PropertyChanged;

        public BookViewModel(IBook book) => _book = book;
    }
}