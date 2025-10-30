using System.Drawing;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Utils.Helpers;

static class MouseHelpers
{
    internal static bool IsMouseOver(IControl control, Point mousePosition)
    {
        if (!IsEventForThisForm(control))
            return false;
        return control.GetScreenBounds().Contains(mousePosition);
    }

    internal static bool IsEventForThisForm(IControl control)
    {
        var form = control.GetForm();
        if (form is null)
            return false;
        var flags = SDLHelpers.SDL_GetWindowFlags(form.Handle);
        bool hasInputFocus = (flags & SDLWindowFlags.INPUT_FOCUS) != 0;
        bool hasMouseFocus = (flags & SDLWindowFlags.MOUSE_FOCUS) != 0;
        return hasInputFocus && hasMouseFocus;
    }
}
