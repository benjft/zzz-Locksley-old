using System.Reflection;
using Locksley.App.Attributes;
using Locksley.App.Data;
using Locksley.App.Services;
using Locksley.App.Services.Interfaces;
using Locksley.App.ViewModels;
using Locksley.App.Views;
#if ANDROID
using Locksley.Platforms.Android.Services;
#endif
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
                v.GetCustomAttribute<ServiceLifetimeAttribute>() ??
                k.GetCustomAttribute<ServiceLifetimeAttribute>();
            switch (serviceAttribute?.Lifetime) {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(k, v);
                    break;
                case ServiceLifetime.Scoped:
                    services.AddScoped(k, v);
                    break;
                case ServiceLifetime.Transient:
                case null:
                default:
                    services.AddTransient(k, v);
                    break;
            }
        }

        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services) {
        return services.AddDbContext<LocksleyDbContext>(options =>
            options.UseLazyLoadingProxies()
                .UseSqlite($"Filename={Constants.DatabasePath}")
        );
    }

    public static IServiceCollection AddOtherServices(this IServiceCollection services) {
        
        #if ANDROID
        services.AddLogging(configure => {
            #if DEBUG
            const LogLevel logLevel = LogLevel.Debug;
            #else
            const LogLevel logLevel = LogLevel.Information;
            #endif
            configure.AddProvider(new AndroidLoggingProvider())
                .AddFilter((_, l) => l >= logLevel);
        });
        #else
        services.AddLogging(configure => {
            configure.AddDebug();
            configure.AddConsole();
        });
        #endif
        return services;
    }
}