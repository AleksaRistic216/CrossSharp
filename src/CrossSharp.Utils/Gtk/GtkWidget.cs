using System.Drawing;
using CrossSharp.Utils.Interfaces;
namespace CrossSharp.Utils.Gtk;

public abstract class GtkWidget : IControl, ISizeProvider {
    public IntPtr Handle { get; set; } = GtkHelpers.gtk_drawing_area_new();
    public IntPtr ParentHandle { get; set; }
    int _width = 0;
    public int Width {
        get => _width;
        set {
            _width = value;
            if (Handle != IntPtr.Zero)
                GtkHelpers.gtk_widget_set_size_request(Handle, _width, _height);
        }
    }
    int _height = 0;
    public int Height {
        get => _height;
        set {
            _height = value;
            if (Handle != IntPtr.Zero)
                GtkHelpers.gtk_widget_set_size_request(Handle, _width, _height);
        }
    }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    protected abstract void OnDraw(IntPtr area, IntPtr cr, int width, int height, IntPtr data);
    public void Initialize() {
        GtkHelpers.DrawFunc drawFunc = OnDraw;
        GtkHelpers.gtk_drawing_area_set_draw_func(Handle, drawFunc, IntPtr.Zero, IntPtr.Zero);
    }
    public void Show() {
        IntPtr parent = GtkHelpers.gtk_widget_get_parent(Handle);
        if(parent == ParentHandle)
            return;
        GtkHelpers.gtk_widget_show(Handle);
        GtkHelpers.gtk_fixed_put(ParentHandle, Handle, 0, 0);
    }
    public void Dispose() {
        GtkHelpers.gtk_widget_unparent(Handle);
        GtkHelpers.g_object_unref(Handle);
    }
}