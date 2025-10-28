using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class TabbedLayout : ITabbedLayout
{
    DynamicControlsController _controlsController;
    private List<IControl> _controls = [];
    IControlsContainer _contentPane;

    internal TabbedLayout()
    {
        Initialize();
    }

    public void Dispose() { }

    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }

    public void LimitClip(ref IGraphics g) { }

    public Point Location { get; set; }
    public EventHandler<Point>? LocationChanged { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public EventHandler<Size>? SizeChanged { get; set; }
    public object Parent { get; set; }
    public bool IsMouseOver { get; set; }
    public bool Visible { get; set; }

    public void Initialize()
    {
        _contentPane = new StaticLayout();
        _contentPane.Location = new Point(0, 0);
        _contentPane.Parent = this;
        _contentPane.Dock = DockPosition.Fill;
        _controls.Add(_contentPane);
        _controlsController = new DynamicControlsController(ref _contentPane);
    }

    public void Invalidate()
    {
        this.PerformDocking();
        _contentPane.Invalidate();
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public void Draw(ref IGraphics graphics)
    {
        _contentPane.Draw(ref graphics);
    }

    public int DockIndex { get; set; }
    public DockPosition Dock { get; set; }
    public ColorRgba BackgroundColor { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }

    public void AddTab(string title, Type content)
    {
        _controlsController.Set(title, content);
        Invalidate();
    }

    public void RemoveTab(string title)
    {
        throw new NotImplementedException();
    }

    public void SelectTab(string title)
    {
        _controlsController.Show(title);
        Invalidate();
    }

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls)
    {
        throw new NotSupportedException("Use AddTab method to add tabs.");
    }

    public void Remove(params IControl[] controls)
    {
        throw new NotSupportedException("Use RemoveTab method to remove tabs.");
    }
}
