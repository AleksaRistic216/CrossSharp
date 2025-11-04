using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Linux;

partial class StackedLayout
{
    readonly IInputHandler _inputHandler;
    readonly List<IControl> _controls = [];
    Rectangle _viewPort = Rectangle.Empty;

    public object? Parent { get; set; }
    public bool Visible { get; set; }
    public int Index { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
    public int ItemsSpacing { get; set; } = 0;
    public Direction Direction { get; set; } = Direction.Vertical;
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }
    public Point Location { get; set; }
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public bool IsMouseOver { get; set; }
    public Padding Padding { get; set; } = Padding.Zero;
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
