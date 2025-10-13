using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Input() : CrossControl<IInput>(Services.GetSingleton<IInputFactory>().Create()), IInput
{
    public EventHandler? OnClick
    {
        get => Implementation.OnClick;
        set => Implementation.OnClick = value;
    }
    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }
    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set => Implementation.BackgroundColorChanged = value;
    }
    public string Text
    {
        get => Implementation.Text;
        set => Implementation.Text = value;
    }
    public EventHandler? OnTextChanged
    {
        get => Implementation.OnTextChanged;
        set => Implementation.OnTextChanged = value;
    }
    public int FontSize
    {
        get => Implementation.FontSize;
    }
    public bool IsFocused
    {
        get => Implementation.IsFocused;
        set => Implementation.IsFocused = value;
    }
    public EventHandler? OnFocusChanged
    {
        get => Implementation.OnFocusChanged;
        set => Implementation.OnFocusChanged = value;
    }
}
