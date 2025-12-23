using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Common;

/// <summary>
/// Handles SDL hit test callbacks for borderless window resizing and dragging.
/// </summary>
sealed class FormSDLHitTestHandler
{
    const int RESIZE_BORDER_THICKNESS = 8;
    const int TITLE_BAR_HEIGHT = 35;
    const int TITLE_BAR_BUTTONS_WIDTH = 150; // 3 buttons * 50px each

    readonly FormSDL _form;
    readonly SDL_HitTest _hitTestDelegate;

    public FormSDLHitTestHandler(FormSDL form)
    {
        _form = form;
        _hitTestDelegate = HitTestCallback;
    }

    public void Register()
    {
        SDLHelpers.SDL_SetWindowHitTest(_form.Handle, _hitTestDelegate, IntPtr.Zero);
    }

    public void Unregister()
    {
        SDLHelpers.SDL_SetWindowHitTest(_form.Handle, null, IntPtr.Zero);
    }

    SDLHitTestResult HitTestCallback(IntPtr window, ref SDL_Point point, IntPtr data)
    {
        SDLHelpers.SDL_GetWindowSize(window, out int width, out int height);

        int x = point.x;
        int y = point.y;

        // Check if window is maximized - no resize allowed
        var flags = SDLHelpers.SDL_GetWindowFlags(window);
        bool isMaximized = (flags & SDLWindowFlags.MAXIMIZED) != 0;

        if (isMaximized)
        {
            // Only allow title bar dragging for maximized windows
            if (y < TITLE_BAR_HEIGHT && x < width - TITLE_BAR_BUTTONS_WIDTH)
                return SDLHitTestResult.SDL_HITTEST_DRAGGABLE;
            return SDLHitTestResult.SDL_HITTEST_NORMAL;
        }

        // Check if over buttons on the top-right (within title bar); in that case abort
        if (y < TITLE_BAR_HEIGHT && x >= width - TITLE_BAR_BUTTONS_WIDTH)
            return SDLHitTestResult.SDL_HITTEST_NORMAL;

        // Check edges and corners
        bool onLeft = x < RESIZE_BORDER_THICKNESS;
        bool onRight = x >= width - RESIZE_BORDER_THICKNESS;
        bool onTop = y < RESIZE_BORDER_THICKNESS;
        bool onBottom = y >= height - RESIZE_BORDER_THICKNESS;

        // Corner detection (takes priority)
        if (onTop && onLeft)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_TOPLEFT;
        if (onTop && onRight)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_TOPRIGHT;
        if (onBottom && onLeft)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_BOTTOMLEFT;
        if (onBottom && onRight)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_BOTTOMRIGHT;

        // Edge detection
        if (onTop)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_TOP;
        if (onBottom)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_BOTTOM;
        if (onLeft)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_LEFT;
        if (onRight)
            return SDLHitTestResult.SDL_HITTEST_RESIZE_RIGHT;

        // Title bar area (below top resize edge, within title bar height)
        // Exclude the window control buttons area on the right
        if (y >= RESIZE_BORDER_THICKNESS && y < TITLE_BAR_HEIGHT)
        {
            if (x < width - TITLE_BAR_BUTTONS_WIDTH)
                return SDLHitTestResult.SDL_HITTEST_DRAGGABLE;
        }

        return SDLHitTestResult.SDL_HITTEST_NORMAL;
    }
}
