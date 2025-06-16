using Library.Presentation.Model.Interfaces;


namespace Library.Presentation.Model.Obejcts
{
    internal class BookModel: IBookModel
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
        public BookModel(string title, string author, string genre, DateTime year, string isbn, int pages)
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

        public BookModel(string title, string author, string genre, DateTime year, string isbn, int pages, Guid guid)
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

        public BookModel(string title, string author, string genre, DateTime year, string isbn, int pages, Guid guid, Guid ownerGuid)
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
