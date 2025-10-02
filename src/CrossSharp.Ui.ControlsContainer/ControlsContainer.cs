using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class ControlsContainer(
    IntPtr parentHandle,
    ISizeProvider sizeProvider,
    IBackgroundColorProvider backgroundColorProvider
) : IControlsContainer
{
    readonly IControlsContainer _impl = Services
        .GetSingleton<IControlsContainerFactory>()
        .Create(parentHandle, sizeProvider, backgroundColorProvider);

    public void Dispose() => _impl.Dispose();

    public IntPtr Handle
    {
        get => _impl.Handle;
    }
    public IntPtr ParentHandle
    {
        get => _impl.ParentHandle;
        set => _impl.ParentHandle = value;
    }
    public object Parent
    {
        get => _impl.Parent;
        set => _impl.Parent = value;
    }
    public List<IControl> Items => _impl.Items;

    public void Show() => _impl.Show();

    public void Add(IControl control) => _impl.Add(control);

    public void Redraw() => _impl.Redraw();

    public void PerformLayout() => _impl.PerformLayout();

    public void Attach(IControl control) => _impl.Attach(control);

    public void Attach(IControl control, int column, int row, int width, int height) =>
        _impl.Attach(control, column, row, width, height);

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
    public int BorderWidth
    {
        get => _impl.BorderWidth;
        set => _impl.BorderWidth = value;
    }
    public ColorRgba BorderColor
    {
        get => _impl.BorderColor;
        set => _impl.BorderColor = value;
    }

    public void LimitClip(ref Graphics g) => _impl.LimitClip(ref g);
}
