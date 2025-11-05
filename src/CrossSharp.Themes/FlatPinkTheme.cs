using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class FlatPinkTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public virtual RenderStyle Style { get; set; } = RenderStyle.Flat;
    public virtual int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public virtual FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public virtual ColorRgba BackgroundColor { get; set; } = ColorRgba.Pink;
    public virtual ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.LightPink;
    public virtual ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.HotPink;
    public virtual ColorRgba InputBackgroundColor { get; set; } = ColorRgba.MediumVioletRed;
    public virtual int DefaultCornerRadius { get; set; } = Constants.DEFAULT_FLAT_CORNER_RADIUS;
    public virtual int DefaultLayoutItemSpacing { get; set; } =
        Constants.DEFAULT_FLAT_LAYOUT_ITEM_SPACING;
}
