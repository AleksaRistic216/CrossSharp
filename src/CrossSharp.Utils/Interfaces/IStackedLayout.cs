using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IStackedLayout : IControlsContainer, IPadding, IScrollable, IRoundedCorners
{
    int ItemsSpacing { get; set; }
    Orientation Orientation { get; set; }
    EventHandler? OrientationChanged { get; set; }
}
