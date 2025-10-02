using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
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
        set => _impl.Handle = value;
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
}
