using System.Drawing;
using System.Runtime.InteropServices;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.X11;

namespace CrossSharp.Ui.Linux;

partial class Form : IForm
{
    public Form()
    {
        AppInstance = Services.GetSingleton<IApplication>();
        ParentHandle = AppInstance.MainWindowHandle;
        Initialize();
    }

    public void Initialize()
    {
        Handle = GtkHelpers.gtk_application_window_new(ParentHandle);
        Controls = new ControlsContainer(Handle, this, this);
        Controls.Parent = this;
        SubscribeToGtkSignals();
        Invalidate();
    }

    public void Close()
    {
        Dispose();
        GtkHelpers.gtk_window_close(Handle);
    }

    public void PerformLayout()
    {
        Invalidate();
        Redraw();
    }

    public void Minimize()
    {
        GtkHelpers.gtk_window_minimize(Handle);
    }

    public void Maximize()
    {
        GtkHelpers.gtk_window_maximize(Handle);
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
        GtkHelpers.g_signal_connect_data(
            Handle,
            "notify::maximized",
            (GtkHelpers.MaximizedCallback)SignalOnMaximized,
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

    void SignalOnMaximized(IntPtr widget, IntPtr a)
    {
        var maximized = GtkHelpers.gtk_window_is_maximized(Handle);
        if (!maximized)
            return;
        var screenSize = GetScreenSize();
        this.Location = new Point(0, 0);
        this.Width = screenSize.Width;
        this.Height = screenSize.Height;
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
        X11Helpers.XFlush(x11Display);
    }

    Size GetScreenSize()
    {
        if (WindowSurfaceHandle == IntPtr.Zero)
            return Size.Empty;
        uint x11Surface = GtkHelpers.gdk_x11_surface_get_xid(WindowSurfaceHandle);
        if (x11Surface == 0)
            return Size.Empty;
        IntPtr x11Display = GtkHelpers.gdk_x11_display_get_xdisplay(DisplayHandle);
        if (x11Display == IntPtr.Zero)
            return Size.Empty;

        X11Helpers.XGetWindowAttributes(x11Display, x11Surface, out XWindowAttributes attrs);
        return new Size(attrs.width, attrs.height);
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

    public void Invalidate()
    {
        if (!UseNativeTitleBar && TitleBar is null)
        {
            TitleBar = new FormTitleBar(this);
            GtkHelpers.gtk_window_set_decorated(Handle, false);
        }
        if (UseNativeTitleBar)
        {
            TitleBar = null;
            GtkHelpers.gtk_window_set_decorated(Handle, true);
        }
        TitleBar?.Invalidate();
        foreach (var control in Controls.Items)
            control.Invalidate();
    }

    public void Show()
    {
        GtkHelpers.gtk_widget_show(Handle);
        Controls.Show();
        TitleBar?.Show();
    }

    public void Redraw()
    {
        // Controls.Redraw();
        // TitleBar.Redraw();
    }

    public void Dispose()
    {
        Controls.Dispose();
    }
}
