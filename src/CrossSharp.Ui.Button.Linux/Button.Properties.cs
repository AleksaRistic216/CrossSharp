using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Button
{
    ITheme _theme = Services.GetSingleton<ITheme>();
    Rectangle? _textBounds;
    string? _text;
    public string? Text
    {
        get => _text;
        set
        {
            if (string.Equals(_text, value, StringComparison.Ordinal))
                return;
            _text = value;
            OnTextChangedInternal();
        }
    }
    object? _tag;
    public object? Tag
    {
        get => _tag;
        set
        {
            if (Equals(_tag, value))
                return;
            _tag = value;
            OnTagChangedInternal();
        }
    }
    public ColorRgba BackgroundColor { get; set; } =
        Services.GetSingleton<ITheme>().ButtonBackgroundColor;

    public RenderStyle Style { get; set; }

    Alignment _alignment = Alignment.Center;
    public Alignment TextAlignment
    {
        get => _alignment;
        set
        {
            if (_alignment == value)
                return;
            _alignment = value;
            OnTextAlignmentChangedInternal();
        }
    }
    SizeF _imageScale = new(1, 1);
    public SizeF ImageScale
    {
        get => _imageScale;
        set
        {
            if (_imageScale == value)
                return;
            _imageScale = value;
            OnImageScaleChangedInternal();
        }
    }
    IEfficientImage? _scaledToFitImage;
    IEfficientImage? _image;
    public IEfficientImage? Image
    {
        get => _image;
        set
        {
            if (Equals(_image, value))
                return;
            _image = value;
            OnImageChangedInternal();
        }
    }
    Rectangle? _imageBounds;
    const int _padding = 4;
    ButtonImagePlacement _imagePlacement = ButtonImagePlacement.BeforeText;
    public ButtonImagePlacement ImagePlacement
    {
        get => _imagePlacement;
        set
        {
            if (_imagePlacement == value)
                return;
            _imagePlacement = value;
            OnImagePlacementChangedInternal();
        }
    }

    public bool AutoSize { get; set; }
    public int? MaxWidth { get; set; }
    public int? MaxHeight { get; set; }
    public bool IsSelected { get; set; }
}
