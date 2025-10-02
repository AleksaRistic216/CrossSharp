using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;

namespace CrossSharp.Ui.Linux;

class FormTitleBarMinimizeButton() : Ui.Button(new FormTitleBarMinimizeButtonLinux());

class FormTitleBarMinimizeButtonLinux : Button
{
    const int GLYPH_OFFSET = 16;
    const int BORDER_WIDTH = 1;

    public override void DrawContent(ref Graphics g)
    {
        g.DrawRectangle(
            Location.X + GLYPH_OFFSET / 2,
            Location.Y + Height / 2,
            Width - GLYPH_OFFSET,
            1,
            ColorRgba.Black,
            BORDER_WIDTH
        );
    }
}
