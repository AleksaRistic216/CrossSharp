using System.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

static class CoordinateHelpers
{
    internal static Rectangle GetFormRelativeBounds(IRelativeHandle target, Rectangle targetBounds)
    {
        Rectangle rect = Rectangle.Empty;
        rect.Width = targetBounds.Width;
        rect.Height = targetBounds.Height;
        while (target is IRelativeHandle)
        {
            if (target is IForm)
                break;
            if (target is ILocationProvider parent)
            {
                rect.X += parent.Location.X;
                rect.Y += parent.Location.Y;
            }
            if (target.Parent is IRelativeHandle rh)
            {
                target = rh;
                continue;
            }
            break;
        }
        return rect;
    }

    internal static Rectangle GetScreenBounds(
        IForm form,
        IRelativeHandle target,
        Rectangle targetBounds
    )
    {
        var formRelativeBounds = GetFormRelativeBounds(target, targetBounds);
        return new Rectangle(
            form.Location.X + formRelativeBounds.X,
            form.Location.Y + formRelativeBounds.Y,
            targetBounds.Width,
            targetBounds.Height
        );
    }

    internal static Rectangle GetScreenBounds(IForm form, Rectangle targetBounds)
    {
        return new Rectangle(
            form.Location.X + targetBounds.Location.X,
            form.Location.Y + targetBounds.Location.Y,
            targetBounds.Width,
            targetBounds.Height
        );
    }
}
