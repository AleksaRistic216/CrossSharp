using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IStackedLayout : IControlsContainer, IPadding, IScrollable
{
    int ItemsSpacing { get; set; }
    Direction Direction { get; set; }
}
