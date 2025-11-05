using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

class Panel : ControlBase, IPanel
{
    public ColorRgba BackgroundColor { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }

    public override void Initialize() { }

    public override void PerformTheme() { }

    public override void Invalidate() { }

    public override void Redraw() { }
}
