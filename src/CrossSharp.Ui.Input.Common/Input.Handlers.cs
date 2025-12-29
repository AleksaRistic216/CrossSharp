using System.Drawing;
using System.Runtime.InteropServices;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Common;

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
        // Click was beyond the text - place cursor at end
        _caretPosition = new Point(text.Length, lineIndex);
        InvalidateCaretText();
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
        // Check if this was a programmatic change (not from typing)
        if (_text != _textBeforeCaret + _textAfterCaret)
        {
            // Reset caret state for programmatic text changes
            // Place caret at end of text
            if (MultiLine && _text.Contains(Environment.NewLine))
            {
                var lines = _text.Split(Environment.NewLine);
                _caretPosition = new System.Drawing.Point(lines[^1].Length, lines.Length - 1);
            }
            else
            {
                _caretPosition = new System.Drawing.Point(_text.Length, 0);
            }
            _textBeforeCaret = _text;
            _textAfterCaret = string.Empty;
        }
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

        // Handle clipboard shortcuts first
        if (e.IsCtrlPressed)
        {
            if (e.KeyCode == KeyCode.VcA)
            {
                SelectAll();
                return;
            }
            if (e.KeyCode == KeyCode.VcC)
            {
                CopySelection();
                return;
            }
            if (e.KeyCode == KeyCode.VcX)
            {
                CutSelection();
                return;
            }
            if (e.KeyCode == KeyCode.VcV)
            {
                PasteFromClipboard();
                return;
            }
        }

        if (HandleCaretMovement(e))
        {
            InvalidateCaretText();
            return;
        }
        if (e.KeyCode == KeyCode.VcBackspace)
        {
            if (HasSelection)
            {
                DeleteSelectedText();
                return;
            }
            if (_textBeforeCaret.Length <= 0)
                return;
            _textBeforeCaret = _textBeforeCaret[..^1];
            Text = _textBeforeCaret + _textAfterCaret;
            ShiftCaretPosition(-1, false);
            return;
        }
        if (e.KeyCode == KeyCode.VcDelete)
        {
            if (HasSelection)
            {
                DeleteSelectedText();
                return;
            }
            if (_textAfterCaret.Length <= 0)
                return;
            _textAfterCaret = _textAfterCaret[1..];
            Text = _textBeforeCaret + _textAfterCaret;
            return;
        }
        if (e.KeyCode == KeyCode.VcEnter && MultiLine)
        {
            if (HasSelection)
                DeleteSelectedText();
            _textBeforeCaret += Environment.NewLine;
            Text = _textBeforeCaret + _textAfterCaret;
            _caretPosition.Y++;
            _caretPosition.X = 0;
            ClearSelection();
            return;
        }
        if (e.Char is null)
            return;

        // Character input - delete selection first if any
        if (HasSelection)
            DeleteSelectedText();

        _textBeforeCaret += e.Char;
        Text = _textBeforeCaret + _textAfterCaret;
        ShiftCaretPosition(1, false);
        ClearSelection();
    }

    void CopySelection()
    {
        if (!HasSelection)
            return;
        var text = GetSelectedText();
        SDLHelpers.SDL_SetClipboardText(text);
    }

    void CutSelection()
    {
        if (!HasSelection)
            return;
        CopySelection();
        DeleteSelectedText();
    }

    void PasteFromClipboard()
    {
        if (!SDLHelpers.SDL_HasClipboardText())
            return;

        var ptr = SDLHelpers.SDL_GetClipboardText();
        if (ptr == IntPtr.Zero)
            return;

        var text = Marshal.PtrToStringUTF8(ptr);
        SDLHelpers.SDL_free(ptr);

        if (string.IsNullOrEmpty(text))
            return;

        if (HasSelection)
            DeleteSelectedText();

        // Insert at caret position
        _textBeforeCaret += text;
        Text = _textBeforeCaret + _textAfterCaret;

        // Update caret position
        if (!MultiLine || !text.Contains(Environment.NewLine))
        {
            _caretPosition.X += text.Length;
        }
        else
        {
            var pastedLines = text.Split(Environment.NewLine);
            _caretPosition.Y += pastedLines.Length - 1;
            _caretPosition.X = pastedLines[^1].Length;
        }
        InvalidateCaretText();
        ClearSelection();
    }

    void InputHandlerOnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!IsFocused)
        {
            OnClickInternal(e);
            return;
        }

        var screenBounds = this.GetScreenBounds();
        if (!screenBounds.Contains(new Point(e.X, e.Y)))
            return;

        // Handle different click counts
        switch (e.Clicks)
        {
            case 1:
                // Single click - position caret and start potential drag selection
                UpdateCaretPositionOnClick(new Point(e.X, e.Y));
                _selectionAnchor = _caretPosition;
                _isSelecting = true;
                break;

            case 2:
                // Double click - select word at caret
                UpdateCaretPositionOnClick(new Point(e.X, e.Y));
                SelectWordAtCaret();
                _isSelecting = false;
                break;

            case >= 3:
                // Triple click - select line (or all for single-line)
                UpdateCaretPositionOnClick(new Point(e.X, e.Y));
                SelectCurrentLine();
                _isSelecting = false;
                break;
        }

        RaiseClick();
    }

    void InputHandlerOnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (!IsFocused || !_isSelecting)
            return;

        var screenBounds = this.GetScreenBounds();
        if (!screenBounds.Contains(new Point(e.X, e.Y)))
            return;

        // Update caret position based on drag - anchor stays where click started
        UpdateCaretPositionOnClick(new Point(e.X, e.Y));
    }

    void InputHandlerOnMouseReleased(object? sender, MouseInputArgs e)
    {
        _isSelecting = false;
    }
}
