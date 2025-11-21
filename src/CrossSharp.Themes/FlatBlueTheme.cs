using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class FlatBlueTheme : ThemeBase
{
    public override RenderStyle Style { get; set; } = RenderStyle.Flat;
    public override int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public override FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public override ColorRgba LayoutBackgroundColor { get; set; } = Constants.DefaultLayoutBackgroundColorLight;
    public override ColorRgba PrimaryColor { get; set; } = ColorRgba.DodgerBlue;
    public override ColorRgba SecondaryColor { get; set; } = ColorRgba.DeepSkyBlue;
    public override int DefaultCornerRadius { get; set; } = Constants.DEFAULT_FLAT_CORNER_RADIUS;
    public override int DefaultLayoutItemSpacing { get; set; } = Constants.DEFAULT_FLAT_LAYOUT_ITEM_SPACING;
}
