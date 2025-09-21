using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Button()
    : CrossWidget<IButton>(ServicesPool.GetSingleton<IButtonFactory>().Create()),
        IButton
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
    public EventHandler? OnClick
    {
        get => _impl.OnClick;
        set => _impl.OnClick = value;
    }
    public ColorRgba BackgroundColor
    {
        get => _impl.BackgroundColor;
        set => _impl.BackgroundColor = value;
    }
    public string Text
    {
        get => _impl.Text;
        set => _impl.Text = value;
    }
}
