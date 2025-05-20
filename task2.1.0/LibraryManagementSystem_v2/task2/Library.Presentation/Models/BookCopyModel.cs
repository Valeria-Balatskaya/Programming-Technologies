using Library.Logic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.Presentation.Models
{
    public class BookCopyModel : INotifyPropertyChanged
    {
        private int _id;
        private string _isbn;
        private BookStatus _status;
        private DateTime _acquisitionDate;
        private string _location;
        private int? _currentBorrowerId;
        private DateTime? _dueDate;
        private string _bookTitle;
        private string _borrowerName;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ISBN
        {
            get => _isbn;
            set
            {
                if (_isbn != value)
                {
                    _isbn = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public BookStatus Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime AcquisitionDate
        {
            get => _acquisitionDate;
            set
            {
                if (_acquisitionDate != value)
                {
                    _acquisitionDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? CurrentBorrowerId
        {
            get => _currentBorrowerId;
            set
            {
                if (_currentBorrowerId != value)
                {
                    _currentBorrowerId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                if (_dueDate != value)
                {
                    _dueDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string BookTitle
        {
            get => _bookTitle;
            set
            {
                if (_bookTitle != value)
                {
                    _bookTitle = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string BorrowerName
        {
            get => _borrowerName;
            set
            {
                if (_borrowerName != value)
                {
                    _borrowerName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}