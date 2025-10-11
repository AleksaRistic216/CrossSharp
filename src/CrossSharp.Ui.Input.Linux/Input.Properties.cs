using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Input
{
    DateTime _lastCaretStateUpdate = DateTime.MinValue;
    int _caretGap = 4;
    bool _caretVisible = false;
    public ColorRgba BackgroundColor { get; set; } =
        Services.GetSingleton<ITheme>().InputBackgroundColor;

    public int FontSize { get; private set; }
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
}
