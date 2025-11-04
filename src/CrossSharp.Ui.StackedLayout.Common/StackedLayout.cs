using System.Collections;
using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class StackedLayout : IStackedLayout
{
    public StackedLayout()
    {
        _inputHandler = Services.GetSingleton<IInputHandler>();
        SubscribeToInputHandlerEvents();
    }

    public void LimitClip(ref IGraphics g) { }

    public void Invalidate()
    {
        this.PerformDocking();
        if (Orientation == Orientation.Vertical)
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
        var width = Enumerable.Max<IControl>(_controls, c => c.Location.X + c.Width);
        var height = Enumerable.Max<IControl>(_controls, c => c.Location.Y + c.Height);
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
        foreach (
            var c in Enumerable.Where<IControl>(_controls, x => x.Visible).OrderBy(x => x.Index)
        )
        {
            var marginTop = 0;
            var marginBottom = 0;
            var marginLeft = 0;
            var marginRight = 0;
            if (c is IMarginProvider mp)
            {
                marginTop = mp.MarginTop;
                marginBottom = mp.MarginBottom;
                marginLeft = mp.MarginLeft;
                marginRight = mp.MarginRight;
            }
            c.Location = new Point(Padding.Left + marginLeft, currentY + marginTop);
            c.Width = Width - Padding.Horizontal - marginLeft - marginRight;
            currentY += c.Height + marginTop + marginBottom + ItemsSpacing;
        }
    }

    void InvalidateStackHorizontal()
    {
        var currentX = Padding.Left;
        foreach (
            var c in Enumerable.Where<IControl>(_controls, x => x.Visible).OrderBy(x => x.Index)
        )
        {
            currentX += c.MarginLeft;
            c.Location = new Point(currentX, Padding.Top + c.MarginTop);
            c.Height = Height - Padding.Vertical - c.MarginTop - c.MarginBottom;
            currentX += c.Width + ItemsSpacing + c.MarginRight;
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

    public void Clear() => _controls.Clear();

    public void Draw(ref IGraphics graphics)
    {
        var clientBounds = this.GetClientBounds();
        graphics.SetOffset(clientBounds.X, clientBounds.Y);
        graphics.SetClip(clientBounds, 0);
        DrawBackground(ref graphics);
        foreach (var c in Enumerable.Where<IControl>(_controls, ShouldControlBeDrawn))
            c.Draw(ref graphics);
        ScrollableHelpers.DrawScrollBar(ref graphics, this);
    }

    bool ShouldControlBeDrawn(IControl control)
    {
        if (!control.Visible)
            return false;
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

    public void Dispose() => OnDisposeInternal();
}
