using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IForm : IControl, ITitleBarProvider, IBackgroundColorProvider
{
    IControlsContainer Controls { get; }
    IntPtr Handle { get; }
    bool UseNativeTitleBar { get; set; }
    string Title { get; set; }
    EventHandler? OnTitleChanged { get; set; }
    IApplication AppInstance { get; }
    void Close();
    EventHandler? OnShow { get; set; }
    EventHandler? OnClose { get; set; }
    void PerformLayout();
    void Minimize();
    void Maximize();
    void Restore();
    void Show();
    void Redraw();
    WindowState State { get; set; }
}
