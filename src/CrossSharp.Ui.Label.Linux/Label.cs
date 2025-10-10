using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class Label : ControlBase, ILabel
{
    public EventHandler<EventArgs>? OnTextChanged { get; set; }
    public string Text { get; set; }
    public FontFamily FontFamily { get; set; } = Services.GetSingleton<ITheme>().DefaultFontFamily;
    public int FontSize { get; set; } = Services.GetSingleton<ITheme>().DefaultFontSize;

    public override void Initialize() { }

    public override void Invalidate() { }

    public override void Redraw()
    {
        // idea is to not draw each time but to have flag, and redraw only when that flag is updated using this
        throw new NotImplementedException();
    }

    public override void DrawContent(ref IGraphics g)
    {
        if (string.IsNullOrEmpty(Text))
            return;
        if (FontSize <= 0)
            return;
        g.DrawText(Text, Location.X, Location.Y, FontFamily, FontSize, ForegroundColor);
    }
}
