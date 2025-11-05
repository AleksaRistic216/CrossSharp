using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Label : ControlBase, ILabel
{
    public override void Initialize() { }

    public override void PerformTheme()
    {
        FontFamily = Theme.DefaultFontFamily;
        FontSize = Theme.DefaultFontSize;
        ForegroundColor = Theme.LayoutBackgroundColor.Contrasted;
    }

    public override void Invalidate()
    {
        if (this.GetForm() is not IFormSDL form)
            return;
        // I do not want to auto size labels. No need to do that without IAutoSize
        // using var graphics = new SDLGraphics(form.Renderer);
        // var size = graphics.MeasureText(Text, FontFamily, FontSize);
        // Width = size.Width;
        // Height = size.Height;
        if (Equals(ForegroundColor, ColorRgba.Transparent))
            ForegroundColor = Theme.PrimaryColor.Contrasted;
    }

    public override void Redraw()
    {
        // idea is to not draw each time but to have flag, and redraw only when that flag is updated using this
    }

    public override void DrawContent(ref IGraphics g)
    {
        if (string.IsNullOrEmpty(Text))
            return;
        if (FontSize <= 0)
            return;
        g.DrawText(Text, 0, 0, FontFamily, FontSize, ForegroundColor);
    }
}
