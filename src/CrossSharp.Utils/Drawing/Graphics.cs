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
        new(locationProvider.Location, new Size(sizeProvider.Width, sizeProvider.Height));
    Point _offset = Point.Empty;
    internal IntPtr ContextHandle { get; set; } = contextHandle;

    void ApplyOffset(ref int x, ref int y)
    {
        x += _offset.X;
        y += _offset.Y;
    }

    internal void DrawRectangle(
        int x,
        int y,
        int width,
        int height,
        ColorRgba borderColor,
        float borderWidth
    )
    {
        ApplyOffset(ref x, ref y);
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
        ApplyOffset(ref x, ref y);
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
        ApplyOffset(ref x, ref y);
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

    internal void SetClip(Rectangle rect)
    {
        CairoHelpers.cairo_rectangle(ContextHandle, rect.X, rect.Y, rect.Width, rect.Height);
        CairoHelpers.cairo_clip(ContextHandle);
    }

    internal void SetClip(Rectangle rect, double radius)
    {
        double x = rect.X;
        double y = rect.Y;
        double width = rect.Width;
        double height = rect.Height;

        double r = Math.Min(radius, Math.Min(width / 2.0, height / 2.0)); // Clamp radius

        // Start drawing the rounded rectangle path
        CairoHelpers.cairo_new_path(ContextHandle);

        CairoHelpers.cairo_arc(ContextHandle, x + width - r, y + r, r, -Math.PI / 2, 0); // Top-right corner
        CairoHelpers.cairo_arc(ContextHandle, x + width - r, y + height - r, r, 0, Math.PI / 2); // Bottom-right
        CairoHelpers.cairo_arc(ContextHandle, x + r, y + height - r, r, Math.PI / 2, Math.PI); // Bottom-left
        CairoHelpers.cairo_arc(ContextHandle, x + r, y + r, r, Math.PI, 3 * Math.PI / 2); // Top-left

        CairoHelpers.cairo_close_path(ContextHandle);
        CairoHelpers.cairo_clip(ContextHandle);
    }

    public void Dispose()
    {
        ContextHandle = IntPtr.Zero;
        GC.SuppressFinalize(this);
    }

    public void ResetOffset()
    {
        _offset = Point.Empty;
    }

    public void SetOffset(int locationX, int locationY)
    {
        _offset = new Point(locationX, locationY);
    }
}
