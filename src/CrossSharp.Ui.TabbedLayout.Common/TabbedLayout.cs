using System.Collections;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class TabbedLayout : ITabbedLayout
{
    protected TabbedLayout()
    {
        InitializeHeader();
        InitializeContent();
        PerformTheme();
    }

    void InitializeContent()
    {
        _tabs = new StaticLayout();
        _tabs.Parent = this;
        _tabs.DockIndex = 1;
        _tabs.Dock = DockStyle.Fill;
        _controls.Add(_tabs);
        _controlsController = new DynamicControlsController(ref _tabs);
    }

    void InitializeHeader()
    {
        _header = new StackedLayout();
        _header.Orientation = Orientation.Horizontal;
        _header.BackgroundColor = Theme.SecondaryColor;
        _header.ItemsSpacing = HeaderItemsSpacing;
        _header.Parent = this;
        _header.Padding = HeaderPadding;
        _header.DockIndex = 0;
        _header.Dock = DockStyle.Top;
        _header.Height = HeaderHeight;
        _controls.Add(_header);
    }

    public void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().PrimaryColor;
        HeaderItemsSpacing = Services.GetSingleton<ITheme>().DefaultCornerRadius > 0 ? 8 : 0;
        HeaderPadding = Services.GetSingleton<ITheme>().DefaultCornerRadius > 0 ? new Padding(8, 4) : new Padding(0);
        foreach (var control in _controls)
            control.PerformTheme();
    }

    public void Invalidate()
    {
        this.PerformDocking();
        _header.Invalidate();
        _tabs.Invalidate();
        InvalidateSelectedHeaderButton();
    }

    void InvalidateSelectedHeaderButton()
    {
        foreach (var btn in _header.OfType<IButton>())
            btn.IsSelected = btn.Text == (string?)_controlsController.CurrentPage;
    }

    public void Dispose() => OnDisposeInternal();

    public void LimitClip(ref IGraphics g) { }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public void Draw(ref IGraphics graphics)
    {
        _header.Draw(ref graphics);
        _tabs.Draw(ref graphics);
    }

    public void AddTab(string title, Type content)
    {
        if (!typeof(ITabbedLayoutTab).IsAssignableFrom(content))
            throw new ArgumentException("Content type must implement ITabbedLayoutTab interface.", nameof(content));

        IButton tabButton = new Button();
        tabButton.Text = title;
        StyleTabButton(ref tabButton);
        tabButton.Click += OnTabButtonClicked;
        _header.Add(tabButton);
        _controlsController.Register(title, content);
        Invalidate();
    }

    public IButton CreateTabButton()
    {
        IButton tabButton = new Button();
        StyleTabButton(ref tabButton);
        tabButton.Index = int.MaxValue;
        _header.Add(tabButton);
        Invalidate();
        return tabButton;
    }

    void StyleTabButton(ref IButton button)
    {
        button.AutoSize = true;
        button.MinHeight = _header.Height - _header.Padding.Vertical;
        button.MaxHeight = _header.Height - _header.Padding.Vertical;
    }

    void OnTabButtonClicked(object? sender, EventArgs e)
    {
        if (sender is not IButton button)
            return;
        SelectTab(button.Text!);
    }

    public void RemoveTab(string title)
    {
        throw new NotImplementedException();
    }

    public void SelectTab(string title)
    {
        _controlsController.Show(title);
        var tab = _controlsController.GetCurrentControl() as ITabbedLayoutTab;
        tab?.OnTabFocusGained(title);
        OnCurrentTabChanged();
    }

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls)
    {
        throw new NotSupportedException("Use AddTab method to add tabs.");
    }

    public void Remove(params IControl[] controls)
    {
        throw new NotSupportedException("Use RemoveTab method to remove tabs.");
    }

    public void Clear() => throw new NotImplementedException(); // IDk if I should have it here, think abt it later
}
