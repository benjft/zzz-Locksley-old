using BenJFT.Locksley.App.ViewModels;

namespace BenJFT.Locksley.App.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage {

    protected MainViewModel _viewModel;
    public MainPage(MainViewModel viewModel) {
        InitializeComponent();

        BindingContext = _viewModel = viewModel;
    }
    
    private async void Button_NewScoreSheet(object? sender, EventArgs e) {
        await _viewModel.NewScoreSheet();
    }
}