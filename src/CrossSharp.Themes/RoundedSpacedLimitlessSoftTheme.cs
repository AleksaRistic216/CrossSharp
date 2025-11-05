using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class RoundedSpacedLimitlessSoftTheme : ITheme
{
    public int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public RenderStyle Style { get; set; } = RenderStyle.Contained;
    public int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.LimitlessBackground;
    public ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.LimitlessPrimary;
    public ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.LimitlessAccent;
    public ColorRgba InputBackgroundColor { get; set; } = ColorRgba.LimitlessInput;
    public int DefaultCornerRadius { get; set; } = Constants.DEFAULT_ROUNDED_CORNER_RADIUS;
    public int DefaultLayoutItemSpacing { get; set; } = Constants.DEFAULT_ROUNDED_LAYOUT_ITEM_SPACING;
}
