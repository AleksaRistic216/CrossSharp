using System.Collections;
using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class FlowLayout : IFlowLayout
{
    readonly List<IControl> _controls = [];

    protected FlowLayout()
    {
        _inputHandler = Services.GetSingleton<IInputHandler>();
        InitializeScrollbarHandler();
        PerformTheme();
    }

    public void Dispose() => OnDisposeInternal();

    public void PerformTheme()
    {
        Margin = new Margin(Services.GetSingleton<ITheme>().DefaultLayoutItemSpacing);
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor;
        ItemsSpacing = Services.GetSingleton<ITheme>().DefaultLayoutItemSpacing;
        foreach (var control in _controls.ToList())
            control.PerformTheme();
        OnThemePerformed();
    }

    public void Invalidate()
    {
        this.PerformDocking();
        InvalidateFlow();
        foreach (IControl control in _controls.ToList())
            control.Invalidate();
        InvalidateContentBounds();
        InvalidateViewport();
        OnInvalidated();
    }

    public void Initialize() { }

    void InitializeScrollbarHandler()
    {
        _scrollbarHandler = new ScrollbarInteractionHandler<FlowLayout>(
            _inputHandler,
            this,
            () => _viewPort,
            vp => _viewPort = vp,
            OnScrolled,
            () => IsMouseOver,
            v => IsMouseOver = v
        );
        _scrollbarHandler.Subscribe();
    }

    void DisposeScrollbarHandler()
    {
        _scrollbarHandler?.Dispose();
        _scrollbarHandler = null;
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

    void InvalidateFlow()
    {
        var currentX = 0;
        var currentY = 0;
        var maxHeightInRow = 0;
        List<IControl> rowControls = [];
        foreach (var c in _controls.ToList())
        {
            if (currentX + c.Width > Width)
            {
                currentX = 0;
                currentY += maxHeightInRow + ItemsSpacing;
                maxHeightInRow = 0;
                JustifyWithinRow(rowControls);
                rowControls.Clear();
            }
            rowControls.Add(c);
            c.Location = new Point(Location.X + currentX, Location.Y + currentY);
            currentX += c.Width + ItemsSpacing;
            if (c.Height > maxHeightInRow)
                maxHeightInRow = c.Height;
        }
        JustifyWithinRow(rowControls);
    }

    void JustifyWithinRow(List<IControl> rowControls)
    {
        if (rowControls.Count == 0)
            return;
        var totalWidth = rowControls.Sum(c => c.Width + ItemsSpacing) - ItemsSpacing;
        if (totalWidth >= Width)
            return;
        var extraSpace = Width - totalWidth;
        var currentX = JustifyContentHorizontal switch
        {
            FlowAlignment.Start => 0,
            FlowAlignment.Center => extraSpace / 2,
            FlowAlignment.End => extraSpace,
            _ => 0,
        };
        var rowHeight = rowControls.Max(c => c.Height);
        foreach (var c in rowControls)
        {
            var remainingHeight = rowHeight - c.Height;
            var y =
                c.Location.Y
                + (
                    JustifyContentVertical switch
                    {
                        FlowAlignment.Start => 0,
                        FlowAlignment.Center => remainingHeight / 2,
                        FlowAlignment.End => remainingHeight,
                        _ => 0,
                    }
                );
            c.Location = new Point(currentX, y);
            currentX += c.Width + ItemsSpacing;
        }
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public void Draw(ref IGraphics graphics)
    {
        var oldOffset = graphics.GetOffset();
        var oldClipState = graphics.GetClipState();
        var clientBounds = this.GetClientBounds();
        graphics.SetOffset(clientBounds.Location);
        graphics.SetClip(
            NoClip ? ClipState.Max(CornerRadius) : ClipState.Create(oldClipState, clientBounds, CornerRadius)
        );
        DrawBackground(ref graphics);
        foreach (var c in _controls.Where(ShouldControlBeDrawn).ToList())
            c.Draw(ref graphics);
        // ScrollableHelpers.DrawScrollBar(ref graphics, this);
        // DrawBorders(ref graphics);
        graphics.SetClip(oldClipState);
        graphics.SetOffset(oldOffset);
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

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _controls.GetEnumerator();

    public void Add(params IControl[] controls)
    {
        foreach (IControl control in controls)
            control.Parent = this;
        _controls.AddRange(controls);
        Invalidate();
    }

    public void Remove(params IControl[] controls) => _controls.RemoveAll(controls.Contains);

    public void Clear() => _controls.Clear();
}
