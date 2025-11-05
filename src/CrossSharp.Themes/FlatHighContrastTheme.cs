using CrossSharp.Utils.Enums;

namespace CrossSharp.Themes;

public class FlatHighContrastTheme : HighContrastTheme
{
    public override RenderStyle Style { get; set; } = RenderStyle.Flat;
}
