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

        var verticalMain = new StackedLayout();
        verticalMain.ItemsDirection = Direction.Vertical;
        verticalMain.Width = Width;
        verticalMain.Height = Height;
        Controls.Add(verticalMain);

        var horizontal = GenerateStackedLayout();
        horizontal.ItemsDirection = Direction.Horizontal;
        horizontal.Width = Width;
        horizontal.Height = Height - 50;
        verticalMain.Add(horizontal);

        _middlePanelHeight = horizontal.Height / _middlePanelCount;
        InitializeBottomPanel(ref verticalMain);

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
        {
            var b = GenerateButton();
            vertical3.Add(b);
            b.SetStackedLayoutItemSizing(ControlSizing.Fill);
        }
        horizontal.Add(vertical3);
    }

    void InitializeBottomPanel(ref StackedLayout main)
    {
        var horizontal1 = new StackedLayout();
        horizontal1.ItemsDirection = Direction.Horizontal;
        horizontal1.Width = Width;
        horizontal1.Height = 50;
        main.Add(horizontal1);

        var button1 = new Button();
        button1.Text = "SomeButton";
        button1.Width = 100;
        button1.Height = 1;
        button1.BackgroundColor = ColorRgba.RandomColor;
        horizontal1.Add(button1);

        var button2 = new Button();
        button2.Text = "AnotherButton";
        button2.Width = 150;
        button2.Height = 1;
        button2.BackgroundColor = ColorRgba.RandomColor;
        horizontal1.Add(button2);

        var button3 = new Button();
        button3.Text = "ThirdButton";
        button3.Width = 150;
        button3.Height = 1;
        button3.BackgroundColor = ColorRgba.RandomColor;
        horizontal1.Add(button3);

        var button4 = new Button();
        button4.Text = "FourthButton";
        button4.Width = 150;
        button4.Height = 1;
        button4.BackgroundColor = ColorRgba.RandomColor;
        horizontal1.Add(button4);

        button1.SetStackedLayoutItemSizing(ControlSizing.Fill);
        button2.SetStackedLayoutItemSizing(ControlSizing.Fill);
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
