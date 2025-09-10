using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public partial class PanelControl : GtkWidget, IBackgroundColorProvider
{
    public override void Invalidate()
    {
        Redraw();
    }
}
