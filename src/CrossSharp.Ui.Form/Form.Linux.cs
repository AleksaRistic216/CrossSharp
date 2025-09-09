using CrossSharp.Utils.Gtk;
namespace CrossSharp.Ui;

public partial class Form {
    void InitializeLinux() {
        Handle = GtkHelpers.gtk_application_window_new(ParentHandle);
        Controls = new ControlsContainer(Handle, this);
        GtkHelpers.gtk_window_set_title(Handle, Title);
        SubscribeToGtkSignals();
    }
    void ShowLinux()
    {
        GtkHelpers.gtk_widget_show(Handle);
        Controls.Show();
    }
    #region Gtk signals and handlers
    void SubscribeToGtkSignals() {
        GtkHelpers.g_signal_connect_data(Handle, "map", (GtkHelpers.MapCallback) SignalOnWidgetMapped, IntPtr.Zero, IntPtr.Zero, 0);
        GtkHelpers.g_signal_connect_data(Handle, "close-request", (GtkHelpers.CloseRequestCallback) SignalOnClose, IntPtr.Zero, IntPtr.Zero, 0);
    }
    void SignalOnWidgetMapped(IntPtr widget, IntPtr _) {
        RaiseOnShow();
    }
    bool SignalOnClose(IntPtr windowPtr, IntPtr _) {
        // ParentHandle any cleanup if necessary
        Dispose();
        RaiseOnClose();
        return false; // Return true to prevent the window from closing
    }
    #endregion
    void DisposeLinux()
    {
        // Utils.GtkHelpers.g_object_unref(Handle);
        // Handle = IntPtr.Zero;
    }
}