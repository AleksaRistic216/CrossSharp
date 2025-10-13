using System.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IScrollable
{
    ScrollableMode Scrollable { get; set; }
    Rectangle Viewport { get; set; }
}
