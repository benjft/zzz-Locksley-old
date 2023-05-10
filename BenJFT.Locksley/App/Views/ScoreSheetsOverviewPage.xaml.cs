using BenJFT.Locksley.App.ViewModels;

namespace BenJFT.Locksley.App.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ScoreSheetsOverviewPage {
    public ScoreSheetsOverviewPage(ScoreSheetOverviewViewModel viewModel) {
        InitializeComponent();

        BindingContext = viewModel;

        ScoreSheets.ItemTapped +=
            (o, e) => viewModel.NavigateToScoreSheet(e.ItemIndex);
    }
}