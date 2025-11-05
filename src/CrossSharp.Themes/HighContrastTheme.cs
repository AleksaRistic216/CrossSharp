using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class HighContrastTheme : ITheme
{
    public int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public virtual RenderStyle Style { get; set; } = RenderStyle.Contained;
    public int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Black;
    public ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.White;
    public ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.Yellow;
    public ColorRgba InputBackgroundColor { get; set; } = ColorRgba.Cyan;
    public int DefaultCornerRadius { get; set; } = 0;
}
