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
        {
            InvalidateCaretText();
            return;
        }
        if (e.KeyCode == KeyCode.VcBackspace)
        {
            if (_textBeforeCaret.Length <= 0)
                return;
            _textBeforeCaret = _textBeforeCaret[..^1];
            Text = _textBeforeCaret + _textAfterCaret;
            ShiftCaretPosition(-1, false);
            return;
        }
        if (e.KeyCode == KeyCode.VcDelete)
        {
            if (_textAfterCaret.Length <= 0)
                return;
            _textAfterCaret = _textAfterCaret[1..];
            Text = _textBeforeCaret + _textAfterCaret;
            return;
        }
        if (e.KeyCode == KeyCode.VcEnter && MultiLine)
        {
            _textBeforeCaret += Environment.NewLine;
            Text = _textBeforeCaret + _textAfterCaret;
            _caretPosition.Y++;
            _caretPosition.X = 0;
            return;
        }
        if (e.Char is null)
            return;
        _textBeforeCaret += e.Char;
        Text = _textBeforeCaret + _textAfterCaret;
        ShiftCaretPosition(1, false);
    }
}
