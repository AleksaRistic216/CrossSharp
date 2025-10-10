using System.Drawing;
using CrossSharp.Utils.DI;
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
        OnSizeChanged = null;
        OnLocationChanged = null;
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
        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
    }

    public virtual void DrawBorders(ref IGraphics g)
    {
        if (BorderWidth <= 0)
            return;
        if (BorderColor == ColorRgba.Transparent)
            return;
        if (Width <= 0 || Height <= 0)
            return;
        g.DrawRectangle(
            Location.X + BorderWidth / 2,
            Location.Y + BorderWidth / 2,
            Width - BorderWidth,
            Height - BorderWidth,
            BorderColor,
            BorderWidth
        );
    }

    public virtual void DrawContent(ref IGraphics g) { }

    public void Draw(ref IGraphics graphics)
    {
        if (!Visible)
            return;
        LimitClip(ref graphics);
        DrawShadows(ref graphics);
        DrawBackground(ref graphics);
        DrawBorders(ref graphics);
        DrawContent(ref graphics);
    }

    public IForm? GetForm() => ControlsHelpers.GetForm(this);

    public virtual void LimitClip(ref IGraphics g)
    {
        g.SetClip(new Rectangle(Location.X, Location.Y, Width, Height));
    }
}
