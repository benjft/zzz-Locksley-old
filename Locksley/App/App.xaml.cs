using Locksley.App.Services.Interfaces;
using Locksley.App.Views;

namespace Locksley.App;

public partial class App {
    public App(MainPage mainPage, IDataProvider dataProvider) {
        InitializeComponent();

        MainPage = mainPage;
    }
}