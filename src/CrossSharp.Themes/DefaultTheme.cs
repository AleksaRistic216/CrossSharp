using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class DefaultTheme : ITheme
{
    public virtual ColorRgba BackgroundColor { get; set; } = ColorRgba.WhiteSmoke;
    public virtual ColorRgba ButtonBackgroundColor { get; set; } = ColorRgba.LightGray;
    public virtual bool UseNativeTitleBar { get; set; } = false;
}
