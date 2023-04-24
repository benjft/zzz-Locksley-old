using BenJFT.Locksley.App;
using Foundation;

namespace BenJFT.Locksley.Platforms.MacCatalyst;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate {
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}