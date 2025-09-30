using System.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public partial class Control
{
    #region private
    Point _location = new Point(0, 0);
    int _width = 0;
    int _height = 0;
    bool _visible = true;
    #endregion

    internal bool _suspendLayout = false;

    #region abstract
    public abstract IntPtr Handle { get; set; }
    #endregion

    #region exposed
    internal IInputHandler InputHandler { get; set; }

    public bool IsMouseOver { get; internal set; }
    public IntPtr ParentHandle { get; set; }
    public object Parent { get; set; }
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
                return;
            _visible = value;
            if (Handle == IntPtr.Zero)
                return;
            if (!_suspendLayout)
            {
                if (_visible)
                    GtkHelpers.gtk_widget_show(Handle);
                else
                    GtkHelpers.gtk_widget_hide(Handle);
            }
        }
    }
    public int Width
    {
        get => _width;
        set
        {
            if (_width == value)
                return;
            _width = value;
            if (Handle == IntPtr.Zero)
                return;
            if (!_suspendLayout)
            {
                GtkHelpers.gtk_widget_set_size_request(
                    Handle,
                    _location.X + _width,
                    _location.Y + _height
                );
                Redraw();
            }
            RaiseOnSizeChanged(new Size(Width, Height));
        }
    }
    public int Height
    {
        get => _height;
        set
        {
            _height = value;
            if (Handle == IntPtr.Zero)
                return;
            if (!_suspendLayout)
            {
                GtkHelpers.gtk_widget_set_size_request(
                    Handle,
                    _location.X + _width,
                    _location.Y + _height
                );
                Redraw();
            }
            RaiseOnSizeChanged(new Size(Width, Height));
        }
    }
    public Point Location
    {
        get => _location;
        set
        {
            if (_location == value)
                return;
            _location = value;
            if (Handle == IntPtr.Zero)
                return;
            if (!_suspendLayout)
            {
                GtkHelpers.gtk_widget_set_size_request(
                    Handle,
                    _location.X + _width,
                    _location.Y + _height
                );
                Redraw();
            }
            RaiseOnLocationChanged(value);
        }
    }
    public int ZIndex { get; set; } = 0;
    #endregion
}
