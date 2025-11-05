using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IForm : IControl, IBackgroundColorProvider
{
    IControlsContainer Controls { get; }
    IntPtr Handle { get; }
    string Title { get; set; }
    EventHandler? TitleChanged { get; set; }
    IApplication AppInstance { get; }
    void Close();
    EventHandler? Shown { get; set; }
    EventHandler? OnClose { get; set; }
    void Minimize();
    void Maximize();
    void Restore();
    void Show();
    void Redraw();
    WindowState State { get; set; }
}
