using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class ModularForm : FormSDL, IModularForm
{
    internal ModularForm()
    {
        InitializeSubTitleBar();
        InitializeLeftNavigationPane();
        InitializeTopNavigationPane();
        InitializeContentPane();
        // Subscribe to ThemePerformed to restore navigation pane colors after theme is applied
        // (StackedLayout.PerformTheme resets BackgroundColor to LayoutBackgroundColor)
        ThemePerformed += OnThemePerformedRestoreColors;
        PerformTheme();
    }

    protected override void DrawContent(ref IGraphics g)
    {
        base.DrawContent(ref g);
    }

    void OnThemePerformedRestoreColors(object? sender, EventArgs e)
    {
        // Restore navigation pane colors to PrimaryColor (same as title bar) after theme is applied
        _subTitleBar.BackgroundColor = _theme.PrimaryColor;
        LeftNavigationPane.BackgroundColor = _theme.PrimaryColor;
        TopNavigationPane.BackgroundColor = _theme.PrimaryColor;
    }

    void InitializeSubTitleBar()
    {
        _subTitleBar = new StackedLayout();
        _subTitleBar.BackgroundColor = _theme.PrimaryColor;
        _subTitleBar.Orientation = Orientation.Horizontal;
        _subTitleBar.Dock = DockStyle.Top;
        _subTitleBar.DockIndex = 0;
        _subTitleBar.Height = 35;
        _subTitleBar.Visible = false;
        _subTitleBar.Margin = new Margin(_theme.DefaultLayoutItemSpacing);
        _subTitleBar.Invalidated += OnSubTitleBarInvalidated;
        Controls.Add(_subTitleBar);
    }

    void OnSubTitleBarInvalidated(object? sender, EventArgs e)
    {
        var hasControls = _subTitleBar.Any();
        if (_subTitleBar.Visible != hasControls)
        {
            _subTitleBar.Visible = hasControls;
            UpdateContentPaneLayout();
        }
    }

    void InitializeLeftNavigationPane()
    {
        LeftNavigationPane = new StackedLayout();
        LeftNavigationPane.BackgroundColor = _theme.PrimaryColor;
        LeftNavigationPane.Orientation = Orientation.Vertical;
        LeftNavigationPane.Dock = DockStyle.Left;
        LeftNavigationPane.DockIndex = 1;
        LeftNavigationPane.Width = 150;
        LeftNavigationPane.Margin = new Margin(_theme.DefaultLayoutItemSpacing);
        Controls.Add(LeftNavigationPane);
    }

    void InitializeTopNavigationPane()
    {
        TopNavigationPane = new StackedLayout();
        TopNavigationPane.BackgroundColor = _theme.PrimaryColor;
        TopNavigationPane.Orientation = Orientation.Horizontal;
        TopNavigationPane.Dock = DockStyle.Top;
        TopNavigationPane.DockIndex = 2;
        TopNavigationPane.Height = TopNavigationPaneHeight;
        TopNavigationPane.Margin = new Margin(_theme.DefaultLayoutItemSpacing);
        Controls.Add(TopNavigationPane);
    }

    void OnClick(object? sender, EventArgs e)
    {
        var btn = sender as IButton;
        if (btn == null)
            return;
        if (btn.Tag == null)
            return;
        _viewer.Show(btn.Tag);
    }

    void InitializeContentPane()
    {
        _contentPane = new StaticLayout();
        _contentPane.Location = new Point(0, TopNavigationPane.Height);
        _contentPane.Width = Width;
        _contentPane.Dock = DockStyle.Fill;
        _contentPane.DockIndex = 3;
        Controls.Add(_contentPane);
        SizeChanged += (s, e) => UpdateContentPaneLayout();
        _viewer = new DynamicControlsController(ref _contentPane);
    }

    void UpdateContentPaneLayout()
    {
        var subTitleBarOffset = _subTitleBar.Visible ? _subTitleBar.Height : 0;
        var topNavOffset = TopNavigationVisible ? TopNavigationPane.Height : 0;
        var topOffset = subTitleBarOffset + topNavOffset;
        _contentPane.Location = new Point(0, topOffset);
        _contentPane.Width = Width;
        _contentPane.Height = Height - topOffset;
        _contentPane.Invalidate();
    }

    /// <summary>
    /// Adds a page to the modular form.
    /// Instance of the page will be cerated when the page is first shown.
    /// To show the page use NavigateToPage method with provided identifier.
    /// If you want to pass anything to the page constructor, register it as a singleton first using RegisterContentSingleton method.
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="pageType"></param>
    public void AddPage(object identifier, Type pageType)
    {
        _viewer.Register(identifier, pageType);
    }

    /// <summary>
    /// Registers a singleton instance that can be injected into pages.
    /// If overrideExisting is true, existing instance of the type will be replaced.
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="overrideExisting"></param>
    /// <typeparam name="T"></typeparam>
    public void RegisterContentSingleton<T>(T instance, bool overrideExisting = false)
        where T : class => _viewer.AddSingleton(instance, overrideExisting);

    /// <summary>
    /// Navigates to the page with provided identifier.
    /// If the page was not shown before, instance of the page will be created.
    /// </summary>
    /// <param name="identifier"></param>
    public void NavigateToPage(object identifier)
    {
        _viewer.Show(identifier);
    }

    /// <summary>
    /// Adds a page with a button to all navigation panes.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pageType"></param>
    /// <returns>Identifier of the page.</returns>
    public string AddPageWithNavigation(string name, Type pageType)
    {
        string id = Guid.NewGuid().ToString("N");
        AddPage(id, pageType);
        using var btn = new Button();
        btn.Text = name;
        using var g = new SDLGraphics(Renderer);
        var textSize = g.MeasureText(name, _theme.DefaultFontFamily, _theme.DefaultFontSize);
        btn.Width = textSize.Width + _navigationButtonsPadding * 2;
        btn.Height = textSize.Height + _navigationButtonsPadding * 2;
        btn.Click += OnClick;
        btn.Tag = id;
        TopNavigationPane.Add(btn.Clone());
        LeftNavigationPane.Add(btn.Clone());
        return id;
    }
}
