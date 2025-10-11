using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IDockable : ISizeProvider, ILocationProvider, IChild
{
    DockPosition Dock { get; set; }
}
