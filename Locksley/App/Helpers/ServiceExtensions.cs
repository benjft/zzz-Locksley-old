using System.Reflection;
using Locksley.App.Attributes;
using Locksley.App.Data;
using Locksley.App.Services;
using Locksley.App.ViewModels;
using Locksley.App.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

#if ANDROID
using Locksley.Platforms.Android.Services;
#endif

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
        var servicesNamespace = typeof(NavigationService).Namespace;
        var classes = ReflectionHelper.GetAllClassesInNamespace(servicesNamespace);

        var clsInterfaces = classes.ToDictionary(
            c => c, 
            c => c.GetInterfaces()
                .Where(i => i.GetCustomAttribute<ServiceLifetimeAttribute>() != null)
            );

        foreach (var (cls, interfaces) in clsInterfaces) {
            var itfArr = interfaces as Type[] ?? interfaces.ToArray();
            
            var serviceLifetime =
                cls.GetCustomAttribute<ServiceLifetimeAttribute>()?.Lifetime ??
                itfArr.Select(j => j.GetCustomAttribute<ServiceLifetimeAttribute>()).Min(lta => lta?.Lifetime) ?? 
                ServiceLifetime.Transient;

            Func<IServiceProvider, object>? clsFactory = null;
            if (itfArr.Length > 1 || cls.GetCustomAttribute<ServiceLifetimeAttribute>() != null) {
                services.RegisterService(serviceLifetime, cls);
                clsFactory = x => x.GetRequiredService(cls);
            }
            
            foreach (var itf in itfArr) {
                if (clsFactory != null) {
                    services.AddTransient(itf, clsFactory);
                } else {
                    services.RegisterService(serviceLifetime, cls, itf);
                }
            }
        }
        
        return services;
    }

    private static void RegisterService(this IServiceCollection services, ServiceLifetime lifetime, Type cls, Type? itf = null) {
        itf ??= cls;
        _ = lifetime switch {
            ServiceLifetime.Singleton => services.AddSingleton(itf, cls),
            ServiceLifetime.Scoped => services.AddScoped(itf, cls),
            ServiceLifetime.Transient => services.AddTransient(itf, cls),
            _ => services.AddTransient(itf, cls)
        };
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