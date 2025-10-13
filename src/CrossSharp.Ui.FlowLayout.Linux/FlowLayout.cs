using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class FlowLayout : IFlowLayout
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }

    public void LimitClip(ref IGraphics g)
    {
        throw new NotImplementedException();
    }

    public Point Location { get; set; }
    public EventHandler<Point>? OnLocationChanged { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public object Parent { get; set; }
    public bool IsMouseOver { get; set; }
    public bool Visible { get; set; }

    public void Initialize()
    {
        throw new NotImplementedException();
    }

    public void Invalidate()
    {
        throw new NotImplementedException();
    }

    public void SuspendLayout()
    {
        throw new NotImplementedException();
    }

    public void ResumeLayout()
    {
        throw new NotImplementedException();
    }

    public void Draw(ref IGraphics graphics)
    {
        throw new NotImplementedException();
    }

    public IForm? GetForm()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<IControl> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int DockIndex { get; set; }
    public DockPosition Dock { get; set; }
    public ColorRgba BackgroundColor { get; set; }
    public EventHandler? OnBackgroundColorChange { get; set; }

    public void Add(params IControl[] controls)
    {
        throw new NotImplementedException();
    }

    public void Remove(params IControl[] controls)
    {
        throw new NotImplementedException();
    }
}
