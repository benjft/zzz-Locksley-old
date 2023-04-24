﻿using BenJFT.Locksley.App;
using Foundation;

namespace BenJFT.Locksley.Platforms.iOS;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate {
    protected override MauiApp CreateMauiApp() {
        return MauiProgram.CreateMauiApp();
    }
}