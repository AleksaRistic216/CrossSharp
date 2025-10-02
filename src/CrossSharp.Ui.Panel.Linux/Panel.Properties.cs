using CrossSharp.Utils;

namespace CrossSharp.Ui.Linux;

public partial class Panel
{
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            RaiseBackgroundColorChanged();
            Redraw();
        }
    }

    ColorRgba _foregroundColor = ColorRgba.Black;
    public ColorRgba ForegroundColor
    {
        get => _foregroundColor;
        set
        {
            if (_foregroundColor == value)
                return;
            _foregroundColor = value;
            // RaiseForegroundColorChanged();
            Redraw();
        }
    }
}
