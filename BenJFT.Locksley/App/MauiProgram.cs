﻿using BenJFT.Locksley.App.Helpers;
using BenJFT.Locksley.Common.Helpers;
using BenJFT.Locksley.Data.Helpers;

namespace BenJFT.Locksley.App;

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
            .AddCommonServices()
            .AddDataServices()
            .AddAppServices();

        return builder.Build();
    }
}