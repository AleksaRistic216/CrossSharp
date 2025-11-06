using CrossSharp.Themes;
using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace Demos.AllInOne.MainFormViews;

public sealed class DropdownsView : StackedLayout
{
    const int LABEL_HEIGHT = 24;

    public DropdownsView()
    {
        Dock = DockStyle.Fill;

        var label1 = new Label();
        label1.Text = "Here are examples of dropdown controls usage.";
        label1.Height = LABEL_HEIGHT;
        Add(label1);

        InitializeDropDownWithActions();
    }

    void InitializeDropDownWithActions()
    {
        var label = new Label();
        label.Text = "Dropdown with buttons invoking action on click:";
        label.Height = LABEL_HEIGHT;
        Add(label);

        var label2 = new Label();
        label2.Text = "Select a theme:";
        label2.Height = LABEL_HEIGHT;
        Add(label2);

        var dropdown = new Dropdown();
        dropdown.State = DropdownState.Collapsed;
        dropdown.CollapsedHeight = 30;
        Add(dropdown);

        var item1 = new Button();
        item1.Text = "Default Theme";
        item1.Style = RenderStyle.Flat;
        item1.Height = 30;
        item1.Click += (s, _) =>
        {
            SelectTheme((s as IButton)!, new DefaultTheme(), dropdown);
        };
        dropdown.AddItem(item1);

        var item2 = new Button();
        item2.Text = "Flat Pink Theme";
        item2.Style = RenderStyle.Flat;
        item2.Height = 30;
        item2.Click += (s, _) =>
        {
            SelectTheme((s as IButton)!, new FlatPinkTheme(), dropdown);
        };
        dropdown.AddItem(item2);

        var item3 = new Button();
        item3.Text = "Rounded Spaced Dark Theme";
        item3.Style = RenderStyle.Flat;
        item3.Height = 30;
        item3.Click += (s, _) =>
        {
            SelectTheme((s as IButton)!, new RoundedSpacedDarkTheme(), dropdown);
        };
        dropdown.AddItem(item3);
    }

    void SelectTheme(IButton sender, ITheme theme, IDropdown dropdown)
    {
        Services.AddSingleton(theme, true);
        this.GetForm()!.PerformTheme();
        dropdown.State = DropdownState.Collapsed;
        // Since we use buttons as items within this dropdown, SelectedItem is not handled by dropdown itself
        // we will pass whole item
        dropdown.SelectedItem = sender;
    }
}
