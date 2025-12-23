using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

static class ScrollableHelpers
{
    internal const int BarThickness = 20;
    const int BarSizeK = 5;

    static float GetScrolledPercentX(Rectangle viewPort, IScrollable scrollable)
    {
        if (viewPort.X == 0 || scrollable.ContentBounds.Width <= scrollable.Width)
            return 0.0f;
        return (float)viewPort.X / ((float)scrollable.ContentBounds.Width - (float)scrollable.Width);
    }

    static float GetScrolledPercentY(Rectangle viewPort, IScrollable scrollable)
    {
        if (viewPort.Y == 0 || scrollable.ContentBounds.Height <= scrollable.Height)
            return 0.0f;
        return (float)viewPort.Y / ((float)scrollable.ContentBounds.Height - (float)scrollable.Height);
    }

    internal static Rectangle GetVerticalScrollTrackRect<T>(T scrollable)
        where T : IScrollable, ISizeProvider, ILocationProvider, IChild
    {
        if (scrollable.Scrollable != ScrollableMode.Vertical && scrollable.Scrollable != ScrollableMode.Both)
            return Rectangle.Empty;
        if (scrollable.ContentBounds.Height <= scrollable.Height)
            return Rectangle.Empty;

        var screenBounds = CoordinateHelpers.GetScreenBounds(scrollable.GetForm()!, scrollable.GetClientBounds());
        return new Rectangle(
            screenBounds.X + scrollable.Width - BarThickness,
            screenBounds.Y,
            BarThickness,
            scrollable.Height
        );
    }

    internal static Rectangle GetHorizontalScrollTrackRect<T>(T scrollable)
        where T : IScrollable, ISizeProvider, ILocationProvider, IChild
    {
        if (scrollable.Scrollable != ScrollableMode.Horizontal && scrollable.Scrollable != ScrollableMode.Both)
            return Rectangle.Empty;
        if (scrollable.ContentBounds.Width <= scrollable.Width)
            return Rectangle.Empty;

        var screenBounds = CoordinateHelpers.GetScreenBounds(scrollable.GetForm()!, scrollable.GetClientBounds());
        return new Rectangle(
            screenBounds.X,
            screenBounds.Y + scrollable.Height - BarThickness,
            scrollable.Width,
            BarThickness
        );
    }

    internal static ScrollDragState BeginVerticalDrag<T>(T scrollable)
        where T : IScrollable, ISizeProvider, ILocationProvider, IChild
    {
        var screenBounds = CoordinateHelpers.GetScreenBounds(scrollable.GetForm()!, scrollable.GetClientBounds());
        var barSize = scrollable.Height / BarSizeK;
        var trackSize = scrollable.Height - barSize;
        var maxScroll = scrollable.ContentBounds.Height - scrollable.Height;
        return new ScrollDragState(screenBounds.Y, barSize, trackSize, maxScroll);
    }

    internal static ScrollDragState BeginHorizontalDrag<T>(T scrollable)
        where T : IScrollable, ISizeProvider, ILocationProvider, IChild
    {
        var screenBounds = CoordinateHelpers.GetScreenBounds(scrollable.GetForm()!, scrollable.GetClientBounds());
        var barSize = scrollable.Width / BarSizeK;
        var trackSize = scrollable.Width - barSize;
        var maxScroll = scrollable.ContentBounds.Width - scrollable.Width;
        return new ScrollDragState(screenBounds.X, barSize, trackSize, maxScroll);
    }

    internal static void ScrollToY(ScrollDragState state, int screenY, ref Rectangle viewPort)
    {
        var localY = screenY - state.ScreenOffset - state.BarSize / 2;
        var percent = Math.Clamp((float)localY / state.TrackSize, 0f, 1f);
        var scrollY = (int)(percent * state.MaxScroll);
        viewPort = new Rectangle(viewPort.X, scrollY, viewPort.Width, viewPort.Height);
    }

    internal static void ScrollToX(ScrollDragState state, int screenX, ref Rectangle viewPort)
    {
        var localX = screenX - state.ScreenOffset - state.BarSize / 2;
        var percent = Math.Clamp((float)localX / state.TrackSize, 0f, 1f);
        var scrollX = (int)(percent * state.MaxScroll);
        viewPort = new Rectangle(scrollX, viewPort.Y, viewPort.Width, viewPort.Height);
    }

    internal sealed class ScrollDragController
    {
        const int ThrottleMs = 8; // ~120fps max
        const int MinDelta = 2; // minimum pixels to trigger update

        readonly ScrollDragState _state;
        readonly bool _isVertical;
        readonly Action _onScrolled;
        readonly Func<Rectangle> _getViewport;
        readonly Action<Rectangle> _setViewport;

        long _lastUpdateTicks;
        int _lastPosition;
        int _pendingPosition;
        int _isProcessing;

        public bool IsVertical { get; }

        public ScrollDragController(
            ScrollDragState state,
            bool isVertical,
            Func<Rectangle> getViewport,
            Action<Rectangle> setViewport,
            Action onScrolled
        )
        {
            _state = state;
            _isVertical = isVertical;
            IsVertical = isVertical;
            _getViewport = getViewport;
            _setViewport = setViewport;
            _onScrolled = onScrolled;
            _lastPosition = int.MinValue;
        }

        public void Update(int screenPosition)
        {
            // Skip if change is too small
            if (Math.Abs(screenPosition - _lastPosition) < MinDelta)
                return;

            _pendingPosition = screenPosition;

            // Throttle: skip if updated too recently
            var now = Environment.TickCount64;
            if (now - _lastUpdateTicks < ThrottleMs)
            {
                // Schedule async update if not already processing
                if (Interlocked.CompareExchange(ref _isProcessing, 1, 0) == 0)
                {
                    ThreadPool.QueueUserWorkItem(_ => ProcessPendingAsync());
                }
                return;
            }

            ApplyUpdate(screenPosition);
        }

        void ProcessPendingAsync()
        {
            var delay = ThrottleMs - (int)(Environment.TickCount64 - _lastUpdateTicks);
            if (delay > 0)
                Thread.Sleep(delay);

            var position = _pendingPosition;
            Interlocked.Exchange(ref _isProcessing, 0);

            // Skip if position hasn't changed enough
            if (Math.Abs(position - _lastPosition) >= MinDelta)
            {
                ApplyUpdate(position);
            }
        }

        void ApplyUpdate(int screenPosition)
        {
            _lastUpdateTicks = Environment.TickCount64;
            _lastPosition = screenPosition;

            var viewPort = _getViewport();
            if (_isVertical)
                ScrollToY(_state, screenPosition, ref viewPort);
            else
                ScrollToX(_state, screenPosition, ref viewPort);
            _setViewport(viewPort);
            _onScrolled();
        }
    }

    internal readonly struct ScrollDragState(int screenOffset, int barSize, int trackSize, int maxScroll)
    {
        public readonly int ScreenOffset = screenOffset;
        public readonly int BarSize = barSize;
        public readonly int TrackSize = trackSize;
        public readonly int MaxScroll = maxScroll;
    }

    internal static void Scroll(Orientation orientation, int amount, IScrollable scrollable, ref Rectangle viewPort)
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
        else if (orientation == Orientation.Horizontal)
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
        if (scrollable.ContentBounds.Width <= scrollable.Width && scrollable.ContentBounds.Height <= scrollable.Height)
            return;
        if (scrollable.Scrollable == ScrollableMode.Vertical && scrollable.ContentBounds.Height <= scrollable.Height)
            return;
        if (scrollable.Scrollable == ScrollableMode.Horizontal && scrollable.ContentBounds.Width <= scrollable.Width)
            return;
        ColorRgba barColor = Services.GetSingleton<ITheme>().SecondaryColor;
        barColor = new ColorRgba(barColor.R, barColor.G, barColor.B, 0.2f);
        if (scrollable.Scrollable == ScrollableMode.None)
            return;
        var viewPort = scrollable.Viewport;
        var clientBounds = scrollable.GetClientBounds();
        var oldState = g.GetClipState();
        var oldOffset = g.GetOffset();
        g.SetClip(ClipState.Create(oldState, clientBounds, 0));
        g.SetOffset(clientBounds.Location);
        var scrolledPercentX = GetScrolledPercentX(viewPort, scrollable);
        var scrolledPercentY = GetScrolledPercentY(viewPort, scrollable);
        if (scrollable.Scrollable == ScrollableMode.Horizontal || scrollable.Scrollable == ScrollableMode.Both)
        {
            var barWidth = scrollable.Width / BarSizeK;
            var barX = (scrolledPercentX * (scrollable.Width - barWidth));
            g.FillRectangle((int)barX, scrollable.Height - BarThickness, barWidth, BarThickness, barColor);
        }
        if (scrollable.Scrollable == ScrollableMode.Vertical || scrollable.Scrollable == ScrollableMode.Both)
        {
            var barHeight = scrollable.Height / BarSizeK;
            var barY = (scrolledPercentY * (scrollable.Height - barHeight));
            g.FillRectangle(scrollable.Width - BarThickness, (int)barY, BarThickness, barHeight, barColor);
        }
        g.SetClip(oldState);
        g.SetOffset(oldOffset);
    }
}
