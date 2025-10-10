using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IStackedLayout : IControlsContainer
{
    Direction Direction { get; set; }
}
