using System.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

public static class ControlsHelpers
{
    public static IForm? GetForm(this IControl control)
    {
        return control.Parent switch
        {
            IForm form => form,
            IControl parentControl => parentControl.GetForm(),
            _ => null,
        };
    }

    public static Rectangle GetClientBounds(this IControl control)
    {
        var location = control.Location;
        var parent = control.Parent;
        while (parent is IControl parentControl)
        {
            if (parent is IForm)
                break;
            location.Offset(parentControl.Location);
            parent = parentControl.Parent;
        }
        return new Rectangle(location, control.Size);
    }

    public static Rectangle GetScreenBounds(this IControl control)
    {
        var location = control.Location;
        var parent = control.Parent;
        while (parent is IControl parentControl)
        {
            location.Offset(parentControl.Location);
            parent = parentControl.Parent;
        }
        return new Rectangle(location, control.Size);
    }
}
