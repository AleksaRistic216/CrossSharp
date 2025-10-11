using System.Drawing;
using CrossSharp.Utils.Enums;
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

    public static void PerformDocking(this IDockable control)
    {
        if (control.Dock == DockPosition.None)
            return;
        if (control.Parent is not IControlsContainer parent)
            return;
        var parentBounds = new Rectangle(Point.Empty, parent.Size);
        foreach (var sibling in parent)
        {
            if (
                Equals(sibling, control)
                || sibling is not IDockable dockedSibling
                || dockedSibling.Dock == DockPosition.None
            )
                continue;
            var siblingBounds = new Rectangle(dockedSibling.Location, dockedSibling.Size);
            switch (dockedSibling.Dock)
            {
                case DockPosition.Top:
                    parentBounds.Y += siblingBounds.Height;
                    parentBounds.Height -= siblingBounds.Height;
                    break;
                case DockPosition.Bottom:
                    parentBounds.Height -= siblingBounds.Height;
                    break;
                case DockPosition.Left:
                    parentBounds.X += siblingBounds.Width;
                    parentBounds.Width -= siblingBounds.Width;
                    break;
                case DockPosition.Right:
                    parentBounds.Width -= siblingBounds.Width;
                    break;
                case DockPosition.Fill:
                    parentBounds = Rectangle.Empty;
                    break;
            }
        }
        switch (control.Dock)
        {
            case DockPosition.Top:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Width = parentBounds.Width;
                break;
            case DockPosition.Bottom:
                control.Location = new Point(
                    parentBounds.X,
                    parentBounds.Y + parentBounds.Height - control.Height
                );
                control.Width = parentBounds.Width;
                break;
            case DockPosition.Left:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Height = parentBounds.Height;
                break;
            case DockPosition.Right:
                control.Location = new Point(
                    parentBounds.X + parentBounds.Width - control.Width,
                    parentBounds.Y
                );
                control.Height = parentBounds.Height;
                break;
            case DockPosition.Fill:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Width = parentBounds.Width;
                control.Height = parentBounds.Height;
                break;
        }
    }
}
