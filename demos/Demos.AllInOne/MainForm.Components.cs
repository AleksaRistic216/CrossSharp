using CrossSharp.Ui;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.AllInOne;

public partial class MainForm
{
    IAccordion _accordion = null!;
    const int LEFT_MENU_WIDTH = 250;

    void InitializeSideMenu()
    {
        _accordion = new Accordion();
        _accordion.Dock = DockStyle.Left;
        _accordion.DockIndex = 0;
        _accordion.Width = LEFT_MENU_WIDTH;
        _accordion.State = AccordionState.Collapsed;
        Controls.Add(_accordion);

        var btn1 = new Button();
        btn1.Text = "Button 1";
        btn1.Height = 40;
        _accordion.AddItem(btn1);
    }
}
