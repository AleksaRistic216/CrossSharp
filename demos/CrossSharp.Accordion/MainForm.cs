using CrossSharp.Desktop;
using CrossSharp.Ui;
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
        _leftMenu.Width = 200;
        Controls.Add(_leftMenu);

        var btn = new Button();
        btn.Text = "Click Me";
        btn.Height = 30;
        btn.Click += (s, e) =>
        {
            Notifications.Show("Button Clicked", "You have clicked the button.");
        };
        _leftMenu.AddItem(btn);

        var btn2 = new Button();
        btn2.Text = "Another Button";
        btn2.Height = 30;
        btn2.Click += (s, e) =>
        {
            Notifications.Show("Another Button Clicked", "You have clicked another button.");
        };
        _leftMenu.AddItem(btn2);
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
