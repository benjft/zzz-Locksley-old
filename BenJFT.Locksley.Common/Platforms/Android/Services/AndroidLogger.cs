using Java.Lang;
using Microsoft.Extensions.Logging;
using Enum = System.Enum;
using Exception = System.Exception;
using Logcat = Android.Util.Log;

namespace BenJFT.Locksley.Common.Platforms.Android.Services;

public class AndroidLogger : ILogger {
    private readonly string _category;

    public AndroidLogger(string category) {
        _category = category;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter) {
        var message = formatter(state, exception);
        var throwable = exception != null ? Throwable.FromException(exception) : null;

        _ = (logLevel, throwable) switch {
            (LogLevel.Trace, null) => Logcat.Verbose(_category, message),
            (LogLevel.Debug, null) => Logcat.Debug(_category, message),
            (LogLevel.Information, null) => Logcat.Info(_category, message),
            (LogLevel.Warning, null) => Logcat.Warn(_category, message),
            (LogLevel.Error, null) => Logcat.Error(_category, message),
            (LogLevel.Critical, null) => Logcat.Wtf(_category, message),
            (LogLevel.None, null) => Logcat.Verbose(_category, message),

            (LogLevel.Trace, _) => Logcat.Verbose(_category, throwable, message),
            (LogLevel.Debug, _) => Logcat.Debug(_category, throwable, message),
            (LogLevel.Information, _) => Logcat.Info(_category, throwable, message),
            (LogLevel.Warning, _) => Logcat.Warn(_category, throwable, message),
            (LogLevel.Error, _) => Logcat.Error(_category, throwable, message),
            (LogLevel.Critical, _) => Logcat.Wtf(_category, throwable, message),
            (LogLevel.None, _) => Logcat.Verbose(_category, throwable, message),

            _ => throw new IndexOutOfRangeException($"LogLevel {Enum.GetName(logLevel)} didn't match any known value")
        };
    }

    public bool IsEnabled(LogLevel logLevel) {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
        return null;
    }
}