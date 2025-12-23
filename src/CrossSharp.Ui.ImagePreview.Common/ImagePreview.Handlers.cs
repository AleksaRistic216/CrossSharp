namespace CrossSharp.Ui.Common;

partial class ImagePreview
{
    public EventHandler? OnImageChanged { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }

    void OnImageChangedInternal()
    {
        Invalidate();
        OnImageChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
    }
}
