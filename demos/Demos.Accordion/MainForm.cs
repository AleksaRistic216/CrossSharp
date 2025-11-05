using CrossSharp.Desktop;
using CrossSharp.Themes;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.Accordion;

public class MainForm : Form
{
    IAccordion _leftMenu;
    IStackedLayout _contentArea;

    public MainForm()
    {
        InitializeLeftMenu();
        InitializeContentArea();
    }

    void InitializeLeftMenu()
    {
        _leftMenu = new CrossSharp.Ui.Accordion();
        _leftMenu.Dock = DockStyle.Left;
        _leftMenu.Width = 300;
        _leftMenu.State = AccordionState.Collapsed;
        Controls.Add(_leftMenu);

        var btn = new Button();
        btn.Text = "Default theme";
        btn.Height = 30;
        btn.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, DefaultTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn);

        var btn2 = new Button();
        btn2.Text = "Rounded dark theme";
        btn2.Height = 30;
        btn2.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, RoundedSpacedDarkTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn2);

        var btn3 = new Button();
        btn3.Text = "Flat pink theme";
        btn3.Height = 30;
        btn3.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, FlatPinkTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn3);

        var btn5 = new Button();
        btn5.Text = "High Contrast Theme";
        btn5.Height = 30;
        btn5.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, HighContrastTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn5);
    }

    void InitializeContentArea()
    {
        _contentArea = new StackedLayout();
        _contentArea.Dock = DockStyle.Fill;
        _contentArea.DockIndex = 1;
        Controls.Add(_contentArea);

        var textLineHeight = 16;
        var label = new Label();
        label.Text = "Welcome to the Accordion Demo!";
        label.Height = textLineHeight;
        _contentArea.Add(label);

        var label1 = new Label();
        label1.Text = "On the left side, you can find accordion with vertical orientation.";
        label1.Height = textLineHeight;
        _contentArea.Add(label1);

        var label2 = new Label();
        label2.Text = "Bellow is an example of horizontal accordion.";
        label2.Height = textLineHeight;
        _contentArea.Add(label2);

        var horizontalAccordion = new CrossSharp.Ui.Accordion();
        horizontalAccordion.Height = 300;
        horizontalAccordion.Orientation = Orientation.Horizontal;
        _contentArea.Add(horizontalAccordion);

        var l = new Label();
        l.Text = "This is horizontal accordion item.";
        l.Height = textLineHeight;
        horizontalAccordion.AddItem(l);

        var btnInside = new Button();
        btnInside.Text = "Click me!";
        btnInside.Click += (s, e) =>
        {
            Notifications.Show(
                "Clicked",
                "You clicked the button inside horizontal accordion item."
            );
        };
        btnInside.Height = 30;
        horizontalAccordion.AddItem(btnInside);
    }
}
