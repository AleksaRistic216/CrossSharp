using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
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
    public RenderStyle Style
    {
        get => Implementation.Style;
        set => Implementation.Style = value;
    }
    public Alignment TextAlignment
    {
        get => Implementation.TextAlignment;
        set => Implementation.TextAlignment = value;
    }
    public EventHandler? OnTextAlignmentChange
    {
        get => Implementation.OnTextAlignmentChange;
        set => Implementation.OnTextAlignmentChange = value;
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
    public object? Tag
    {
        get => Implementation.Tag;
        set => Implementation.Tag = value;
    }
    public EventHandler? OnTagChange
    {
        get => Implementation.OnTagChange;
        set => Implementation.OnTagChange = value;
    }
}
