using System.Runtime.InteropServices;

namespace CrossSharp.Utils.Gtk;

class GdkMonitor : GObject
{
    internal GdkMonitor(IntPtr handle)
        : base(handle) { }

    [DllImport(GtkHelpers.GTK, CharSet = CharSet.Unicode)]
    static extern string gdk_monitor_get_connector(IntPtr monitor);

    internal string GetConnector() => gdk_monitor_get_connector(Handle);

    [DllImport(GtkHelpers.GTK, CharSet = CharSet.Unicode)]
    static extern string gdk_monitor_get_description(IntPtr monitor);

    internal string GetDescription() => gdk_monitor_get_description(Handle);

    [DllImport(GtkHelpers.GTK)]
    static extern IntPtr gdk_monitor_get_display(IntPtr monitor);

    internal GdkDisplay GetDisplay() => new GdkDisplay(gdk_monitor_get_display(Handle));

    [DllImport(GtkHelpers.GTK)]
    static extern int gdk_monitor_get_height_mm(IntPtr monitor);

    internal int GetHeightMm() => gdk_monitor_get_height_mm(Handle);

    [DllImport(GtkHelpers.GTK)]
    static extern int gdk_monitor_get_width_mm(IntPtr monitor);

    internal int GetWidthMm() => gdk_monitor_get_width_mm(Handle);

    [DllImport(GtkHelpers.GTK)]
    static extern void gdk_monitor_get_geometry(IntPtr monitor, out GdkRectangle geometry);

    internal GdkRectangle GetGeometry()
    {
        gdk_monitor_get_geometry(Handle, out var geometry);
        return geometry;
    }
}
