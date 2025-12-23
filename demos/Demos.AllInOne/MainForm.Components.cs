using System.Drawing;
using CrossSharp;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using Demos.AllInOne.MainFormViews;
using SkiaSharp;

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
        _viewer.Register(nameof(DataGridView), typeof(DataGridView));
        _viewer.Show(nameof(HomeView));
    }

    IAccordion _accordion = null!;
    const int LEFT_MENU_WIDTH = 250;
    List<IAccordionItem> _accordionItems = [];

    void InitializeSideMenu()
    {
        SizeF menuItemImageScale = new SizeF(0.7f, 0.7f);
        int menuItemButtonHeight = 40;
        _accordion = new Accordion();
        _accordion.Dock = DockStyle.Left;
        _accordion.DockIndex = 0;
        _accordion.Width = LEFT_MENU_WIDTH;
        _accordion.State = AccordionState.Collapsed;
        Controls.Add(_accordion);

        var btn1 = new Button();
        btn1.Image = EfficientImage.GetIcon(Icon.Home, SKColors.White);
        btn1.ImageScale = menuItemImageScale;
        btn1.Height = menuItemButtonHeight;
        btn1.Tag = nameof(HomeView);
        btn1.Click += OnAccordionButtonClick;
        _accordion.AddItem(btn1);
        _accordionItems.Add(btn1);

        var btn2 = new Button();
        btn2.Image = EfficientImage.GetIcon(Icon.Palette, SKColors.White);
        btn2.ImageScale = menuItemImageScale;
        btn2.Height = menuItemButtonHeight;
        btn2.Tag = nameof(ThemesView);
        btn2.Click += OnAccordionButtonClick;
        _accordion.AddItem(btn2);
        _accordionItems.Add(btn2);

        var btn3 = new Button();
        btn3.Image = EfficientImage.GetIcon(Icon.Dropdown, SKColors.White);
        btn3.ImageScale = menuItemImageScale;
        btn3.Height = menuItemButtonHeight;
        btn3.Tag = nameof(DropdownsView);
        btn3.Click += OnAccordionButtonClick;
        _accordion.AddItem(btn3);
        _accordionItems.Add(btn3);

        var btn4 = new Button();
        btn4.Image = EfficientImage.GetIcon(Icon.DataGrid, SKColors.White);
        btn4.ImageScale = menuItemImageScale;
        btn4.Height = menuItemButtonHeight;
        btn4.Tag = nameof(DataGridView);
        btn4.Click += OnAccordionButtonClick;
        _accordion.AddItem(btn4);
        _accordionItems.Add(btn4);
    }

    void OnAccordionButtonClick(object? sender, EventArgs e)
    {
        if (sender is not IButton { Tag: string pageName })
            return;
        _viewer.Show(pageName);
    }
}
