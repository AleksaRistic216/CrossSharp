using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using Tests.Base;
using Xunit;

namespace Tests.Docking;

public class CommonDockingTests : UiTestBase
{
    [Fact]
    public void NestedAndDockedStackedLayoutTest()
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
        Assert.True(nestedLayoutItem1.Location.Y == 495);
    }
}
