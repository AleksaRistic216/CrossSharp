namespace CrossSharp.Ui.Common;

partial class Chart
{
    public EventHandler? BackgroundColorChanged { get; set; }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        RaiseBackgroundColorChanged();
    }

    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
}
