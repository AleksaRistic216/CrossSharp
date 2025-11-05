using System.Drawing;
using CrossSharp;
using CrossSharp.Ui;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.AllInOne;

public partial class MainForm
{
    DynamicControlsController _viewer = null!;
    IControlsContainer _contentPane = null!;

    void InitializeViewer()
    {
        // _contentPane = new StaticLayout();
        // _contentPane.Location = new Point(0, Height);
        // _contentPane.Width = Width;
        // _contentPane.Dock = DockStyle.Fill;
        // _contentPane.DockIndex = 2;
        // Controls.Add(_contentPane);
        //
        // _viewer = new DynamicControlsController(ref _contentPane);
        // _viewer.Dock = DockStyle.Fill;
        // _viewer.DockIndex = 1;
        // Controls.Add(_viewer);
        //
        // _contentPane = _viewer.AddContainer();
    }

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
        btn1.Text = "Home";
        btn1.Height = 40;
        _accordion.AddItem(btn1);
    }
}
