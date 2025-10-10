namespace CrossSharp.Ui.Linux;

partial class Button
{
    public EventHandler? OnTextChange { get; set; }

    void OnTextChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnTextChange();
    }

    void RaiseOnTextChange() => OnTextChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnBackgroundColorChange { get; set; }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnBackgroundColorChange();
    }

    void RaiseOnBackgroundColorChange() => OnBackgroundColorChange?.Invoke(this, EventArgs.Empty);
}
