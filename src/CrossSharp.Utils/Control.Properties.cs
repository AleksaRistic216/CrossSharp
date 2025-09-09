using CrossSharp.Utils.Gtk;
namespace CrossSharp.Utils;

public partial class Control {
    #region private
    int _width = 0;
    int _height = 0;
    #endregion

    #region abstract
    public abstract IntPtr Handle { get; }
    #endregion
    
    #region exposed
    public IntPtr ParentHandle { get; set; }
    public bool Visible { get; set; }
    public int Width {
        get => _width;
        set {
            _width = value;
            if (Handle != IntPtr.Zero)
                GtkHelpers.gtk_widget_set_size_request(Handle, _width, _height);
        }
    }
    public int Height {
        get => _height;
        set {
            _height = value;
            if (Handle != IntPtr.Zero)
                GtkHelpers.gtk_widget_set_size_request(Handle, _width, _height);
        }
    }
    #endregion
}