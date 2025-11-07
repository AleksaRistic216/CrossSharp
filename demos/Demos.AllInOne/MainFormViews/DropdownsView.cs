using System.Reflection;
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

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        List<Type> themeTypes = [];
        foreach (var type in assemblies.SelectMany(x => x.GetTypes()))
            if (type is { IsClass: true, IsAbstract: false } && typeof(ITheme).IsAssignableFrom(type))
                themeTypes.Add(type);

        foreach (var themeType in themeTypes)
        {
            var item = new Button();
            item.Text = System.Text.RegularExpressions.Regex.Replace(
                themeType.Name.Replace("Theme", ""),
                "(\\B[A-Z])",
                " $1"
            );
            item.Style = RenderStyle.Flat;
            item.Height = 30;
            item.Click += (s, _) =>
            {
                if (Activator.CreateInstance(themeType) is ITheme themeInstance)
                    SelectTheme((s as IButton)!, themeInstance, dropdown);
            };
            dropdown.AddItem(item);
        }
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
