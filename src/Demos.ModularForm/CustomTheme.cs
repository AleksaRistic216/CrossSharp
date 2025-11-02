using CrossSharp.Themes;
using CrossSharp.Utils.Enums;

namespace Demos.ModularForm;

public class CustomTheme : DefaultTheme
{
    public override RenderStyle Style { get; set; } = RenderStyle.Flat;
}
