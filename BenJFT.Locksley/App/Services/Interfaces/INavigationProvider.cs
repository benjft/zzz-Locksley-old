namespace BenJFT.Locksley.App.Services.Interfaces;

public interface INavigationProvider {
    Task Navigate<T>() where T : Page;
}