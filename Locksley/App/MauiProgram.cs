using Locksley.App.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
#if ANDROID
using Locksley.Platforms.Android.Services;
#endif

namespace Locksley.App;

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            .AddViews()
            .AddViewModels()
            .AddServices();

#if ANDROID
        builder.Services.AddLogging(configure => {
  #if DEBUG
            var logLevel = LogLevel.Debug;
  #else
            var logLevel = LogLevel.Information;
  #endif
            configure.AddProvider(new AndroidLoggingProvider())
                .AddFilter("Locksley", logLevel);
        });
#else
        builder.Services.AddLogging(configure => {
            configure.AddDebug();
            configure.AddConsole();
        });
#endif

        return builder.Build();
    }
}