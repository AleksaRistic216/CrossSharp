using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
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

    public int Index { get; set; }
    public object? Parent { get; set; }

    public abstract void PerformTheme();

    public abstract void Invalidate();

    public Size GetSize() => new(Width, Height);

    public virtual void Dispose() => OnDisposingInternal();

    public virtual void DrawShadows(ref IGraphics g) { }

    public virtual void DrawBackground(ref IGraphics g)
    {
        var color = this.GetBackgroundColor();
        g.FillRectangle(0, 0, Width, Height, color);
    }

    public virtual void DrawBorders(ref IGraphics g)
    {
        if (BorderWidth <= 0)
            return;
        if (Equals(BorderColor, ColorRgba.Transparent))
            return;
        if (Width <= 0 || Height <= 0)
            return;

        var cornersRadius = (this as IRoundedCorners)?.CornerRadius ?? 0;
        g.DrawRectangle(0, 0, Width, Height, BorderColor, BorderWidth, cornersRadius);
    }

    public virtual void DrawContent(ref IGraphics g) { }

    public void Draw(ref IGraphics graphics)
    {
        if (!Visible)
            return;
        var oldState = graphics.GetClipState();
        var clientBounds = this.GetClientBounds();
        graphics.SetClip(ClipState.Create(oldState, clientBounds, this is IRoundedCorners rc ? rc.CornerRadius : 0));
        graphics.SetOffset(clientBounds.Location);
        DrawShadows(ref graphics);
        DrawBackground(ref graphics);
        DrawBorders(ref graphics);
        DrawContent(ref graphics);
        graphics.SetClip(oldState);
    }
}
