using System.Drawing;
using System.Numerics;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Input
{
    DateTime _lastCaretStateUpdate = DateTime.MinValue;
    int _lineGap = 4;
    bool _caretVisible = false;
    public ColorRgba BackgroundColor { get; set; } =
        Services.GetSingleton<ITheme>().InputBackgroundColor;

    Point _caretPosition = Point.Empty;
    Rectangle _placeholderBounds = Rectangle.Empty;
    Rectangle _contentBounds = Rectangle.Empty;
    Rectangle _caretBounds = Rectangle.Empty;
    int _fontSize = Services.GetSingleton<ITheme>().DefaultFontSize;
    public int FontSize
    {
        get => _fontSize;
        set
        {
            if (_fontSize == value)
                return;
            _fontSize = value;
            Invalidate();
        }
    }
    public int CornerRadius { get; set; }
    int LineHeight => MultiLine ? FontSize + _lineGap : Height - _lineGap - BorderWidth * 2;
    bool _multiLine;
    public bool MultiLine
    {
        get => _multiLine;
        set
        {
            if (_multiLine == value)
                return;
            _multiLine = value;
            Invalidate();
        }
    }
    string _text = string.Empty;

    public string Text
    {
        get => _text;
        set
        {
            if (_text == value)
                return;
            _text = value;
            OnTextChangedInternal();
        }
    }

    bool _isFocused = false;
    public bool IsFocused { get; set; }
    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
    string? _placeholder = null;
    public string? Placeholder
    {
        get => _placeholder;
        set
        {
            if (_placeholder == value)
                return;
            _placeholder = value;
            OnPlaceholderChangedInternal();
        }
    }
}
