using System.Runtime.InteropServices;

namespace CrossSharp.Utils.Cairo;

static class CairoHelpers
{
    const string CAIRO = "libcairo.so.2";

    // Create a Cairo context from a surface
    [DllImport(CAIRO)]
    public static extern IntPtr cairo_create(IntPtr surface);

    // Destroy the Cairo context
    [DllImport(CAIRO)]
    public static extern void cairo_destroy(IntPtr cr);

    // Set RGB color for drawing
    [DllImport(CAIRO)]
    public static extern void cairo_set_source_rgba(
        IntPtr cr,
        double r,
        double g,
        double b,
        double a
    );

    // Move the drawing cursor to a position
    [DllImport(CAIRO)]
    public static extern void cairo_move_to(IntPtr cr, double x, double y);

    // Show simple text (used in pure Cairo)
    [DllImport(CAIRO)]
    public static extern void cairo_show_text(IntPtr cr, string text);

    [DllImport(CAIRO)]
    internal static extern void cairo_rectangle(
        IntPtr cr,
        double x,
        double y,
        double width,
        double height
    );

    [DllImport(CAIRO)]
    internal static extern void cairo_fill(IntPtr cr);

    [DllImport(CAIRO)]
    internal static extern void cairo_clip(IntPtr cr);

    [DllImport(CAIRO)]
    internal static extern void cairo_set_source_rgb(IntPtr cr, double r, double g, double b);

    [DllImport(CAIRO)]
    internal static extern void cairo_set_line_width(IntPtr cr, double width);

    [DllImport(CAIRO)]
    internal static extern void cairo_stroke(IntPtr cr);

    [DllImport(CAIRO)]
    internal static extern IntPtr cairo_image_surface_create(int format, int width, int height);

    [DllImport(CAIRO)]
    internal static extern void cairo_surface_destroy(IntPtr surface);

    [DllImport((CAIRO))]
    internal static extern void cairo_translate(IntPtr cr, double tx, double ty);

    [DllImport(CAIRO)]
    internal static extern int cairo_surface_write_to_png(IntPtr surface, string filename);
}
