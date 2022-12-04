using Locksley.App.ViewModels;

namespace Locksley.App.Views;

public partial class MainPage {

    public MainPage(MainViewModel viewModel) {
        InitializeComponent();

        BindingContext = viewModel;
    }

}