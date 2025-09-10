namespace CrossSharp.Ui.FormTitleBar;

public partial class FormTitleBarControl
{
    public EventHandler? TypeChanged { get; set; }

    void RaiseTypeChanged()
    {
        Invalidate();
        TypeChanged?.Invoke(this, EventArgs.Empty);
    }
}
