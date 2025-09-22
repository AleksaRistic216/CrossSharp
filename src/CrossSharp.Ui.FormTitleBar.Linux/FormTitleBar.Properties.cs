using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class FormTitleBar
{
    #region private
    static int _height = 36;
    static int _applicationButtonWidth = 36;
    const int MOVEMENT_TRESHOLD = 1;
    int _coreFps = Services.GetSingleton<IApplicationConfiguration>().CoreFps;
    Task? _formDragTask;
    CancellationTokenSource? _formDragCancellationTokenSource;
    Point? _formDragDestination = null;
    DateTime? _lastFormDragTime;
    int _deltaX = 0;
    int _deltaY = 0;
    TitleBarType _type = TitleBarType.CrossSharp;
    Panel _mainPanel;
    FormTitleBarMaximizeButton? _maximizeButton;
    Button? _closeButton;
    Label? _titleLabel;
    IForm _form;
    int _width = 0;
    Point? _mouseDownMousePosition;
    Point? _mouseDownFormPosition;
    #endregion

    #region exposed
    public TitleBarType Type
    {
        get => _type;
        set
        {
            if (_type == value)
                return;
            _type = value;
            RaiseTypeChanged();
        }
    }
    public int Width
    {
        get => _width;
        set
        {
            if (_width == value)
                return;
            _mainPanel.Width = value;
            _width = value;
            OnSizeChanged?.Invoke(this, new Size(_width, _height));
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
            _mainPanel.Height = _height;
            OnSizeChanged?.Invoke(this, new Size(_width, _height));
        }
    }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public bool IsMouseOver { get; set; }
    public Point Location { get; set; } = new(0, 0);
    public EventHandler<Point>? OnLocationChanged { get; set; }
    #endregion
}
