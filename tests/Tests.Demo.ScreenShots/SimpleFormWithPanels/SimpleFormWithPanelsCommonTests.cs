using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Helpers;
using Demos.SimpleFormWithPanels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Tests.Demo.Utils;
using Xunit;

namespace Tests.Demo.ScreenShots.SimpleFormWithPanels;

public class SimpleFormWithPanelsCommonTests : BaseDemosTest<MainForm>
{
    [Fact]
    public void PanelsRenderedTest()
    {
        var panels = _application.MainForm.Controls.OfType<Panel>().ToList();
        Assert.Equal(4, panels.Count);
        foreach (var panel in panels)
        {
            var screenBounds = panel.GetScreenBounds();
            var ss = DesktopHelpers.TakeScreenshot();
            var img = Image.Load<Rgba32>(ss);
            var cropped = img.Clone(ctx =>
                ctx.Crop(
                    new Rectangle(
                        screenBounds.X,
                        screenBounds.Y,
                        screenBounds.Width,
                        screenBounds.Height
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
            Assert.Equal(panel.BackgroundColor, topLeftColor);

            var topRightPixel = cropped[cropped.Width - 1, 0];
            var topRightColor = ColorRgba.FromBytes(
                topRightPixel.R,
                topRightPixel.G,
                topRightPixel.B,
                topRightPixel.A
            );
            Assert.Equal(panel.BackgroundColor, topRightColor);

            var bottomLeftPixel = cropped[0, cropped.Height - 1];
            var bottomLeftColor = ColorRgba.FromBytes(
                bottomLeftPixel.R,
                bottomLeftPixel.G,
                bottomLeftPixel.B,
                bottomLeftPixel.A
            );
            Assert.Equal(panel.BackgroundColor, bottomLeftColor);

            var bottomRightPixel = cropped[cropped.Width - 1, cropped.Height - 1];
            var bottomRightColor = ColorRgba.FromBytes(
                bottomRightPixel.R,
                bottomRightPixel.G,
                bottomRightPixel.B,
                bottomRightPixel.A
            );
            Assert.Equal(panel.BackgroundColor, bottomRightColor);
        }
    }
}
