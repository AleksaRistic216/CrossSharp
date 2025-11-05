using System.Drawing;
using CrossSharp;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using Demos.AllInOne.MainFormViews;

namespace Demos.AllInOne;

public partial class MainForm
{
    DynamicControlsController _viewer = null!;
    IControlsContainer _contentPane = null!;

    void InitializeViewer()
    {
        _contentPane = new StaticLayout();
        _contentPane.Dock = DockStyle.Fill;
        _contentPane.DockIndex = 2;
        Controls.Add(_contentPane);
        _viewer = new DynamicControlsController(ref _contentPane);
        _viewer.CurrentPageChanged += (s, e) =>
        {
            foreach (var accordionItem in _accordionItems)
                if (accordionItem is IButton btn)
                    btn.IsSelected = (string?)btn.Tag == (string?)_viewer.CurrentPage;
        };
        _viewer.Register(nameof(HomeView), typeof(HomeView));
        _viewer.Register(nameof(ThemesView), typeof(ThemesView));
        _viewer.Register(nameof(DropdownsView), typeof(DropdownsView));
        _viewer.Show(nameof(HomeView));
    }

    IAccordion _accordion = null!;
    const int LEFT_MENU_WIDTH = 250;
    List<IAccordionItem> _accordionItems = [];

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
        btn1.Tag = nameof(HomeView);
        btn1.Click += OnAccordionButtonClick;
        _accordion.AddItem(btn1);
        _accordionItems.Add(btn1);

        var btn2 = new Button();
        btn2.Text = "Themes";
        btn2.Height = 40;
        btn2.Tag = nameof(ThemesView);
        btn2.Click += OnAccordionButtonClick;
        _accordion.AddItem(btn2);
        _accordionItems.Add(btn2);

        var btn3 = new Button();
        btn3.Text = "Dropdowns";
        btn3.Height = 40;
        btn3.Tag = nameof(DropdownsView);
        btn3.Click += OnAccordionButtonClick;
        _accordion.AddItem(btn3);
        _accordionItems.Add(btn3);
    }

    void OnAccordionButtonClick(object? sender, EventArgs e)
    {
        if (sender is not IButton { Tag: string pageName })
            return;
        _viewer.Show(pageName);
    }
}
