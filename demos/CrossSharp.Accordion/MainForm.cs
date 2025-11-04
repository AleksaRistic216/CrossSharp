using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Accordion;

public class MainForm : Form
{
    IAccordion _leftMenu;

    public MainForm()
    {
        InitializeLeftMenu();
    }

    void InitializeLeftMenu()
    {
        _leftMenu = new Ui.Accordion();
        _leftMenu.Dock = DockStyle.Left;
        _leftMenu.Width = 200;
        _leftMenu.BackgroundColor = ColorRgba.Black;
        Controls.Add(_leftMenu);
    }
}
