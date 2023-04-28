using BenJFT.Locksley.Data.Models;
using BenJFT.Locksley.Data.Providers;
using BenJFT.Locksley.Data.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BenJFT.Locksley.Data.Helpers;

public static class ServiceExtensions {
    public static IServiceCollection AddDataServices(this IServiceCollection services) {
        services.AddDatabase();
        services.AddSingleton<IDataProvider<ScoreSheet>, ScoreSheetDataProvider>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services) {
        return services.AddDbContext<LocksleyDbContext>(options =>
            options.UseLazyLoadingProxies()
                .UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, "LocksleyData.sqlite")}")
        );
    }
}