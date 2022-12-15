using System.Reflection;
using Locksley.App.Attributes;
using Locksley.App.Services;
using Locksley.App.Services.Interfaces;
using Locksley.App.ViewModels;
using Locksley.App.Views;

namespace Locksley.App.Helpers;

public static class ServiceExtensions {
    public static IServiceCollection AddViews(this IServiceCollection services) {
        var viewsNamespace = typeof(MainPage).Namespace;
        var viewTypes = ReflectionHelper.GetAllSubclassesInNamespace<Page>(viewsNamespace);

        foreach (var viewType in viewTypes) services.AddTransient(viewType);

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services) {
        var viewModelsNamespace = typeof(MainViewModel).Namespace;
        var viewModelTypes = ReflectionHelper.GetAllClassesInNamespace(viewModelsNamespace);

        foreach (var viewModelType in viewModelTypes) services.AddTransient(viewModelType);

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services) {
        var serviceInterfacesNamespace = typeof(INavigationService).Namespace;
        var interfaces = ReflectionHelper.GetAllInterfacesInNamespace(serviceInterfacesNamespace);

        var servicesNamespace = typeof(NavigationService).Namespace;
        var classes = ReflectionHelper.GetAllClassesInNamespace(servicesNamespace);

        var implementations = interfaces.ToDictionary(i => i, i => classes.Single(i.IsAssignableFrom));

        foreach (var (k, v) in implementations) {
            var serviceAttribute =
                v.GetCustomAttribute<ServiceAttribute>() ??
                k.GetCustomAttribute<ServiceAttribute>();
            switch (serviceAttribute?.Lifetime) {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(k, v);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(k, v);
                    break;
                case null:
                case ServiceLifetime.Transient:
                default:
                    services.AddTransient(k, v);
                    break;
            }
        }

        return services;
    }
}