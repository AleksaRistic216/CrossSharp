using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class RoundedSpacedLimitlessSoftTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public virtual RenderStyle Style { get; set; } = RenderStyle.Contained;
    public virtual int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public virtual FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public virtual ColorRgba BackgroundColor { get; set; } = ColorRgba.LimitlessBackground;
    public virtual ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.LimitlessPrimary;
    public virtual ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.LimitlessAccent;
    public virtual ColorRgba InputBackgroundColor { get; set; } = ColorRgba.LimitlessInput;
    public virtual int DefaultCornerRadius { get; set; } = Constants.DEFAULT_ROUNDED_CORNER_RADIUS;
    public virtual int DefaultLayoutItemSpacing { get; set; } = Constants.DEFAULT_ROUNDED_LAYOUT_ITEM_SPACING;
}
