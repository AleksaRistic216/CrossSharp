using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace StackedLayoutDemo;

public class MainForm : Form
{
    readonly int _verticalLayoutWidth;
    readonly int _middlePanelHeight;
    readonly int _middlePanelCount = 30;

    public MainForm()
    {
        Width = 1000;
        Height = 800;
        _verticalLayoutWidth = Width / 3;
        _middlePanelHeight = Height / _middlePanelCount;

        var horizontal = GenerateStackedLayout();
        horizontal.ItemsDirection = Direction.Horizontal;
        horizontal.Width = Width;
        Controls.Add(horizontal);

        var vertical1 = GenerateStackedLayout();
        vertical1.ItemsDirection = Direction.Vertical;
        for (var i = 0; i < 5; i++)
            vertical1.Add(GenerateButton());
        horizontal.Add(vertical1);

        var vertical2 = GenerateStackedLayout();
        vertical2.ItemsDirection = Direction.Vertical;
        for (var i = 0; i < _middlePanelCount; i++)
            vertical2.Add(GeneratePanel());
        horizontal.Add(vertical2);

        var vertical3 = GenerateStackedLayout();
        vertical3.ItemsDirection = Direction.Vertical;
        for (var i = 0; i < 7; i++)
            vertical3.Add(GenerateButton());
        horizontal.Add(vertical3);
    }

    StackedLayout GenerateStackedLayout()
    {
        var stackedLayout = new StackedLayout();
        stackedLayout.Width = _verticalLayoutWidth;
        stackedLayout.Height = Height;
        stackedLayout.Location = Point.Empty;
        stackedLayout.BackgroundColor = ColorRgba.RandomColor;
        return stackedLayout;
    }

    Button GenerateButton()
    {
        var button = new Button();
        button.Text = Guid.NewGuid().ToString();
        // button.Width = 200; // upon vertical stacking, width is set to the container width
        button.Height = 50;
        button.BackgroundColor = ColorRgba.RandomColor;
        return button;
    }

    Panel GeneratePanel()
    {
        var panel = new Panel();
        // panel.Width = 200; // upon vertical stacking, width is set to the container width
        panel.Height = _middlePanelHeight;
        panel.BackgroundColor = ColorRgba.RandomColor;
        return panel;
    }
}
