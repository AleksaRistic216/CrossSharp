using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl : GtkWidget, ITitleBar, IBackgroundColorProvider
{
    public EventHandler? TypeChanged { get; set; }

    void RaiseTypeChanged()
    {
        Invalidate();
        TypeChanged?.Invoke(this, EventArgs.Empty);
    }
}
