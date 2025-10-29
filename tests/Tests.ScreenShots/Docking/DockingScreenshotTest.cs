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

public class DockingScreenshotTest : ApplicationTestBase<UiTestForm>
{
    [Fact]
    public void NestedDockingTest()
    {
        IStackedLayout stackedLayout = new StackedLayout();
        stackedLayout.Dock = DockPosition.Fill;
        stackedLayout.BackgroundColor = ColorRgba.Blue;
        Form.Controls.Add(stackedLayout);

        var nestedLayout = new StackedLayout();
        nestedLayout.Direction = Direction.Horizontal;
        nestedLayout.Dock = DockPosition.Bottom;
        nestedLayout.Height = 105;
        nestedLayout.BackgroundColor = ColorRgba.Green;
        stackedLayout.Add(nestedLayout);

        var nestedLayoutItem1 = new StackedLayout();
        nestedLayoutItem1.Direction = Direction.Vertical;
        nestedLayoutItem1.Dock = DockPosition.Left;
        nestedLayoutItem1.Width = 100;
        nestedLayoutItem1.BackgroundColor = ColorRgba.Yellow;
        nestedLayout.Add(nestedLayoutItem1);

        Form.Invalidate();
        Form.Redraw();

        Thread.Sleep(2000); // should do application.DoEvents() but I do not have it yet

        var nestedLayoutClientBounds = nestedLayout.GetClientBounds();
        var expectedNestedLayoutItem1ClientBounds = new Rectangle(
            nestedLayoutClientBounds.X,
            nestedLayoutClientBounds.Y,
            nestedLayoutItem1.Width,
            nestedLayout.Height
        );
        var formBounds = new Rectangle(Form.Location.X, Form.Location.Y, Form.Width, Form.Height);
        var ss = DesktopHelpers.TakeScreenshot();
        var img = Image.Load<Rgba32>(ss);
        var cropped = img.Clone(ctx =>
            ctx.Crop(
                new Rectangle(
                    (formBounds.X + expectedNestedLayoutItem1ClientBounds.X),
                    (formBounds.Y + expectedNestedLayoutItem1ClientBounds.Y),
                    nestedLayoutItem1.Width,
                    nestedLayout.Height
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
        Assert.Equal(nestedLayoutItem1.BackgroundColor, topLeftColor);
    }
}
