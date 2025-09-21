using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class Label : GtkWidget, ILabel
{
    public override void Invalidate()
    {
        throw new NotImplementedException();
    }

    public override void DrawShadows(Graphics g)
    {
        throw new NotImplementedException();
    }

    public override void DrawBackground(Graphics g)
    {
        throw new NotImplementedException();
    }

    public override void DrawBorders(Graphics g)
    {
        throw new NotImplementedException();
    }

    public override void DrawContent(Graphics g)
    {
        throw new NotImplementedException();
    }
}
