namespace CrossSharp.Ui.Linux;

partial class Label
{
    public EventHandler<EventArgs>? OnTextChanged { get; set; }

    void OnTextChangedInternal()
    {
        Invalidate();
        RaiseTextChanged();
    }

    void RaiseTextChanged()
    {
        OnTextChanged?.Invoke(this, EventArgs.Empty);
    }
}
