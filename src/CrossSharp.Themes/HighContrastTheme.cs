using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class HighContrastTheme : ThemeBase
{
    public override RenderStyle Style { get; set; } = RenderStyle.Contained;
    public override int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public override FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public override ColorRgba LayoutBackgroundColor { get; set; } = Constants.DefaultLayoutBackgroundColorDark;
    public override ColorRgba PrimaryColor { get; set; } = ColorRgba.Black;
    public override ColorRgba SecondaryColor { get; set; } = ColorRgba.White;
    public override int DefaultCornerRadius { get; set; } = Constants.DEFAULT_CONTAINED_CORNER_RADIUS;
    public override int DefaultLayoutItemSpacing { get; set; } = Constants.DEFAULT_CONTAINED_LAYOUT_ITEM_SPACING;
}
