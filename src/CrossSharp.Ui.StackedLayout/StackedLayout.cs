using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class StackedLayout()
    : CrossWrapper<IStackedLayout>(Services.GetSingleton<IStackedLayoutFactory>().Create()),
        IStackedLayout
{
    IStackedLayout _impl => GetImplementation();
    public Direction Direction
    {
        get => _impl.Direction;
        set => _impl.Direction = value;
    }

    public DockPosition Dock
    {
        get => _impl.Dock;
        set => _impl.Dock = value;
    }

    public IEnumerator<IControl> GetEnumerator() => _impl.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls) => _impl.Add(controls);

    public void Remove(params IControl[] controls) => _impl.Remove(controls);

    public void Dispose() => _impl.Dispose();

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

    public void LimitClip(ref IGraphics g) => _impl.LimitClip(ref g);

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
    public object Parent
    {
        get => _impl.Parent;
        set => _impl.Parent = value;
    }
    public bool Visible
    {
        get => _impl.Visible;
        set => _impl.Visible = value;
    }

    public virtual void Initialize() => _impl.Initialize();

    public virtual void Invalidate() => _impl.Invalidate();

    public virtual void SuspendLayout() => _impl.SuspendLayout();

    public virtual void ResumeLayout() => _impl.ResumeLayout();

    public void Draw(ref IGraphics graphics) => _impl.Draw(ref graphics);

    public IForm? GetForm() => ControlsHelpers.GetForm(this);
}
