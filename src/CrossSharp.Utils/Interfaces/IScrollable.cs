using System.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IScrollable : ISizeProvider, ILocationProvider
{
    ScrollableMode Scrollable { get; set; }

    /// <summary>
    /// Current viewport within the content bounds.
    /// </summary>
    Rectangle Viewport { get; set; }

    /// <summary>
    /// Total content bounds that can be scrolled.
    /// This will be used to calculate scroll percentages and scrollbar sizes.
    /// </summary>
    Rectangle ContentBounds { get; set; }
}
