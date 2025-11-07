using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

class Panel : ControlBase, IPanel
{
    public int CornerRadius { get; set; }
    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public EventHandler? BackgroundColorChanged { get; set; }

    protected Panel()
    {
        PerformTheme();
    }

    public sealed override void PerformTheme()
    {
        OnThemePerformed();
    }

    public override void Invalidate() { }
}
