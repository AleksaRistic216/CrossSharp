using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Panel() : CrossWidget<IPanel>(Services.GetSingleton<IPanelFactory>().Create()), IPanel
{
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
    public ColorRgba BackgroundColor
    {
        get => _impl.BackgroundColor;
        set => _impl.BackgroundColor = value;
    }

    Size ICenterPanelChild.GetSize() => _impl.GetSize();

    public EventHandler? LayoutChanged
    {
        get => _impl.LayoutChanged;
        set => _impl.LayoutChanged = value;
    }
    public ColorRgba ForegroundColor
    {
        get => _impl.ForegroundColor;
        set => _impl.ForegroundColor = value;
    }
}
