using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.StackedLayout;

public class MainForm : Form
{
    public MainForm()
    {
        Width = 800;
        Height = 600;
        // InitializeVerticalStackedLayout();
        InitializeHorizontalStackedLayout();
    }

    void InitializeVerticalStackedLayout()
    {
        var verticalStackedLayout = new CrossSharp.Ui.StackedLayout();
        verticalStackedLayout.Width = Width / 4;
        verticalStackedLayout.Height = Height;
        var verticalItems = 3;
        for (var i = 0; i < verticalItems; i++)
            verticalStackedLayout.Add(GenerateVPanel(verticalStackedLayout.Height / verticalItems));
        Controls.Add(verticalStackedLayout);
    }

    void InitializeHorizontalStackedLayout()
    {
        var horizontalStackedLayout = new CrossSharp.Ui.StackedLayout();
        horizontalStackedLayout.Location = new Point(Width / 4, 0);
        horizontalStackedLayout.Direction = Direction.Horizontal;
        horizontalStackedLayout.Width = Width / 4 * 3;
        horizontalStackedLayout.Height = Height;
        var horizontalItems = 3;
        for (var i = 0; i < horizontalItems; i++)
            horizontalStackedLayout.Add(
                GenerateHPanel(horizontalStackedLayout.Width / horizontalItems)
            );
        Controls.Add(horizontalStackedLayout);
    }

    IControl GenerateVPanel(int height) =>
        new Panel() { Height = height, BackgroundColor = ColorRgba.RandomColor };

    IControl GenerateHPanel(int width) =>
        new Panel() { Width = width, BackgroundColor = ColorRgba.RandomColor };
}
