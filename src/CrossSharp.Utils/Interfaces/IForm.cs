namespace CrossSharp.Utils.Interfaces;

public interface IForm : IControl, ISizeProvider, ITitleBarProvider, IBackgroundColorProvider
{
    bool UseNativeTitleBar { get; set; }
    IntPtr DisplayHandle { get; set; }
    IntPtr WindowSurfaceHandle { get; set; }
    string Title { get; set; }
    IApplication AppInstance { get; }
    void Close();
    IControlsContainer Controls { get; }
    EventHandler? OnShow { get; set; }
    EventHandler? OnClose { get; set; }
    void PerformLayout();
    void Minimize();
    void Maximize();
}
