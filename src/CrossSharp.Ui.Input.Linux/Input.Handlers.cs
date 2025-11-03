namespace CrossSharp.Ui.Linux;

partial class Input
{
    public EventHandler? Click { get; set; }

    void RaiseClick() => Click?.Invoke(this, EventArgs.Empty);

    void OnClickInternal()
    {
        RaiseClick();
    }

    public EventHandler? BackgroundColorChanged { get; set; }

    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        RaiseBackgroundColorChanged();
    }

    public EventHandler? PlaceholderChanged { get; set; }

    void RaisePlaceholderChanged() => PlaceholderChanged?.Invoke(this, EventArgs.Empty);

    void OnPlaceholderChangedInternal()
    {
        Invalidate();
        RaisePlaceholderChanged();
    }

    public EventHandler? TextChanged { get; set; }

    void RaiseTextChanged()
    {
        TextChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnTextChangedInternal()
    {
        InvalidateContentBounds();
        RaiseTextChanged();
    }

    public EventHandler? OnFocusChanged { get; set; }

    void RaiseOnFocusChanged() => OnFocusChanged?.Invoke(this, EventArgs.Empty);

    void OnFocusChangedInternal()
    {
        Invalidate();
        RaiseOnFocusChanged();
    }
}
