using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Pango;

namespace CrossSharp.Ui.Linux;

public partial class Button : GtkWidget, IButton
{
    public Button()
    {
        SubscribeToInputs();
    }

    public override void Invalidate()
    {
        base.Invalidate();
        var rectRect = GtkHelpers.GetTextRenderRect(Handle, Text, "Arial", 12);
        _textLocation = new Point(
            Location.X + (Width - rectRect.Width) / 2,
            Location.Y + (Height - rectRect.Height) / 2
        );
    }

    public override void DrawShadows(Graphics g) { }

    public override void DrawBackground(Graphics g)
    {
        var color = !IsMouseOver ? BackgroundColor : BackgroundColor.Highlighted;
        g.FillRectangle(Location.X, Location.Y, Width, Height, color);
    }

    public override void DrawBorders(Graphics g) { }

    public override void DrawContent(Graphics g)
    {
        if (string.IsNullOrWhiteSpace(Text))
            return;
        g.DrawText(
            Text,
            _textLocation.X,
            _textLocation.Y,
            ColorRgba.Black,
            "Arial",
            12,
            PangoWeight.Normal,
            PangoStyle.Normal
        );
    }
}
