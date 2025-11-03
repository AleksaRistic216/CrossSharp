using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.TextEditor;

public class TextEditTab : StaticLayout, ITabbedLayoutTab
{
    Input _input = new();

    public TextEditTab()
    {
        Initialize();
    }

    void Initialize()
    {
        Dock = DockStyle.Fill;

        _input.Width = Width;
        _input.Height = Height;
        _input.MultiLine = true;
        Add(_input);

        SizeChanged += (s, e) =>
        {
            _input.Width = Width;
            _input.Height = Height;
        };
    }

    TextEditTabDataProvider GetDataProvider() => Services.GetSingleton<TextEditTabDataProvider>();

    public void OnTabFocusGained(string title)
    {
        var dataProvider = GetDataProvider();
        var text = dataProvider.GetFileContents(title);
        // _input.Text = text;
    }
}
