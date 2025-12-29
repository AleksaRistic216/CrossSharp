using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;
using Xunit;

namespace Tests.Utils;

public class StackedLayoutContentBoundsTests : IDisposable
{
    public StackedLayoutContentBoundsTests()
    {
        try
        {
            if (!Services.IsRegistered<IInputHandler>())
                Services.AddSingleton<IInputHandler>(new MockInputHandler());
        }
        catch (InvalidOperationException) { }
        try
        {
            if (!Services.IsRegistered<ITheme>())
                Services.AddSingleton<ITheme>(new MockTheme());
        }
        catch (InvalidOperationException) { }
    }

    public void Dispose() { }

    [Fact]
    public void ContentBounds_WithNoControls_ReturnsEmpty()
    {
        var layout = new TestableStackedLayout();

        layout.Invalidate();

        Assert.Equal(Rectangle.Empty, layout.ContentBounds);
    }

    [Fact]
    public void ContentBounds_WithVisibleControls_IncludesAllVisible()
    {
        var layout = new TestableStackedLayout { Width = 200, Height = 400 };
        var control1 = new MockControl
        {
            Width = 100,
            Height = 50,
            Visible = true,
        };
        var control2 = new MockControl
        {
            Width = 100,
            Height = 50,
            Visible = true,
        };

        layout.Add(control1, control2);
        layout.Invalidate();

        Assert.True(layout.ContentBounds.Height > 0);
        Assert.True(layout.ContentBounds.Width > 0);
    }

    [Fact]
    public void ContentBounds_WithHiddenControls_ExcludesHidden()
    {
        var layout = new TestableStackedLayout { Width = 200, Height = 400 };
        var visibleControl = new MockControl
        {
            Width = 100,
            Height = 50,
            Visible = true,
        };
        var hiddenControl = new MockControl
        {
            Width = 100,
            Height = 500,
            Visible = false,
        };

        layout.Add(visibleControl, hiddenControl);
        layout.Invalidate();

        // ContentBounds should only include the visible control
        // The hidden control with height 500 should not affect content bounds
        Assert.True(layout.ContentBounds.Height < 500);
    }

    [Fact]
    public void ContentBounds_AllControlsHidden_ReturnsEmpty()
    {
        var layout = new TestableStackedLayout { Width = 200, Height = 400 };
        var control1 = new MockControl
        {
            Width = 100,
            Height = 50,
            Visible = false,
        };
        var control2 = new MockControl
        {
            Width = 100,
            Height = 50,
            Visible = false,
        };

        layout.Add(control1, control2);
        layout.Invalidate();

        Assert.Equal(Rectangle.Empty, layout.ContentBounds);
    }

    [Fact]
    public void ContentBounds_AfterHidingControl_UpdatesCorrectly()
    {
        var layout = new TestableStackedLayout { Width = 200, Height = 400 };
        var control1 = new MockControl
        {
            Width = 100,
            Height = 50,
            Visible = true,
        };
        var control2 = new MockControl
        {
            Width = 100,
            Height = 100,
            Visible = true,
        };

        layout.Add(control1, control2);
        layout.Invalidate();

        var boundsWithBoth = layout.ContentBounds;

        // Hide the second control
        control2.Visible = false;
        layout.Invalidate();

        var boundsWithOne = layout.ContentBounds;

        // Content bounds should be smaller after hiding the larger control
        Assert.True(boundsWithOne.Height < boundsWithBoth.Height);
    }

    class TestableStackedLayout : CrossSharp.Ui.Common.StackedLayout { }

#pragma warning disable CS0067 // Event is never used
    class MockInputHandler : IInputHandler
    {
        public void StartListeningAsync(CancellationToken cancellationToken) { }

        public event EventHandler<KeyInputArgs>? KeyPressed;
        public event EventHandler<MouseInputArgs>? MousePressed;
        public event EventHandler<MouseInputArgs>? MouseReleased;
        public event EventHandler<MouseInputArgs>? MouseMoved;
        public event EventHandler<MouseWheelInputArgs>? MouseWheel;
        public event EventHandler<MouseInputArgs>? MouseDragged;
    }
#pragma warning restore CS0067

    class MockTheme : ITheme
    {
        public RenderStyle Style { get; set; } = RenderStyle.Flat;
        public int DefaultFontSize { get; set; } = 14;
        public FontFamily DefaultFontFamily { get; set; } = FontFamily.Default;
        public ColorRgba LayoutBackgroundColor { get; set; } = ColorRgba.Transparent;
        public ColorRgba PrimaryColor { get; set; } = ColorRgba.Transparent;
        public ColorRgba SecondaryColor { get; set; } = ColorRgba.Transparent;
        public ColorRgba SelectionColor { get; set; } = ColorRgba.Blue;
        public int DefaultCornerRadius { get; set; } = 0;
        public int DefaultLayoutItemSpacing { get; set; } = 0;
    }

    class MockControl : IControl
    {
        public void Dispose() { }

        public void PerformTheme() { }

        public EventHandler? ThemePerformed { get; set; }
        public bool Visible { get; set; } = true;

        public void Invalidate() { }

        public EventHandler? Invalidated { get; set; }

        public void Draw(ref IGraphics graphics) { }

        public EventHandler? Disposing { get; set; }
        public int Index { get; set; }
        public bool NoClip { get; set; }
        public int BorderWidth { get; set; }
        public ColorRgba BorderColor { get; set; } = ColorRgba.Transparent;
        public Point Location { get; set; }
        public EventHandler<Point>? LocationChanged { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public EventHandler<Size>? SizeChanged { get; set; }
        public object? Parent { get; set; }
        public bool IsMouseOver { get; set; }
        public Margin Margin { get; set; }
        public EventHandler? MarginChanged { get; set; }
    }
}
