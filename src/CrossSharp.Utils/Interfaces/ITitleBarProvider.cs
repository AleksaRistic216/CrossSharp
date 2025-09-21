namespace CrossSharp.Utils.Interfaces;

public interface ITitleBarProvider : ILocationProvider
{
    IntPtr Handle { get; }
    ITitleBar? TitleBar { get; }
}
