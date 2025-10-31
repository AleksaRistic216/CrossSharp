using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Input() : CrossControl<IInput>(Services.GetSingleton<IInputFactory>().Create()), IInput
{
    public EventHandler? Click
    {
        get => Implementation.Click;
        set => Implementation.Click = value;
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

    public string? Placeholder
    {
        get => Implementation.Placeholder;
        set => Implementation.Placeholder = value;
    }
    public EventHandler? PlaceholderChanged
    {
        get => Implementation.PlaceholderChanged;
        set => Implementation.PlaceholderChanged = value;
    }
    public bool MultiLine
    {
        get => Implementation.MultiLine;
        set => Implementation.MultiLine = value;
    }

    public string Text
    {
        get => Implementation.Text;
        set => Implementation.Text = value;
    }
    public EventHandler? TextChanged
    {
        get => Implementation.TextChanged;
        set => Implementation.TextChanged = value;
    }
    public int FontSize
    {
        get => Implementation.FontSize;
        set => Implementation.FontSize = value;
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
    public int DockIndex
    {
        get => Implementation.DockIndex;
        set => Implementation.DockIndex = value;
    }
    public DockStyle Dock
    {
        get => Implementation.Dock;
        set => Implementation.Dock = value;
    }
    public int CornerRadius
    {
        get => Implementation.CornerRadius;
        set => Implementation.CornerRadius = value;
    }
}
