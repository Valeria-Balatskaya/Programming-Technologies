namespace Library.Presentation.Services
{
    public interface IDialogService
    {
        void ShowMessage(string message, string title);
        bool Confirm(string message, string title);
    }
}