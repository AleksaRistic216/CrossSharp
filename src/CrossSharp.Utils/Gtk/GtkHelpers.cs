using System.Runtime.InteropServices;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Label")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Form")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui.Panel")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CrossSharp.Ui")]
namespace CrossSharp.Utils.Gtk;

static class GtkHelpers {
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_label_new(string text);
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_window_set_child(IntPtr window, IntPtr child);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_widget_show(IntPtr handle);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_widget_unparent(IntPtr handle);
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_fixed_new();
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_application_window_new(IntPtr app);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_window_set_title(IntPtr window, string title);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_window_set_default_size(IntPtr window, int width, int height);
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_widget_get_parent(IntPtr widget);
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_application_new(string appId, int flags);
    [DllImport("libgtk-4.so.1")] internal static extern int g_application_run(IntPtr app, int argc, IntPtr argv);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_fixed_put(IntPtr fixedContainer, IntPtr widget, int x, int y);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_widget_set_size_request(IntPtr widget, int width, int height);
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_drawing_area_new();
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_widget_get_type();
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_widget_class_set_layout_manager_type(IntPtr widgetClass, IntPtr layoutType);
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_window_new(int type);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_window_present(IntPtr window);
    [DllImport("libgtk-4.so.1")] internal static extern IntPtr gtk_widget_snapshot(IntPtr widget, IntPtr snapshot);
    [DllImport("libgtk-4.so.1")] public static extern void gtk_fixed_move(IntPtr container, IntPtr widget, int x, int y);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_snapshot_append_color(IntPtr snapshot, ref GtkRgba color, ref GtkRectangle rect);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_fixed_remove(IntPtr fixedContainer, IntPtr widget);
    [DllImport("libgtk-4.so.1")] internal static extern void gtk_drawing_area_set_draw_func(IntPtr drawingArea, DrawFunc drawFunc, IntPtr userData, IntPtr destroyNotify);
    [Obsolete("This crashes for some reason", true)]
    [DllImport("libgdk-4.so.1")] internal static extern void gdk_cairo_set_source_rgba(IntPtr cr, ref GtkRgba color);
    [DllImport("libgtk-4.so.1", CallingConvention = CallingConvention.Cdecl)] internal static extern void gtk_widget_queue_draw(IntPtr widget);



    // Signals
    [DllImport("libgobject-2.0.so.0")] internal static extern void g_object_unref(IntPtr handle);
    [DllImport("libgobject-2.0.so.0")] internal static extern void gtk_container_get_children(IntPtr container, out IntPtr children);
    [DllImport("libgobject-2.0.so.0")] internal static extern ulong g_signal_connect_data(IntPtr instance, string detailed_signal, Delegate handler, IntPtr data, IntPtr destroy_data, uint connect_flags);
    
    // Cairo
    [DllImport("libcairo.so.2")] internal static extern void cairo_rectangle(IntPtr cr, double x, double y, double width, double height);
    [DllImport("libcairo.so.2")] internal static extern void cairo_fill(IntPtr cr);
    [DllImport("libcairo.so.2")] public static extern void cairo_set_source_rgba(IntPtr cr, double r, double g, double b, double a);
    
    
    // Callbacks
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void ActivateCallback(IntPtr app, IntPtr user_data);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void MapCallback(IntPtr widget, IntPtr userData);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate bool CloseRequestCallback(IntPtr window, IntPtr user_data);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void SnapshotHandler(IntPtr widget, IntPtr snapshot, IntPtr user_data);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void DrawFunc(IntPtr drawingArea, IntPtr cairoContext, int width, int height, IntPtr userData);
}