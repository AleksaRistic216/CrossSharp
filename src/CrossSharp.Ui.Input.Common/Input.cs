using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Input : ControlBase, IInput
{
    protected Input()
    {
        BorderColor = ColorRgba.Gray;
        BorderWidth = 1;
        InputHandler.KeyPressed += InputHandlerOnKeyPressed;
        InputHandler.MousePressed += InputHandlerOnMousePressed;
        InputHandler.MouseDragged += InputHandlerOnMouseDragged;
        InputHandler.MouseReleased += InputHandlerOnMouseReleased;
        PerformTheme();
    }

    public override void Invalidate()
    {
        this.PerformDocking();
        CalcFontSize();
        InvalidatePlaceholderBounds();
        InvalidateContentBounds();
    }

    void InvalidateContentBounds()
    {
        _contentBounds = new Rectangle(
            LINE_GAP + CornerRadius / 2 + BorderWidth,
            LINE_GAP / 2 + BorderWidth,
            Width - CornerRadius - LINE_GAP * 2,
            Height - LINE_GAP - BorderWidth * 2
        );
    }

    void InvalidatePlaceholderBounds()
    {
        _placeholderBounds = new Rectangle(
            LINE_GAP + CornerRadius / 2 + BorderWidth,
            LINE_GAP / 2 + BorderWidth,
            Width - CornerRadius - LINE_GAP * 2,
            LineHeight
        );
    }

    internal void InvalidateCaretText()
    {
        if (!MultiLine)
        {
            // Clamp X to text length
            var x = Math.Min(_caretPosition.X, Text.Length);
            _textBeforeCaret = Text[..x];
            _textAfterCaret = Text[x..];
            return;
        }

        _textAfterCaret = string.Empty;
        _textBeforeCaret = string.Empty;
        var lines = Text.Split(Environment.NewLine);

        // If Y is beyond lines, all text is before caret
        if (_caretPosition.Y >= lines.Length)
        {
            _textBeforeCaret = Text;
            return;
        }

        for (var i = 0; i < lines.Length; i++)
        {
            if (i < _caretPosition.Y)
            {
                _textBeforeCaret += lines[i] + Environment.NewLine;
                continue;
            }
            if (i == _caretPosition.Y)
            {
                var line = lines[i];
                if (i < lines.Length - 1)
                    line += Environment.NewLine;
                // Clamp X to line length
                var x = Math.Min(_caretPosition.X, line.Length);
                _textBeforeCaret += line[..x];
                _textAfterCaret += line[x..];
            }
            if (i > _caretPosition.Y)
            {
                _textAfterCaret += lines[i];
                if (i < lines.Length - 1)
                    _textAfterCaret += Environment.NewLine;
            }
        }
    }

    void CalcFontSize()
    {
        // I do not like this at all
        if (MultiLine)
        {
            if (FontSize <= 0) // First time setup use calculation as singe line and breaks, so this fixes it back
                FontSize = Services.GetSingleton<ITheme>().DefaultFontSize;
            return;
        }

        // If single liner, override font size to fit height
        FontSize = Height - LINE_GAP * 2 - BorderWidth * 2;
    }

    public override void DrawContent(ref IGraphics g)
    {
        DrawPlaceholder(ref g);
        DrawSelection(ref g);
        DrawText(ref g);
        DrawCaret(ref g);
    }

    void DrawSelection(ref IGraphics g)
    {
        if (!HasSelection || !IsFocused)
            return;

        var (start, end) = GetNormalizedSelection();
        var selectionColor = Services.GetSingleton<ITheme>().SelectionColor;

        // Use font-based height for selection to avoid corner radius clipping
        var selectionHeight = FontSize + LINE_GAP;

        if (!MultiLine)
        {
            // Single line selection
            var textBefore = Text[..start.X];
            var selectedText = Text[start.X..end.X];

            var beforeSize = g.MeasureText(textBefore, FontFamily.Default, FontSize);
            var selectedSize = g.MeasureText(selectedText, FontFamily.Default, FontSize);

            var x = _contentBounds.X + beforeSize.Width;
            var y = _contentBounds.Y;

            var clipState = g.GetClipState();
            g.SetClip(ClipState.Create(clipState, clipState.Bounds, 0));
            g.FillRectangle(x, y, selectedSize.Width, selectionHeight, selectionColor);
            g.SetClip(clipState);
            return;
        }

        // Multi-line selection
        var lines = Text.Split(Environment.NewLine);
        for (int lineY = start.Y; lineY <= end.Y && lineY < lines.Length; lineY++)
        {
            var line = lines[lineY];
            var startX = (lineY == start.Y) ? start.X : 0;
            var endX = (lineY == end.Y) ? end.X : line.Length;

            // Clamp to line length
            startX = Math.Min(startX, line.Length);
            endX = Math.Min(endX, line.Length);

            var clipState = g.GetClipState();
            if (startX >= endX && lineY != end.Y)
            {
                // Empty line in middle of selection - draw small indicator
                var rectY = _contentBounds.Y + lineY * LineHeight;

                g.SetClip(ClipState.Create(clipState, clipState.Bounds, 0));
                g.FillRectangle(_contentBounds.X, rectY, 4, selectionHeight, selectionColor);
                g.SetClip(clipState);
                continue;
            }

            var textBefore = line[..startX];
            var selectedText = line[startX..endX];

            var beforeSize = g.MeasureText(textBefore, FontFamily.Default, FontSize);
            var selectedSize = g.MeasureText(selectedText, FontFamily.Default, FontSize);

            var rectX = _contentBounds.X + beforeSize.Width;
            var rectYPos = _contentBounds.Y + lineY * LineHeight;

            g.SetClip(ClipState.Create(clipState, clipState.Bounds, 0));
            g.FillRectangle(rectX, rectYPos, selectedSize.Width, selectionHeight, selectionColor);
            g.SetClip(clipState);
        }
    }

    void DrawPlaceholder(ref IGraphics g)
    {
        if (!string.IsNullOrEmpty(Text))
            return;
        if (string.IsNullOrEmpty(Placeholder))
            return;
        g.DrawText(
            Placeholder!,
            _placeholderBounds.X,
            _placeholderBounds.Y,
            FontFamily.Default,
            FontSize,
            ColorRgba.Gray
        );
    }

    void DrawText(ref IGraphics g)
    {
        var x = _contentBounds.X;
        var y = _contentBounds.Y;
        // var clientBounds = this.GetClientBounds();
        foreach (var line in Text.Split(Environment.NewLine))
        {
            // g.SetClip(clientBounds with { Width = Width - CornerRadius, Height = Height }, CornerRadius);
            g.DrawText(line, x, y, FontFamily.Default, FontSize, ColorRgba.Black);
            y += LineHeight;
        }
    }

    public sealed override void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor.Highlighted;
        FontSize = Services.GetSingleton<ITheme>().DefaultFontSize;
        CornerRadius = Services.GetSingleton<ITheme>().DefaultCornerRadius;
        OnThemePerformed();
    }
}
