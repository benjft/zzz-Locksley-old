using Locksley.App.Services.Interfaces;

namespace Locksley.App.Services;

public class NavigationService : INavigationService {
    private readonly IServiceProvider _services;

    public NavigationService(IServiceProvider services) {
        _services = services;
    }

    private INavigation? Navigation => Application.Current?.MainPage?.Navigation;
}