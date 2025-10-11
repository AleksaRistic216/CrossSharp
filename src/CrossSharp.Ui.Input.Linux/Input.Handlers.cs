namespace CrossSharp.Ui.Linux;

partial class Input
{
    public EventHandler? OnClick { get; set; }
    public EventHandler? OnBackgroundColorChange { get; set; }
    public EventHandler? OnTextChanged { get; set; }
    public EventHandler? OnFocusChanged { get; set; }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        RaiseOnBackgroundColorChange();
    }

    void RaiseOnBackgroundColorChange() => OnBackgroundColorChange?.Invoke(this, EventArgs.Empty);

    void RaiseOnClick() => OnClick?.Invoke(this, EventArgs.Empty);

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
