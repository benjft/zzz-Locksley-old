using BenJFT.Locksley.App.Services.Interfaces;

namespace BenJFT.Locksley.App.Services;

public class NavigationProvider : INavigationProvider {
    private readonly IServiceProvider _services;

    public NavigationProvider(IServiceProvider services) {
        _services = services;
    }

    private INavigation? Navigation => Application.Current?.MainPage?.Navigation;

    public async Task Navigate<T>() where T : Page {
        if (Navigation == null) {
            throw new NullReferenceException($"Failed to navigate to {typeof(T).Name} as navigation is not properly initialised");
        }
        
        await Navigation.PushAsync(_services.GetRequiredService<T>());
    }
}