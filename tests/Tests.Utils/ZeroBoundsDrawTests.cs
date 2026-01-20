using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace Tests.Utils;

public class ZeroBoundsDrawTests : IDisposable
{
    public ZeroBoundsDrawTests()
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
    public void StackedLayout_Draw_WithZeroWidth_ReturnsEarly()
    {
        var layout = new TestableStackedLayout { Width = 0, Height = 100 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void StackedLayout_Draw_WithZeroHeight_ReturnsEarly()
    {
        var layout = new TestableStackedLayout { Width = 100, Height = 0 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void StackedLayout_Draw_WithBothZero_ReturnsEarly()
    {
        var layout = new TestableStackedLayout { Width = 0, Height = 0 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void StackedLayout_Draw_WithValidDimensions_DrawsNormally()
    {
        var layout = new TestableStackedLayout { Width = 100, Height = 100 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.True(graphics.SetOffsetCalled);
        Assert.True(graphics.SetClipCalled);
        Assert.True(graphics.FillRectangleCalled);
    }

    [Fact]
    public void FlowLayout_Draw_WithZeroWidth_ReturnsEarly()
    {
        var layout = new TestableFlowLayout { Width = 0, Height = 100 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void FlowLayout_Draw_WithZeroHeight_ReturnsEarly()
    {
        var layout = new TestableFlowLayout { Width = 100, Height = 0 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void FlowLayout_Draw_WithValidDimensions_DrawsNormally()
    {
        var layout = new TestableFlowLayout { Width = 100, Height = 100 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.True(graphics.SetOffsetCalled);
        Assert.True(graphics.SetClipCalled);
        Assert.True(graphics.FillRectangleCalled);
    }

    [Fact]
    public void StaticLayout_Draw_WithZeroWidth_ReturnsEarly()
    {
        var layout = new TestableStaticLayout { Width = 0, Height = 100 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void StaticLayout_Draw_WithZeroHeight_ReturnsEarly()
    {
        var layout = new TestableStaticLayout { Width = 100, Height = 0 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void StaticLayout_Draw_WithValidDimensions_DrawsNormally()
    {
        var layout = new TestableStaticLayout { Width = 100, Height = 100 };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        layout.Draw(ref g);

        Assert.True(graphics.SetOffsetCalled);
        Assert.True(graphics.SetClipCalled);
        Assert.True(graphics.FillRectangleCalled);
    }

    [Fact]
    public void ControlBase_Draw_WithZeroWidth_ReturnsEarly()
    {
        var control = new TestableControl
        {
            Width = 0,
            Height = 100,
            Visible = true,
        };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        control.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void ControlBase_Draw_WithZeroHeight_ReturnsEarly()
    {
        var control = new TestableControl
        {
            Width = 100,
            Height = 0,
            Visible = true,
        };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        control.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    [Fact]
    public void ControlBase_Draw_WithValidDimensions_DrawsNormally()
    {
        var control = new TestableControl
        {
            Width = 100,
            Height = 100,
            Visible = true,
        };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        control.Draw(ref g);

        Assert.True(graphics.SetOffsetCalled);
        Assert.True(graphics.SetClipCalled);
        Assert.True(graphics.FillRectangleCalled);
    }

    [Fact]
    public void ControlBase_Draw_WhenNotVisible_ReturnsEarly()
    {
        var control = new TestableControl
        {
            Width = 100,
            Height = 100,
            Visible = false,
        };
        var graphics = new MockGraphics();
        IGraphics g = graphics;

        control.Draw(ref g);

        Assert.False(graphics.SetOffsetCalled);
        Assert.False(graphics.SetClipCalled);
        Assert.False(graphics.FillRectangleCalled);
    }

    class TestableStackedLayout : CrossSharp.Ui.Common.StackedLayout { }

    class TestableFlowLayout : CrossSharp.Ui.Common.FlowLayout { }

    class TestableStaticLayout : CrossSharp.Ui.Common.StaticLayout { }

    class TestableControl : ControlBase
    {
        public override void PerformTheme() { }

        public override void Invalidate() { }
    }

    class MockGraphics : IGraphics
    {
        public bool SetOffsetCalled { get; private set; }
        public bool SetClipCalled { get; private set; }
        public bool FillRectangleCalled { get; private set; }
        public bool DrawRectangleCalled { get; private set; }

        public void Dispose() { }

        public void DrawImage(Image<Rgba32> image, Rectangle rect) { }

        public void Render() { }

        public void DrawRectangle(
            int x,
            int y,
            int width,
            int height,
            ColorRgba borderColor,
            int borderWidth,
            int roundedCornersRadius
        )
        {
            DrawRectangleCalled = true;
        }

        public void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor)
        {
            FillRectangleCalled = true;
        }

        public void DrawText(string text, int x, int y, FontFamily fontFamily, int fontSize, ColorRgba textColor) { }

        public Size MeasureText(string text, FontFamily fontFamily, int fontSize) => Size.Empty;

        public void SetOffset(System.Drawing.Point offset)
        {
            SetOffsetCalled = true;
        }

        public System.Drawing.Point GetOffset() => System.Drawing.Point.Empty;

        public void SetClip(ClipState state)
        {
            SetClipCalled = true;
        }

        public ClipState GetClipState() => ClipState.Max(0);
    }

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
}
