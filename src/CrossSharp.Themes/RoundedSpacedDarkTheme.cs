using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class RoundedSpacedDarkTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public RenderStyle Style { get; set; } = RenderStyle.Contained;
    public int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.DarkGray;
    public ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.DimGray;
    public ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.Gray;
    public ColorRgba InputBackgroundColor { get; set; } = ColorRgba.LightGray;
    public int DefaultCornerRadius { get; set; } = 12;
}
