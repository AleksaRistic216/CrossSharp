using System.Drawing;
using CrossSharp.Utils.Gtk;
namespace CrossSharp.Utils;

public partial class Control {
    #region private
    Point _location = new Point(0, 0);
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
    public Point Location {
        get => _location;
        set {
            if(_location == value)
                return;
            _location = value;
            OnLocationChanged?.Invoke(this, _location);
            if(ParentHandle == IntPtr.Zero || Handle == IntPtr.Zero)
                return;
            
            GtkHelpers.gtk_widget_set_size_request(Handle, Location.X + Width, Location.Y + Height);
            Redraw();
        }
    }
    #endregion
}