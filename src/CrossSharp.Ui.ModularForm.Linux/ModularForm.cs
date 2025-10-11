using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class ModularForm : FormSDL, IModularForm
{
    public ModularForm()
    {
        InitializeTopNavigationPane();
        InitializeContentPane();
    }

    void InitializeTopNavigationPane()
    {
        TopNavigationPane = new StackedLayout();
        TopNavigationPane.Direction = Direction.Horizontal;
        TopNavigationPane.Width = Width;
        TopNavigationPane.Height = 28;
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
        _contentPane.Height = Height - TopNavigationPane.Height;
        ContentHeight = _contentPane.Height;
        Controls.Add(_contentPane);
        this.OnSizeChanged += (s, e) =>
        {
            TopNavigationPane.Width = Width;
            _contentPane.Width = Width;
            _contentPane.Height = Height - TopNavigationPane.Height;
            ContentHeight = _contentPane.Height;
            _contentPane.Invalidate();
        };
        _viewer = new DynamicControlsController(ref _contentPane);
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
        _viewer.Set(identifier, pageType);
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
    /// Adds a page with a button to the top navigation pane.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="pageType"></param>
    /// <returns>Identifier of the page.</returns>
    public string AddPageWithNavigation(string name, Type pageType)
    {
        string id = Guid.NewGuid().ToString("N");
        AddPage(id, pageType);
        var btn = new Button();
        btn.Text = name;
        using var g = new SDLGraphics(Renderer);
        var textSize = g.MeasureText(name, _theme.DefaultFontFamily, _theme.DefaultFontSize);
        btn.Width = 100;
        btn.Height = textSize.Height + 8;
        btn.OnClick += OnClick;
        btn.Tag = id;
        TopNavigationPane.Add(btn);
        return id;
    }
}
