using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public partial class PanelControl : GtkWidget, IBackgroundColorProvider
{
    readonly IPanelControl _impl;

    public PanelControl()
    {
        _impl = ServicesPool.GetSingleton<IPanelControlFactory>().Create(this);
    }

    public override void Invalidate() => _impl.Invalidate();

    public override void DrawShadows(Graphics g) => _impl.DrawShadows(g);

    public override void DrawBackground(Graphics g) => _impl.DrawBackground(g);

    public override void DrawBorders(Graphics g) => _impl.DrawBorders(g);

    public override void DrawContent(Graphics g) => _impl.DrawContent(g);
}
