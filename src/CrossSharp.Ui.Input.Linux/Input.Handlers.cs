namespace CrossSharp.Ui.Linux;

partial class Input
{
    public EventHandler? Click { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }
    public EventHandler? PlaceholderChanged { get; set; }

    void RaisePlaceholderChanged() => PlaceholderChanged?.Invoke(this, EventArgs.Empty);

    void OnPlaceholderChangedInternal()
    {
        Invalidate();
        RaisePlaceholderChanged();
    }

    public EventHandler? TextChanged { get; set; }
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
        InvalidateCaretBounds();
        InvalidateContentBounds();
        RaiseTextChanged();
    }

    void RaiseTextChanged()
    {
        TextChanged?.Invoke(this, EventArgs.Empty);
    }
}
