using System.Drawing;
using CrossSharp.Utils.Cairo;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Pango;
using CrossSharp.Utils.PangoCairo;

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
        ColorRgba borderColor,
        float borderWidth
    )
    {
        if (!ClipRectangle.IntersectsWith(new Rectangle(x, y, width, height)))
            return;
        SetClip(ClipRectangle);
        SetColor(borderColor);
        CairoHelpers.cairo_set_line_width(ContextHandle, borderWidth);
        CairoHelpers.cairo_rectangle(ContextHandle, x, y, width, height);
        CairoHelpers.cairo_stroke(ContextHandle);
    }

    internal void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor)
    {
        if (!ClipRectangle.IntersectsWith(new Rectangle(x, y, width, height)))
            return;
        SetClip(ClipRectangle);
        SetColor(fillColor);
        CairoHelpers.cairo_rectangle(ContextHandle, x, y, width, height);
        CairoHelpers.cairo_fill(ContextHandle);
    }

    internal void DrawText(
        string text,
        int x,
        int y,
        ColorRgba textColor,
        string fontFamily,
        int fontSize,
        PangoWeight fontWeight,
        PangoStyle fontStyle
    )
    {
        SetClip(ClipRectangle);
        SetColor(textColor);
        CairoHelpers.cairo_move_to(ContextHandle, x, y);

        IntPtr layout = PangoCairoHelpers.pango_cairo_create_layout(ContextHandle);
        PangoHelpers.pango_layout_set_text(layout, text, -1);

        IntPtr pangoDesc = PangoHelpers.CreateDescription(
            fontFamily,
            fontSize,
            fontWeight,
            fontStyle
        );
        PangoHelpers.pango_layout_set_font_description(layout, pangoDesc);
        PangoCairoHelpers.pango_cairo_show_layout(ContextHandle, layout);
    }

    void SetColor(ColorRgba color)
    {
        var gtkColor = color.ToGtkRgba();
        CairoHelpers.cairo_set_source_rgba(
            ContextHandle,
            gtkColor.red,
            gtkColor.green,
            gtkColor.blue,
            gtkColor.alpha
        );
    }

    internal void SetClip(int x, int y, int width, int height)
    {
        CairoHelpers.cairo_rectangle(ContextHandle, x, y, width, height);
        CairoHelpers.cairo_clip(ContextHandle);
    }

    internal void SetClip(Point location, Size size)
    {
        CairoHelpers.cairo_rectangle(
            ContextHandle,
            location.X,
            location.Y,
            size.Width,
            size.Height
        );
        CairoHelpers.cairo_clip(ContextHandle);
    }

    internal void SetClip(Rectangle rect)
    {
        CairoHelpers.cairo_rectangle(ContextHandle, rect.X, rect.Y, rect.Width, rect.Height);
        CairoHelpers.cairo_clip(ContextHandle);
    }

    internal void ResetClip()
    {
        var clipRect = new Rectangle(
            locationProvider.Location,
            new Size(sizeProvider.Width, sizeProvider.Height)
        );
        CairoHelpers.cairo_rectangle(
            ContextHandle,
            clipRect.X,
            clipRect.Y,
            clipRect.Width,
            clipRect.Height
        );
        CairoHelpers.cairo_clip(ContextHandle);
    }

    public void Dispose()
    {
        ContextHandle = IntPtr.Zero;
        GC.SuppressFinalize(this);
    }
}
