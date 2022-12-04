using Locksley.App.Services.Interfaces;

namespace Locksley.App.Services; 

public class NavigationService : INavigationService {
    private readonly IServiceProvider _services;
    
    private INavigation? Navigation => Application.Current?.MainPage?.Navigation;

    public NavigationService(IServiceProvider services) {
        _services = services;
    }
}