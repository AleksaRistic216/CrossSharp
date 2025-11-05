using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class FlatPinkTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public RenderStyle Style { get; set; } = RenderStyle.Flat;
    public int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Pink;
    public ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.LightPink;
    public ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.HotPink;
    public ColorRgba InputBackgroundColor { get; set; } = ColorRgba.MediumVioletRed;
    public int DefaultCornerRadius { get; set; } = 0;
}
