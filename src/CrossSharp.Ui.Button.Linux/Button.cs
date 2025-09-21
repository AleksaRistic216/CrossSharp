using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class Button : GtkWidget, IButton
{
    public ColorRgba BackgroundColor { get; set; } =
        ServicesPool.GetSingleton<ITheme>().ButtonBackgroundColor;

    public Button()
    {
        SubscribeToInputs();
    }

    public override void Invalidate() { }

    public override void DrawShadows(Graphics g) { }

    public override void DrawBackground(Graphics g)
    {
        var color = !IsMouseOver ? BackgroundColor : BackgroundColor.Highlighted;
        g.FillRectangle(Location.X, Location.Y, Width, Height, color);
    }

    public override void DrawBorders(Graphics g) { }

    public override void DrawContent(Graphics g) { }
}
