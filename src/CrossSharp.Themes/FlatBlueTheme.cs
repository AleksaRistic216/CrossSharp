using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class FlatBlueTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public virtual RenderStyle Style { get; set; } = RenderStyle.Flat;
    public virtual int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public virtual FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public virtual ColorRgba LayoutBackgroundColor { get; set; } = Constants.DefaultLayoutBackgroundColorLight;
    public virtual ColorRgba PrimaryColor { get; set; } = ColorRgba.Blue;
    public virtual ColorRgba SecondaryColor { get; set; } = ColorRgba.LightBlue;
    public virtual int DefaultCornerRadius { get; set; } = Constants.DEFAULT_FLAT_CORNER_RADIUS;
    public virtual int DefaultLayoutItemSpacing { get; set; } = Constants.DEFAULT_FLAT_LAYOUT_ITEM_SPACING;
}
