using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Tests.Common;
using Xunit;

namespace Tests.ScreenShots.Docking;

public class TestForm : Form
{
    public StackedLayout NestedLayoutItem1 = new StackedLayout();
    public StackedLayout NestedLayout = new StackedLayout();

    public TestForm()
    {
        IStackedLayout stackedLayout = new StackedLayout();
        stackedLayout.Dock = DockStyle.Fill;
        stackedLayout.BackgroundColor = ColorRgba.Blue;
        Controls.Add(stackedLayout);

        NestedLayout.Orientation = Orientation.Horizontal;
        NestedLayout.Dock = DockStyle.Bottom;
        NestedLayout.Height = 105;
        NestedLayout.BackgroundColor = ColorRgba.Green;
        stackedLayout.Add(NestedLayout);

        NestedLayoutItem1.Orientation = Orientation.Vertical;
        NestedLayoutItem1.Dock = DockStyle.Left;
        NestedLayoutItem1.Width = 100;
        NestedLayoutItem1.BackgroundColor = ColorRgba.Yellow;
        NestedLayout.Add(NestedLayoutItem1);
    }
}

public class DockingScreenshotTest : ApplicationTestBase<TestForm>
{
    [Fact]
    public void NestedDockingTest()
    {
        var nestedLayoutClientBounds = Form.NestedLayoutItem1.GetClientBounds();
        var expectedNestedLayoutItem1ClientBounds = new Rectangle(
            nestedLayoutClientBounds.X,
            nestedLayoutClientBounds.Y,
            Form.NestedLayoutItem1.Width,
            Form.NestedLayout.Height
        );
        var formBounds = new Rectangle(Form.Location.X, Form.Location.Y, Form.Width, Form.Height);
        var ss = DesktopHelpers.TakeScreenshot();
        var img = Image.Load<Rgba32>(ss);
        var cropped = img.Clone(ctx =>
            ctx.Crop(
                new Rectangle(
                    (formBounds.X + expectedNestedLayoutItem1ClientBounds.X),
                    (formBounds.Y + expectedNestedLayoutItem1ClientBounds.Y),
                    Form.NestedLayoutItem1.Width,
                    Form.NestedLayout.Height
                )
            )
        );
        var topLeftPixel = cropped[0, 0];
        var topLeftColor = ColorRgba.FromBytes(
            topLeftPixel.R,
            topLeftPixel.G,
            topLeftPixel.B,
            topLeftPixel.A
        );
        Assert.Equal(Form.NestedLayoutItem1.BackgroundColor, topLeftColor);
    }
}
