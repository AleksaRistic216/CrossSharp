namespace CrossSharp.Ui;

public partial class PanelControl {
    public EventHandler? BackgroundColorChanged { get; set; }
    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
}