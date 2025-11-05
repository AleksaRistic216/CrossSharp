using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface ITheme
{
    int AntiAliasingLevel { get; set; }
    RenderStyle Style { get; set; }
    int DefaultFontSize { get; set; }
    FontFamily DefaultFontFamily { get; set; }
    ColorRgba BackgroundColor { get; set; }
    ColorRgba SecondaryBackgroundColor { get; set; }
    ColorRgba ButtonBackgroundColor { get; set; }
    ColorRgba InputBackgroundColor { get; set; }
    int DefaultCornerRadius { get; set; }
    int DefaultLayoutItemSpacing { get; set; }
}
