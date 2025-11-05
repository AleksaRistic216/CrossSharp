using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class HighContrastTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public virtual RenderStyle Style { get; set; } = RenderStyle.Contained;
    public virtual int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public virtual FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public virtual ColorRgba BackgroundColor { get; set; } = ColorRgba.Black;
    public virtual ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.White;
    public virtual ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.Yellow;
    public virtual ColorRgba InputBackgroundColor { get; set; } = ColorRgba.Cyan;
    public virtual int DefaultCornerRadius { get; set; } =
        Constants.DEFAULT_CONTAINED_CORNER_RADIUS;
    public virtual int DefaultLayoutItemSpacing { get; set; } =
        Constants.DEFAULT_CONTAINED_LAYOUT_ITEM_SPACING;
}
