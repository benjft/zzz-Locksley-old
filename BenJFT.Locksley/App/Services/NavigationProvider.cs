using BenJFT.Locksley.App.Services.Interfaces;

namespace BenJFT.Locksley.App.Services;

public class NavigationProvider : INavigationProvider {
    private readonly IServiceProvider _services;

    public NavigationProvider(IServiceProvider services) {
        _services = services;
    }

    private INavigation Navigation => Application.Current?.MainPage?.Navigation ??
                                      throw new NullReferenceException("Failed to instantiate navigation");

    public async Task Navigate<T>() where T : Page => await Navigation.PushAsync(_services.GetRequiredService<T>());

    public async Task Return() => await Navigation.PopAsync();
}