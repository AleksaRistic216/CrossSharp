using System.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract partial class Control : IControl, ISizeProvider, ILocationProvider
{
    public abstract void Initialize();
    public abstract void Invalidate();
    public abstract void Show();
    public EventHandler? OnShow { get; set; }

    public virtual void Dispose()
    {
        OnSizeChanged = null;
        GtkHelpers.gtk_widget_unparent(Handle);
        GtkHelpers.g_object_unref(Handle);
    }

    protected void Redraw()
    {
        if (Handle != IntPtr.Zero)
            GtkHelpers.gtk_widget_queue_draw(Handle);
    }

    public IForm GetForm()
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
            throw new Exception(
                "I hope I do not get here but I expect to cuz I have no idea what to set here..."
            );
        }
    }

    public Rectangle GetFormRelativeBounds()
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

    public Rectangle GetScreenBounds()
    {
        IForm form = GetForm();
        var formRelativeBounds = GetFormRelativeBounds();
        return new Rectangle(
            form.Location.X + formRelativeBounds.X,
            form.Location.Y + formRelativeBounds.Y,
            _width,
            _height
        );
    }
}
