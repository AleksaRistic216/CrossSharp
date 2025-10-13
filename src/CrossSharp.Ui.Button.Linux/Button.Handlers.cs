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

    public EventHandler? BackgroundColorChanged { get; set; }

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnBackgroundColorChange();
    }

    void RaiseOnBackgroundColorChange() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnClick { get; set; }

    void RaiseOnClick() => OnClick?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnTagChange { get; set; }

    void OnTagChangedInternal()
    {
        RaiseOnTagChange();
    }

    void RaiseOnTagChange() => OnTagChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnTextAlignmentChange { get; set; }

    void OnTextAlignmentChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnTextAlignmentChange();
    }

    void RaiseOnTextAlignmentChange() => OnTextAlignmentChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnImageChange { get; set; }

    void OnImageChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnImageChange();
    }

    void RaiseOnImageChange() => OnImageChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnImagePlacementChange { get; set; }

    void OnImagePlacementChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnImagePlacementChange();
    }

    void RaiseOnImagePlacementChange() => OnImagePlacementChange?.Invoke(this, EventArgs.Empty);

    public EventHandler? OnImageScaleChange { get; set; }

    void OnImageScaleChangedInternal()
    {
        Invalidate();
        Redraw();
        RaiseOnImageScaleChange();
    }

    void RaiseOnImageScaleChange() => OnImageScaleChange?.Invoke(this, EventArgs.Empty);
}
