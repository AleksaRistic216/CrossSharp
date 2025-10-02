using CrossSharp.Themes;
using CrossSharp.Utils;

namespace ThemeDemo;

public class MyCustomTheme : DefaultTheme
{
    public override ColorRgba BackgroundColor { get; set; } = ColorRgba.Pink;
    public override ColorRgba TitleBarBackgroundColor { get; set; } = ColorRgba.Purple;
    public override ColorRgba TitleBarForegroundColor { get; set; } = ColorRgba.White;
}
