using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Button
{
    ITheme _theme = Services.GetSingleton<ITheme>();
    Point _textLocation = Point.Empty;
    string _text = "Button";
    public string Text
    {
        get => _text;
        set
        {
            if (string.Equals(_text, value, StringComparison.Ordinal))
                return;
            _text = value;
            OnTextChangedInternal();
        }
    }
    object? _tag;
    public object? Tag
    {
        get => _tag;
        set
        {
            if (Equals(_tag, value))
                return;
            _tag = value;
            OnTagChangedInternal();
        }
    }
    public ColorRgba BackgroundColor { get; set; } =
        Services.GetSingleton<ITheme>().ButtonBackgroundColor;

    public RenderStyle Style { get; set; }

    Alignment _alignment = Alignment.Center;
    public Alignment TextAlignment
    {
        get => _alignment;
        set
        {
            if (_alignment == value)
                return;
            _alignment = value;
            OnTextAlignmentChangedInternal();
        }
    }
}
