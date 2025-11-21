using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public abstract class ThemeBase : ITheme
{
    public virtual RenderStyle Style { get; set; }
    public virtual int DefaultFontSize { get; set; }
    public virtual FontFamily DefaultFontFamily { get; set; }
    public virtual ColorRgba LayoutBackgroundColor { get; set; } = ColorRgba.White;
    public virtual ColorRgba PrimaryColor { get; set; } = ColorRgba.Black;
    public virtual ColorRgba SecondaryColor { get; set; } = ColorRgba.Gray;
    public virtual int DefaultCornerRadius { get; set; }
    public virtual int DefaultLayoutItemSpacing { get; set; }
}
