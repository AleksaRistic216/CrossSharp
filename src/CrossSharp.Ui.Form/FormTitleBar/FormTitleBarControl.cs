using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl : GtkWidget, ITitleBar, IBackgroundColorProvider
{
    public override void Initialize()
    {
        Invalidate();
        base.Initialize();
    }
}
