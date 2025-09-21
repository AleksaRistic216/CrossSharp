using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class Button
{
    Point _textLocation = Point.Empty;
    string _text = string.Empty;

    public ColorRgba BackgroundColor { get; set; } =
        Services.GetSingleton<ITheme>().ButtonBackgroundColor;

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
}
