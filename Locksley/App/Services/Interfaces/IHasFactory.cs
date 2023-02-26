namespace Locksley.App.Services.Interfaces;

public interface IHasFactory<out T> {
    public static abstract T CreateNewInstance(IServiceProvider services);
}