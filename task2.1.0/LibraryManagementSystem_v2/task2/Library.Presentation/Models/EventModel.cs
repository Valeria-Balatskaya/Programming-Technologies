using Library.Logic;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.Presentation.Models
{
    public class EventModel : INotifyPropertyChanged
    {
        private int _id;
        private EventType _type;
        private int? _userId;
        private string _isbn;
        private int? _bookCopyId;
        private DateTime _timestamp;
        private string _description;
        private string _userName;
        private string _bookTitle;

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

        public EventType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int? UserId
        {
            get => _userId;
            set
            {
                if (_userId != value)
                {
                    _userId = value;
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

        public int? BookCopyId
        {
            get => _bookCopyId;
            set
            {
                if (_bookCopyId != value)
                {
                    _bookCopyId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTime Timestamp
        {
            get => _timestamp;
            set
            {
                if (_timestamp != value)
                {
                    _timestamp = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName != value)
                {
                    _userName = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}