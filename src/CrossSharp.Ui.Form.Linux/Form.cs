using System.Runtime.InteropServices;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
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

    public void Minimize()
    {
        State = WindowState.Minimized;
    }

    public void Maximize()
    {
        State = WindowState.Maximized;
    }

    public void Restore()
    {
        State = WindowState.Normal;
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
        GtkHelpers.g_signal_connect_data(
            Handle,
            "notify::default-width",
            (GtkHelpers.NotifyResizeCallback)SignalOnResize,
            IntPtr.Zero,
            IntPtr.Zero,
            0
        );
        GtkHelpers.g_signal_connect_data(
            Handle,
            "notify::default-height",
            (GtkHelpers.NotifyResizeCallback)SignalOnResize,
            IntPtr.Zero,
            IntPtr.Zero,
            0
        );
        GtkHelpers.g_signal_connect_data(
            Handle,
            "notify::is-active",
            (GtkHelpers.NotifyCommonCallback)SignalOnIsVisibleChange,
            IntPtr.Zero,
            IntPtr.Zero,
            0
        );
    }

    void SignalOnIsVisibleChange(IntPtr widget, IntPtr a)
    {
        TitleBar?.Redraw();
        PerformLayout();
    }

    void SignalOnResize(IntPtr widget, IntPtr a)
    {
        GtkHelpers.gtk_window_get_default_size(Handle, out int width, out int height);
        // Fallback if GTK doesn't resize for some reason (it happens)
        if (width != _width || height != _height)
            PerformLayout();
        // ===========================
    }

    void SignalOnRealize(IntPtr widget, IntPtr a)
    {
        WindowSurfaceHandle = GtkHelpers.gtk_native_get_surface(widget);
        DisplayHandle = GtkHelpers.gtk_root_get_display(widget);
        InitializeMonitor();
        PerformLayout();
    }

    void InitializeMonitor()
    {
        _monitor = new GdkMonitor(
            GtkHelpers.gdk_display_get_monitor_at_surface(
                DisplayHandle,
                GtkHelpers.gtk_native_get_surface(Handle)
            )
        );
    }

    void SignalOnMaximized(IntPtr widget, IntPtr a)
    {
        throw new Exception("Do not use built int maximize/unmaximize!");
    }

    public void PerformLayout()
    {
        if (_suspendLayout)
            return;
        Invalidate();
        PerformSize();

        GtkInvoker.InvokeAfterTimeout(
            50,
            () =>
            {
                PerformLocation();
                Redraw();
            }
        );
    }

    void PerformSize()
    {
        Controls.Width = _width;
        Controls.Height = _height;
        Controls.PerformLayout();
        GtkHelpers.gtk_window_set_default_size(Handle, _width, _height);
    }

    void PerformLocation()
    {
        GtkInvoker.InvokeSafe(() =>
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
        });
    }

    void SignalOnWidgetMapped(IntPtr widget, IntPtr _)
    {
        string css = """
                window {
                    background-color: rgba(0, 0, 0, 0);
                }
            """;

        IntPtr cssProvider = GtkHelpers.gtk_css_provider_new();
        GtkHelpers.gtk_css_provider_load_from_data(
            cssProvider,
            css,
            (UIntPtr)css.Length,
            IntPtr.Zero
        );
        GtkHelpers.gtk_style_context_add_provider_for_display(DisplayHandle, cssProvider, 600);
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
        InvalidateState();
        InvalidateTitleBar();
        InvalidateControls();
    }

    void InvalidateState()
    {
        if (State == WindowState.Minimized && GtkHelpers.gtk_window_is_active(Handle))
            _state = WindowState.Normal;
    }

    void InvalidateControls()
    {
        foreach (var control in Controls.Items)
            control.Invalidate();
    }

    void InvalidateTitleBar()
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
    }

    public void Show()
    {
        GtkHelpers.gtk_widget_show(Handle);
        Controls.Show();
        TitleBar?.Show();
    }

    public void Redraw()
    {
        Controls.Redraw();
    }

    public void SuspendLayout()
    {
        _suspendLayout = true;
    }

    public void ResumeLayout()
    {
        _suspendLayout = false;
        PerformLayout();
    }

    public void Dispose()
    {
        Controls.Dispose();
    }
}
