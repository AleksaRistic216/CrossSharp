using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class Label : ControlBase, ILabel
{
    public EventHandler<EventArgs>? OnTextChanged { get; set; }
    public string Text { get; set; }
    public string FontFamily { get; set; }
    public int FontSize { get; set; }

    public override void Initialize() { }

    public override void Invalidate() { }

    public override void Redraw() { }

    public override void DrawContent(ref IGraphics g)
    {
        base.DrawContent(ref g);
        g.DrawText("Hello", 0, 0, Utils.Enums.FontFamily.Default, 12, ColorRgba.White);
    }
}
