using CrossSharp.Utils.Gtk;
namespace CrossSharp.Ui;

public class PanelControl : GtkWidget {
    protected override void OnDraw(IntPtr area, IntPtr cr, int width, int height, IntPtr data) {
        if (cr == IntPtr.Zero) return;
        GtkHelpers.cairo_set_source_rgba(cr, Random.Shared.NextSingle(), Random.Shared.NextSingle(), Random.Shared.NextSingle(), 1.0);
        GtkHelpers.cairo_rectangle(cr, 0, 0, width, height);
        GtkHelpers.cairo_fill(cr);
    }
}