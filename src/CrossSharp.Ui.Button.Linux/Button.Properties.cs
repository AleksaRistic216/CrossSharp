using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
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
    public ColorRgba BackgroundColor { get; set; }
}
