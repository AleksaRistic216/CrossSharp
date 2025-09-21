using System.Runtime.InteropServices;

namespace CrossSharp.Utils.Pango;

static class PangoHelpers
{
    const string PANGO = "libpango-1.0.so.0";

    [DllImport(PANGO)]
    internal static extern IntPtr pango_layout_new(IntPtr context);

    [DllImport(PANGO)]
    internal static extern void pango_layout_set_text(IntPtr layout, string text, int length);

    [DllImport(PANGO)]
    internal static extern IntPtr pango_font_description_new();

    [DllImport(PANGO)]
    internal static extern void pango_font_description_set_size(IntPtr desc, int size);

    [DllImport(PANGO)]
    internal static extern void pango_font_description_set_style(IntPtr desc, PangoStyle style);

    [DllImport(PANGO)]
    internal static extern void pango_font_description_set_family(IntPtr desc, string family);

    [DllImport(PANGO)]
    internal static extern void pango_font_description_set_weight(IntPtr desc, PangoWeight weight);

    [DllImport(PANGO)]
    internal static extern void pango_layout_set_font_description(IntPtr layout, IntPtr desc);

    [DllImport(PANGO)]
    internal static extern IntPtr pango_font_description_to_string(IntPtr desc);

    [DllImport(PANGO)]
    internal static extern void pango_layout_get_size(IntPtr layout, out int width, out int height);

    internal static IntPtr CreateDescription(
        string family,
        int size,
        PangoWeight weight,
        PangoStyle style
    )
    {
        IntPtr desc = pango_font_description_new();
        pango_font_description_set_family(desc, family);
        pango_font_description_set_size(desc, size * 1024);
        pango_font_description_set_weight(desc, weight);
        pango_font_description_set_style(desc, style);
        return desc;
    }
}
