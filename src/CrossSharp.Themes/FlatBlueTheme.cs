using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class FlatBlueTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public RenderStyle Style { get; set; } = RenderStyle.Flat;
    public int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Blue;
    public ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.LightBlue;
    public ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.DodgerBlue;
    public ColorRgba InputBackgroundColor { get; set; } = ColorRgba.SteelBlue;
    public int DefaultCornerRadius { get; set; } = 0;
}
