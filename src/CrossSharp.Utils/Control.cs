using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract partial class Control : IControl, ISizeProvider
{
    public abstract void Initialize();
    public abstract void Invalidate();
    public abstract void Show();
    public EventHandler? OnShow { get; set; }

    protected Control()
    {
        InputHandler = Services.GetSingleton<IInputHandler>();
        SubscribeToInputEvents();
    }

    public virtual void Dispose()
    {
        Handle = IntPtr.Zero;
        OnSizeChanged = null;
        OnShow = null;
        OnLocationChanged = null;
        InputHandler.MouseMoved -= OnMouseMoved;
    }

    public void Redraw()
    {
        if (Handle != IntPtr.Zero)
            GtkHelpers.gtk_widget_queue_draw(Handle);
    }

    protected IForm? GetForm()
    {
        IRelativeHandle obj = this;
        while (true)
        {
            var p = obj.Parent;
            switch (p)
            {
                case IForm form:
                    return form;
                case IRelativeHandle rh:
                    obj = rh;
                    continue;
            }
            return null;
        }
    }

    protected Stack<IClipLimiter> GetClipLimiters()
    {
        Stack<IClipLimiter> stack = new();
        IRelativeHandle obj = this;
        while (true)
        {
            if (obj != this && obj is IClipLimiter cl)
                stack.Push(cl);
            var p = obj.Parent;
            switch (p)
            {
                case IRelativeHandle rh:
                    obj = rh;
                    continue;
            }
            break;
        }
        return stack;
    }

    internal Rectangle GetFormRelativeBounds()
    {
        Rectangle rect = Rectangle.Empty;
        rect.Width = Width;
        rect.Height = Height;
        IRelativeHandle obj = this;
        while (obj is IRelativeHandle)
        {
            if (obj is IForm)
                break;
            if (obj is ILocationProvider parent)
            {
                rect.X += parent.Location.X;
                rect.Y += parent.Location.Y;
            }
            if (obj.Parent is IRelativeHandle rh)
            {
                obj = rh;
                continue;
            }
            break;
        }
        return rect;
    }

    internal Rectangle GetScreenBounds()
    {
        IForm? form = GetForm();
        if (form == null)
            return Rectangle.Empty;
        var formRelativeBounds = GetFormRelativeBounds();
        return new Rectangle(
            form.Location.X + formRelativeBounds.X,
            form.Location.Y + formRelativeBounds.Y,
            _width,
            _height
        );
    }

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

    public virtual void DrawShadows(Graphics g) { }

    public virtual void DrawBackground(Graphics g) { }

    public virtual void DrawBorders(Graphics g)
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

    public virtual void DrawContent(Graphics g) { }

    public virtual void LimitClip(ref Graphics g)
    {
        g.SetClip(new Rectangle(Location.X, Location.Y, Width, Height));
    }
}
