using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;

namespace CrossSharp.Ui.Linux;

class FormTitleBarMaximizeButton() : Ui.Button(new FormTitleBarMaximizeButtonLinux());

class FormTitleBarMaximizeButtonLinux : Button
{
    const int GLYPH_OFFSET = 24;

    public override void DrawContent(Graphics g)
    {
        g.DrawRectangle(
            Location.X + GLYPH_OFFSET / 2,
            Location.Y + GLYPH_OFFSET / 2,
            Width - GLYPH_OFFSET,
            Height - GLYPH_OFFSET,
            ColorRgba.Black,
            2
        );
        // base.DrawContent(g);
    }
}
