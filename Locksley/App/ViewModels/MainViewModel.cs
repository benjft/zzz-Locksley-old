using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Locksley.App.Data;
using Locksley.App.Data.Models;

namespace Locksley.App.ViewModels;

public class MainViewModel : INotifyPropertyChanged {

    private readonly LocksleyDbContext _dbContext;
    public MainViewModel(LocksleyDbContext dbContext) {
        _dbContext = dbContext;
    }

    public ObservableCollection<ScoreSheet> ScoreSheets { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) {
            return false;
        }
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}