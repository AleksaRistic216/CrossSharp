using System.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormLinux;

partial class FormLinux
{
    #region private
    int _width = 800;
    int _height = 600;
    Point _location = new(100, 100);
    #endregion
    #region exposed
    public string Title { get; set; } = "Form";
    public IApplication AppInstance { get; }
    public IControlsContainer Controls { get; private set; }
    public IntPtr Handle { get; set; }
    public IntPtr ParentHandle { get; set; }
    public bool Visible { get; set; }
    public int Width
    {
        get => _width;
        set
        {
            if (_width == value)
                return;
            _width = value;
            RaiseOnSizeChanged(new Size(_width, _height));
        }
    }
    public int Height
    {
        get => _height;
        set
        {
            if (_height == value)
                return;
            _height = value;
            RaiseOnSizeChanged(new Size(_width, _height));
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
            RaiseOnLocationChanged(_location);
        }
    }
    #endregion
}
