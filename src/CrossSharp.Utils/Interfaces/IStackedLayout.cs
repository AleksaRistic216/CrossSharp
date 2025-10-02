using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IStackedLayout : IControl, IBackgroundColorProvider, IControlsContainer
{
    Direction ItemsDirection { get; set; }
}
