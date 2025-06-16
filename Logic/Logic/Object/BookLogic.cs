using Logic.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Logic.Object
{
    internal class BookLogic:IBookLogic
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime Year { get; set; }
        public Guid Guid { get; set; }
        public int Pages { get; set; }
        public bool IsAvailable { get; set; }
        public Guid OwnerId { get; set; }
        public BookLogic(string title, string author, string genre, DateTime year, string isbn, int pages)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Guid = Guid.NewGuid();
            Isbn = isbn;
            Pages = pages;
            IsAvailable = true;
            OwnerId = Guid.Empty;
        }

        public BookLogic(string title, string author, string genre, DateTime year, string isbn, int pages, Guid guid)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Guid = guid;
            Isbn = isbn;
            Pages = pages;
            IsAvailable = true;
            OwnerId = Guid.Empty;
        }

        public BookLogic(string title, string author, string genre, DateTime year, string isbn, int pages, Guid guid, Guid ownerGuid)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Year = year;
            Guid = guid;
            Isbn = isbn;
            Pages = pages;
            IsAvailable = true;
            OwnerId = ownerGuid;

        }

        public void SetAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }

        public void SetOwner(Guid ownerId)
        {
            OwnerId = ownerId;
        }
    }
}
