using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class ModularForm
{
    ITheme _theme = Services.GetSingleton<ITheme>();
    DynamicControlsController _viewer;
    IControlsContainer _contentPane;
    public StackedLayout TopNavigationPane { get; private set; }
    public int ContentHeight { get; private set; }
}
