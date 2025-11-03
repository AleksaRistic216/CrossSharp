using CrossSharp.Utils.Input;

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

    // ====
    void InputHandlerOnKeyPressed(object? sender, KeyInputArgs e)
    {
        if (!IsFocused)
            return;
        if (HandleCaretMovement(e))
            return;
        if (e.KeyCode == KeyCode.VcBackspace)
        {
            if (Text.Length > 0)
                Text = Text[..^1];
            ShiftCaretPosition(-1);
            return;
        }
        if (e.KeyCode == KeyCode.VcEnter && MultiLine)
        {
            Text += Environment.NewLine;
            _caretPosition.Y++;
            _caretPosition.X = 0;
            return;
        }
        if (e.Char is null)
            return;
        Text += e.Char;
        ShiftCaretPosition(1);
    }
}
