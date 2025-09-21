namespace CrossSharp.Utils.Interfaces;

public interface IApplication
{
    Type? MainFormType { get; set; }
    IntPtr MainWindowHandle { get; internal set; }
    bool DevelopersMode { get; set; }
    EventHandler? DevelopersModeChanged { get; set; }
    void SetTheme(ITheme theme);
    void Start();
}
