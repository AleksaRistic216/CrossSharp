namespace CrossSharp.Ui.Linux;

public partial class Panel
{
    public event EventHandler? BackgroundColorChanged;

    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
}
