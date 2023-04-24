using System.Reflection;
using BenJFT.Locksley.App.Services;
using BenJFT.Locksley.App.Services.Interfaces;
using BenJFT.Locksley.App.ViewModels;
using BenJFT.Locksley.Common.Helpers;

namespace BenJFT.Locksley.App.Helpers;

public static class ServiceExtensions {
    private static void AddViews(IServiceCollection services) {
        var viewTypes = Assembly.GetCallingAssembly().GetSubClassesOf<Page>();

        foreach (var viewType in viewTypes)
            services.AddTransient(viewType);
    }

    private static void AddViewModels(IServiceCollection services) {
        var viewModelTypes = Assembly.GetCallingAssembly().GetSubClassesOf<BaseViewModel>();

        foreach (var viewModelType in viewModelTypes)
            services.AddTransient(viewModelType);
    }

    public static IServiceCollection AddAppServices(this IServiceCollection services) {
        AddViews(services);
        AddViewModels(services);

        services.AddSingleton<INavigationProvider, NavigationProvider>();
        
        return services;
    }
}