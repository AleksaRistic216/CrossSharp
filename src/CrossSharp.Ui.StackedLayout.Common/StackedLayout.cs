using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Extensions;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class StackedLayout : IStackedLayout
{
    protected StackedLayout()
    {
        _inputHandler = Services.GetSingleton<IInputHandler>();
        SubscribeToInputHandlerEvents();
        PerformTheme();
    }

    public void PrepareClipAndOffset(ref IGraphics g)
    {
        var clientBounds = this.GetClientBounds();
        g.SetOffset(clientBounds.X, clientBounds.Y);
        g.SetClip(clientBounds, CornerRadius);
    }

    public void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor;
        CornerRadius = Services.GetSingleton<ITheme>().DefaultCornerRadius;
        ItemsSpacing = Services.GetSingleton<ITheme>().DefaultCornerRadius > 0 ? 8 : 0;
        Padding = Services.GetSingleton<ITheme>().DefaultCornerRadius > 0 ? new Padding(8, 4) : new Padding(0);
        // this.SetMargin(Services.GetSingleton<ITheme>().DefaultLayoutItemSpacing); // Do not do this, it is bugged :)
        foreach (var control in _controls)
            control.PerformTheme();
        OnThemePerformed();
    }

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

    public void Initialize() { }

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
        foreach (var c in _controls.Where(x => x.Visible).OrderBy(x => x.Index))
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
        foreach (var c in _controls.Where(x => x.Visible).OrderBy(x => x.Index))
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

    public void Draw(ref IGraphics graphics)
    {
        PrepareClipAndOffset(ref graphics);
        DrawBackground(ref graphics);
        foreach (var c in _controls.Where(ShouldControlBeDrawn))
            c.Draw(ref graphics);
        PrepareClipAndOffset(ref graphics);
        ScrollableHelpers.DrawScrollBar(ref graphics, this);
        DrawBorders(ref graphics);
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

    void DrawBorders(ref IGraphics graphics)
    {
        if (BorderWidth <= 0)
            return;
        if (Equals(BorderColor, ColorRgba.Transparent))
            return;
        if (Width <= 0 || Height <= 0)
            return;

        var cornersRadius = (this as IRoundedCorners)?.CornerRadius ?? 0;
        graphics.DrawRectangle(0, 0, Width, Height, BorderColor, BorderWidth, cornersRadius);
    }

    public void Dispose() => OnDisposeInternal();
}
