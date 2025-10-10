namespace CrossSharp.Diagnostics;

public static class Ui
{
    static int LastReturned { get; set; }
    internal static DateTime LastRead { get; set; } = DateTime.MinValue;
    internal static int FrameCount { get; set; }
    public static int TicksPerSecond
    {
        get
        {
            var now = DateTime.UtcNow;
            var diff = now - LastRead;
            if (diff.TotalSeconds < 1)
                return LastReturned;
            LastReturned = (int)(FrameCount / diff.TotalSeconds);
            LastRead = now;
            FrameCount = 0;
            return LastReturned;
        }
    }
}
