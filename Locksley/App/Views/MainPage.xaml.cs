using Locksley.App.ViewModels;

namespace Locksley.App.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class MainPage {
    public MainPage(MainViewModel viewModel) {
        InitializeComponent();

        BindingContext = viewModel;
    }
}