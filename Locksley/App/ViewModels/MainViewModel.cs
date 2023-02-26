using System.ComponentModel;
using System.Runtime.CompilerServices;
using Locksley.App.Data.Models;
using Locksley.App.Services.Interfaces;

namespace Locksley.App.ViewModels;

public class MainViewModel : INotifyPropertyChanged {
    private readonly IDataProvider<ScoreSheet> _scoreSheetProvider;

    public MainViewModel(IDataProvider<ScoreSheet> scoreSheetProvider) {
        _scoreSheetProvider = scoreSheetProvider;
    }

    public IEnumerable<ScoreSheet> ScoreSheets { get; } = new ScoreSheet[] {
        new() {Title = "Score Sheet 1", CreatedDate = DateTime.Now},
        new() {Title = "Score Sheet 2", CreatedDate = DateTime.Now + TimeSpan.FromDays(5)}
    }; //_scoreSheetProvider.GetAll();

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}