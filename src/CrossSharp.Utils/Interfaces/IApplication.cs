namespace CrossSharp.Utils.Interfaces;

public interface IApplication
{
    IntPtr MainWindowHandle { get; internal set; }
    bool DevelopersMode { get; set; }
    EventHandler? DevelopersModeChanged { get; set; }
}
