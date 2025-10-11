namespace CrossSharp.Ui.Linux;

partial class Input
{
    public EventHandler? OnClick { get; set; }
    public EventHandler? OnBackgroundColorChange { get; set; }
    public EventHandler? OnTextChanged { get; set; }

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
