using Android.App;
using Android.Runtime;
using BenJFT.Locksley.App;

namespace BenJFT.Locksley.Platforms.Android;

[Application]
public class MainApplication : MauiApplication {
    public MainApplication(nint handle, JniHandleOwnership ownership)
        : base(handle, ownership) { }

    protected override MauiApp CreateMauiApp() {
        return MauiProgram.CreateMauiApp();
    }
}