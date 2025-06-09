using Library.Logic;
using Library.Presentation.Models;
using Library.Presentation.ViewModels.Tabs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Library.Presentation.Tests
{
    [TestClass]
    public class BooksViewModelTests
    {
        [TestMethod]
        public void LoadBooks_PopulatesBooksList()
        {
            ILibraryService mockService = new MockLibraryService();
            var viewModel = new BooksViewModel(mockService);

            Assert.AreEqual(2, viewModel.Books.Count);
            Assert.IsTrue(viewModel.Books.Any(b => b.ISBN == "978-0-061-12241-5"));
            Assert.IsTrue(viewModel.Books.Any(b => b.ISBN == "978-0-743-27325-1"));
        }

        [TestMethod]
        public void NewBook_CreatesNewBookAndEnablesEditing()
        {
            ILibraryService mockService = new MockLibraryService();
            var viewModel = new BooksViewModel(mockService);

            viewModel.NewBookCommand.Execute(null);

            Assert.IsTrue(viewModel.IsEditing);
            Assert.IsTrue(viewModel.IsNewBook);
            Assert.IsNotNull(viewModel.SelectedBook);
        }

        [TestMethod]
        public void EditBook_EnablesEditingForSelectedBook()
        {
            ILibraryService mockService = new MockLibraryService();
            var viewModel = new BooksViewModel(mockService);

            Console.WriteLine($"Test - Books count: {viewModel.Books.Count}");

            Assert.IsTrue(viewModel.Books.Any(), "No books were loaded from the mock service");

            viewModel.SelectedBook = viewModel.Books.First();

            viewModel.EditBookCommand.Execute(null);

            Assert.IsTrue(viewModel.IsEditing);
            Assert.IsFalse(viewModel.IsNewBook);
        }
    }
}