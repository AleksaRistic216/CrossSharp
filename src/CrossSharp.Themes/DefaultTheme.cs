using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Themes;

public class DefaultTheme : ITheme
{
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.WhiteSmoke;
}
