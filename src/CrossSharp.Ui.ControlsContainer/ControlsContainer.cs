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
    readonly IControlsContainer _impl = ServicesPool
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

    public ColorRgba BackgroundColor
    {
        get => _impl.BackgroundColor;
        set => _impl.BackgroundColor = value;
    }
}
