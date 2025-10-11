using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IDockable : ISizeProvider, ILocationProvider, IChild
{
    int DockIndex { get; set; }
    DockPosition Dock { get; set; }
}
