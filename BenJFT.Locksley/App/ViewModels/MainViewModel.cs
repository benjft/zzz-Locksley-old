using System.ComponentModel;
using System.Windows.Input;
using BenJFT.Locksley.App.Services.Interfaces;
using BenJFT.Locksley.App.Views;
using BenJFT.Locksley.Data.Models;
using BenJFT.Locksley.Data.Providers.Interfaces;
using Microsoft.Extensions.Logging;

namespace BenJFT.Locksley.App.ViewModels;

public class MainViewModel : BaseViewModel {
    private readonly IDataProvider<ScoreSheet> _scoreSheetProvider;
    private readonly ILogger<MainViewModel> _log;
    private readonly INavigationProvider _navigationProvider;

    private ICommand? _cmdNewScoreSheet;
    public ICommand CmdNewScoreSheet => _cmdNewScoreSheet ??= new Command(CreateNewScoreSheet);

    public MainViewModel(
        IDataProvider<ScoreSheet> scoreSheetProvider,
        ILogger<MainViewModel> log,
        INavigationProvider navigationProvider) {
        _scoreSheetProvider = scoreSheetProvider;
        _log = log;
        _navigationProvider = navigationProvider;

        _scoreSheetProvider.PropertyChanged += OnScoreSheetsChanged;
    }

    public IEnumerable<ScoreSheet> ScoreSheets => _scoreSheetProvider.GetAll();

    private void OnScoreSheetsChanged(object? sender, PropertyChangedEventArgs args) {
        OnPropertyChanged(nameof(ScoreSheets));
    }
 
    private async void CreateNewScoreSheet() {
        await _navigationProvider.Navigate<NewScoreSheetPage>();
    }
}