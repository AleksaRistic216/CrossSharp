using CrossSharp.Ui.FormTitleBar;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormLinux;

partial class FormLinux : IForm
{
    public FormLinux()
    {
        AppInstance = ServicesPool.GetSingleton<IApplication>();
        ParentHandle = AppInstance.MainWindowHandle;
        Initialize();
    }

    public void Initialize()
    {
        Handle = GtkHelpers.gtk_application_window_new(ParentHandle);
        Controls = new ControlsContainer(Handle, this);
        TitleBar = new FormTitleBarControl(this, Controls.Handle, this);
        SubscribeToGtkSignals();
    }

    void SubscribeToGtkSignals()
    {
        GtkHelpers.g_signal_connect_data(
            Handle,
            "map",
            (GtkHelpers.MapCallback)SignalOnWidgetMapped,
            IntPtr.Zero,
            IntPtr.Zero,
            0
        );
        GtkHelpers.g_signal_connect_data(
            Handle,
            "close-request",
            (GtkHelpers.CloseRequestCallback)SignalOnClose,
            IntPtr.Zero,
            IntPtr.Zero,
            0
        );
    }

    void SignalOnWidgetMapped(IntPtr widget, IntPtr _)
    {
        RaiseOnShow();
    }

    bool SignalOnClose(IntPtr windowPtr, IntPtr _)
    {
        // ParentHandle any cleanup if necessary
        Dispose();
        RaiseOnClose();
        return false; // Return true to prevent the window from closing
    }

    public void Invalidate() { }

    public void Show()
    {
        GtkHelpers.gtk_widget_show(Handle);
        Controls.Show();
        TitleBar.Show();
    }

    public void Dispose()
    {
        // GtkHelpers.gtk_widget_unparent(Handle);
        // GtkHelpers.g_object_unref(Handle);
        // Controls.Dispose();
    }
}
