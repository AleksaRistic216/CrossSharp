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
        CalcSize();
    }

    void CalcSize()
    {
        var pangoContext = GtkHelpers.gtk_widget_get_pango_context(Handle);
        var layout = PangoHelpers.pango_layout_new(pangoContext);
        PangoHelpers.pango_layout_set_text(layout, Text, -1);

        IntPtr pangoDesc = PangoHelpers.CreateDescription(
            FontFamily,
            FontSize,
            PangoWeight.Normal, // TODO: Property
            PangoStyle.Normal // TOOD: Property
        );
        PangoHelpers.pango_layout_set_font_description(layout, pangoDesc);
        PangoHelpers.pango_layout_get_size(layout, out int width, out int height);
        Width = width / 1024;
        Height = height / 1024;
    }

    public override void DrawShadows(Graphics g) { }

    public override void DrawBackground(Graphics g)
    {
        g.FillRectangle(Location.X, Location.Y, Width, Height, ColorRgba.Pink);
    }

    public override void DrawBorders(Graphics g) { }

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
