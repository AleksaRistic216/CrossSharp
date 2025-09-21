namespace CrossSharp.Ui.Linux;

public partial class Label
{
    public EventHandler<EventArgs>? OnTextChanged { get; set; }
    public EventHandler<EventArgs>? OnFontFamilyChanged { get; set; }
    public EventHandler<EventArgs>? OnFontSizeChanged { get; set; }
    public EventHandler? LayoutChanged { get; set; }

    void RaiseTextChanged()
    {
        Invalidate();
        Redraw();
        OnTextChanged?.Invoke(this, EventArgs.Empty);
        RaiseLayoutChanged();
    }

    void RaiseFontFamilyChanged()
    {
        Invalidate();
        Redraw();
        OnFontFamilyChanged?.Invoke(this, EventArgs.Empty);
        RaiseLayoutChanged();
    }

    void RaiseFontSizeChanged()
    {
        Invalidate();
        Redraw();
        OnFontSizeChanged?.Invoke(this, EventArgs.Empty);
        RaiseLayoutChanged();
    }

    void RaiseLayoutChanged() => LayoutChanged?.Invoke(this, EventArgs.Empty);
}
