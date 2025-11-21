using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class ContainedSpacedLightTheme : ITheme
{
    public RenderStyle Style { get; set; } = RenderStyle.Contained;
    public int DefaultFontSize { get; set; } = 16;
    public FontFamily DefaultFontFamily { get; set; } = FontFamily.Default;
    public ColorRgba LayoutBackgroundColor { get; set; } = ColorRgba.FloralWhite;
    public ColorRgba PrimaryColor { get; set; } = ColorRgba.FromBytes(161, 214, 178, 255);
    public ColorRgba SecondaryColor { get; set; } = ColorRgba.LightGray;
    public int DefaultCornerRadius { get; set; } = 0;
    public int DefaultLayoutItemSpacing { get; set; } = 0;
}
