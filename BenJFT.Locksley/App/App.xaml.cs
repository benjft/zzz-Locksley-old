using BenJFT.Locksley.App.Views;

namespace BenJFT.Locksley.App;

public partial class App {
    public App(ScoreSheetsOverviewPage scoreSheetsOverviewPage) {
        InitializeComponent();

        MainPage = new NavigationPage(scoreSheetsOverviewPage);
    }
}