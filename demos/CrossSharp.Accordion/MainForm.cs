using CrossSharp.Desktop;
using CrossSharp.Themes;
using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Accordion;

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
        _leftMenu = new Ui.Accordion();
        _leftMenu.Dock = DockStyle.Left;
        _leftMenu.Width = 300;
        Controls.Add(_leftMenu);

        var btn = new Button();
        btn.Text = "Use default theme";
        btn.Height = 30;
        btn.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, DefaultTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn);

        var btn2 = new Button();
        btn2.Text = "Use rounded dark theme";
        btn2.Height = 30;
        btn2.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, RoundedSpacedDarkTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn2);

        var btn3 = new Button();
        btn3.Text = "Use flat pink theme";
        btn3.Height = 30;
        btn3.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, FlatPinkTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn3);

        var btn4 = new Button();
        btn4.Text = "Use flat blue theme";
        btn4.Height = 30;
        btn4.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, FlatBlueTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn4);

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

        var btn6 = new Button();
        btn6.Text = "Flat high contrast theme";
        btn6.Height = 30;
        btn6.Click += (s, e) =>
        {
            Services.AddSingleton<ITheme, FlatHighContrastTheme>(true);
            PerformTheme();
            Invalidate();
        };
        _leftMenu.AddItem(btn6);
    }

    void InitializeContentArea()
    {
        _contentArea = new StackedLayout();
        _contentArea.Dock = DockStyle.Fill;
        _contentArea.DockIndex = 1;
        Controls.Add(_contentArea);

        var label = new Label();
        label.Text = "Welcome to the Accordion Demo!";
        _contentArea.Add(label);
    }
}
