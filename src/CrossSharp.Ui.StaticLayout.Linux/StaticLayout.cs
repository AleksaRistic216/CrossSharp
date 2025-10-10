using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class StaticLayout : IStaticLayout
{
    readonly List<IControl> _controls = [];

    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }

    public Point Location { get; set; }
    public EventHandler<Point>? OnLocationChanged { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public bool Visible { get; set; }

    public void LimitClip(ref IGraphics g) { }

    public void Initialize() { }

    public void Invalidate() { }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls) => _controls.AddRange(controls);

    public void Remove(params IControl[] controls) => _controls.RemoveAll(controls.Contains);

    public void Draw(ref IGraphics graphics)
    {
        foreach (var c in _controls)
            c.Draw(ref graphics);
    }

    public void Dispose()
    {
        foreach (var c in _controls)
            c.Dispose();
        _controls.Clear();
    }
}
