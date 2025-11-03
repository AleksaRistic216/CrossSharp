using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Input
{
    public EventHandler? Click { get; set; }

    void RaiseClick() => Click?.Invoke(this, EventArgs.Empty);

    void OnClickInternal(MouseInputArgs e)
    {
        if (!IsFocused)
            return;
        UpdateCaretPositionOnClick(new Point(e.X, e.Y));
        RaiseClick();
    }

    void UpdateCaretPositionOnClick(Point mousePos)
    {
        if (string.IsNullOrWhiteSpace(Text))
        {
            _caretPosition = new Point(0, 0);
            return;
        }

        var screenBounds = this.GetScreenBounds();
        if (!screenBounds.Contains(mousePos))
            return;

        var form = this.GetForm() as IFormSDL;
        if (form is null)
            return;

        var contentScreenBounds = new Rectangle(
            screenBounds.X + _contentBounds.X,
            screenBounds.Y + _contentBounds.Y,
            _contentBounds.Width,
            _contentBounds.Height
        );
        if (!contentScreenBounds.Contains(mousePos))
            return;

        using var g = new SDLGraphics(form.Renderer);
        var textSize = g.MeasureText(Text, FontFamily.Default, FontSize);
        if (textSize.Width <= 0 || textSize.Height <= 0)
            return;

        var y = mousePos.Y - contentScreenBounds.Y;
        var lineIndex = y / LineHeight;
        if (lineIndex < 0)
            lineIndex = 0;
        var lines = Text.Split(Environment.NewLine);
        if (lineIndex >= lines.Length)
            lineIndex = lines.Length - 1;

        var textWidthTillMousePosition = mousePos.X - contentScreenBounds.X;
        var text = MultiLine ? Text.Split(Environment.NewLine)[lineIndex] : Text;
        for (var i = 0; i <= text.Length; i++)
        {
            var subText = text[..i];
            var subTextSize = g.MeasureText(subText, FontFamily.Default, FontSize);
            if (subTextSize.Width < textWidthTillMousePosition)
                continue;
            _caretPosition = new Point(i, lineIndex);
            InvalidateCaretText();
            return;
        }
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

    void InputHandlerOnMousePressed(object? sender, MouseInputArgs e)
    {
        OnClickInternal(e);
    }
}
