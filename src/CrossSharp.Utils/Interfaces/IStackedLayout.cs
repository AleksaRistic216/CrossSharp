using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IStackedLayout : IControlsContainer, IPadding, IScrollable, IMaxSize
{
    int ItemsSpacing { get; set; }
    Orientation Orientation { get; set; }
    EventHandler? OrientationChanged { get; set; }
}
