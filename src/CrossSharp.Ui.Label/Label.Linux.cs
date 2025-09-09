using CrossSharp.Utils.Gtk;

namespace CrossSharp.Ui;

public partial class Label {
    void InitializeLinux() {
        GtkHelpers.gtk_widget_set_size_request(Handle, 100, 30);
    }
    void ShowLinux() {
        GtkHelpers.gtk_widget_show(Handle);
        GtkHelpers.gtk_fixed_put(ContainerHandle, Handle, 10, 10);
    }
    void DisposeLinux() {
        GtkHelpers.gtk_widget_unparent(Handle);
        GtkHelpers.g_object_unref(Handle);
    }
}