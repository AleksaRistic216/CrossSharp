using System.Drawing;
using System.Text;

namespace CrossSharp.Ui.Common;

partial class Input
{
    /// <summary>
    /// Gets the normalized selection range (start always before end in document order).
    /// </summary>
    (Point Start, Point End) GetNormalizedSelection()
    {
        if (!HasSelection)
            return (_caretPosition, _caretPosition);

        var anchor = _selectionAnchor;
        var caret = _caretPosition;

        // Compare Y first, then X
        if (anchor.Y < caret.Y || (anchor.Y == caret.Y && anchor.X <= caret.X))
            return (anchor, caret);
        return (caret, anchor);
    }

    /// <summary>
    /// Gets the selected text.
    /// </summary>
    string GetSelectedText()
    {
        if (!HasSelection)
            return string.Empty;

        var (start, end) = GetNormalizedSelection();

        if (!MultiLine)
            return Text[start.X..end.X];

        // Multi-line selection
        var lines = Text.Split(Environment.NewLine);
        var result = new StringBuilder();

        for (int y = start.Y; y <= end.Y; y++)
        {
            var line = lines[y];
            var startX = (y == start.Y) ? start.X : 0;
            var endX = (y == end.Y) ? end.X : line.Length;

            result.Append(line[startX..endX]);
            if (y < end.Y)
                result.Append(Environment.NewLine);
        }

        return result.ToString();
    }

    /// <summary>
    /// Clears the current selection.
    /// </summary>
    void ClearSelection()
    {
        _selectionAnchor = _caretPosition;
        _isSelecting = false;
    }

    /// <summary>
    /// Selects all text.
    /// </summary>
    void SelectAll()
    {
        if (string.IsNullOrEmpty(Text))
            return;

        _selectionAnchor = new Point(0, 0);
        if (!MultiLine)
        {
            _caretPosition = new Point(Text.Length, 0);
        }
        else
        {
            var lines = Text.Split(Environment.NewLine);
            _caretPosition = new Point(lines[^1].Length, lines.Length - 1);
        }
        InvalidateCaretText();
    }

    /// <summary>
    /// Deletes selected text and positions caret at selection start.
    /// </summary>
    void DeleteSelectedText()
    {
        if (!HasSelection)
            return;

        var (start, end) = GetNormalizedSelection();

        // Build text without selection
        var before = GetTextBefore(start);
        var after = GetTextAfter(end);

        _caretPosition = start;
        _selectionAnchor = start;
        _textBeforeCaret = before;
        _textAfterCaret = after;
        Text = before + after;
    }

    /// <summary>
    /// Gets text before the specified position.
    /// </summary>
    string GetTextBefore(Point pos)
    {
        if (string.IsNullOrEmpty(Text))
            return string.Empty;

        if (!MultiLine)
            return Text[..Math.Min(pos.X, Text.Length)];

        var lines = Text.Split(Environment.NewLine);
        var result = new StringBuilder();

        for (int i = 0; i < pos.Y && i < lines.Length; i++)
            result.Append(lines[i] + Environment.NewLine);

        if (pos.Y < lines.Length)
            result.Append(lines[pos.Y][..Math.Min(pos.X, lines[pos.Y].Length)]);

        return result.ToString();
    }

    /// <summary>
    /// Gets text after the specified position.
    /// </summary>
    string GetTextAfter(Point pos)
    {
        if (string.IsNullOrEmpty(Text))
            return string.Empty;

        if (!MultiLine)
            return Text[Math.Min(pos.X, Text.Length)..];

        var lines = Text.Split(Environment.NewLine);
        if (pos.Y >= lines.Length)
            return string.Empty;

        var result = new StringBuilder();
        result.Append(lines[pos.Y][Math.Min(pos.X, lines[pos.Y].Length)..]);

        for (int i = pos.Y + 1; i < lines.Length; i++)
        {
            result.Append(Environment.NewLine);
            result.Append(lines[i]);
        }

        return result.ToString();
    }

    /// <summary>
    /// Finds word boundaries for double-click selection.
    /// </summary>
    (int Start, int End) GetWordBoundaries(int lineIndex, int charIndex)
    {
        var lines = MultiLine ? Text.Split(Environment.NewLine) : new[] { Text };
        if (lineIndex >= lines.Length)
            return (charIndex, charIndex);

        var line = lines[lineIndex];
        if (string.IsNullOrEmpty(line) || charIndex >= line.Length)
            return (line?.Length ?? 0, line?.Length ?? 0);

        // Find word start
        int start = charIndex;
        while (start > 0 && char.IsLetterOrDigit(line[start - 1]))
            start--;

        // Find word end
        int end = charIndex;
        while (end < line.Length && char.IsLetterOrDigit(line[end]))
            end++;

        return (start, end);
    }

    /// <summary>
    /// Selects the word at the current caret position.
    /// </summary>
    void SelectWordAtCaret()
    {
        var (wordStart, wordEnd) = GetWordBoundaries(_caretPosition.Y, _caretPosition.X);
        _selectionAnchor = new Point(wordStart, _caretPosition.Y);
        _caretPosition = new Point(wordEnd, _caretPosition.Y);
        InvalidateCaretText();
    }

    /// <summary>
    /// Selects the current line (for multi-line) or all text (for single-line).
    /// </summary>
    void SelectCurrentLine()
    {
        if (!MultiLine)
        {
            SelectAll();
            return;
        }

        var lines = Text.Split(Environment.NewLine);
        if (_caretPosition.Y >= lines.Length)
            return;

        _selectionAnchor = new Point(0, _caretPosition.Y);
        _caretPosition = new Point(lines[_caretPosition.Y].Length, _caretPosition.Y);
        InvalidateCaretText();
    }

    /// <summary>
    /// Moves caret to the next/previous word boundary.
    /// </summary>
    void MoveCaretToWordBoundary(int direction)
    {
        var lines = MultiLine ? Text.Split(Environment.NewLine) : new[] { Text };
        if (_caretPosition.Y >= lines.Length)
            return;

        var line = lines[_caretPosition.Y];

        if (direction < 0)
        {
            // Move left to word boundary
            int pos = _caretPosition.X;
            // Skip whitespace
            while (pos > 0 && char.IsWhiteSpace(line[pos - 1]))
                pos--;
            // Skip word characters
            while (pos > 0 && char.IsLetterOrDigit(line[pos - 1]))
                pos--;
            _caretPosition.X = pos;
        }
        else
        {
            // Move right to word boundary
            int pos = _caretPosition.X;
            // Skip word characters
            while (pos < line.Length && char.IsLetterOrDigit(line[pos]))
                pos++;
            // Skip whitespace
            while (pos < line.Length && char.IsWhiteSpace(line[pos]))
                pos++;
            _caretPosition.X = pos;
        }
        InvalidateCaretText();
    }
}
