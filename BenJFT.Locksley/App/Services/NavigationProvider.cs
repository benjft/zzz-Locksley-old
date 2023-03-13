using BenJFT.Locksley.App.Services.Interfaces;

namespace BenJFT.Locksley.App.Services;

public class NavigationProvider : INavigationProvider {
    private readonly IServiceProvider _services;

    public NavigationProvider(IServiceProvider services) {
        _services = services;
    }

    private INavigation? Navigation => Application.Current?.MainPage?.Navigation;
}