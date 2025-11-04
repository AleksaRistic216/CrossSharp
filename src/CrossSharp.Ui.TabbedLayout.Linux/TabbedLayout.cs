using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Linux;

class TabbedLayout : ITabbedLayout
{
    ITheme _theme => Services.GetSingleton<ITheme>();
    DynamicControlsController _controlsController;
    readonly List<IControl> _controls = [];
    IControlsContainer _tabs;
    IStackedLayout _header;
    private int headerHeight = 30;

    internal TabbedLayout()
    {
        Initialize();
    }

    public void Initialize()
    {
        InitializeContent();
        InitializeHeader();
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
        _header.Direction = Direction.Horizontal;
        _header.BackgroundColor = _theme.SecondaryBackgroundColor;
        _header.ItemsSpacing = 4;
        _header.Parent = this;
        _header.Padding = new Padding(4);
        _header.DockIndex = 0;
        _header.Dock = DockStyle.Top;
        _header.Height = headerHeight;
        _controls.Add(_header);
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

    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }

    public void LimitClip(ref IGraphics g) { }

    public Point Location { get; set; }
    public EventHandler<Point>? LocationChanged { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public EventHandler<Size>? SizeChanged { get; set; }
    public object? Parent { get; set; }
    public bool IsMouseOver { get; set; }
    bool _visible = true;
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
                return;
            _visible = value;
            Invalidate();
        }
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public void Draw(ref IGraphics graphics)
    {
        _header.Draw(ref graphics);
        _tabs.Draw(ref graphics);
    }

    public EventHandler? Disposing { get; set; }
    public int Index { get; set; }

    void RaiseDisposing() => Disposing?.Invoke(this, EventArgs.Empty);

    void OnDisposeInternal() => RaiseDisposing();

    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
    public ColorRgba BackgroundColor { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }

    public EventHandler? CurrentTabChanged { get; set; }

    void RaiseCurrentTabChanged() => CurrentTabChanged?.Invoke(this, EventArgs.Empty);

    void OnCurrentTabChanged()
    {
        Invalidate();
        RaiseCurrentTabChanged();
    }

    public void AddTab(string title, Type content)
    {
        if (!typeof(ITabbedLayoutTab).IsAssignableFrom(content))
            throw new ArgumentException(
                "Content type must implement ITabbedLayoutTab interface.",
                nameof(content)
            );
        AddTabButtonInternal(title, OnTabButtonClicked);
        _controlsController.Register(title, content);
        Invalidate();
    }

    public void AddTabButton(string text, Action onClick)
    {
        var btn = AddTabButtonInternal(text, (s, e) => onClick());
        btn.Index = int.MaxValue;
        Invalidate();
    }

    Button AddTabButtonInternal(string text, EventHandler onClick)
    {
        var tabButton = new Button();
        tabButton.Text = text;
        tabButton.AutoSize = true;
        tabButton.MaxHeight = _header.Height - _header.Padding.Vertical;
        tabButton.Click += onClick;
        _header.Add(tabButton);
        return tabButton;
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

    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
    public EventHandler? MarginChanged { get; set; }
}
