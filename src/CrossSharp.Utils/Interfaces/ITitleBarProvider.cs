namespace CrossSharp.Utils.Interfaces;

public interface ITitleBarProvider
{
    IntPtr Handle { get; }
    ITitleBar TitleBar { get; }
}
