using BenJFT.Locksley.Data.Models;

namespace BenJFT.Locksley.App.ViewModels;

public class ScoreSheetViewModel : BaseViewModel {
    private ScoreSheet? _scoreSheet;

    public string Title {
        get => _scoreSheet?.Title ?? "";
        set {
            ScoreSheet.Title = value;
            OnPropertyChanged();
        }
    }

    public ScoreSheet ScoreSheet {
        get => _scoreSheet ?? throw new NullReferenceException("ScoreSheet not properly initialized");
        set {
            if (_scoreSheet != null) throw new Exception("This ViewModel already wraps a ScoreSheet.");

            SetField(ref _scoreSheet, value);
        }
    }
}