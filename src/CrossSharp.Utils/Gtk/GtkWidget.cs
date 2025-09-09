using System.Drawing;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Utils.Gtk;

public abstract class GtkWidget : Control {
    public override IntPtr Handle { get; } = GtkHelpers.gtk_drawing_area_new();
    protected abstract void OnDraw(IntPtr area, IntPtr cr, int width, int height, IntPtr data);
    public override void Initialize() {
        GtkHelpers.DrawFunc drawFunc = OnDraw;
        GtkHelpers.gtk_drawing_area_set_draw_func(Handle, drawFunc, IntPtr.Zero, IntPtr.Zero);
    }
    public override void Show() {
        IntPtr parent = GtkHelpers.gtk_widget_get_parent(Handle);
        if(parent == ParentHandle)
            return;
        GtkHelpers.gtk_widget_show(Handle);
        GtkHelpers.gtk_fixed_put(ParentHandle, Handle, 0, 0);
    }
    public override void Dispose() {
        GtkHelpers.gtk_widget_unparent(Handle);
        GtkHelpers.g_object_unref(Handle);
    }
}