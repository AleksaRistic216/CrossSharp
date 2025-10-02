using CrossSharp.Utils;

namespace CrossSharp.Ui.Linux;

public partial class Label
{
    string _text = string.Empty;
    public string Text
    {
        get => _text;
        set
        {
            if (_text == value)
                return;
            _text = value;
            RaiseTextChanged();
        }
    }
    string _fontFamily = "Arial";
    public string FontFamily
    {
        get => _fontFamily;
        set
        {
            if (_fontFamily == value)
                return;
            _fontFamily = value;
            RaiseFontFamilyChanged();
        }
    }

    int _fontSize = 12;
    public int FontSize
    {
        get => _fontSize;
        set
        {
            if (_fontSize == value)
                return;
            _fontSize = value;
            RaiseFontSizeChanged();
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
