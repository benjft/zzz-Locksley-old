using Microsoft.Extensions.Logging;

namespace Locksley.Platforms.Android.Services; 

public class AndroidLoggingProvider : ILoggerProvider {
    public void Dispose() { }

    public ILogger CreateLogger(string categoryName) {
        categoryName = categoryName.Split('.').Last();
        return new AndroidLogger(categoryName);
    }
}