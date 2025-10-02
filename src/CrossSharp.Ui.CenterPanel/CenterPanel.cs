using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class CenterPanel()
    : Control<ICenterPanel>(Services.GetSingleton<ICenterPanelFactory>().Create()),
        ICenterPanel
{
    public ColorRgba BackgroundColor
    {
        get => _impl.BackgroundColor;
        set => _impl.BackgroundColor = value;
    }
    public int Width
    {
        get => _impl.Width;
        set => _impl.Width = value;
    }
    public int Height
    {
        get => _impl.Height;
        set => _impl.Height = value;
    }
    public EventHandler<Size>? OnSizeChanged
    {
        get => _impl.OnSizeChanged;
        set => _impl.OnSizeChanged = value;
    }
    public IControl? Child
    {
        get => _impl.Child;
        set => _impl.Child = value;
    }
}
