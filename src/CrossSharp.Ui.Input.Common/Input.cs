using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
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
    }

    public override void Initialize() { }

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

    void InvalidateCaretText()
    {
        if (!MultiLine)
        {
            _textBeforeCaret = Text[.._caretPosition.X];
            _textAfterCaret = Text[_caretPosition.X..];
            return;
        }
        _textAfterCaret = string.Empty;
        _textBeforeCaret = string.Empty;
        var lines = Text.Split(Environment.NewLine);
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
                _textBeforeCaret += line[.._caretPosition.X];
                _textAfterCaret += line[_caretPosition.X..];
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

    public override void Redraw() { }

    public override void DrawContent(ref IGraphics g)
    {
        DrawPlaceholder(ref g);
        DrawText(ref g);
        DrawCaret(ref g);
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
        var clientBounds = this.GetClientBounds();
        foreach (var line in Text.Split(Environment.NewLine))
        {
            g.SetClip(clientBounds with { Width = Width - CornerRadius, Height = Height }, CornerRadius);
            g.DrawText(line, x, y, FontFamily.Default, FontSize, ColorRgba.Black);
            y += LineHeight;
        }
    }

    public override void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().PrimaryColor;
        FontSize = Services.GetSingleton<ITheme>().DefaultFontSize;
        CornerRadius = Services.GetSingleton<ITheme>().DefaultCornerRadius;
    }
}
