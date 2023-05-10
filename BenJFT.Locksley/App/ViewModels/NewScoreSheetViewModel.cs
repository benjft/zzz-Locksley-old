using System.Windows.Input;
using BenJFT.Locksley.App.Services.Interfaces;
using BenJFT.Locksley.App.Views;
using BenJFT.Locksley.Data.Models;
using BenJFT.Locksley.Data.Providers.Interfaces;

namespace BenJFT.Locksley.App.ViewModels;

// ReSharper disable once ClassNeverInstantiated.Global
public class NewScoreSheetViewModel : BaseViewModel {
    private readonly INavigationProvider _navigation;

    private readonly ScoreSheet _scoreSheet;
    private readonly IDataProvider<ScoreSheet> _scoreSheetProvider;

    private ICommand? _cmdCreateScoreSheet;

    public NewScoreSheetViewModel(
        IDataProvider<ScoreSheet> scoreSheetProvider,
        INavigationProvider navigation) {
        _scoreSheetProvider = scoreSheetProvider;
        _navigation = navigation;

        _scoreSheet = scoreSheetProvider.New();
    }

    public string Title {
        get => _scoreSheet.Title;
        set {
            _scoreSheet.Title = value;
            OnPropertyChanged();
        }
    }

    public ICommand CmdCreateScoreSheet => _cmdCreateScoreSheet ??= new Command(CreateScoreSheet);

    private async void CreateScoreSheet() {
        _scoreSheetProvider.Save(_scoreSheet);
        await _navigation.InPlace<ScoreSheetPage>(p => p.ViewModel.ScoreSheet = _scoreSheet);
    }
}