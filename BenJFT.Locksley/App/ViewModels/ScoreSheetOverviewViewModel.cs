using System.ComponentModel;
using System.Windows.Input;
using BenJFT.Locksley.App.Services.Interfaces;
using BenJFT.Locksley.App.Views;
using BenJFT.Locksley.Data.Models;
using BenJFT.Locksley.Data.Providers.Interfaces;
using Microsoft.Extensions.Logging;

namespace BenJFT.Locksley.App.ViewModels;

public class ScoreSheetOverviewViewModel : BaseViewModel {
    private readonly ILogger<ScoreSheetOverviewViewModel> _log;
    private readonly INavigationProvider _navigationProvider;
    private readonly IDataProvider<ScoreSheet> _scoreSheetProvider;

    private ICommand? _cmdNewScoreSheet;

    private List<ScoreSheet>? _scoreSheets;

    public ScoreSheetOverviewViewModel(
        IDataProvider<ScoreSheet> scoreSheetProvider,
        ILogger<ScoreSheetOverviewViewModel> log,
        INavigationProvider navigationProvider) {
        _scoreSheetProvider = scoreSheetProvider;
        _log = log;
        _navigationProvider = navigationProvider;

        _scoreSheetProvider.PropertyChanged += OnScoreSheetsChanged;
    }

    public ICommand CmdNewScoreSheet => _cmdNewScoreSheet ??= new Command(CreateNewScoreSheet);

    public IEnumerable<ScoreSheet> ScoreSheets {
        get => _scoreSheets ??= _scoreSheetProvider.GetAll().ToList();
        private set {
            _scoreSheets = value.ToList();
            OnPropertyChanged();
        }
    }

    private void OnScoreSheetsChanged(object? sender, PropertyChangedEventArgs args) =>
        ScoreSheets = _scoreSheetProvider.GetAll();

    private async void CreateNewScoreSheet() =>
        await _navigationProvider.Push<NewScoreSheetPage>();

    public void NavigateToScoreSheet(int index) {
        if (_scoreSheets == null || index < 0 || index > _scoreSheets.Count)
            throw new IndexOutOfRangeException();

        var scoreSheet = _scoreSheets[index];
        _navigationProvider.Push<ScoreSheetPage>(x => x.ViewModel.ScoreSheet = scoreSheet);
    }
}