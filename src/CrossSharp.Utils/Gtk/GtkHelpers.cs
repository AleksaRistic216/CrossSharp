using System.Drawing;
using System.Runtime.InteropServices;
using CrossSharp.Utils.Pango;

namespace CrossSharp.Utils.Gtk;

static class GtkHelpers
{
    const string GOBJECT = "libgobject-2.0.so.0";
    internal const string GTK = "libgtk-4.so.1";

    [DllImport(GTK)]
    internal static extern IntPtr gtk_widget_realize(IntPtr widget);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_widget_get_pango_context(IntPtr widget);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_window_set_child(IntPtr window, IntPtr child);

    [DllImport(GTK)]
    internal static extern void gtk_widget_show(IntPtr handle);

    [DllImport(GTK)]
    internal static extern void gtk_widget_hide(IntPtr handle);

    [DllImport(GTK)]
    internal static extern void gtk_widget_unparent(IntPtr handle);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_fixed_new();

    [DllImport(GTK)]
    internal static extern IntPtr gtk_application_window_new(IntPtr app);

    [DllImport(GTK)]
    internal static extern void gtk_window_set_title(IntPtr window, string title);

    [DllImport(GTK)]
    internal static extern void gtk_window_set_default_size(IntPtr window, int width, int height);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_widget_get_parent(IntPtr widget);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_application_new(string appId, int flags);

    [DllImport(GTK)]
    internal static extern int g_application_run(IntPtr app, int argc, IntPtr argv);

    [DllImport(GTK)]
    internal static extern void gtk_fixed_put(IntPtr fixedContainer, IntPtr widget, int x, int y);

    [DllImport(GTK)]
    internal static extern void gtk_widget_set_size_request(IntPtr widget, int width, int height);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_drawing_area_new();

    [DllImport(GTK)]
    internal static extern IntPtr gtk_widget_get_type();

    [DllImport(GTK)]
    internal static extern IntPtr gtk_widget_class_set_layout_manager_type(
        IntPtr widgetClass,
        IntPtr layoutType
    );

    [DllImport(GTK)]
    internal static extern IntPtr gtk_native_get_surface(IntPtr native);

    [DllImport(GTK)]
    internal static extern uint gdk_x11_surface_get_xid(IntPtr native);

    [DllImport("libgtk-4.so.1")]
    internal static extern IntPtr gdk_x11_display_get_xdisplay(IntPtr display);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_root_get_display(IntPtr widget);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_widget_get_display(IntPtr widget);

    [DllImport(GTK)]
    internal static extern IntPtr gdk_display_get_name(IntPtr display);

    [DllImport(GTK)]
    internal static extern IntPtr gdk_display_get_backend(IntPtr display);

    [DllImport(GTK)]
    internal static extern bool gtk_widget_translate_coordinates(
        IntPtr srcWidget,
        IntPtr destWidget,
        int srcX,
        int srcY,
        out int destX,
        out int destY
    );

    [DllImport(GTK)]
    internal static extern IntPtr gtk_window_new(int type);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_window_close(IntPtr window);

    [DllImport(GTK)]
    internal static extern void gtk_window_present(IntPtr window);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_widget_snapshot(IntPtr widget, IntPtr snapshot);

    [DllImport(GTK)]
    internal static extern void gtk_fixed_move(IntPtr container, IntPtr widget, int x, int y);

    [DllImport(GTK)]
    internal static extern void gtk_snapshot_append_color(
        IntPtr snapshot,
        ref GtkRgba color,
        ref GtkRectangle rect
    );

    [DllImport(GTK)]
    internal static extern void gtk_widget_get_allocation(
        IntPtr widget,
        out GtkAllocation allocation
    );

    [DllImport(GTK)]
    internal static extern void gtk_window_minimize(IntPtr window);

    [DllImport(GTK)]
    internal static extern void gtk_window_maximize(IntPtr window);

    [DllImport(GTK)]
    internal static extern bool gtk_window_is_maximized(IntPtr window);

    [DllImport(GTK)]
    internal static extern void gtk_fixed_remove(IntPtr fixedContainer, IntPtr widget);

    [DllImport(GTK)]
    internal static extern void gtk_drawing_area_set_draw_func(
        IntPtr drawingArea,
        DrawFunc drawFunc,
        IntPtr userData,
        IntPtr destroyNotify
    );

    [DllImport(GTK, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void gtk_widget_queue_draw(IntPtr widget);

    [DllImport(GTK)]
    internal static extern void gtk_window_set_decorated(IntPtr window, bool setting);

    [DllImport(GTK)]
    internal static extern void gtk_widget_set_opacity(IntPtr widget, double opacity); // Works but it does on all the children too

    [DllImport(GTK)]
    internal static extern void gtk_window_set_resizable(IntPtr window, bool setting);

    // Signals
    [DllImport(GOBJECT)]
    internal static extern void g_object_unref(IntPtr handle);

    [DllImport(GOBJECT)]
    internal static extern ulong g_signal_connect(
        IntPtr instance,
        string detailed_signal,
        ResizeCallback c_handler,
        IntPtr data
    );

    [DllImport(GOBJECT)]
    internal static extern IntPtr g_type_name(IntPtr gType);

    [DllImport(GOBJECT)]
    internal static extern IntPtr g_type_name_from_instance(IntPtr instance);

    [DllImport(GOBJECT)]
    internal static extern void gtk_container_get_children(IntPtr container, out IntPtr children);

    [DllImport(GOBJECT)]
    internal static extern ulong g_signal_connect_data(
        IntPtr instance,
        string detailed_signal,
        Delegate handler,
        IntPtr data,
        IntPtr destroy_data,
        uint connect_flags
    );

    // Callbacks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ActivateCallback(IntPtr app, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void MapCallback(IntPtr widget, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void RealizeCallback(IntPtr widget, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void MaximizedCallback(IntPtr window, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate bool CloseRequestCallback(IntPtr window, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void SnapshotHandler(IntPtr widget, IntPtr snapshot, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ResizeCallback(
        IntPtr drawingArea,
        int width,
        int height,
        IntPtr userData
    );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void DrawFunc(
        IntPtr drawingArea,
        IntPtr cairoContext,
        int width,
        int height,
        IntPtr userData
    );

    internal static Size GetTextRenderRect(
        IntPtr handle,
        string text,
        string fontFamily,
        int fontSize
    )
    {
        var pangoContext = gtk_widget_get_pango_context(handle);
        var layout = PangoHelpers.pango_layout_new(pangoContext);
        PangoHelpers.pango_layout_set_text(layout, text, -1);

        IntPtr pangoDesc = PangoHelpers.CreateDescription(
            fontFamily,
            fontSize,
            PangoWeight.Normal, // TODO: Property
            PangoStyle.Normal // TOOD: Property
        );
        PangoHelpers.pango_layout_set_font_description(layout, pangoDesc);
        PangoHelpers.pango_layout_get_size(layout, out int width, out int height);
        return new Size(width / 1024, height / 1024);
    }
}
