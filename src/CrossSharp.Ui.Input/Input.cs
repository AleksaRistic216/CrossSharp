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
    public EventHandler? OnBackgroundColorChange
    {
        get => Implementation.OnBackgroundColorChange;
        set => Implementation.OnBackgroundColorChange = value;
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
}
