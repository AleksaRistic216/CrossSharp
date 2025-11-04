using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

static class ScrollableHelpers
{
    static float GetScrolledPercentX(Rectangle viewPort, IScrollable scrollable)
    {
        if (viewPort.X == 0 || scrollable.ContentBounds.Width <= scrollable.Width)
            return 0.0f;
        return (float)viewPort.X
            / ((float)scrollable.ContentBounds.Width - (float)scrollable.Width);
    }

    static float GetScrolledPercentY(Rectangle viewPort, IScrollable scrollable)
    {
        if (viewPort.Y == 0 || scrollable.ContentBounds.Height <= scrollable.Height)
            return 0.0f;
        return (float)viewPort.Y
            / ((float)scrollable.ContentBounds.Height - (float)scrollable.Height);
    }

    internal static void Scroll(
        Orientation orientation,
        int amount,
        IScrollable scrollable,
        ref Rectangle viewPort
    )
    {
        if (orientation == Orientation.Vertical)
        {
            var scrollPercentY = GetScrolledPercentY(viewPort, scrollable);
            if (amount < 0 && scrollPercentY >= 1.0f || amount > 0 && scrollPercentY <= 0.0f)
                return;
            var scrollY = Math.Max(viewPort.Y - amount, 0);
            if (scrollY + viewPort.Height > scrollable.ContentBounds.Height)
                scrollY = scrollable.ContentBounds.Height - viewPort.Height;
            viewPort = new Rectangle(viewPort.X, scrollY, viewPort.Width, viewPort.Height);
        }
        else
        {
            var scrollPercentX = GetScrolledPercentX(viewPort, scrollable);
            if (amount < 0 && scrollPercentX >= 1.0f || amount > 0 && scrollPercentX <= 0.0f)
                return;
            var scrollX = Math.Max(viewPort.X - amount, 0);
            if (scrollX + viewPort.Width > scrollable.ContentBounds.Width)
                scrollX = scrollable.ContentBounds.Width - viewPort.Width;
            viewPort = new Rectangle(scrollX, viewPort.Y, viewPort.Width, viewPort.Height);
        }
    }

    internal static void DrawScrollBar<T>(ref IGraphics g, T scrollable)
        where T : IScrollable, ISizeProvider, ILocationProvider, IChild
    {
        if (scrollable.Scrollable == ScrollableMode.None)
            return;
        if (
            scrollable.ContentBounds.Width <= scrollable.Width
            && scrollable.ContentBounds.Height <= scrollable.Height
        )
            return;
        if (
            scrollable.Scrollable == ScrollableMode.Vertical
            && scrollable.ContentBounds.Height <= scrollable.Height
        )
            return;
        if (
            scrollable.Scrollable == ScrollableMode.Horizontal
            && scrollable.ContentBounds.Width <= scrollable.Width
        )
            return;
        const int barThickness = 20;
        const int barSizeK = 5;
        ColorRgba barColor = Services.GetSingleton<ITheme>().SecondaryBackgroundColor;
        barColor.A = 0.2f;
        if (scrollable.Scrollable == ScrollableMode.None)
            return;
        var viewPort = scrollable.Viewport;
        var clientBounds = scrollable.GetClientBounds();
        var offsetX = clientBounds.X;
        var offsetY = clientBounds.Y;
        g.SetClip(
            new Rectangle(clientBounds.X, clientBounds.Y, clientBounds.Width, clientBounds.Height),
            0
        );
        g.SetOffset(offsetX, offsetY);
        var scrolledPercentX = GetScrolledPercentX(viewPort, scrollable);
        var scrolledPercentY = GetScrolledPercentY(viewPort, scrollable);
        if (
            scrollable.Scrollable == ScrollableMode.Horizontal
            || scrollable.Scrollable == ScrollableMode.Both
        )
        {
            var barWidth = scrollable.Width / barSizeK;
            var barX = (scrolledPercentX * (scrollable.Width - barWidth));
            g.FillRectangle(
                (int)barX,
                scrollable.Height - barThickness,
                barWidth,
                barThickness,
                barColor
            );
        }
        if (
            scrollable.Scrollable == ScrollableMode.Vertical
            || scrollable.Scrollable == ScrollableMode.Both
        )
        {
            var barHeight = scrollable.Height / barSizeK;
            var barY = (scrolledPercentY * (scrollable.Height - barHeight));
            g.FillRectangle(
                scrollable.Width - barThickness,
                (int)barY,
                barThickness,
                barHeight,
                barColor
            );
        }
    }
}
