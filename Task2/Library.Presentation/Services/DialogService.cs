using System.Windows;

namespace Library.Presentation.Services
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string title) 
            => MessageBox.Show(message, title);

        public bool Confirm(string message, string title) 
            => MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.Yes;
    }
}