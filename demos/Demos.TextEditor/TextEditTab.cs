using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.TextEditor;

public class TextEditTab : StaticLayout
{
    public TextEditTab()
    {
        Dock = DockStyle.Fill;

        var input = new Input();
        input.Width = Width;
        input.Height = Height;
        input.MultiLine = true;
        Add(input);

        SizeChanged += (s, e) =>
        {
            input.Width = Width;
            input.Height = Height;
        };
    }
}
