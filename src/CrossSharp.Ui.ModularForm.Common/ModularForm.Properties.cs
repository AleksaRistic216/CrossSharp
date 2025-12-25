using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ModularForm
{
    int _navigationButtonsPadding = 8;
    ITheme _theme = Services.GetSingleton<ITheme>();
    DynamicControlsController _viewer = null!;
    IControlsContainer _contentPane = null!;
    StackedLayout _subTitleBar = null!;

    // ReSharper disable once MemberCanBePrivate.Global
    public IStackedLayout SubTitleBar => _subTitleBar;

    // ReSharper disable once MemberCanBePrivate.Global
    public StackedLayout TopNavigationPane { get; private set; } = null!;

    // ReSharper disable once MemberCanBePrivate.Global
    public StackedLayout LeftNavigationPane { get; private set; } = null!;
    public int TopNavigationPaneHeight { get; set; } = 40;

    bool _topNavigationVisible = true;

    public bool TopNavigationVisible
    {
        get => _topNavigationVisible;
        set
        {
            if (_topNavigationVisible == value)
                return;
            _topNavigationVisible = value;
            TopNavigationPane.Visible = value;
            UpdateContentPaneLayout();
        }
    }
}
