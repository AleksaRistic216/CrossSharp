using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class StackedLayout
{
    readonly IInputHandler _inputHandler;
    readonly List<IControl> _controls = [];
    Rectangle _viewPort = Rectangle.Empty;

    public object? Parent { get; set; }
    bool _visible = true;
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
                return;
            _visible = value;
            Invalidate();
        }
    }
    public int Index { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
    public int ItemsSpacing { get; set; } =
        Services.GetSingleton<ITheme>().DefaultCornerRadius > 0 ? 8 : 0;
    public Orientation Orientation { get; set; } = Orientation.Vertical;
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }
    public Point Location { get; set; }
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public bool IsMouseOver { get; set; }
    public Padding Padding { get; set; } =
        Services.GetSingleton<ITheme>().DefaultCornerRadius > 0
            ? new Padding(8, 4)
            : new Padding(0);
    public ScrollableMode Scrollable { get; set; } = ScrollableMode.None;
    public Rectangle Viewport
    {
        get
        {
            return Scrollable == ScrollableMode.None
                ? new Rectangle(Location.X, Location.Y, Width, Height)
                : _viewPort;
        }
        set
        {
            if (_viewPort.Equals(value))
                return;
            _viewPort = value;
            // OnViewportChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public Rectangle ContentBounds { get; set; }
    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
    public EventHandler? MarginChanged { get; set; }
}
