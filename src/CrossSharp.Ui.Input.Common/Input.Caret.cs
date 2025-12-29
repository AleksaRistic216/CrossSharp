using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Input
{
    void InvalidateCaretBounds(IGraphics g)
    {
        // Call this within draw because graphics gets fcked up when created here. Should fix it though
        var form = this.GetForm() as IFormSDL;
        if (form is null)
            return;
        var text = MultiLine ? Text.Split(Environment.NewLine)[_caretPosition.Y] : Text;
        text = text[..Math.Min((int)_caretPosition.X, (int)text.Length)];
        var textSize = g.MeasureText(text, FontFamily.Default, FontSize);
        var caretX = LINE_GAP + textSize.Width + CornerRadius / 2 + BorderWidth;
        var caretY = LINE_GAP / 2 + _caretPosition.Y * LineHeight + BorderWidth;
        _caretBounds = new Rectangle(caretX, caretY, 2, LineHeight);
    }

    void DrawCaret(ref IGraphics g)
    {
        if (!IsFocused)
            return;
        InvalidateCaretBounds(g);
        var now = DateTime.Now;
        if ((now - _lastCaretStateUpdate).TotalMilliseconds >= 500)
        {
            _caretVisible = !_caretVisible;
            _lastCaretStateUpdate = now;
        }
        if (!_caretVisible)
            return;
        g.FillRectangle(_caretBounds.X, _caretBounds.Y, 2, LineHeight, ColorRgba.Black);
    }

    internal void ShiftCaretPosition(int amount, bool invalidateBeforeAndAfterText = true)
    {
        if (!MultiLine)
        {
            _caretPosition.X += amount;
            if (_caretPosition.X < 0)
                _caretPosition.X = 0;
            if (_caretPosition.X > Text.Length)
                _caretPosition.X = Text.Length;
            InvalidateCaretText();
            return;
        }

        var lines = Text.Split(Environment.NewLine);
        _caretPosition.X += amount;

        // Handle wrapping to previous line when going left
        while (_caretPosition is { X: < 0, Y: > 0 })
        {
            _caretPosition.Y--;
            _caretPosition.X += lines[_caretPosition.Y].Length + 1; // +1 for the newline
        }
        if (_caretPosition.X < 0)
        {
            _caretPosition.X = 0;
        }

        // Handle wrapping to next line when going right
        while (_caretPosition.Y < lines.Length - 1 && _caretPosition.X > lines[_caretPosition.Y].Length)
        {
            _caretPosition.X -= lines[_caretPosition.Y].Length + 1; // +1 for the newline
            _caretPosition.Y++;
        }

        // Clamp X to line length
        if (_caretPosition.X > lines[_caretPosition.Y].Length)
        {
            _caretPosition.X = lines[_caretPosition.Y].Length;
        }
        InvalidateCaretText();
    }

    bool HandleCaretMovement(KeyInputArgs keyInputArgs)
    {
        // Ignore modifier keys pressed alone - they shouldn't affect selection
        if (IsModifierKey(keyInputArgs.KeyCode))
            return false;

        var isShift = keyInputArgs.IsShiftPressed;
        var isCtrl = keyInputArgs.IsCtrlPressed;

        // If Shift is held, we're extending selection; otherwise clear it
        if (!isShift && HasSelection)
        {
            // When moving without shift and there's a selection,
            // move caret to the appropriate edge of selection based on direction
            var (start, end) = GetNormalizedSelection();

            if (keyInputArgs.KeyCode == KeyCode.VcLeft)
            {
                _caretPosition = start;
                ClearSelection();
                InvalidateCaretText();
                return true;
            }
            if (keyInputArgs.KeyCode == KeyCode.VcRight)
            {
                _caretPosition = end;
                ClearSelection();
                InvalidateCaretText();
                return true;
            }

            // For other keys, just clear selection and proceed
            ClearSelection();
        }

        // If Shift is held and no selection exists, set anchor
        if (isShift && !HasSelection)
        {
            _selectionAnchor = _caretPosition;
        }

        bool handled = false;

        if (keyInputArgs.KeyCode == KeyCode.VcLeft)
        {
            if (isCtrl)
                MoveCaretToWordBoundary(-1);
            else
                ShiftCaretPosition(-1);
            handled = true;
        }
        else if (keyInputArgs.KeyCode == KeyCode.VcRight)
        {
            if (isCtrl)
                MoveCaretToWordBoundary(1);
            else
                ShiftCaretPosition(1);
            handled = true;
        }
        else if (keyInputArgs.KeyCode == KeyCode.VcUp && MultiLine)
        {
            if (_caretPosition.Y > 0)
            {
                _caretPosition.Y--;
                var lines = Text.Split(Environment.NewLine);
                _caretPosition.X = Math.Min((int)_caretPosition.X, (int)lines[_caretPosition.Y].Length);
                InvalidateCaretText();
            }
            handled = true;
        }
        else if (keyInputArgs.KeyCode == KeyCode.VcDown && MultiLine)
        {
            var lines = Text.Split(Environment.NewLine);
            if (_caretPosition.Y < lines.Length - 1)
            {
                _caretPosition.Y++;
                _caretPosition.X = Math.Min((int)_caretPosition.X, (int)lines[_caretPosition.Y].Length);
                InvalidateCaretText();
            }
            handled = true;
        }
        else if (keyInputArgs.KeyCode == KeyCode.VcHome)
        {
            if (isCtrl && MultiLine)
            {
                // Ctrl+Home: go to document start
                _caretPosition = Point.Empty;
            }
            else
            {
                _caretPosition.X = 0;
            }
            InvalidateCaretText();
            handled = true;
        }
        else if (keyInputArgs.KeyCode == KeyCode.VcEnd)
        {
            if (isCtrl && MultiLine)
            {
                // Ctrl+End: go to document end
                var lines = Text.Split(Environment.NewLine);
                _caretPosition = new Point(lines[^1].Length, lines.Length - 1);
            }
            else if (!MultiLine)
            {
                _caretPosition.X = Text.Length;
            }
            else
            {
                var lines = Text.Split(Environment.NewLine);
                _caretPosition.X = lines[_caretPosition.Y].Length;
            }
            InvalidateCaretText();
            handled = true;
        }
        else if (keyInputArgs.KeyCode == KeyCode.VcPageUp && MultiLine)
        {
            _caretPosition.Y = 0;
            var lines = Text.Split(Environment.NewLine);
            _caretPosition.X = Math.Min((int)_caretPosition.X, (int)lines[_caretPosition.Y].Length);
            InvalidateCaretText();
            handled = true;
        }
        else if (keyInputArgs.KeyCode == KeyCode.VcPageDown && MultiLine)
        {
            var lines = Text.Split(Environment.NewLine);
            _caretPosition.Y = lines.Length - 1;
            _caretPosition.X = Math.Min((int)_caretPosition.X, (int)lines[_caretPosition.Y].Length);
            InvalidateCaretText();
            handled = true;
        }

        // If movement occurred without Shift, clear selection (sync anchor with caret)
        if (handled && !isShift)
        {
            ClearSelection();
        }

        return handled;
    }

    static bool IsModifierKey(KeyCode keyCode) =>
        keyCode
            is KeyCode.VcLeftShift
                or KeyCode.VcRightShift
                or KeyCode.VcLeftControl
                or KeyCode.VcRightControl
                or KeyCode.VcLeftAlt
                or KeyCode.VcRightAlt
                or KeyCode.VcLeftMeta
                or KeyCode.VcRightMeta;
}
