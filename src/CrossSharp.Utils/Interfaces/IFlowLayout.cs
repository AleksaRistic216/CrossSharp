using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IFlowLayout : IControlsContainer, IScrollable
{
    int ItemsSpacing { get; set; }
    EventHandler? ItemsSpacingChanged { get; set; }
    FlowAlignment JustifyContentHorizontal { get; set; }
    FlowAlignment JustifyContentVertical { get; set; }
    EventHandler? JustifyContentChanged { get; set; }
}
