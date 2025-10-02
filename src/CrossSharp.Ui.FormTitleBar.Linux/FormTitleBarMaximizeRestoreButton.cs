using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;

namespace CrossSharp.Ui.Linux;

class FormTitleBarMaximizeRestoreButton() : Ui.Button(new FormTitleBarMaximizeRestoreButtonLinux());

class FormTitleBarMaximizeRestoreButtonLinux : Button
{
    const int GLYPH_OFFSET = 24;

    public override void DrawContent(ref Graphics g)
    {
        var form = GetForm();
        if (form == null)
            return;

        if (form.State == WindowState.Normal)
        {
            g.DrawRectangle(
                Location.X + GLYPH_OFFSET / 2,
                Location.Y + GLYPH_OFFSET / 2,
                Width - GLYPH_OFFSET,
                Height - GLYPH_OFFSET,
                ColorRgba.Black,
                2
            );
            return;
        }

        if (form.State == WindowState.Maximized)
        {
            const int offset = 4;
            g.DrawRectangle(
                Location.X + GLYPH_OFFSET / 2 - offset / 2,
                Location.Y + GLYPH_OFFSET / 2 - offset / 2,
                Width - GLYPH_OFFSET,
                Height - GLYPH_OFFSET,
                ColorRgba.Black,
                2
            );
            g.DrawRectangle(
                Location.X + GLYPH_OFFSET / 2 + offset,
                Location.Y + GLYPH_OFFSET / 2 + offset,
                Width - GLYPH_OFFSET,
                Height - GLYPH_OFFSET,
                ColorRgba.Black,
                2
            );
            return;
        }
    }
}
