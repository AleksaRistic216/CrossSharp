using System.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Drawing;

public class Graphics(
    IntPtr contextHandle,
    ILocationProvider locationProvider,
    ISizeProvider sizeProvider
) : IDisposable
{
    Rectangle ClipRectangle =>
        new(
            locationProvider.Location,
            new Size(
                locationProvider.Location.X + sizeProvider.Width,
                locationProvider.Location.Y + sizeProvider.Height
            )
        );
    internal IntPtr ContextHandle { get; set; } = contextHandle;

    internal void DrawRectangle(
        int x,
        int y,
        int width,
        int height,
        Color borderColor,
        float borderWidth
    )
    {
        if (!ClipRectangle.IntersectsWith(new Rectangle(x, y, width, height)))
            return;
        SetClip(ClipRectangle);
        SetColor(borderColor);
        GtkHelpers.cairo_set_line_width(ContextHandle, borderWidth);
        GtkHelpers.cairo_rectangle(ContextHandle, x, y, width, height);
        GtkHelpers.cairo_stroke(ContextHandle);
    }

    internal void FillRectangle(int x, int y, int width, int height, Color fillColor)
    {
        if (!ClipRectangle.IntersectsWith(new Rectangle(x, y, width, height)))
            return;
        SetClip(ClipRectangle);
        SetColor(fillColor);
        GtkHelpers.cairo_rectangle(ContextHandle, x, y, width, height);
        GtkHelpers.cairo_fill(ContextHandle);
    }

    internal void SetColor(Color color)
    {
        GtkHelpers.cairo_set_source_rgba(ContextHandle, color.R, color.G, color.B, color.A);
    }

    internal void SetClip(int x, int y, int width, int height)
    {
        GtkHelpers.cairo_rectangle(ContextHandle, x, y, width, height);
        GtkHelpers.cairo_clip(ContextHandle);
    }

    internal void SetClip(Point location, Size size)
    {
        GtkHelpers.cairo_rectangle(ContextHandle, location.X, location.Y, size.Width, size.Height);
        GtkHelpers.cairo_clip(ContextHandle);
    }

    internal void SetClip(Rectangle rect)
    {
        GtkHelpers.cairo_rectangle(ContextHandle, rect.X, rect.Y, rect.Width, rect.Height);
        GtkHelpers.cairo_clip(ContextHandle);
    }

    internal void ResetClip()
    {
        var clipRect = new Rectangle(
            locationProvider.Location,
            new Size(sizeProvider.Width, sizeProvider.Height)
        );
        GtkHelpers.cairo_rectangle(
            ContextHandle,
            clipRect.X,
            clipRect.Y,
            clipRect.Width,
            clipRect.Height
        );
        GtkHelpers.cairo_clip(ContextHandle);
    }

    public void Dispose()
    {
        ContextHandle = IntPtr.Zero;
        GC.SuppressFinalize(this);
    }
}
