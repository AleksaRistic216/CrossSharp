using Demos.ModularForm.Views;

namespace Demos.ModularForm;

public class MainForm : CrossSharp.Ui.ModularForm
{
    public MainForm()
    {
        AddPageWithNavigation("Dashbaord", typeof(DashboardView));
    }
}
