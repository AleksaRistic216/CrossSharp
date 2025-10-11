using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IStackedLayout : IControlsContainer, IPadding
{
    int ItemsSpacing { get; set; }
    Direction Direction { get; set; }
}
