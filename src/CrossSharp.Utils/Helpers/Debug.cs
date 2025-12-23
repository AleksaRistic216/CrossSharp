using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

public enum LogCategory
{
    App,
    SDL,
    DI,
    Input,
    Form,
    Theme,
    Cache,
}

public static class Debug
{
    static readonly object _lock = new();
    static string? _logFilePath;
    static bool _initialized;

    static void EnsureInitialized()
    {
        if (_initialized)
            return;

        lock (_lock)
        {
            if (_initialized)
                return;

            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string appName = "CrossSharp";
            if (Services.IsRegistered<IApplicationConfiguration>())
            {
                var config = Services.GetSingleton<IApplicationConfiguration>();
                appName = config.ApplicationName;
            }

            string logDir = Path.Combine(appDataPath, appName, "logs");
            Directory.CreateDirectory(logDir);

            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            _logFilePath = Path.Combine(logDir, $"debug_{timestamp}.log");

            _initialized = true;
        }
    }

    public static void Log(string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string formattedMessage = $"[{timestamp}] {message}";

        Console.WriteLine(formattedMessage);

        WriteToFile(formattedMessage);
    }

    public static void Log(LogCategory category, string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string formattedMessage = $"[{timestamp}] [{category}] {message}";

        Console.WriteLine(formattedMessage);

        WriteToFile(formattedMessage);
    }

    public static void LogWarning(string message)
    {
        LogInternal("WARNING", message);
    }

    public static void LogError(string message)
    {
        LogInternal("ERROR", message);
    }

    public static void LogError(Exception ex)
    {
        LogInternal("ERROR", $"{ex.Message}\n{ex.StackTrace}");
    }

    public static void LogError(string message, Exception ex)
    {
        LogInternal("ERROR", $"{message}: {ex.Message}\n{ex.StackTrace}");
    }

    static void LogInternal(string category, string message)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        string formattedMessage = $"[{timestamp}] [{category}] {message}";

        Console.WriteLine(formattedMessage);

        WriteToFile(formattedMessage);
    }

    static void WriteToFile(string message)
    {
        try
        {
            EnsureInitialized();

            if (_logFilePath is null)
                return;

            lock (_lock)
            {
                File.AppendAllText(_logFilePath, message + Environment.NewLine);
            }
        }
        catch
        {
            // Silently fail if we can't write to file
        }
    }

    public static string? GetLogFilePath()
    {
        EnsureInitialized();
        return _logFilePath;
    }
}
