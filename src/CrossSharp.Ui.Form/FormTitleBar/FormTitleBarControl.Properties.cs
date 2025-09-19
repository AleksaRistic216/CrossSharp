using System.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl
{
    #region private
    const int MOVEMENT_TRESHOLD = 5;
    const float MOVEMENT_FPS = 60;
    DateTime _lastDragTime = DateTime.MinValue;
    int _deltaX = 0;
    int _deltaY = 0;
    TitleBarType _type = TitleBarType.CrossSharp;
    PanelControl _mainPanel;
    PanelControl _applicationButtonsPanel;
    ITitleBarProvider _titleBarProvider;
    int _height = 36;
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
            _applicationButtonsPanel.Height = _height;
            OnSizeChanged?.Invoke(this, new Size(_width, _height));
        }
    }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public bool IsMouseOver { get; set; }
    public Point Location { get; set; } = new(0, 0);
    public EventHandler<Point>? OnLocationChanged { get; set; }
    #endregion
}
