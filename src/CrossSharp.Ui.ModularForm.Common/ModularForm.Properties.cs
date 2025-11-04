using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ModularForm
{
    int _navigationButtonsPadding = 8;
    ITheme _theme = Services.GetSingleton<ITheme>();
    DynamicControlsController _viewer;
    IControlsContainer _contentPane;
    public StackedLayout TopNavigationPane { get; private set; }
    public StackedLayout LeftNavigationPane { get; private set; }
    public int TopNavigationPaneHeight { get; set; } = 40;
}
