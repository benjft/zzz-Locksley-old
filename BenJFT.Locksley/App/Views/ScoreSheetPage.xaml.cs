using BenJFT.Locksley.App.ViewModels;

namespace BenJFT.Locksley.App.Views; 

public partial class ScoreSheetPage : ContentPage {
    public ScoreSheetPage(ScoreSheetViewModel viewModel) {
        InitializeComponent();

        BindingContext = viewModel;
    }
}