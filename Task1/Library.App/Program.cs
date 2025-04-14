using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library.Data.Entities;
using Library.Data.Implementations;
using Library.Logic;
using Library.Data.Interfaces;

var context = new DataContext();
var repository = new DataRepository(context);
var service = new LibraryService(repository, context); 


service.AddUser(new User { Id = 1, Name = "Alice" });
service.AddBook(new CatalogItem { Id = 101, Title = "C# для профессионалов", Author = "Jon Skeet" });


service.BorrowBook(
    context.Users[0],
    context.Catalog[101]);


Console.WriteLine("Available books:");
foreach (var book in service.GetLibraryState().AvailableBooks) 
{
    Console.WriteLine($"- {book.Title} ({book.Author})");
}

Console.WriteLine("\nEvent log:");
foreach (var e in service.GetEventLog()) 
{
    Console.WriteLine($"[{e.Timestamp}] {e.Type}: {e.User.Name} - {e.Item.Title}");
}