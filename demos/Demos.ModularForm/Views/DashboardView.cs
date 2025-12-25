using CrossSharp.Ui;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace Demos.ModularForm.Views;

public sealed class DashboardView : StackedLayout
{
    public DashboardView()
    {
        Dock = DockStyle.Fill;
        var button = new Button();
        button.Height = 50;
        button.Text = "Toggle Top Navigation";
        button.Click += (sender, args) =>
        {
            var form = this.GetForm();
            if (form is not IModularForm mf)
                return;

            mf.TopNavigationVisible = !mf.TopNavigationVisible;
        };
        Add(button);
    }
}
