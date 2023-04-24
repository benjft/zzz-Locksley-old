#if ANDROID
using BenJFT.Locksley.Common.Platforms.Android.Services;
#endif
using Microsoft.Extensions.Logging;

namespace BenJFT.Locksley.Common.Helpers; 

public static class ServiceExtensions {
    public static IServiceCollection AddCommonServices(this IServiceCollection services) {
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