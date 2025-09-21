namespace CrossSharp.Ui.Linux;

public partial class PanelControl
{
    public event EventHandler? BackgroundColorChanged;

    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
}
