using System.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Helpers;

static class ScrollableHelpers
{
    internal static void Scroll(Direction direction, int amount, ref Rectangle viewPort)
    {
        if (direction == Direction.Vertical)
        {
            viewPort = new Rectangle(
                viewPort.X,
                Math.Max(viewPort.Y - amount, 0),
                viewPort.Width,
                viewPort.Height
            );
        }
        else
        {
            viewPort = new Rectangle(
                Math.Max(viewPort.X - amount, 0),
                viewPort.Y,
                viewPort.Width,
                viewPort.Height
            );
        }
    }
}
