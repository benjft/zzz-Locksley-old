using BenJFT.Locksley.App.ViewModels;

namespace BenJFT.Locksley.App.Views; 

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class NewScoreSheetPage : ContentPage {
    public NewScoreSheetPage(NewScoreSheetViewModel viewModel) {
        InitializeComponent();

        BindingContext = viewModel;
    }
}