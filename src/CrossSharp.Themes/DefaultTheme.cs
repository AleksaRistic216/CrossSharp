using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class DefaultTheme : ITheme
{
    bool _useNativeTitleBar;
    public RenderStyle Style { get; set; } = RenderStyle.Flat;
    public int DefaultFontSize { get; set; } = 16;
    public FontFamily DefaultFontFamily { get; set; } = FontFamily.Default;
    public virtual ColorRgba BackgroundColor { get; set; } = ColorRgba.LimitlessBackground;
    public ColorRgba SecondaryBackgroundColor { get; set; } = ColorRgba.LimitlessPrimary;
    public virtual ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.LimitlessSecondary;
    public ColorRgba InputBackgroundColor { get; set; } = ColorRgba.White;
    public virtual bool UseNativeTitleBar
    {
        get => _useNativeTitleBar;
        set => _useNativeTitleBar = value;
    }
    public virtual int RoundedCornersRadius { get; set; } = 8;
    public virtual int FormBorderWidth { get; set; } = 2;
    public virtual ColorRgba FormBorderColor { get; set; } = ColorRgba.ReallyDarkGray;
    public virtual ColorRgba TitleBarBackgroundColor { get; set; } = ColorRgba.DarkGray;
    public virtual ColorRgba TitleBarForegroundColor { get; set; } = ColorRgba.WhiteSmoke;
}
