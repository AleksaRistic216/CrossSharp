using System.Runtime.InteropServices;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.X11;

namespace CrossSharp.Ui.Linux;

partial class Form : IForm
{
    public object Parent { get; set; } = null!;
    public IntPtr DisplayHandle { get; set; }
    public IntPtr WindowSurfaceHandle { get; set; }

    public void Close()
    {
        Dispose();
        GtkHelpers.gtk_window_close(Handle);
    }

    public void PerformLayout()
    {
        Controls.Redraw();
    }

    public Form()
    {
        AppInstance = ServicesPool.GetSingleton<IApplication>();
        ParentHandle = AppInstance.MainWindowHandle;
        Initialize();
    }

    public int ZIndex { get; set; }

    public void Initialize()
    {
        Handle = GtkHelpers.gtk_application_window_new(ParentHandle);
        Controls = new ControlsContainer(Handle, this, this);
        Controls.Parent = this;
        TitleBar = new FormTitleBar(this);
        SubscribeToGtkSignals();
        OnLocationChanged += (s, e) =>
        {
            UpdatePositionX11();
        };
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
        GtkHelpers.g_signal_connect_data(
            Handle,
            "realize",
            (GtkHelpers.RealizeCallback)SignalOnRealize,
            IntPtr.Zero,
            IntPtr.Zero,
            0
        );
    }

    void SignalOnRealize(IntPtr widget, IntPtr a)
    {
        // Code bellow works! Leave it here for future reference
        WindowSurfaceHandle = GtkHelpers.gtk_native_get_surface(widget);
        DisplayHandle = GtkHelpers.gtk_root_get_display(widget);
        IntPtr namePtr = GtkHelpers.gdk_display_get_name(DisplayHandle);
        IntPtr displayType = GtkHelpers.g_type_name_from_instance(DisplayHandle);
        string n = Marshal.PtrToStringAuto(namePtr) ?? "unknown";
        string t = Marshal.PtrToStringAuto(displayType) ?? "unknown";
        // ===
        UpdatePositionX11();
    }

    void UpdatePositionX11()
    {
        if (WindowSurfaceHandle == IntPtr.Zero)
            return;
        uint x11Surface = GtkHelpers.gdk_x11_surface_get_xid(WindowSurfaceHandle);
        if (x11Surface == 0)
            return;
        IntPtr x11Display = GtkHelpers.gdk_x11_display_get_xdisplay(DisplayHandle);
        if (x11Display == IntPtr.Zero)
            return;

        X11Helpers.XMoveWindow(x11Display, x11Surface, Location.X, Location.Y);
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
        Controls.Dispose();
    }
}
