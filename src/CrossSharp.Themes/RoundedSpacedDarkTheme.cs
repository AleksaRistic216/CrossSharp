using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class RoundedSpacedDarkTheme : ITheme
{
    public virtual RenderStyle Style { get; set; } = RenderStyle.Contained;
    public virtual int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public virtual FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public virtual ColorRgba LayoutBackgroundColor { get; set; } = Constants.DefaultLayoutBackgroundColorDark;
    public virtual ColorRgba PrimaryColor { get; set; } = ColorRgba.DarkGray;
    public virtual ColorRgba SecondaryColor { get; set; } = ColorRgba.DimGray;
    public virtual int DefaultCornerRadius { get; set; } = Constants.DEFAULT_ROUNDED_CORNER_RADIUS;
    public virtual int DefaultLayoutItemSpacing { get; set; } = Constants.DEFAULT_ROUNDED_LAYOUT_ITEM_SPACING;
}
