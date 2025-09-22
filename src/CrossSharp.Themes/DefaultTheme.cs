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
}
