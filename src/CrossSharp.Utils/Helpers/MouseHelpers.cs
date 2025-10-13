using System.Drawing;

namespace CrossSharp.Utils.Helpers;

static class MouseHelpers
{
    internal static bool IsMouseOver(Rectangle screenBounds, Point mousePosition)
    {
        return screenBounds.Contains(mousePosition);
    }
}
