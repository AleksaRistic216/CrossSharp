using System.Collections;
using System.Drawing;
using System.Numerics;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Linux;

class StackedLayout : IStackedLayout
{
    readonly List<IControl> _controls = [];

    public int DockIndex { get; set; }
    public DockPosition Dock { get; set; }
    public int ItemsSpacing { get; set; } = 0;
    public Direction Direction { get; set; } = Direction.Vertical;
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }

    public Point Location { get; set; }
    public EventHandler<Point>? LocationChanged { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public EventHandler<Size>? SizeChanged { get; set; }
    public object Parent { get; set; }
    public bool Visible { get; set; }
    IInputHandler _inputHandler;

    public StackedLayout()
    {
        _inputHandler = Services.GetSingleton<IInputHandler>();
        SubscribeToInputHandlerEvents();
    }

    void SubscribeToInputHandlerEvents()
    {
        _inputHandler.MouseWheel += InputHandlerOnMouseWheel;
        _inputHandler.MouseMoved += OnMouseMoved;
    }

    void UnsubscribeFromInputHandlerEvents()
    {
        _inputHandler.MouseWheel -= InputHandlerOnMouseWheel;
        _inputHandler.MouseMoved -= OnMouseMoved;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this.GetScreenBounds(), new Point(e.X, e.Y));
    }

    void InputHandlerOnMouseWheel(object? sender, MouseWheelInputArgs e)
    {
        if (!IsMouseOver)
            return;

        var rotation = e.Rotation;
        rotation /= 10;
        if (Math.Abs(rotation) <= 0)
            return;
        if (Scrollable == ScrollableMode.Vertical)
            ScrollableHelpers.Scroll(Direction.Vertical, rotation, this, ref _viewPort);
        else if (Scrollable == ScrollableMode.Horizontal)
            ScrollableHelpers.Scroll(Direction.Horizontal, rotation, this, ref _viewPort);
        else if (Scrollable == ScrollableMode.Both)
        {
            // TODO: implement both direction scrolling using mouse and shift key
        }
    }

    public void LimitClip(ref IGraphics g) { }

    public void Initialize() { }

    public void Invalidate()
    {
        this.PerformDocking();
        if (Direction == Direction.Vertical)
            InvalidateStackVertical();
        else
            InvalidateStackHorizontal();
        foreach (IControl control in _controls)
            control.Invalidate();
        InvalidateContentBounds();
        InvalidateViewport();
    }

    void InvalidateContentBounds()
    {
        if (_controls.Count == 0)
        {
            ContentBounds = Rectangle.Empty;
            return;
        }
        var x = 0;
        var y = 0;
        var width = _controls.Max(c => c.Location.X + c.Width);
        var height = _controls.Max(c => c.Location.Y + c.Height);
        ContentBounds = new Rectangle(x, y, width, height);
    }

    void InvalidateViewport()
    {
        if (Scrollable == ScrollableMode.None)
            return;
        Viewport = new Rectangle(Viewport.X, Viewport.Y, Width, Height);
    }

    void InvalidateStackVertical()
    {
        var currentY = Padding.Top;
        foreach (var c in _controls)
        {
            c.Location = new Point(Padding.Left, currentY);
            c.Width = Width - Padding.Horizontal;
            currentY += c.Height + ItemsSpacing;
        }
    }

    void InvalidateStackHorizontal()
    {
        var currentX = Padding.Left;
        foreach (var c in _controls)
        {
            c.Location = new Point(currentX, Padding.Top);
            c.Height = Height - Padding.Vertical;
            currentX += c.Width + ItemsSpacing;
        }
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls)
    {
        foreach (IControl control in controls)
            control.Parent = this;
        _controls.AddRange(controls);
        Invalidate();
    }

    public void Remove(params IControl[] controls) => _controls.RemoveAll(controls.Contains);

    public void Draw(ref IGraphics graphics)
    {
        var clientBounds = this.GetClientBounds();
        graphics.SetOffset(clientBounds.X, clientBounds.Y);
        graphics.SetClip(clientBounds);
        DrawBackground(ref graphics);
        foreach (var c in _controls.Where(ShouldControlBeDrawn))
            c.Draw(ref graphics);
        ScrollableHelpers.DrawScrollBar(ref graphics, this);
    }

    bool ShouldControlBeDrawn(IControl control)
    {
        if (Scrollable == ScrollableMode.None)
            return true;
        return Viewport.IntersectsWith(
            new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height)
        );
    }

    void DrawBackground(ref IGraphics graphics)
    {
        graphics.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    public void Dispose()
    {
        foreach (var c in _controls)
            c.Dispose();
        _controls.Clear();
        UnsubscribeFromInputHandlerEvents();
    }

    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public EventHandler? BackgroundColorChanged { get; set; }
    public bool IsMouseOver { get; set; }
    public Padding Padding { get; set; } = Padding.Zero;
    public ScrollableMode Scrollable { get; set; } = ScrollableMode.None;
    Rectangle _viewPort = Rectangle.Empty;
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
}
