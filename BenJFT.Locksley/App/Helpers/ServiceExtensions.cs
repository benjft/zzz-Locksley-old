using System.Reflection;
using BenJFT.Locksley.App.ViewModels;
using BenJFT.Locksley.Common.Helpers;

namespace BenJFT.Locksley.App.Helpers;

public static class ServiceExtensions {
    public static IServiceCollection AddViews(this IServiceCollection services) {
        var viewTypes = Assembly.GetCallingAssembly().GetSubClassesOf<Page>();

        foreach (var viewType in viewTypes) services.AddTransient(viewType);

        return services;
    }

    public static IServiceCollection AddViewModels(this IServiceCollection services) {
        var viewModelTypes = Assembly.GetCallingAssembly().GetSubClassesOf<BaseViewModel>();

        foreach (var viewModelType in viewModelTypes)
            services.AddTransient(viewModelType);

        return services;
    }
}