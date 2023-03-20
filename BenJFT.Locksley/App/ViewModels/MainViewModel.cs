using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BenJFT.Locksley.Data.Models;
using BenJFT.Locksley.Data.Providers.Interfaces;
using Microsoft.Extensions.Logging;

namespace BenJFT.Locksley.App.ViewModels;

public class MainViewModel : BaseViewModel {
    private readonly IDataProvider<ScoreSheet> _scoreSheetProvider;
    private readonly ILogger<MainViewModel> _log;

    public MainViewModel(
        IDataProvider<ScoreSheet> scoreSheetProvider,
        ILogger<MainViewModel> log) {
        _scoreSheetProvider = scoreSheetProvider;
        _log = log;
        
        _scoreSheetProvider.PropertyChanged += OnScoreSheetsChanged;
    }

    public IEnumerable<ScoreSheet> ScoreSheets => _scoreSheetProvider.GetAll();

    private void OnScoreSheetsChanged(object? sender, PropertyChangedEventArgs args) {
        OnPropertyChanged(nameof(ScoreSheets));
    }

    public async Task NewScoreSheet() {
        _log.LogDebug("Creating new score sheet");
    }
}