using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;
using SkiaSharp;

namespace CrossSharp.Ui.Common;

partial class StackedLayout : IStackedLayout
{
    protected StackedLayout()
    {
        _inputHandler = Services.GetSingleton<IInputHandler>();
        InitializeScrollbarHandler();
        PerformTheme();
    }

    public void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor;
        CornerRadius = Services.GetSingleton<ITheme>().DefaultCornerRadius;
        ItemsSpacing = CornerRadius > 0 ? CornerRadius : 0;
        Padding = CornerRadius > 0 ? new Padding(CornerRadius) : new Padding(0);
        // this.SetMargin(Services.GetSingleton<ITheme>().DefaultLayoutItemSpacing); // Do not do this, it is bugged :)
        foreach (var control in _controls.ToList())
            control.PerformTheme();
        OnThemePerformed();
    }

    public void Invalidate()
    {
        this.PerformDocking();
        InvalidateSize();
        if (Orientation == Orientation.Vertical)
            InvalidateStackVertical();
        else
            InvalidateStackHorizontal();
        foreach (IControl control in _controls.ToList())
            control.Invalidate();
        InvalidateContentBounds();
        InvalidateViewport();
        OnInvalidated();
    }

    void InvalidateSize()
    {
        // if (!AutoSize)
        //     return;
        // if (MaxWidth < MinWidth)
        //     throw new InvalidOperationException("MaxWidth cannot be less than MinWidth.");
        // if (MaxHeight < MinHeight)
        //     throw new InvalidOperationException("MaxHeight cannot be less than MinHeight.");
        // Apply max constraints first
        Width = Math.Min(Width, MaxWidth ?? int.MaxValue);
        Height = Math.Min(Height, MaxHeight ?? int.MaxValue);
        // Then apply min constraints
        // Width = Math.Max(Width, MinWidth ?? 0);
        // Height = Math.Max(Height, MinHeight ?? 0);
    }

    void InvalidateContentBounds()
    {
        var visibleControls = _controls.Where(c => c.Visible).ToList();
        if (visibleControls.Count == 0)
        {
            ContentBounds = Rectangle.Empty;
            return;
        }
        var x = 0;
        var y = 0;
        var width = visibleControls.Max(c => c.Location.X + c.Width) + Padding.Horizontal;
        var height = visibleControls.Max(c => c.Location.Y + c.Height) + Padding.Vertical;
        ContentBounds = new Rectangle(x, y, width, height);
    }

    void InvalidateViewport()
    {
        if (Scrollable == ScrollableMode.None)
            return;
        Viewport = new Rectangle(Viewport.X, Viewport.Y, Width, Height);
    }

    void InvalidateStackVertical()
    {
        var currentY = Padding.Top;
        var grabberOffset = ReorderEnabled ? GrabberSize : 0;
        foreach (var c in _controls.Where(x => x.Visible).OrderBy(x => x.Index).ToList())
        {
            currentY += c.Margin.Top;
            c.Location = new Point(Padding.Left + c.Margin.Left + grabberOffset, currentY + c.Margin.Top);
            c.Width = Width - Padding.Horizontal - c.Margin.Horizontal - grabberOffset;
            currentY += c.Height + c.Margin.Vertical + ItemsSpacing;
        }
    }

    void InvalidateStackHorizontal()
    {
        var currentX = Padding.Left;
        var grabberOffset = ReorderEnabled ? GrabberSize : 0;
        foreach (var c in _controls.Where(x => x.Visible).OrderBy(x => x.Index).ToList())
        {
            if (InvalidateHorizontalItemDropdown(c, ref currentX, grabberOffset))
                continue;
            currentX += c.Margin.Left;
            c.Location = new Point(currentX, Padding.Top + c.Margin.Top + grabberOffset);
            c.Height = Height - Padding.Vertical - c.Margin.Vertical - grabberOffset;
            currentX += c.Width + ItemsSpacing + c.Margin.Right;
        }
    }

    bool InvalidateHorizontalItemDropdown(IControl control, ref int currentX, int grabberOffset)
    {
        if (control is not IDropdown dropdown)
            return false;
        currentX += control.Margin.Left;
        dropdown.Location = new Point(currentX, control.Margin.Top + grabberOffset);
        dropdown.CollapsedHeight = Height - control.Margin.Vertical - grabberOffset;
        currentX += control.Width + ItemsSpacing + control.Margin.Right;
        return true;
    }

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _controls.GetEnumerator();

    public void Add(params IControl[] controls)
    {
        foreach (IControl control in controls)
            control.Parent = this;
        _controls.AddRange(controls);
        Invalidate();
    }

    public void Remove(params IControl[] controls) => _controls.RemoveAll(controls.Contains);

    public void Clear() => _controls.Clear();

    public void Draw(ref IGraphics graphics)
    {
        var clientBounds = this.GetClientBounds();
        var oldOffset = graphics.GetOffset();
        var oldState = graphics.GetClipState();
        graphics.SetOffset(clientBounds.Location);
        graphics.SetClip(NoClip ? ClipState.Max(CornerRadius) : ClipState.Create(oldState, clientBounds, CornerRadius));
        DrawBackground(ref graphics);
        if (ReorderEnabled)
        {
            DrawGrabbers(ref graphics);
            DrawDropZoneHighlight(ref graphics);
        }
        foreach (var c in _controls.Where(ShouldControlBeDrawn).ToList())
            c.Draw(ref graphics);
        ScrollableHelpers.DrawScrollBar(ref graphics, this);
        DrawBorders(ref graphics);
        graphics.SetOffset(oldOffset);
        graphics.SetClip(oldState);
    }

    bool ShouldControlBeDrawn(IControl control)
    {
        if (!control.Visible)
            return false;
        if (Scrollable == ScrollableMode.None)
            return true;
        return Viewport.IntersectsWith(
            new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height)
        );
    }

    void DrawBackground(ref IGraphics graphics)
    {
        graphics.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    void DrawBorders(ref IGraphics graphics)
    {
        if (BorderWidth <= 0)
            return;
        if (Equals(BorderColor, ColorRgba.Transparent))
            return;
        if (Width <= 0 || Height <= 0)
            return;

        var cornersRadius = (this as IRoundedCorners)?.CornerRadius ?? 0;
        graphics.DrawRectangle(0, 0, Width, Height, BorderColor, BorderWidth, cornersRadius);
    }

    void DrawGrabbers(ref IGraphics graphics)
    {
        var theme = Services.GetSingleton<ITheme>();
        var grabberColor = new SKColor(
            theme.PrimaryColor.RByte,
            theme.PrimaryColor.GByte,
            theme.PrimaryColor.BByte,
            (byte)(theme.PrimaryColor.AByte * 0.6f)
        );

        var iconSize = Math.Min(GrabberSize - 4, 20);

        // Account for scroll offset when drawing grabbers
        var scrollOffsetX = Scrollable != ScrollableMode.None ? Viewport.X : 0;
        var scrollOffsetY = Scrollable != ScrollableMode.None ? Viewport.Y : 0;

        foreach (var control in _controls.Where(ShouldControlBeDrawn).ToList())
        {
            var grabberRect = GetGrabberLocalRect(control);
            var icon = EfficientImage.GetIcon(Icon.Grabber, grabberColor, iconSize, iconSize);

            // Center the icon in the grabber area, adjusted for scroll
            var iconX = grabberRect.X - scrollOffsetX + (grabberRect.Width - iconSize) / 2;
            var iconY = grabberRect.Y - scrollOffsetY + (grabberRect.Height - iconSize) / 2;

            graphics.DrawImage(icon.Data, new Rectangle(iconX, iconY, iconSize, iconSize));
        }
    }

    void DrawDropZoneHighlight(ref IGraphics graphics)
    {
        if (DropTargetIndex < 0 || DraggedControl is null)
            return;

        var theme = Services.GetSingleton<ITheme>();
        var highlightColor = new ColorRgba(theme.PrimaryColor.R, theme.PrimaryColor.G, theme.PrimaryColor.B, 1f);

        var controls = _controls.Where(c => c.Visible).OrderBy(c => c.Index).ToList();
        if (controls.Count == 0)
            return;

        const int highlightThickness = 4;

        // Account for scroll offset when drawing highlight
        var scrollOffsetX = Scrollable != ScrollableMode.None ? Viewport.X : 0;
        var scrollOffsetY = Scrollable != ScrollableMode.None ? Viewport.Y : 0;

        // DropTargetIndex is now a list position (0, 1, 2, ...), not an Index value
        if (Orientation == Orientation.Vertical)
        {
            int highlightY;
            if (DropTargetIndex == 0)
            {
                // Before first control
                highlightY = controls[0].Location.Y - ItemsSpacing / 2 - highlightThickness / 2;
            }
            else if (DropTargetIndex >= controls.Count)
            {
                // After last control
                var lastControl = controls[^1];
                highlightY = lastControl.Location.Y + lastControl.Height + ItemsSpacing / 2 - highlightThickness / 2;
            }
            else
            {
                // Between controls - use the control at this position
                var targetControl = controls[DropTargetIndex];
                highlightY = targetControl.Location.Y - ItemsSpacing / 2 - highlightThickness / 2;
            }

            graphics.FillRectangle(
                Padding.Left + GrabberSize - scrollOffsetX,
                highlightY - scrollOffsetY,
                Width - Padding.Horizontal - GrabberSize,
                highlightThickness,
                highlightColor
            );
        }
        else
        {
            int highlightX;
            if (DropTargetIndex == 0)
            {
                // Before first control
                highlightX = controls[0].Location.X - ItemsSpacing / 2 - highlightThickness / 2;
            }
            else if (DropTargetIndex >= controls.Count)
            {
                // After last control
                var lastControl = controls[^1];
                highlightX = lastControl.Location.X + lastControl.Width + ItemsSpacing / 2 - highlightThickness / 2;
            }
            else
            {
                // Between controls - use the control at this position
                var targetControl = controls[DropTargetIndex];
                highlightX = targetControl.Location.X - ItemsSpacing / 2 - highlightThickness / 2;
            }

            graphics.FillRectangle(
                highlightX - scrollOffsetX,
                Padding.Top + GrabberSize - scrollOffsetY,
                highlightThickness,
                Height - Padding.Vertical - GrabberSize,
                highlightColor
            );
        }
    }

    Rectangle GetGrabberLocalRect(IControl control)
    {
        if (Orientation == Orientation.Vertical)
        {
            // Grabber is on the left side
            return new Rectangle(Padding.Left, control.Location.Y, GrabberSize, control.Height);
        }
        else
        {
            // Grabber is above the control
            return new Rectangle(control.Location.X, Padding.Top, control.Width, GrabberSize);
        }
    }

    public void Dispose() => OnDisposeInternal();
}
