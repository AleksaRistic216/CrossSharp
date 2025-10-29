namespace CrossSharp.Ui.Linux;

partial class Input
{
    public EventHandler? Click { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }
    public EventHandler? OnTextChanged { get; set; }
    public EventHandler? OnFocusChanged { get; set; }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        RaiseOnBackgroundColorChange();
    }

    void RaiseOnBackgroundColorChange() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);

    void RaiseOnClick() => Click?.Invoke(this, EventArgs.Empty);

    void OnFocusChangedInternal()
    {
        Invalidate();
        RaiseOnFocusChanged();
    }

    void RaiseOnFocusChanged() => OnFocusChanged?.Invoke(this, EventArgs.Empty);

    void OnTextChangedInternal()
    {
        RaiseTextChanged();
        Invalidate();
    }

    void RaiseTextChanged()
    {
        OnTextChanged?.Invoke(this, EventArgs.Empty);
    }
}
