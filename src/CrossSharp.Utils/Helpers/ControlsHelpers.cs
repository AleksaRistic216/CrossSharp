using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;
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
            if (parent is IScrollable s && s.Scrollable != ScrollableMode.None)
            {
                if (s.Viewport.X > 0)
                    location.X -= s.Viewport.X;
                if (s.Viewport.Y > 0)
                    location.Y -= s.Viewport.Y;
            }
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
        return clientBounds;
    }

    /// <summary>
    /// Returns if the control is visible and all its parent controls are also visible.
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static bool IsReallyVisible(this IControl control)
    {
        if (!control.Visible)
            return false;
        var parent = control.Parent;
        while (parent is IControl parentControl)
        {
            if (!parentControl.Visible)
                return false;
            parent = parentControl.Parent;
        }
        return true;
    }

    public static void PerformDocking(this IDockable control)
    {
        if (control.Dock == DockStyle.None)
            return;
        if (control.Parent is not IControlsContainer parent)
            return;
        var parentBounds = new Rectangle(Point.Empty, parent.Size);
        HashSet<int> recordedDockIndexes = [];
        foreach (var sibling in parent.Where(x => x.Visible))
        {
            if (sibling is not IDockable dockedSibling || dockedSibling.Dock == DockStyle.None)
                continue;
            if (!recordedDockIndexes.Add(dockedSibling.DockIndex))
                throw new Exception(
                    "Duplicate DockIndex detected within the same container. Each docked control must have a unique DockIndex."
                );
            if (Equals(sibling, control))
                continue;
            if (dockedSibling.DockIndex > control.DockIndex)
                continue;
            var siblingBounds = new Rectangle(dockedSibling.Location, dockedSibling.Size);
            var siblingMargin = Margin.Zero;
            if (sibling is IMarginProvider siblingMp)
                siblingMargin = siblingMp.Margin;
            siblingBounds.X -= siblingMargin.Left;
            siblingBounds.Y -= siblingMargin.Top;
            siblingBounds.Width += siblingMargin.Horizontal;
            siblingBounds.Height += siblingMargin.Vertical;
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
                    break;
            }
        }
        Margin margin = Margin.Zero;
        if (control is IMarginProvider mp)
            margin = mp.Margin;
        parentBounds.X += margin.Left;
        parentBounds.Y += margin.Top;
        parentBounds.Width -= margin.Horizontal;
        parentBounds.Height -= margin.Vertical;
        switch (control.Dock)
        {
            case DockStyle.Top:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Width = parentBounds.Width;
                break;
            case DockStyle.Bottom:
                control.Location = new Point(parentBounds.X, parentBounds.Y + parentBounds.Height - control.Height);
                control.Width = parentBounds.Width;
                break;
            case DockStyle.Left:
                control.Location = new Point(parentBounds.X, parentBounds.Y);
                control.Height = parentBounds.Height;
                break;
            case DockStyle.Right:
                control.Location = new Point(parentBounds.X + parentBounds.Width - control.Width, parentBounds.Y);
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
        if (control is ISelectable { IsSelected: true })
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
