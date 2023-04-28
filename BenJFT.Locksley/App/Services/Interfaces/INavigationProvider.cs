namespace BenJFT.Locksley.App.Services.Interfaces;

public interface INavigationProvider {
    Task<T> Push<T>(Action<T>? onCreate = null) where T : Page;
    Task<T> InPlace<T>(Action<T>? onCreate = null) where T : Page;
    Task Pop();
    void Remove(Page page);
    void RemoveLast<T>() where T : Page;
}