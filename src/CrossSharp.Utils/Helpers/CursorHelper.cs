using CrossSharp.Utils.SDL;

namespace CrossSharp.Utils.Helpers;

/// <summary>
/// Helper class for managing mouse cursors.
/// </summary>
public static class CursorHelper
{
    static IntPtr _grabCursor = IntPtr.Zero;
    static IntPtr _defaultCursor = IntPtr.Zero;

    /// <summary>
    /// Sets the cursor to a grabbing hand cursor.
    /// </summary>
    public static void SetGrabCursor()
    {
        if (_grabCursor == IntPtr.Zero)
            _grabCursor = SDLHelpers.SDL_CreateSystemCursor(SDLSystemCursor.SDL_SYSTEM_CURSOR_MOVE);

        if (_grabCursor != IntPtr.Zero)
            SDLHelpers.SDL_SetCursor(_grabCursor);
    }

    /// <summary>
    /// Resets the cursor to the default arrow cursor.
    /// </summary>
    public static void ResetCursor()
    {
        if (_defaultCursor == IntPtr.Zero)
            _defaultCursor = SDLHelpers.SDL_GetDefaultCursor();

        if (_defaultCursor != IntPtr.Zero)
            SDLHelpers.SDL_SetCursor(_defaultCursor);
    }
}
