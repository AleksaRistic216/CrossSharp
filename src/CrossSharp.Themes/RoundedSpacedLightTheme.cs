using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class RoundedSpacedLightTheme : ThemeBase
{
    public override RenderStyle Style { get; set; } = RenderStyle.Contained;
    public override int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public override FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public override ColorRgba LayoutBackgroundColor { get; set; } = Constants.DefaultLayoutBackgroundColorLight;
    public override ColorRgba PrimaryColor { get; set; } = ColorRgba.White;
    public override ColorRgba SecondaryColor { get; set; } = ColorRgba.LightGray;
    public override int DefaultCornerRadius { get; set; } = Constants.DEFAULT_ROUNDED_CORNER_RADIUS;
    public override int DefaultLayoutItemSpacing { get; set; } = Constants.DEFAULT_ROUNDED_LAYOUT_ITEM_SPACING;
}
