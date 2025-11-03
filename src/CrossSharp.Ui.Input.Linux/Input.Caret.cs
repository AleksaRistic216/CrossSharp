using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Input
{
    void InvalidateCaretBounds(IGraphics g)
    {
        // Call this within draw because graphics gets fcked up when created here. Should fix it though
        var form = this.GetForm() as IFormSDL;
        if (form is null)
            return;
        var text = MultiLine ? Text.Split(Environment.NewLine)[_caretPosition.Y] : Text;
        text = text[..Math.Min(_caretPosition.X, text.Length)];
        var textSize = g.MeasureText(text, FontFamily.Default, FontSize);
        var caretX = _lineGap + textSize.Width + CornerRadius / 2 + BorderWidth;
        var caretY = _lineGap / 2 + _caretPosition.Y * LineHeight + BorderWidth;
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

    void ShiftCaretPosition(int amount)
    {
        if (!MultiLine)
        {
            _caretPosition.X += amount;
            if (_caretPosition.X < 0)
                _caretPosition.X = 0;
            if (_caretPosition.X > Text.Length)
                _caretPosition.X = Text.Length;
            return;
        }

        var lines = Text.Split(Environment.NewLine);
        _caretPosition.X += amount;
        while (_caretPosition is { X: < 0, Y: > 0 })
        {
            _caretPosition.Y--;
            _caretPosition.X += lines[_caretPosition.Y].Length;
        }
        if (_caretPosition.X < 0)
        {
            _caretPosition.X = 0;
        }
        else if (
            _caretPosition.Y < lines.Length - 1
            && _caretPosition.X == lines[_caretPosition.Y].Length
            && Text.EndsWith(Environment.NewLine)
        )
        {
            _caretPosition.Y++;
            _caretPosition.X = 0;
        }
    }

    bool HandleCaretMovement(KeyInputArgs keyInputArgs)
    {
        if (keyInputArgs.KeyCode == KeyCode.VcLeft)
        {
            ShiftCaretPosition(-1);
            return true;
        }
        if (keyInputArgs.KeyCode == KeyCode.VcRight)
        {
            ShiftCaretPosition(1);
            return true;
        }
        if (keyInputArgs.KeyCode == KeyCode.VcUp && MultiLine)
        {
            if (_caretPosition.Y <= 0)
                return true;
            _caretPosition.Y--;
            var lines = Text.Split(Environment.NewLine);
            _caretPosition.X = Math.Min(_caretPosition.X, lines[_caretPosition.Y].Length);
            return true;
        }
        if (keyInputArgs.KeyCode == KeyCode.VcDown && MultiLine)
        {
            var lines = Text.Split(Environment.NewLine);
            if (_caretPosition.Y >= lines.Length - 1)
                return true;
            _caretPosition.Y++;
            _caretPosition.X = Math.Min(_caretPosition.X, lines[_caretPosition.Y].Length);
            return true;
        }
        if (keyInputArgs.KeyCode == KeyCode.VcHome)
        {
            _caretPosition.X = 0;
            return true;
        }
        if (keyInputArgs.KeyCode == KeyCode.VcEnd)
        {
            if (!MultiLine)
            {
                _caretPosition.X = Text.Length;
                return true;
            }
            var lines = Text.Split(Environment.NewLine);
            _caretPosition.X = lines[_caretPosition.Y].Length;
            return true;
        }
        if (keyInputArgs.KeyCode == KeyCode.VcPageUp && MultiLine)
        {
            _caretPosition.Y = 0;
            var lines = Text.Split(Environment.NewLine);
            _caretPosition.X = Math.Min(_caretPosition.X, lines[_caretPosition.Y].Length);
            return true;
        }
        if (keyInputArgs.KeyCode == KeyCode.VcPageDown && MultiLine)
        {
            var lines = Text.Split(Environment.NewLine);
            _caretPosition.Y = lines.Length - 1;
            _caretPosition.X = Math.Min(_caretPosition.X, lines[_caretPosition.Y].Length);
            return true;
        }
        return false;
    }
}
