using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class LightTheme : ThemeBase
{
    public override RenderStyle Style { get; set; } = RenderStyle.Contained;
    public override int DefaultFontSize { get; set; } = 16;
    public override FontFamily DefaultFontFamily { get; set; } = FontFamily.Default;
    public override ColorRgba LayoutBackgroundColor { get; set; } = ColorRgba.FloralWhite;
    public override ColorRgba PrimaryColor { get; set; } = ColorRgba.FromBytes(161, 214, 178, 255);
    public override ColorRgba SecondaryColor { get; set; } = ColorRgba.LightGray;
    public override int DefaultCornerRadius { get; set; } = 0;
    public override int DefaultLayoutItemSpacing { get; set; } = 0;
}
