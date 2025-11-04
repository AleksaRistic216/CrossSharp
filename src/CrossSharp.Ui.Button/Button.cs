using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Button()
    : CrossControl<IButton>(Services.GetSingleton<IButtonFactory>().Create()),
        IButton
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
    public RenderStyle Style
    {
        get => Implementation.Style;
        set => Implementation.Style = value;
    }
    public SizeF ImageScale
    {
        get => Implementation.ImageScale;
        set => Implementation.ImageScale = value;
    }
    public EventHandler? OnImageScaleChange
    {
        get => Implementation.OnImageScaleChange;
        set => Implementation.OnImageScaleChange = value;
    }
    public ButtonImagePlacement ImagePlacement
    {
        get => Implementation.ImagePlacement;
        set => Implementation.ImagePlacement = value;
    }
    public EventHandler? OnImagePlacementChange
    {
        get => Implementation.OnImagePlacementChange;
        set => Implementation.OnImagePlacementChange = value;
    }
    public IEfficientImage? Image
    {
        get => Implementation.Image;
        set => Implementation.Image = value;
    }
    public EventHandler? OnImageChange
    {
        get => Implementation.OnImageChange;
        set => Implementation.OnImageChange = value;
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
    public string? Text
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

    public bool AutoSize
    {
        get => Implementation.AutoSize;
        set => Implementation.AutoSize = value;
    }

    public int? MaxWidth
    {
        get => Implementation.MaxWidth;
        set => Implementation.MaxWidth = value;
    }
    public int? MaxHeight
    {
        get => Implementation.MaxHeight;
        set => Implementation.MaxHeight = value;
    }

    public bool IsSelected
    {
        get => Implementation.IsSelected;
        set => Implementation.IsSelected = value;
    }
    public int? MinWidth
    {
        get => Implementation.MinWidth;
        set => Implementation.MinWidth = value;
    }
    public int? MinHeight
    {
        get => Implementation.MinHeight;
        set => Implementation.MinHeight = value;
    }
    public int CornerRadius
    {
        get => Implementation.CornerRadius;
        set => Implementation.CornerRadius = value;
    }
}
