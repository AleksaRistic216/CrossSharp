using System.Drawing;
using System.Runtime.CompilerServices;
using CrossSharp.Utils.Interfaces;
[assembly: InternalsVisibleTo("CrossSharp.Application")]
namespace CrossSharp.Ui;

public partial class Form {
    readonly IApplication _appInstance;
    public IntPtr Handle { get; internal set; }
    public IntPtr ParentHandle { get; internal set; }
    public ControlsContainer Controls { get; internal set; }
    public Point Location { get; set; }
    int _width;
    public int Width {
        get => _width;
        set {
            if(value == _width)
                return;
            _width = value;
            RaiseSizeChanged(new Size(_width, _height));
        }
    }
    int _height;
    public int Height {
        get => _height;
        set {
            if(value == _height)
                return;
            _height = value;
            RaiseSizeChanged(new Size(_width, _height));
        }
    }
    public string Title { get; set; } = "Form";
}