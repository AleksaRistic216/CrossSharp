using System.Drawing;
using System.Runtime.InteropServices;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Pango;

namespace CrossSharp.Ui.Linux;

public partial class Label : GtkWidget, ILabel
{
    public override void Show()
    {
        Invalidate();
        base.Show();
    }

    public override void Invalidate()
    {
        base.Invalidate();
        var renderRect = GtkHelpers.GetTextRenderRect(Handle, Text, FontFamily, FontSize);
        Width = renderRect.Width;
        Height = renderRect.Height;
    }

    public override void DrawShadows(Graphics g) { }

    public override void DrawBackground(Graphics g) { }

    public override void DrawContent(Graphics g)
    {
        g.DrawText(
            Text,
            Location.X,
            Location.Y,
            ColorRgba.Black,
            FontFamily,
            FontSize,
            PangoWeight.Normal,
            PangoStyle.Normal
        );
    }

    Size ICenterPanelChild.GetSize()
    {
        return new Size(Width, Height);
    }
}
