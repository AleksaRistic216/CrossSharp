using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using Omu.ValueInjecter;

namespace CrossSharp.Utils.Helpers;

public static class ControlsHelpers
{
    public static IForm? GetForm<T>(this T control)
        where T : IChild
    {
        return control.Parent switch
        {
            IForm form => form,
            IControl parentControl => parentControl.GetForm(),
            _ => null,
        };
    }

    public static Rectangle GetClientBounds<T>(this T control)
        where T : IChild, ISizeProvider, ILocationProvider
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
        var clientBounds = control.GetClientBounds();
        var form = control.GetForm();
        if (form is null)
            return Rectangle.Empty;
        clientBounds.Offset(form.Location);
        var parent = control.Parent;
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        while (parent is not null)
        {
            if (parent is IScrollable s)
            {
                if (s.Viewport.X > 0)
                    clientBounds.X -= (s.Viewport.X - s.Location.X);
                if (s.Viewport.Y > 0)
                    clientBounds.Y -= (s.Viewport.Y - s.Location.Y);
            }
            if (parent is IChild c)
                parent = c.Parent;
        }

        return clientBounds;
    }

    public static void PerformDocking(this IDockable control)
    {
        if (control.Dock == DockStyle.None)
            return;
        if (control.Parent is not IControlsContainer parent)
            return;
        var parentBounds = new Rectangle(Point.Empty, parent.Size);
        HashSet<int> recordedDockIndexes = [];
        foreach (var sibling in parent)
        {
            if (sibling is not IDockable dockedSibling || dockedSibling.Dock == DockStyle.None)
                continue;
            if (recordedDockIndexes.Contains(dockedSibling.DockIndex))
                throw new Exception(
                    "Duplicate DockIndex detected within the same container. Each docked control must have a unique DockIndex."
                );
            recordedDockIndexes.Add(dockedSibling.DockIndex);
            if (Equals(sibling, control))
                continue;
            if (dockedSibling.DockIndex > control.DockIndex)
                continue;
            var siblingBounds = new Rectangle(dockedSibling.Location, dockedSibling.Size);
            switch (dockedSibling.Dock)
            {
                case DockStyle.Top:
                    parentBounds.Y += siblingBounds.Height;
                    parentBounds.Height -= siblingBounds.Height;
                    break;
                case DockStyle.Bottom:
                    parentBounds.Height -= siblingBounds.Height;
                    break;
                case DockStyle.Left:
                    parentBounds.X += siblingBounds.Width;
                    parentBounds.Width -= siblingBounds.Width;
                    break;
                case DockStyle.Right:
                    parentBounds.Width -= siblingBounds.Width;
                    break;
                case DockStyle.Fill:
                    parentBounds = Rectangle.Empty;
                    break;
            }
        }
        switch (control.Dock)
        {
            case DockStyle.Top:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Width = parentBounds.Width;
                break;
            case DockStyle.Bottom:
                control.Location = new Point(
                    parentBounds.X,
                    parentBounds.Y + parentBounds.Height - control.Height
                );
                control.Width = parentBounds.Width;
                break;
            case DockStyle.Left:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Height = parentBounds.Height;
                break;
            case DockStyle.Right:
                control.Location = new Point(
                    parentBounds.X + parentBounds.Width - control.Width,
                    parentBounds.Y
                );
                control.Height = parentBounds.Height;
                break;
            case DockStyle.Fill:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Width = parentBounds.Width;
                control.Height = parentBounds.Height;
                break;
        }
    }

    public static IControl Clone(this IControl control)
    {
        var type = control.GetType();
        var clone = (IControl)Activator.CreateInstance(type)!;
        clone.InjectFrom(control);
        return clone;
    }

    public static ColorRgba GetBackgroundColor(this IControl control)
    {
        if (control is not IBackgroundColorProvider bgProvider)
            return ColorRgba.Transparent;
        if (control is not IHighlightable)
            return bgProvider.BackgroundColor;
        if (control is ISelectable && ((ISelectable)control).IsSelected)
            return bgProvider.BackgroundColor.Selected;
        var isMouseOver = control is IIsMouseOverProvider { IsMouseOver: true };
        var renderStyle = GetRenderStyle(control);
        return isMouseOver ? bgProvider.BackgroundColor.Highlighted
            : renderStyle is RenderStyle.Flat or RenderStyle.Outlined ? ColorRgba.Transparent
            : bgProvider.BackgroundColor;
    }

    public static RenderStyle GetRenderStyle(this IControl control)
    {
        var theme = Services.GetSingleton<ITheme>();
        if (control is not IRenderStyleProvider rsp)
            return theme.Style;

        return rsp.Style == RenderStyle.Default ? theme.Style : rsp.Style;
    }
}
