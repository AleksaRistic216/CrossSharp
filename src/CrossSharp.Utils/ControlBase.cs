using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract partial class ControlBase : IControl
{
    protected ControlBase()
    {
        InputHandler = Services.GetSingleton<IInputHandler>();
        SubscribeToInputEvents();
    }

    public object Parent { get; set; }
    public abstract void Initialize();
    public abstract void Invalidate();

    public Size GetSize() => new(Width, Height);

    public virtual void Dispose()
    {
        SizeChanged = null;
        LocationChanged = null;
        InputHandler.MouseMoved -= OnMouseMoved;
    }

    public abstract void Redraw();

    public void SuspendLayout()
    {
        _suspendLayout = true;
    }

    public void ResumeLayout()
    {
        _suspendLayout = false;
        Invalidate();
        Redraw();
    }

    public virtual void DrawShadows(ref IGraphics g) { }

    public virtual void DrawBackground(ref IGraphics g)
    {
        var color = this.GetThemedBackgroundColor();
        g.FillRectangle(0, 0, Width, Height, color);
    }

    public virtual void DrawBorders(ref IGraphics g)
    {
        if (BorderWidth <= 0)
            return;
        if (BorderColor == ColorRgba.Transparent)
            return;
        if (Width <= 0 || Height <= 0)
            return;
        g.FillRectangle(0, 0, Width, BorderWidth, BorderColor);
        g.FillRectangle(0, Height - BorderWidth, Width, BorderWidth, BorderColor);
        g.FillRectangle(0, 0, BorderWidth, Height, BorderColor);
        g.FillRectangle(Width - BorderWidth, 0, BorderWidth, Height, BorderColor);
    }

    public virtual void DrawContent(ref IGraphics g) { }

    public void Draw(ref IGraphics graphics)
    {
        if (!Visible)
            return;
        var clientBounds = this.GetClientBounds();
        var offsetX = clientBounds.X;
        var offsetY = clientBounds.Y;

        if (
            Parent is IScrollable scrollableParent
            && scrollableParent.Scrollable != ScrollableMode.None
        )
        {
            offsetX -= scrollableParent.Viewport.X;
            offsetY -= scrollableParent.Viewport.Y;
        }
        graphics.SetOffset(offsetX, offsetY);
        LimitClip(ref graphics);
        DrawShadows(ref graphics);
        DrawBackground(ref graphics);
        DrawBorders(ref graphics);
        DrawContent(ref graphics);
    }

    public virtual void LimitClip(ref IGraphics g)
    {
        var clipBounds = this.GetClientBounds();
        if (
            Parent is IScrollable scrollableParent
            && scrollableParent.Scrollable != ScrollableMode.None
        )
        {
            clipBounds.X -= scrollableParent.Viewport.X;
            clipBounds.Y -= scrollableParent.Viewport.Y;
        }
        g.SetClip(clipBounds);
    }
}
