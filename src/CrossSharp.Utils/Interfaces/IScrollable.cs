using System.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IScrollable : ISizeProvider, ILocationProvider
{
    ScrollableMode Scrollable { get; set; }
    Rectangle Viewport { get; set; }
    Rectangle ContentBounds { get; set; }
}
