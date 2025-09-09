using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Utils;

public abstract partial class Control : IControl, ISizeProvider, ILocationProvider {
    public abstract void Initialize();
    public abstract void Show();
    public virtual void Dispose() {
        OnSizeChanged = null;
        GtkHelpers.gtk_widget_unparent(Handle);
        GtkHelpers.g_object_unref(Handle);
    }
    protected void Redraw() {
        if(Handle != IntPtr.Zero)
            GtkHelpers.gtk_widget_queue_draw(Handle);
    }
}