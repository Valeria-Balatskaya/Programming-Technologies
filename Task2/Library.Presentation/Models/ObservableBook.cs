using System.ComponentModel;

public class ObservableBook : INotifyPropertyChanged
{
    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}