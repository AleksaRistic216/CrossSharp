using System.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IForm : IControl, IBackgroundColorProvider
{
    IControlsContainer Controls { get; }
    IntPtr Handle { get; }
    string Title { get; set; }
    EventHandler? TitleChanged { get; set; }
    void Close();
    EventHandler? Shown { get; set; }
    EventHandler? OnClose { get; set; }
    void Minimize();
    void Maximize();
    void Restore();
    void Show();
    void Redraw();
    WindowState State { get; set; }
    EventHandler? StateChanged { get; set; }

    /// <summary>
    /// Move the form to the specified location.
    /// </summary>
    /// <param name="location"></param>
    void Move(Point location);
}
