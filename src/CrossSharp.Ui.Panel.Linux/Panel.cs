using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class Panel : ControlBase, IPanel
{
    public ColorRgba BackgroundColor { get; set; }
    public EventHandler? OnBackgroundColorChange { get; set; }

    public override void Initialize() { }

    public override void Invalidate() { }

    public override void Redraw() { }
}
