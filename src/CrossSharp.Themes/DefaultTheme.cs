using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class DefaultTheme : ITheme
{
    public virtual int AntiAliasingLevel { get; set; } = Constants.ANTI_ALIASING_LEVEL;
    public virtual RenderStyle Style { get; set; } = RenderStyle.Contained;
    public virtual int DefaultFontSize { get; set; } = Constants.DEFAULT_FONT_SIZE;
    public virtual FontFamily DefaultFontFamily { get; set; } = Constants.DEFAULT_FONT_FAMILY;
    public virtual ColorRgba BackgroundColor { get; set; } = ColorRgba.LimitlessBackground;
    public virtual ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.LimitlessPrimary;
    public virtual ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.LimitlessSecondary;
    public virtual ColorRgba InputBackgroundColor { get; set; } = ColorRgba.White;
    public virtual int DefaultCornerRadius { get; set; } = 6;
}
