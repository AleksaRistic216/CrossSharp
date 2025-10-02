using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class DefaultTheme : ITheme
{
    bool _useNativeTitleBar = false;
    public virtual ColorRgba BackgroundColor { get; set; } = ColorRgba.WhiteSmoke;
    public virtual ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.LightGray;
    public virtual bool UseNativeTitleBar
    {
        get => _useNativeTitleBar;
        set => _useNativeTitleBar = value;
    }
    public int RoundedCornersRadius { get; set; } = 8;
    public int FormBorderWidth { get; set; } = 2;
    public ColorRgba FormBorderColor { get; set; } = ColorRgba.ReallyDarkGray;
    public ColorRgba TitleBarBackgroundColor { get; set; } = ColorRgba.DarkGray;
    public ColorRgba TitleBarForegroundColor { get; set; } = ColorRgba.WhiteSmoke;
}
