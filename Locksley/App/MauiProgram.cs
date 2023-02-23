using Locksley.App.Helpers;

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
            .AddServices()
            .AddDatabase()
            .AddOtherServices();

        return builder.Build();
    }
}