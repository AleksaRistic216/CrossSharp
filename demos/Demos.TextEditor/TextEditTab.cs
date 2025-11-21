using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.TextEditor;

public class TextEditTab : StaticLayout, ITabbedLayoutTab
{
    string? _title;
    readonly Input _input = new();
    ITheme Theme => Services.GetSingleton<ITheme>();

    public TextEditTab()
    {
        Dock = DockStyle.Fill;
        _input.Dock = DockStyle.Fill;
        _input.DockIndex = 1;
        _input.MultiLine = true;
        _input.TextChanged += TextChanged;
        Add(_input);
    }

    void TextChanged(object? sender, EventArgs e) { }

    TextEditTabDataProvider GetDataProvider() => Services.GetSingleton<TextEditTabDataProvider>();

    public void OnTabFocusGained(string title)
    {
        var dataProvider = GetDataProvider();
        var text = dataProvider.GetFileContents(title);
        _input.Text = text;
        _title = title;
    }
}
