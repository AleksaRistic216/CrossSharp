using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
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
    static readonly ConcurrentQueue<string> _messageQueue = new();
    static readonly Thread _writerThread;
    static readonly AutoResetEvent _signal = new(false);
    static volatile bool _running = true;
    static string? _logFilePath;
    static volatile bool _initialized;
    static StreamWriter? _writer;

    // Pre-cached category names to avoid Enum.ToString() allocations
    static readonly string[] CategoryNames = ["App", "SDL", "DI", "Input", "Form", "Theme", "Cache"];

    static Debug()
    {
        _writerThread = new Thread(ProcessQueue)
        {
            IsBackground = true,
            Name = "DebugLogWriter",
            Priority = ThreadPriority.BelowNormal,
        };
        _writerThread.Start();

        AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
    }

    static void OnProcessExit(object? sender, System.EventArgs e)
    {
        Flush();
    }

    static void EnsureInitialized()
    {
        if (_initialized)
            return;

        lock (_messageQueue)
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

            try
            {
                _writer = new StreamWriter(_logFilePath, append: true, Encoding.UTF8) { AutoFlush = false };
            }
            catch
            {
                // Silently fail if we can't create the log file
            }

            _initialized = true;
        }
    }

    public static void Log(string message)
    {
        var timestamp = DateTime.Now;
        var sb = new StringBuilder(64 + message.Length);
        AppendTimestamp(sb, timestamp);
        sb.Append("] ");
        sb.Append(message);

        EnqueueMessage(sb.ToString());
    }

    public static void Log(LogCategory category, string message)
    {
        var timestamp = DateTime.Now;
        var categoryName = CategoryNames[(int)category];

        var sb = new StringBuilder(64 + categoryName.Length + message.Length);
        AppendTimestamp(sb, timestamp);
        sb.Append("] [");
        sb.Append(categoryName);
        sb.Append("] ");
        sb.Append(message);

        EnqueueMessage(sb.ToString());
    }

    public static void LogWarning(string message)
    {
        var timestamp = DateTime.Now;
        var sb = new StringBuilder(64 + message.Length);
        AppendTimestamp(sb, timestamp);
        sb.Append("] [WARNING] ");
        sb.Append(message);

        EnqueueMessage(sb.ToString());
    }

    public static void LogError(string message)
    {
        var timestamp = DateTime.Now;
        var sb = new StringBuilder(64 + message.Length);
        AppendTimestamp(sb, timestamp);
        sb.Append("] [ERROR] ");
        sb.Append(message);

        EnqueueMessage(sb.ToString());
    }

    public static void LogError(Exception ex)
    {
        LogError($"{ex.Message}\n{ex.StackTrace}");
    }

    public static void LogError(string message, Exception ex)
    {
        LogError($"{message}: {ex.Message}\n{ex.StackTrace}");
    }

    static void AppendTimestamp(StringBuilder sb, DateTime timestamp)
    {
        sb.Append('[');
        sb.Append(timestamp.Year);
        sb.Append('-');
        Append2Digits(sb, timestamp.Month);
        sb.Append('-');
        Append2Digits(sb, timestamp.Day);
        sb.Append(' ');
        Append2Digits(sb, timestamp.Hour);
        sb.Append(':');
        Append2Digits(sb, timestamp.Minute);
        sb.Append(':');
        Append2Digits(sb, timestamp.Second);
        sb.Append('.');
        Append3Digits(sb, timestamp.Millisecond);
    }

    static void Append2Digits(StringBuilder sb, int value)
    {
        if (value < 10)
            sb.Append('0');
        sb.Append(value);
    }

    static void Append3Digits(StringBuilder sb, int value)
    {
        if (value < 10)
            sb.Append("00");
        else if (value < 100)
            sb.Append('0');
        sb.Append(value);
    }

    static void EnqueueMessage(string message)
    {
        Console.WriteLine(message);
        _messageQueue.Enqueue(message);
        _signal.Set();
    }

    static void ProcessQueue()
    {
        while (_running || !_messageQueue.IsEmpty)
        {
            _signal.WaitOne(100); // Wake up every 100ms or when signaled

            WriteBufferedMessages();
        }

        // Final flush
        WriteBufferedMessages();
        _writer?.Dispose();
    }

    static void WriteBufferedMessages()
    {
        if (_messageQueue.IsEmpty)
            return;

        try
        {
            EnsureInitialized();

            if (_writer is null)
                return;

            while (_messageQueue.TryDequeue(out var message))
            {
                _writer.WriteLine(message);
            }

            _writer.Flush();
        }
        catch
        {
            // Silently fail
        }
    }

    public static void Flush()
    {
        _running = false;
        _signal.Set();
        _writerThread.Join(1000); // Wait up to 1 second for flush
    }

    public static string? GetLogFilePath()
    {
        EnsureInitialized();
        return _logFilePath;
    }

    public static void LogStack()
    {
        var sb = new StringBuilder();
        var stackTrace = new StackTrace(true);

        sb.AppendLine("=== Current Stack Trace ===");
        foreach (var frame in stackTrace.GetFrames())
        {
            var method = frame.GetMethod();
            string? fileName = frame.GetFileName();
            int lineNumber = frame.GetFileLineNumber();

            sb.AppendLine($"{method?.DeclaringType}.{method?.Name} in {fileName}:{lineNumber}");
        }
        sb.AppendLine("===========================");

        EnqueueMessage(sb.ToString());
    }
}
