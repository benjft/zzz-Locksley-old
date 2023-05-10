using BenJFT.Locksley.App.Services.Interfaces;

namespace BenJFT.Locksley.App.Services;

public class NavigationProvider : INavigationProvider {
    private readonly IServiceProvider _services;

    public NavigationProvider(IServiceProvider services) {
        _services = services;
    }

    private static INavigation Navigation => Application.Current?.MainPage?.Navigation ??
                                             throw new NullReferenceException("Failed to instantiate navigation");

    public async Task<T> Push<T>(Action<T>? onCreate = null) where T : Page {
        var page = _services.GetRequiredService<T>();
        onCreate?.Invoke(page);
        await Navigation.PushAsync(page, true);
        return page;
    }

    public async Task<T> InPlace<T>(Action<T>? onCreate = null) where T : Page {
        var currentPage = Navigation.NavigationStack[^1];
        var newPage = await Push(onCreate);
        Navigation.RemovePage(currentPage);

        return newPage;
    }

    public async Task Pop() => await Navigation.PopAsync();

    public void Remove(Page page) => Navigation.RemovePage(page);

    public void RemoveLast<T>() where T : Page => Navigation.RemovePage(Navigation.NavigationStack.OfType<T>().Last());
}