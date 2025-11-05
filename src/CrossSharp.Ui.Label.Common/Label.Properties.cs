using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Label
{
    ITheme Theme => Services.GetSingleton<ITheme>();
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
    public FontFamily FontFamily { get; set; }
    public int FontSize { get; set; }
}
