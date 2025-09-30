using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class FormTitleBar(IForm form) : IFormTitleBar
{
    readonly IFormTitleBar _impl = Services.GetSingleton<IFormTitleBarFactory>().Create(form);
    public Point Location
    {
        get => _impl.Location;
        set => _impl.Location = value;
    }
    public EventHandler<Point>? OnLocationChanged
    {
        get => _impl.OnLocationChanged;
        set => _impl.OnLocationChanged = value;
    }
    public int Width
    {
        get => _impl.Width;
        set => _impl.Width = value;
    }
    public TitleBarType Type
    {
        get => _impl.Type;
        set => _impl.Type = value;
    }
    public EventHandler? TypeChanged
    {
        get => _impl.TypeChanged;
        set => _impl.TypeChanged = value;
    }
    public int Height
    {
        get => _impl.Height;
        set => _impl.Height = value;
    }

    public void Show() => _impl.Show();

    public void Invalidate() => _impl.Invalidate();

    public EventHandler<Size>? OnSizeChanged
    {
        get => _impl.OnSizeChanged;
        set => _impl.OnSizeChanged = value;
    }
    public bool IsMouseOver
    {
        get => _impl.IsMouseOver;
        set => _impl.IsMouseOver = value;
    }
}
