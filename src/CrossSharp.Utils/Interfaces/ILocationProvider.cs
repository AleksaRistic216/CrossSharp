using System.Drawing;

namespace CrossSharp.Utils.Interfaces;

public interface ILocationProvider
{
    Point Location { get; set; }
    EventHandler<Point>? LocationChanged { get; set; }
}
