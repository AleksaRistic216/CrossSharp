using CrossSharp.Ui;

namespace Demos.AllInOne;

public partial class MainForm : Form
{
    public override void Initialize()
    {
        InitializeSideMenu();
        InitializeViewer();
    }
}
