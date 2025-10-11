using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Label
{
    ITheme _theme = Services.GetSingleton<ITheme>();
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
    public FontFamily FontFamily { get; set; } = Services.GetSingleton<ITheme>().DefaultFontFamily;
    public int FontSize { get; set; } = Services.GetSingleton<ITheme>().DefaultFontSize;
}
