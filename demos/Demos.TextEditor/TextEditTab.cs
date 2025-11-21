using CrossSharp.Desktop;
using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace Demos.TextEditor;

public class TextEditTab : StaticLayout, ITabbedLayoutTab
{
    string? _title;
    readonly Input _input = new();
    readonly StackedLayout _editorBar = new();
    ITheme Theme => Services.GetSingleton<ITheme>();

    public TextEditTab()
    {
        InitializeEditorBar();
        Dock = DockStyle.Fill;
        _input.Dock = DockStyle.Fill;
        _input.DockIndex = 1;
        _input.MultiLine = true;
        _input.TextChanged += TextChanged;
        Add(_input);
    }

    void TextChanged(object? sender, EventArgs e) { }

    void InitializeEditorBar()
    {
        var editorBarHeight = 30;
        _editorBar.Height = editorBarHeight;
        _editorBar.BackgroundColor = Theme.SecondaryColor;
        _editorBar.Orientation = Orientation.Horizontal;
        _editorBar.Dock = DockStyle.Top;
        _editorBar.DockIndex = 0;
        Add(_editorBar);

        var btn = new Button();
        btn.Image = EfficientImage.Get(nameof(Constants.SaveFileIconKind));
        btn.Width = editorBarHeight;
        btn.Margin = new Margin(2);
        btn.Click += (_, _) =>
        {
            if (string.IsNullOrWhiteSpace(_title))
            {
                Notifications.Show("Cannot save file: no title specified.");
                return;
            }
            var dataProvider = GetDataProvider();
            dataProvider.SaveFileContents(_title, _input.Text);
        };
        _editorBar.Add(btn);
    }

    TextEditTabDataProvider GetDataProvider() => Services.GetSingleton<TextEditTabDataProvider>();

    public void OnTabFocusGained(string title)
    {
        var dataProvider = GetDataProvider();
        var text = dataProvider.GetFileContents(title);
        _input.Text = text;
        _title = title;
    }
}
