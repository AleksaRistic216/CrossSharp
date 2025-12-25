using CrossSharp.Utils.Enums;
using CrossSharp.Utils.EventArgs;

namespace CrossSharp.Utils.Interfaces;

public interface IStackedLayout : IControlsContainer, IPadding, IScrollable, IMaxSize
{
    int ItemsSpacing { get; set; }
    Orientation Orientation { get; set; }
    EventHandler? OrientationChanged { get; set; }
    bool ReorderEnabled { get; set; }
    int GrabberSize { get; set; }
    EventHandler<ControlsReorderedEventArgs>? ControlsReordered { get; set; }
}
