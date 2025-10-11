using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Button : ControlBase, IButton
{
    public override void Initialize() { }

    public override void Invalidate()
    {
        if (GetForm() is not IFormSDL form)
            return;
        using var graphics = new SDLGraphics(form.Renderer);
        var textSize = graphics.MeasureText(Text, _theme.DefaultFontFamily, _theme.DefaultFontSize);
        _textLocation = new Point((Width - textSize.Width) / 2, (Height - textSize.Height) / 2);
        if (BorderWidth == 0 && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderWidth = 2;
        if (BorderColor == ColorRgba.Transparent && this.GetRenderStyle() == RenderStyle.Outlined)
            BorderColor = BackgroundColor;
    }

    public override void DrawContent(ref IGraphics g)
    {
        g.DrawText(
            Text,
            _textLocation.X,
            _textLocation.Y,
            _theme.DefaultFontFamily,
            _theme.DefaultFontSize,
            ForegroundColor == ColorRgba.Transparent
                ? this.GetThemedBackgroundColor().Contrasted
                : ForegroundColor
        );
    }

    public override void Redraw()
    {
        // idea is to not draw each time but to have flag, and redraw only when that flag is updated using this
    }
}
