using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Button()
    : CrossControl<IButton>(Services.GetSingleton<IButtonFactory>().Create()),
        IButton
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
    public EventHandler? OnTextChange
    {
        get => Implementation.OnTextChange;
        set => Implementation.OnTextChange = value;
    }
}
