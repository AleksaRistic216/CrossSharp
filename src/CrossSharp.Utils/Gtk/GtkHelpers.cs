using System.Runtime.InteropServices;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Label")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Form")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Panel")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui")]

namespace CrossSharp.Utils.Gtk;

static class GtkHelpers
{
    const string GOBJECT = "libgobject-2.0.so.0";
    const string CAIRO = "libcairo.so.2";
    const string GTK = "libgtk-4.so.1";

    [DllImport(GTK)]
    internal static extern IntPtr gtk_label_new(string text);

    [DllImport(GTK)]
    internal static extern IntPtr gtk_window_set_child(IntPtr window, IntPtr child);

    [DllImport(GTK)]
    internal static extern void gtk_widget_show(IntPtr handle);

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

    // Cairo
    [DllImport(CAIRO)]
    internal static extern void cairo_rectangle(
        IntPtr cr,
        double x,
        double y,
        double width,
        double height
    );

    [DllImport(CAIRO)]
    internal static extern void cairo_fill(IntPtr cr);

    [DllImport(CAIRO)]
    internal static extern void cairo_set_source_rgba(
        IntPtr cr,
        double r,
        double g,
        double b,
        double a
    );

    [DllImport(CAIRO)]
    internal static extern IntPtr cairo_create(IntPtr surface);

    [DllImport(CAIRO)]
    internal static extern void cairo_destroy(IntPtr cr);

    [DllImport(CAIRO)]
    internal static extern void cairo_set_source_rgb(IntPtr cr, double r, double g, double b);

    [DllImport(CAIRO)]
    internal static extern void cairo_set_line_width(IntPtr cr, double width);

    [DllImport(CAIRO)]
    internal static extern void cairo_stroke(IntPtr cr);

    [DllImport(CAIRO)]
    internal static extern IntPtr cairo_image_surface_create(int format, int width, int height);

    [DllImport(CAIRO)]
    internal static extern void cairo_surface_destroy(IntPtr surface);

    [DllImport((CAIRO))]
    internal static extern void cairo_translate(IntPtr cr, double tx, double ty);

    [DllImport(CAIRO)]
    internal static extern int cairo_surface_write_to_png(IntPtr surface, string filename);

    // Callbacks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void ActivateCallback(IntPtr app, IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void MapCallback(IntPtr widget, IntPtr userData);

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
}
