using CrossSharp.Themes;

namespace FirstDemo;

public class CustomTheme : DefaultTheme
{
    public override bool UseNativeTitleBar { get; set; } = false;
}
