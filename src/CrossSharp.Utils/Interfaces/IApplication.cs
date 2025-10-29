namespace CrossSharp.Utils.Interfaces;

public interface IApplication
{
    IForm MainForm { get; }
    Type? MainFormType { get; set; }
    IntPtr MainWindowHandle { get; internal set; }
    bool DevelopersMode { get; set; }
    EventHandler? DevelopersModeChanged { get; set; }
    HashSet<IForm> Forms { get; }
    void SetTheme(ITheme theme);
    void Start();
    EventHandler? Tick { get; set; }
}
