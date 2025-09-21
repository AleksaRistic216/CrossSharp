using System.Runtime.InteropServices;

namespace CrossSharp.Utils.PangoCairo;

static class PangoCairoHelpers
{
    const string PANGOCAIRO = "libpangocairo-1.0.so.0";

    // Create a Pango layout from a Cairo context
    [DllImport(PANGOCAIRO)]
    public static extern IntPtr pango_cairo_create_layout(IntPtr cr);

    // Render the layout to the Cairo context
    [DllImport(PANGOCAIRO)]
    public static extern void pango_cairo_show_layout(IntPtr cr, IntPtr layout);
}
