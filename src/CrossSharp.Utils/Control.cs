using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Utils;

public abstract partial class Control : IControl, ISizeProvider {
    public abstract void Initialize();
    public abstract void Show();
    public virtual void Dispose() {
        OnSizeChanged = null;
        GtkHelpers.gtk_widget_unparent(Handle);
        GtkHelpers.g_object_unref(Handle);
    }
}