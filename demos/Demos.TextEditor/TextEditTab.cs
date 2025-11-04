using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Extensions;
using CrossSharp.Utils.Interfaces;
using Material.Icons;

namespace Demos.TextEditor;

public class TextEditTab : StaticLayout, ITabbedLayoutTab
{
    readonly Input _input = new();
    readonly StackedLayout _editorBar = new();
    bool _textChanged;
    ITheme Theme => Services.GetSingleton<ITheme>();

    public TextEditTab()
    {
        Initialize();
    }

    void Initialize()
    {
        InitializeEditorBar();
        Dock = DockStyle.Fill;
        _input.Dock = DockStyle.Fill;
        _input.DockIndex = 1;
        _input.MultiLine = true;
        _input.TextChanged += TextChanged;
        Add(_input);
    }

    void TextChanged(object? sender, EventArgs e)
    {
        _textChanged = true;
    }

    void InitializeEditorBar()
    {
        var editorBarHeight = 30;
        _editorBar.Height = editorBarHeight;
        _editorBar.BackgroundColor = Theme.SecondaryBackgroundColor;
        _editorBar.Direction = Direction.Horizontal;
        _editorBar.Dock = DockStyle.Top;
        _editorBar.DockIndex = 0;
        Add(_editorBar);

        var btn = new Button();
        btn.Image = EfficientImage.Get(nameof(Constants.SaveFileIconKind));
        btn.Width = editorBarHeight;
        btn.SetMargin(2);
        _editorBar.Add(btn);
    }

    TextEditTabDataProvider GetDataProvider() => Services.GetSingleton<TextEditTabDataProvider>();

    public void OnTabFocusGained(string title)
    {
        var dataProvider = GetDataProvider();
        var text = dataProvider.GetFileContents(title);
        _input.Text = text;
    }
}
